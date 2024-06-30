using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
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

        [HttpPost]
        public ActionResult SetUserMode(string mode)
        {
            if (mode == "seller")
            {
                Session["mode"] = "seller";
            }
            else
            {
                Session["mode"] = "buyer";
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterProperty(property model)
        {
            if (ModelState.IsValid)
            {
                string sellerId = Session["seller_id"]?.ToString();
                if (Database_helper.RegisterProperty(sellerId, model.Title, model.Location, model.Price, model.Area, model.Bed, model.Bath, model.type, model.status, model.Image_01))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Sihnup", "Home");
                    
                }
            }
            return View(model);
        }

    }
}