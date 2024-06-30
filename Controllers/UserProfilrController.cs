using System;

public class UserProfileController : Controller
{
    private readonly RealEstateContext _context;

    public UserProfileController()
    {
        _context = new RealEstateContext();
    }

    // GET: UserProfile/Details/5
    public async Task<ActionResult> Details(int id)
    {
        var userProfile = await _context.UserProfiles
                                         .Include(u => u.Listings)
                                         .FirstOrDefaultAsync(u => u.UserId == id);
        if (userProfile == null)
        {
            return HttpNotFound();
        }
        return View(userProfile);
    }
}

