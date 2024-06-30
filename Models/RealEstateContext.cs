using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UrbanNest.Models
{
    public class RealEstateContext : DbContext
    {
        public DbSet<Agent> Agents { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<RealEstateListing> RealEstateListings { get; set; }
    }
}
