using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class GigsViewModel
    {
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public bool ShowActions { get; set; }
        public string SearchTerm { get; set; }
        public ILookup<int, Attendance> Attendacnes { get; set; }
        public ILookup<string, Following> Followees { get; set; }
    }
}