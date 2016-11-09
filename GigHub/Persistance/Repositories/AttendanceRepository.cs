using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Persistance.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly IApplicationDbContext _context;

        public AttendanceRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public Attendance GetAttendance(int gigId, string userId)
        {
            return _context.Attendances
                .FirstOrDefault(a => a.GigId == gigId && a.AttendeeId == userId);
        }

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToList();
        }

        public Attendance Add(Attendance attendance)
        {
            return _context.Attendances.Add(attendance);
        }

        public Attendance Remove(Attendance attendance)
        {
            return _context.Attendances.Remove(attendance);
        }
    }
}