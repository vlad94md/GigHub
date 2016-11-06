using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        Gig Add(Gig gig);
        Gig GetGig(int gigId);
        Gig GetGigWithAttendanees(int gigId);
        Gig GetGigWithArtist(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        IEnumerable<Gig> GetUpcommingGigsByArtist(string artistId);
        IEnumerable<Gig> GetAllUpcommingGigs();
    }
}