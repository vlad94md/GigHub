using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Tests.Controllers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Controllers.Api
{
    public class AttendanceContollerTests
    {
        private AttendancesController _controller;
        private Mock<IAttendanceRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInialize()
        {
            _mockRepository = new Mock<IAttendanceRepository>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.Attendances).Returns(_mockRepository.Object);

            _controller = new AttendancesController();
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }


    }
}
