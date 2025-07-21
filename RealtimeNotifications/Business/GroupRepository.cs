using Microsoft.EntityFrameworkCore;
using RealtimeNotifications.Interfaces;
using RealtimeNotifications.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace RealtimeNotifications.Business
{
    public class GroupRepository(NotificationContext notificationContext, ILogger<GroupRepository> logger) : IGroupRepository
    {
        private readonly NotificationContext _context = notificationContext;
        private readonly ILogger<GroupRepository> _logger = logger;

        public Task CreatGroup(Group group)
        {
            try
            {
                _context.Groups.Add(group);
                _context.SaveChanges();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError($"By calling CreatGroup getting error : {ex.Message} on {DateTime.Now.ToShortTimeString}");
                throw;
            }
        }

        public async Task<List<GroupsDto>> GetAllGroups(int userId)
        {
            List<GroupsDto> result = [];
            try
            {
                var query = from groups in _context.Groups
                            join userGroup in _context.UserGroups.Where(g => g.UserId == userId)
                                on groups.GrupeId equals userGroup.UserGroupId into userGroups
                            from g in userGroups.DefaultIfEmpty()
                            select new GroupsDto
                            {
                                Id = string.IsNullOrEmpty(g.Id.ToString()) ? 0 : g.Id,
                                GroupName = groups.GroupName,
                                GroupId = groups.GrupeId,
                                isJoined = !string.IsNullOrEmpty(g.UserId.ToString()),
                            };
                result = await Task.FromResult(query.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError($"By calling GetAllGroups getting error : {ex.Message} on {DateTime.Now.ToShortTimeString}");
            }
            return result;
        }

        public Task UpdateUserGroup(UserGroupDto group)
        {
            try
            {
                if (group.isJoined)
                {
                    var usergrp = _context.UserGroups.Where(u => u.Id == group.Id).FirstOrDefault();
                    if (usergrp is not null)
                        _context.UserGroups.Remove(usergrp);
                }
                else
                    _context.UserGroups.Add(new UserGroup { UserId = group.UserId, UserGroupId = group.UserGroupId });
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Creating/Updating UserGroup: {Message}", ex.Message);
            }
            return Task.FromResult(0);
        }
    }
}
