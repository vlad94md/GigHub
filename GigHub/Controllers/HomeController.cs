using GigHub.Models;
using GigHub.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();;
        }

        public ActionResult Index(string querry = null)
        {
            var upcomingGigs = _context.Gigs
                .Include("Artist")
                .Include("Genre")
                .Where(g => g.DateTime > DateTime.Now);


            if (!String.IsNullOrWhiteSpace(querry))
            {
                upcomingGigs = upcomingGigs
                    .Where(g =>
                        g.Artist.Name.Contains(querry) ||
                        g.Genre.Name.Contains(querry) ||
                        g.Venue.Contains(querry));
            }

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                SearchTerm = querry
            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}