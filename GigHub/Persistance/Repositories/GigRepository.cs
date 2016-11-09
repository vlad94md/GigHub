using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core;

namespace GigHub.Persistance.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly IApplicationDbContext _context;

        public GigRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public Gig Add(Gig gig)
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

        public IEnumerable<Gig> GetAllUpcommingGigs()
        {
            return _context.Gigs
                .Include("Artist")
                .Include("Genre")
                .Where(g => g.DateTime > DateTime.Now);
        } 
    }
}