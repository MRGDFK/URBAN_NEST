using System;

public class RealEstateListing
{
    public int ListingId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public DateTime PostedDate { get; set; }
    public int UserId { get; set; }
    public UserProfile UserProfile { get; set; }
}

