using System;
using System.Web.Http.Results;
using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Controllers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
        public void Attend_AttendanceAlreadyExists_ShouldBadRequest()
        {
            var attendanceDto = new Attendance();
            attendanceDto.GigId = _gigId;
            attendanceDto.AttendeeId = _userId;

            _mockRepository.Setup(r => r.GetAttendance(_gigId, _userId)).Returns(attendanceDto);

            var result = _controller.Attend(new AttendanceDto() {GigId = _gigId });

            result.Should().BeOfType<BadRequestResult>();
        }

        [TestMethod]
        public void Attend_AttendanceNotExists_ShouldOk()
        {
            var attendanceDto = new Attendance();
            attendanceDto.GigId = _gigId;
            attendanceDto.AttendeeId = _userId;

            _mockRepository.Setup(r => r.GetAttendance(_gigId, _userId)).Returns((Attendance) null);

            var result = _controller.Attend(new AttendanceDto() { GigId = _gigId });

            result.Should().BeOfType<OkResult>();
        }

    }
}
