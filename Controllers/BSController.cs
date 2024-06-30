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
        /*
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
                */

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterProperty(property model, HttpPostedFileBase Image_01)
        {
            if (ModelState.IsValid)
            {
                if (Image_01 != null && Image_01.ContentLength > 0)
                {
                    // Generate a unique file name to prevent file name collisions
                    var fileName = Path.GetFileNameWithoutExtension(Image_01.FileName);
                    var extension = Path.GetExtension(Image_01.FileName);
                    var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                    // Define the path where the file will be saved
                    var path = Path.Combine(Server.MapPath("~/UploadedImages"), uniqueFileName);

                    // Save the file
                    Image_01.SaveAs(path);

                    // Update the model with the image path
                    model.Image_01 = uniqueFileName;
                }

                // Get the seller ID from the session
                

                // Register the property with the updated image path and seller ID
                string sellerId = Session["seller_id"]?.ToString();
                if (Database_helper.RegisterProperty(sellerId, model.Title, model.Location, model.Price, model.Area, model.Bed, model.Bath, model.type, model.status, model.Image_01))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Property registration failed.");
                }
            }

            return View(model);
        }
    }
}