using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistance;
using GigHub.Persistance.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;

namespace GigHub.Tests.Persistance.Repositories
{
    [TestClass]
    public class NotificationRepositoryTests
    {
        private NotificationRepository _repository;
        private Mock<DbSet<Notification>> _mockNotifications;
        private Mock<DbSet<UserNotification>> _mockUserNotifications;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            var mockContext = new Mock<IApplicationDbContext>();
            _mockNotifications = new Mock<DbSet<Notification>>();
            _mockUserNotifications = new Mock<DbSet<UserNotification>>();
            _userId = "1";


            mockContext.SetupGet(c => c.Notifications).Returns(_mockNotifications.Object);
            mockContext.SetupGet(c => c.UserNotifications).Returns(_mockUserNotifications.Object);

            _repository = new NotificationRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetNewNotificationsFor_NoNotificationsForUser_ShouldNotBeReturn()
        {
            var notification = Notification.GigCreated(new Gig());
            var user = new ApplicationUser() {Id = _userId + "1", Email = "user@domain.com"};
            var userNotification = new UserNotification(user, notification);


            _mockNotifications.SetSource(new[] { notification });
            _mockUserNotifications.SetSource(new[] { userNotification });

            var result = _repository.GetNewNotificationsFor(_userId);

            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNewNotificationsFor_NotificationsExistAndNotRead_ShouldBeReturn()
        {
            var notification = Notification.GigCreated(new Gig());
            var user = new ApplicationUser() { Id = _userId, Email = "user@domain.com" };
            var userNotification = new UserNotification(user, notification);


            _mockNotifications.SetSource(new[] { notification });
            _mockUserNotifications.SetSource(new[] { userNotification });

            var result = _repository.GetNewNotificationsFor(_userId);

            result.Should().Contain(notification);
        }

        [TestMethod]
        public void GetNewNotificationsFor_AllNotificationsAreRead_ShouldNotBeReturn()
        {
            var notification = Notification.GigCreated(new Gig());
            var user = new ApplicationUser() { Id = _userId, Email = "user@domain.com" };
            var userNotification = new UserNotification(user, notification);
            userNotification.Read();

            _mockNotifications.SetSource(new[] { notification });
            _mockUserNotifications.SetSource(new[] { userNotification });

            var result = _repository.GetNewNotificationsFor(_userId);

            result.Should().BeEmpty();
        }
    }
}
