using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UrbanNest.Data;
using UrbanNest.Models;

namespace UrbanNest.Controllers
{

    public class AgentController : Controller
    {
        private readonly RealEstateContext _context;

        public AgentController()
        {
            _context = new RealEstateContext();
        }

        // GET: Agent/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var agent = await _context.Agents
                                      .Include(a => a.Listings)
                                      .FirstOrDefaultAsync(a => a.AgentId == id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }
    }
}

