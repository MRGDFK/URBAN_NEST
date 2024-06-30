using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using UrbanNest.Data;
using UrbanNest.Models;
using UrbanNest.ViewModels;

namespace UrbanNest.Controllers
{
    public class BSController : Controller
    {
        // GET: BS
        public ActionResult Sell()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Buy()
        {
            var model = new PropertyViewModel
            {
                RecentProperties = GetRecentProperties(),
                SearchCriteria = new PropertySearchCriteria(),
                SearchResults = new List<Property>()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Search(PropertySearchCriteria searchCriteria)
        {
            var model = new PropertyViewModel
            {
                RecentProperties = GetRecentProperties(),
                SearchCriteria = searchCriteria,
                SearchResults = SearchProperties(searchCriteria)
            };

            return View("Buy", model);
        }

        private List<Property> SearchProperties(PropertySearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        private List<Property> GetRecentProperties()
        {
            throw new NotImplementedException();
        }
    }
}
