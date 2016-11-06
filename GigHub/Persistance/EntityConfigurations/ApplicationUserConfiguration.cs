using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Persistance.EntityConfigurations
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            Property(g => g.Name).IsRequired().HasMaxLength(100);

            HasMany(a => a.Followers)
           .WithRequired(f => f.Follower)
           .WillCascadeOnDelete(false);

            HasMany(a => a.Followees)
            .WithRequired(f => f.Followee)
            .WillCascadeOnDelete(false);
        }
    }
}