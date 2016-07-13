using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    [Authorize]
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Create()
        {
            var model = new GigFormViewModel()
            {
                Genres = _context.Genres.ToList()
            };
            return View(model);
        }

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
                return View("Create", viewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include("Artist")
                .Include("Genre")
                .ToList();

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }
    }
}