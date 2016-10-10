using GigHub.Models;
using GigHub.Repositories;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private AttendanceRepository _attendaceRepository;
        private GigRepository _gigRepository;
        private FollowingRepository _followingRepository;

        public GigsController()
        {
            _context = new ApplicationDbContext();
            _attendaceRepository = new AttendanceRepository(_context);
            _gigRepository = new GigRepository(_context);
            _followingRepository = new FollowingRepository(_context);
        }

        public ActionResult Details(int id)
        {
            var gig = _context.Gigs
                .Where(g => g.Id == id)
                .Include(a => a.Artist)
                .FirstOrDefault();

            if (gig == null)
                return HttpNotFound();

            var viewModel = new GigDetailsViewModel()
            {
                Gig = gig,
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.isAttending = _context.Attendances
                    .Any(a => a.GigId == gig.Id && a.AttendeeId == userId);

                viewModel.isFollowing = _context.Followings
                    .Any(a => a.FollowerId == userId && a.FolloweeId == gig.ArtistId);
            }

            return View("Details", viewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel()
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add a Gig"
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var gig = new Gig()
                {
                    GenreId = viewModel.Genre,
                    ArtistId = User.Identity.GetUserId(),
                    Venue = viewModel.Venue,
                    DateTime = viewModel.GetDateTime()
                };

                _context.Gigs.Add(gig);
                _context.SaveChanges();

            }
            else
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var gig = _gigRepository.GetGigWithAttendanees(viewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Update(viewModel.Venue, viewModel.Genre);
;   
           _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Edit(int gigId)
        {

            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == gigId && g.ArtistId == userId);


            var viewModel = new GigFormViewModel()
            {
                Genres = _context.Genres,
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Id = gigId,
                Heading = "Edit a Gig"

            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.IsCancel)
                .Include("Genre")
                .ToList();

            return View(gigs);
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { querry = viewModel.SearchTerm });
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = _gigRepository.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Attendacnes = _attendaceRepository.GetFutureAttendances(userId).ToLookup(a => a.GigId),
                Followees = _followingRepository.GetFollowingsWhereUserIsFollower(userId).ToLookup(x => x.FolloweeId)
            };

            return View(viewModel);
        }
    }
}