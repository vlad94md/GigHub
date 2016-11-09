using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistance;
using GigHub.Persistance.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;

namespace GigHub.Tests.Persistance.Repositories
{
    [TestClass]
    public class GigRepositoryTests
    {
        private GigRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;
        private Mock<DbSet<Attendance>> _mockAttendances;
        private string _userId;
        private Gig _futureGigWithUserId;

        [TestInitialize]
        public void TestInitialize()
        {
            var mockContext = new Mock<IApplicationDbContext>();
            _mockGigs = new Mock<DbSet<Gig>>();
            _mockAttendances = new Mock<DbSet<Attendance>>();
            _userId = "1";
            _futureGigWithUserId = new Gig()
            {
                Id = 1,
                DateTime = DateTime.Now.AddDays(+1),
                ArtistId = _userId,
                Artist = new ApplicationUser(),
                Genre = new Genre()
            };

            mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);
            mockContext.SetupGet(c => c.Attendances).Returns(_mockAttendances.Object);

            _repository = new GigRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(-1)};

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcommingGigsByArtist("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsCancelled_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(+1), ArtistId = _userId};
            gig.Cancel();

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcommingGigsByArtist(gig.ArtistId);

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForDifferentArtist_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(+1), ArtistId = _userId };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcommingGigsByArtist(gig.ArtistId + "1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsOwnAndInTheFuture_ShouldBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(+1), ArtistId = _userId };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcommingGigsByArtist(gig.ArtistId);

            gigs.Should().Contain(gig);
        }

        [TestMethod]
        public void GetGigsUserAttending_GigsWithAttendeeCurentUserDontExist_ShouldNotBeReturned()
        {
            var gig = _futureGigWithUserId;

            var attendance = new Attendance() {Gig = gig, AttendeeId = _userId};

            _mockGigs.SetSource(new[] { gig });
            _mockAttendances.SetSource(new [] { attendance });

            var gigs = _repository.GetGigsUserAttending(gig.ArtistId + "1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsUserAttending_GigsWithAttendeeCurentUserExist_ShouldBeReturned()
        {
            var gig = _futureGigWithUserId;
            var attendance = new Attendance() { Gig = gig, AttendeeId = _userId };

            _mockGigs.SetSource(new[] { gig });
            _mockAttendances.SetSource(new[] { attendance });

            var gigs = _repository.GetGigsUserAttending(gig.ArtistId);

            gigs.Should().Contain(gig);
        }
    }
}