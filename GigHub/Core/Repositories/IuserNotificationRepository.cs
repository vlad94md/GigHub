using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IUserNotificationRepository
    {
        IEnumerable<Notification> GetNewNotificationsByUser(string userId);
        IEnumerable<UserNotification> GetNewUserNotificationsByUser(string userId);
    }
}
