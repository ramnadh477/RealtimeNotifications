using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealtimeNotifications.Handlers;
using RealtimeNotifications.Interfaces;
using RealtimeNotifications.Models;
using System.Threading;

namespace RealtimeNotifications.Business
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationContext _context;
        private readonly ILogger<NotificationRepository> _logger;
        public NotificationRepository(NotificationContext context, ILogger<NotificationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public Task CreateAsync(Notification notification)
        {
            try
            {
                _context.Notifications.Add(notification);
                _context.SaveChanges();
                _logger.LogInformation("Notification Created");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating notification: {Message} ", ex.Message);
            }
            return Task.CompletedTask;
        }

        public Task<List<NotificationObj>> GetNotificationById(int Id)
        {
            try
            {
                var result = _context.Notifications.Where(n => n.UserId == Id).Select(n => new NotificationObj(n.Id, n.UserId, n.Message, n.IsRead, n.Timestamp)).ToList();
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"By calling GetNotificationById getting error : {ex.Message} on {DateTime.Now.ToShortTimeString}");
                throw;
            }
        }

        public Task<List<getreaNotificationObj>> GetNotificationByIdandReadStatus(int Id, bool isRead)
        {
            try
            {
                var result = _context.Notifications.Where(n => n.UserId == Id && n.IsRead == isRead).Select(n => new getreaNotificationObj(n.Id, n.UserId, n.Message, n.IsRead, n.Timestamp)).ToList();
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"By calling GetNotificationByIdandReadStatus getting error : {ex.Message} on {DateTime.Now.ToShortTimeString}");
                throw;
            }
        }

        public Task<List<UpdateNotificationObj>> Update(Notification notification)
        {
            try
            {
                _context.Notifications.Attach(notification);
                _context.Entry(notification).Property(p => p.IsRead).IsModified = true;
                _context.SaveChanges();
                var result = _context.Notifications.Where(n => n.Id == notification.Id).Select(n => new UpdateNotificationObj(n.Id, n.UserId, n.Message, n.IsRead, n.Timestamp)).ToList();
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"By calling GetNotificationByIdandReadStatus getting error : {ex.Message} on {DateTime.Now.ToShortTimeString}");
                throw;
            }
        }

        public Task UpdateStatus(int Id, bool IsRead)
        {
            _context.Notifications.Where(n => n.Id == Id).ExecuteUpdate(u => u.SetProperty(p => p.IsRead, IsRead));
            return Task.CompletedTask;
        }
    }
}
