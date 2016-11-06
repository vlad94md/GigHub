using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Persistance;

namespace GigHub.Controllers
{
    [Authorize]
    public class ArtistController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArtistController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();

            var artists = _unitOfWork.Followings
                .GetFollowingsWhereUserIsFollower(userId)
                .Select(f => f.Followee)
                .ToList();

            return View(artists);
        }
    }
}