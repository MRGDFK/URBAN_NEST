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
                    var path = Path.Combine(Server.MapPath("~/Property_up_Images"), uniqueFileName);

                    // Save the file
                    Image_01.SaveAs(path);

                    // Update the model with the image path
                    model.Image_01 = uniqueFileName;
                }

                // Get the seller ID from the session
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

            string connectionString = "Data Source=MRGDFK\\SQLEXPRESS;Initial Catalog=real_estate_listing_properties;Integrated Security=True"; // or with User ID and Password

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
                                propId = Convert.ToInt32(reader["prop_id"]),
                                Title = reader["prop_description"].ToString(),
                                Location = reader["prop_location"].ToString(),
                                Price = reader["prop_price"].ToString(),
                                Area = reader["prop_size"].ToString(),
                                Bed = reader["prop_bed"].ToString(),
                                Bath = reader["prop_bath"].ToString(),
                                type = reader["prop_type"].ToString(),
                                status = reader["prop_status"].ToString(),
                                Image_01 = reader["prop_image"].ToString(),
                                SellerId = reader["seller_id"].ToString()
                            });
                        }
                    }
                }
            }

            return properties;
        }

        [HttpGet]
        public ActionResult SearchProperties(SearchViewModel model)
        {
            var filteredProperties = FilterPropertiesFromDatabase(model);
            ViewBag.FilteredProperties = filteredProperties; // Add filtered properties to ViewBag
            var properties = GetPropertiesFromDatabase(); // Get all properties for the original Buy view
            return View("Buy", properties);
        }

        private IEnumerable<property> FilterPropertiesFromDatabase(SearchViewModel model)
        {
            List<property> properties = new List<property>();

            // Implement your database query logic here based on the search criteria
            // Example query construction
            string query = "SELECT prop_id, seller_id, prop_location, prop_type, prop_description, prop_size, prop_price, prop_status, prop_image, prop_bed, prop_bath FROM property WHERE 1=1";

            if (!string.IsNullOrEmpty(model.Location))
            {
                query += $" AND prop_location LIKE '%{model.Location}%'";
            }

            if (!string.IsNullOrEmpty(model.PropertyType) && model.PropertyType.ToLower() != "any")
            {
                query += $" AND prop_type = '{model.PropertyType}'";
            }

            // Execute the query
            string connectionString = "Data Source=MRGDFK\\SQLEXPRESS;Initial Catalog=real_estate_listing_properties;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            properties.Add(new property
                            {
                                propId = Convert.ToInt32(reader["prop_id"]),
                                Title = reader["prop_description"].ToString(),
                                Location = reader["prop_location"].ToString(),
                                Price = reader["prop_price"].ToString(),
                                Area = reader["prop_size"].ToString(),
                                Bed = reader["prop_bed"].ToString(),
                                Bath = reader["prop_bath"].ToString(),
                                type = reader["prop_type"].ToString(),
                                status = reader["prop_status"].ToString(),
                                Image_01 = reader["prop_image"].ToString(),
                                SellerId = reader["seller_id"].ToString()
                            });
                        }
                    }
                }
            }

            return properties;
        }

        public ActionResult PropertyDetails(int id)
        {
            
                var property = GetPropertyById(id);
                if (property == null)
                {
                    ViewBag.ErrorMessage = "Property not found.";
                    return View("Error");
                }

                var seller = GetSellerById(property.SellerId);
                if (seller == null)
                {
                    ViewBag.ErrorMessage = "Seller not found.";
                    return View("Error");
                }

                var viewModel = new PropertyDetailsViewModel
                {
                    Property = property,
                    Seller = seller
                };

                return View(viewModel);
           
        }


        private property GetPropertyById(int id)
        {
            property prop = null;

            string connectionString = "Data Source=MRGDFK\\SQLEXPRESS;Initial Catalog=real_estate_listing_properties;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT prop_id, seller_id, prop_location, prop_type, prop_description, prop_size, prop_price, prop_status, prop_image, prop_bed, prop_bath FROM property WHERE prop_id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            prop = new property
                            {
                                propId = Convert.ToInt32(reader["prop_id"]),
                                Title = reader["prop_description"].ToString(),
                                Location = reader["prop_location"].ToString(),
                                Price = reader["prop_price"].ToString(),
                                Area = reader["prop_size"].ToString(),
                                Bed = reader["prop_bed"].ToString(),
                                Bath = reader["prop_bath"].ToString(),
                                type = reader["prop_type"].ToString(),
                                status = reader["prop_status"].ToString(),
                                Image_01 = reader["prop_image"].ToString(),
                                SellerId = reader["seller_id"].ToString()
                            };
                        }
                    }
                }
            }

            return prop;
        }

        private Seller GetSellerById(string sellerId)
        {
            Seller seller = null;
            string userId = null;

            string connectionString = "Data Source=MRGDFK\\SQLEXPRESS;Initial Catalog=real_estate_listing_properties;Integrated Security=True";

            // Get the user_id from the generate table using seller_id
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT users_id FROM generate WHERE seller_id = @sellerId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@sellerId", sellerId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userId = reader["users_id"].ToString();
                        }
                    }
                }
            }

            // Get the seller details from the users table using user_id
            if (!string.IsNullOrEmpty(userId))
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT users_id, users_name, users_email, users_contact FROM users WHERE users_id = @userId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                seller = new Seller
                                {
                                    Id = reader["users_id"].ToString(),
                                    Name = reader["users_name"].ToString(),
                                    Email = reader["users_email"].ToString(),
                                    Phone = reader["users_contact"].ToString()
                                };
                            }
                        }
                    }
                }
            }

            return seller;
        }
    }
}
