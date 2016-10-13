using GigHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class FollowingRepository
    {
        private ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string artistId, string userId)
        {
            return _context.Followings
                .FirstOrDefault(a => a.FollowerId == userId && a.FolloweeId == artistId);
        }

        public IEnumerable<Following> GetFollowingsWhereUserIsFollower(string userId)
        {
            return _context.Followings
                .Where(f => f.FollowerId == userId)
                .ToList();
        }
    }
}