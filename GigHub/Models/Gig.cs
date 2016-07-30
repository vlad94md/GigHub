using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCancel { get; private set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public Genre Genre { get; set; }

        [Required]
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

        public void Update(string venue, byte genre)
        {
            var notification = Notification.GigUpdated(this, DateTime, Venue);

            Venue = venue;
            GenreId = genre;
            DateTime = DateTime.Now;

            foreach (var user in Attendacnces.Select(x => x.Attendee))
            {
                user.Notify(notification);
            }
        }
    }
}