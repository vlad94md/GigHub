using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Persistance.EntityConfigurations
{
    public class GigConfiguration : EntityTypeConfiguration<Gig>
    {
        public GigConfiguration()
        {
            Property(g => g.ArtistId).IsRequired();
            Property(g => g.Venue).IsRequired().HasMaxLength(255);
            Property(g => g.GenreId).IsRequired();

            HasMany(a => a.Attendacnces)
            .WithRequired(a => a.Gig)
            .WillCascadeOnDelete(false);
        }
    }
}