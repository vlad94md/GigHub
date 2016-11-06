using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using GigHub.Core.Models;
using GigHub.Persistance;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int gigId)
        {
            string userId = User.Identity.GetUserId();

            var gig = _context.Gigs
                .Include(a => a.Attendacnces.Select(x => x.Attendee))
                .Single(g => g.Id == gigId && g.ArtistId == userId);

            if (gig.IsCancel)
            {
                return NotFound();
            }

            gig.Cancel();

            _context.SaveChanges();

            return Ok(gigId);
        }
    }
}
