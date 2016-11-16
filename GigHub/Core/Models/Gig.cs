using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GigHub.Core.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCancel { get; private set; }

        public ApplicationUser Artist { get; set; }

        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public Genre Genre { get; set; }

        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendacnces { get; set; }

        public Gig()
        {
            Attendacnces = new Collection<Attendance>();
        }
        public void Cancel()
        {
            IsCancel = true;

            var notification = Notification.GigCanceled(this);

            foreach (var user in Attendacnces.Select(a => a.Attendee))
            {
                user.Notify(notification);
            }
        }

        public void Update(string venue, byte genre, DateTime dateTime)
        {
            var notification = Notification.GigUpdated(this, DateTime, Venue);

            Venue = venue;
            GenreId = genre;
            DateTime = dateTime;

            foreach (var user in Attendacnces.Select(x => x.Attendee))
            {
                user.Notify(notification);
            }
        }
    }
}