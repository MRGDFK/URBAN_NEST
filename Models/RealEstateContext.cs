using System;

public class RealEstateContext : DbContext
{
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<RealEstateListing> RealEstateListings { get; set; }
}

