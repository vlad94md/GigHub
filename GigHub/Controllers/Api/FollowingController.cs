using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public FollowingController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId))
            {
                return BadRequest("Following already exists");
            }

            var following = new Following()
            {
                FolloweeId = dto.FolloweeId,
                FollowerId = userId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Unfollow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            var following = _context.Followings.FirstOrDefault(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId);

            if(following == null)
                return NotFound();


            _context.Followings.Remove(following);
            _context.SaveChanges();

            return Ok(dto.FolloweeId);
        }

    }
}
