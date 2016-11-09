using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IGenreRepository Genres { get; set; }
        IFollowingRepository Followings { get; set; }
        IAttendanceRepository Attendances { get; set; }
        IGigRepository Gigs { get; set; }
        //INotificationRepository Notifications { get; set; }
        IUserNotificationRepository UserNotifications { get; set; }
        void Commit();
    }
}