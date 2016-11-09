using GigHub.Core;
using GigHub.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(string querry = null)
        {
            var upcomingGigs = _unitOfWork.Gigs.GetAllUpcommingGigs(); //TODO: Exclude My gigs

            if (!String.IsNullOrWhiteSpace(querry))
            {
                upcomingGigs = upcomingGigs
                    .Where(g =>
                        g.Artist.Name.ToLower().Contains(querry.ToLower()) ||
                        g.Genre.Name.ToLower().Contains(querry.ToLower()) ||
                        g.Venue.ToLower().Contains(querry.ToLower()));
            }

            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                SearchTerm = querry,
                Attendacnes = _unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(g => g.GigId),
                Followees = _unitOfWork.Followings.GetFollowingsWhereUserIsFollower(userId).ToLookup(x => x.FolloweeId)
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