using FluentAssertions;
using GigHub.Controllers;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.IntegrationsTests.Extensions;
using GigHub.Persistance;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.IntegrationsTests.Controllers
{
    [TestFixture]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Mine_WhenCalled_ShoulReturnUpcomingGigs()
        {
            // Arrange
            var user = _context.Users.First(); // specific user for tests in db

            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.First();
            var gig = new Gig {Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "Somewhere"};
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            // Act
            var result = (ViewResult)_controller.Mine();

            // Assert
            (result.ViewData.Model as IEnumerable<Gig>).Should().HaveCount(1);
        }

        [Test, Isolated]
        public void Update_WhenCalled_ShoulUpdateTheGiveGig()
        {
            // Arrange
            var user = _context.Users.First(); // specific user for tests in db
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.Single(g => g.Id == 1);
            var gig = new Gig { Artist = user, DateTime = DateTime.Now.AddMonths(1), Genre = genre, Venue = "Somewhere" };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            // Act
            var result = _controller.Update(new GigFormViewModel()
            {
                Id = gig.Id,
                Date = DateTime.Now.AddMonths(2).ToString("d MMM yyyy"),
                Time = "20:00",
                Venue = "Venue",
                Genre = 2
            });

            // Assert
            _context.Entry(gig).Reload();
            gig.DateTime.Month.Should().Be(DateTime.Now.AddMonths(2).Month);
            gig.DateTime.ToString("HH:mm").Should().Be("20:00");
            gig.Venue.Should().Be("Venue");
            gig.GenreId.Should().Be(2);
        }
    }
}
