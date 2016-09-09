using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }


        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto attendaceDto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == attendaceDto.GigId))
            {
                return BadRequest("The attendance already exists.");
            }

            var attendance = new Attendance()
            {
                GigId = attendaceDto.GigId,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }


        [HttpDelete]
        public IHttpActionResult DeleteAttend(int id)
        {
            var userId = User.Identity.GetUserId();
            var attendance = _context.Attendances.FirstOrDefault(a => a.AttendeeId == userId && a.GigId == id);

            if (attendance == null)
            {
                return NotFound();
            }

            _context.Attendances.Remove(attendance);
            _context.SaveChanges();

            return Ok();
        }
    }
}
