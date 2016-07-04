using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {
            var model = new GigFormViewModel()
            {
                Genres = _context.Genres.ToList()
            };
            return View(model);
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
                return View("Create", viewModel);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}