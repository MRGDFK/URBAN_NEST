using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrbanNest.Models;

namespace UrbanNest.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult UserDashboard()
        {
           
            return View();
        }
        public ActionResult CDB()
        {
            var currentUser = GetCurrentUser();
            if (currentUser != null)
            {
                ViewBag.UserName = currentUser.users_name;
                ViewBag.UserEmail = currentUser.users_email;
                ViewBag.FullName = currentUser.users_name;
                ViewBag.Email = currentUser.users_email;
                ViewBag.Phone = currentUser.users_contact;
                ViewBag.Address = currentUser.users_address;
              //  ViewBag.JoinedDate = DateTime.Now.ToString("MMMM dd, yyyy"); // Assuming joined date is now for demo purposes

                return View();
            }
            else
            {
                // Handle user not found scenario
                return RedirectToAction("Login", "Home");
            }
        }

        private User GetCurrentUser()
        {
            User user = null;

            // Assuming you have a way to get the logged-in user's username or ID from the session
            string loggedInUsername = Session["Username"] as string;

            // Connection string directly in the code
            string connectionString = "Data Source=DESKTOP-AIKR8ED\\SQLEXPRESS01;Initial Catalog=real_estate_listing_properties;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT users_id, users_name, users_username, users_image, users_address, users_contact, users_email, users_password FROM users WHERE users_username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@User_name", loggedInUsername);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User()
                            {
                                users_id = Convert.ToInt32(reader["users_id"]),
                                users_name = reader["users_name"].ToString(),
                                users_username = reader["users_username"].ToString(),
                                users_image = reader["users_image"].ToString(),
                                users_address = reader["users_address"].ToString(),
                                users_contact = reader["users_contact"].ToString(),
                                users_email = reader["users_email"].ToString(),
                                users_password = reader["users_password"].ToString()
                            };
                        }
                    }
                }
            }

            return user;
        }

    }
}