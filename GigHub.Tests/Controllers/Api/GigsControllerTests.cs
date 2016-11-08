﻿using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Controllers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private Mock<IGigRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInialize()
        {
            _mockRepository = new Mock<IGigRepository>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.Gigs).Returns(_mockRepository.Object);

            _controller = new GigsController(mockUnitOfWork.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigIsCancelled_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();

            _mockRepository.Setup(r => r.GetGigWithAttendanees(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_TryCancelAnotherUserGig_Unauthorized()
        {
            var gig = new Gig() { ArtistId = _userId + "1" };

            _mockRepository.Setup(r => r.GetGigWithAttendanees(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_OwnGig_ShouldReturnOk()
        {
            var gig = new Gig() { ArtistId = _userId };

            _mockRepository.Setup(r => r.GetGigWithAttendanees(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void Cancel_Gig_ShouldReturnIsCanceledTrue()
        {
            var gig = new Gig() { ArtistId = _userId };

            _mockRepository.Setup(r => r.GetGigWithAttendanees(1)).Returns(gig);

            _controller.Cancel(1);

            var result = gig.IsCancel;

            result.Should().BeTrue();
        }
    }
}
