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

       
       

    }
}