using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using UrbanNest.Data;
using UrbanNest.Models;



namespace UrbanNest.Controllers
{
    public class BSController : Controller
    {
        // GET: BS
        public ActionResult Sell()
        {
            return View();
        }

        public ActionResult Buy()
        {
            var properties = GetPropertiesFromDatabase();
            return View(properties);
        }

        private IEnumerable<property> GetPropertiesFromDatabase()
        {
            // This is just a placeholder. Replace it with your database fetching logic.
            return new List<property>
            {
                new property { Title = "Beautiful Family House", Location = "BADDA", Price = "350000", Area = "2500 sqft", Bed ="3", Bath="4", Status="on sell", Image_01 ="prop1.jpeg"},
                
            };
        }



    }
}