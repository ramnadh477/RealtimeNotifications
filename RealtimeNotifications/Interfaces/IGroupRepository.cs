using RealtimeNotifications.Models;

namespace RealtimeNotifications.Interfaces
{
    public interface IGroupRepository
    {
        Task<List<GroupsDto>> GetAllGroups(int userId);
        Task UpdateUserGroup(UserGroupDto group);
    }
}
