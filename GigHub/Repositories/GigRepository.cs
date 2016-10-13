using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Repositories
{
    public class GigRepository
    {
        private ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Gig AddGig(Gig gig)
        {
            return _context.Gigs.Add(gig);
        }

        public Gig GetGig(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .FirstOrDefault(g => g.Id == gigId);
        }

        public Gig GetGigWithAttendanees(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Attendacnces.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == gigId);
        }

        public Gig GetGigWithArtist(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include("Artist")
                .Include("Genre")
                .ToList();
        }

        public IEnumerable<Gig> GetUpcommingGigsByArtist(string artistId)
        {
            return _context.Gigs
                .Where(g => g.ArtistId == artistId && g.DateTime > DateTime.Now && !g.IsCancel)
                .Include("Genre")
                .ToList();
        }
    }
}