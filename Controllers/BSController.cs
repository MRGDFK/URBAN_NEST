using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

        public ActionResult Buy()
        {
            var properties = GetPropertiesFromDatabase();
            return View(properties);
        }

        private IEnumerable<property> GetPropertiesFromDatabase()
        {
            List<property> properties = new List<property>();

            // Connection string directly in the code
            string connectionString = "Data Source=DESKTOP-AIKR8ED\\SQLEXPRESS01;Initial Catalog=real_estate_listing_properties;Integrated Security=True"; // or with User ID and Password

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT prop_id, seller_id, prop_location, prop_type, prop_description, prop_size, prop_price, prop_status, prop_image, prop_bed, prop_bath FROM property";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            properties.Add(new property
                            {

                                Title = reader["prop_description"].ToString(),
                                Location = reader["prop_location"].ToString(),
                                Price = reader["prop_price"].ToString(),
                                Area = reader["prop_size"].ToString(),
                               
                                Bed = reader["prop_bed"].ToString(),
                                Bath =reader["prop_bath"].ToString(),
                                type = reader["prop_type"].ToString(),
                                
                                status = reader["prop_status"].ToString(),
                                Image_01 = reader["prop_image"].ToString()
                                
                                
                            });
                        }
                    }
                }
            }

            return properties;
        }


    }
}