using GigHub.Models;
using GigHub.ViewModels;
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

        public ActionResult Create()
        {
            GigFormViewModel model = new GigFormViewModel()
            {
                Genres = _context.Genres
            };
            return View(model);
        }
    }
}