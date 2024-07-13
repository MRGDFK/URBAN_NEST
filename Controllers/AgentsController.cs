using System.Collections.Generic;
using System.Web.Mvc;
using UrbanNest.Data;
using UrbanNest.Models;

namespace UrbanNest.Controllers
{
    public class AgentsController : Controller
    {
        // GET: Agents
        public ActionResult Agent()
        {
            List<Agent> agents = Database_helper.GetAllAgents();
            if (agents == null) // Safety check to ensure agents is not null
            {
                agents = new List<Agent>();
            }
            return View(agents);
        }
    }
}
