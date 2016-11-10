using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistance.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IApplicationDbContext _context;

        public NotificationRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetNewNotificationsFor(string userId)
        {
            return _context.UserNotifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .Select(n => n.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();
        }

        public IEnumerable<UserNotification> GetNewUserNotificationsFor(string userId)
        {
            return _context.UserNotifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToList();
        }
    }
}