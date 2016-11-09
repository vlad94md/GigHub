using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;
using GigHub.Tests.Extensions;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class AttendanceContollerTests
    {
        private AttendancesController _controller;
        private Mock<IAttendanceRepository> _mockRepository;
        private string _userId;
        private int _gigId;

        [TestInitialize]
        public void TestInialize()
        {
            _mockRepository = new Mock<IAttendanceRepository>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.Attendances).Returns(_mockRepository.Object);

            _controller = new AttendancesController(mockUnitOfWork.Object);
            _userId = "1";
            _gigId = 1;

            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Attend_AttendanceAlreadyExists_ReturnBadRequest()
        {
            var attendance = new Attendance();
            attendance.GigId = _gigId;
            attendance.AttendeeId = _userId;

            _mockRepository.Setup(r => r.GetAttendance(_gigId, _userId)).Returns(attendance);

            var result = _controller.Attend(new AttendanceDto() {GigId = _gigId });

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Attend_AttendanceNotExists_ReturnOk()
        {
            var attendance = new Attendance();
            attendance.GigId = _gigId;
            attendance.AttendeeId = _userId;

            _mockRepository.Setup(r => r.GetAttendance(_gigId, _userId)).Returns((Attendance) null);

            var result = _controller.Attend(new AttendanceDto() { GigId = _gigId });

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void DeleteAttend_AttendanceExists_ReturndOk()
        {
            var attendance = new Attendance();
            attendance.GigId = _gigId;
            attendance.AttendeeId = _userId;

            _mockRepository.Setup(r => r.GetAttendance(_gigId, _userId)).Returns(attendance);

            var result = _controller.DeleteAttend(_gigId);

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void DeleteAttend_AttendanceNotExists_ReturnOk()
        {
            var attendance = new Attendance();
            attendance.GigId = _gigId;
            attendance.AttendeeId = _userId;

            _mockRepository.Setup(r => r.GetAttendance(_gigId, _userId)).Returns((Attendance)null);

            var result = _controller.DeleteAttend(_gigId);

            result.Should().BeOfType<NotFoundResult>();
        }

    }
}
