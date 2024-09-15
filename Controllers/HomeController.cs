using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrbanNest.Data;
using UrbanNest.Models;

namespace UrbanNest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Your Login page.";

            return View();
        }

        public ActionResult Signup()
        {
            ViewBag.Message = "Your Signup page.";

            return View();
        }

        public ActionResult Sell()
        {
            ViewBag.Message = "Your Sell Page";
            return View();
        }

        public ActionResult UserDashboard()
        {
            if (Session["LoggedIn"] != null && Session["LoggedIn"].ToString() == "active")
            {
                ViewBag.FullName = Session["FullName"];
                ViewBag.Email = Session["Email"];
                ViewBag.Phone = Session["Phone"];
                ViewBag.Address = Session["Address"];
                ViewBag.JoinedDate = Session["JoinedDate"];

                ViewBag.ProfilePicture = Session["ProfilePicture"] != null ? Session["ProfilePicture"].ToString() : "default.png";  // Fallback to default if no picture

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");  // Redirect to login if not authenticated
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup (Signup model)
        {
            if (ModelState.IsValid)
            {
                int newUserId = Database_helper.RegisterUser(model.FirstName, model.LastName, model.Email, model.Password, model.Address, model.Phone);
                if (newUserId>0)
                {
                    
                    string sellerId = Database_helper.GetSellerId(newUserId);
                    if (!string.IsNullOrEmpty(sellerId))
                    {
                        // Step 3: Store the seller_id in the session
                        Session["seller_id"] = sellerId;

                        // Redirect to the home page or another appropriate page
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to retrieve seller ID. Please try again.");
                    }
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed");
                }
            }
            return View(model);
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                if (Database_helper.ValidUser(model.Email, model.Password))
                {   

                    Session["LoggedIn"] = "active";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed");
                }
            }
            return View(model);
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                int userId = Database_helper.GetUserId(model.Email, model.Password);
                if (userId > 0)
                {
                    string sellerId = Database_helper.GetSellerId(userId);

                    if (sellerId != null)
                    {
                        var userDetails = Database_helper.GetUserDetails(userId);

                        Session["LoggedIn"] = "active";
                        Session["seller_id"] = sellerId;
                        Session["FullName"] = userDetails.FullName;
                        Session["Email"] = userDetails.Email;
                        Session["Phone"] = userDetails.Phone;
                        Session["Address"] = userDetails.Address;
                        Session["ProfilePicture"] = userDetails.ProfilePicture;
                        Session["JoinedDate"] = userDetails.JoinedDate.ToString("MMMM dd, yyyy");


                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Failed to retrieve seller ID. Please try again.";
                        return RedirectToAction("Login");
                        //ModelState.AddModelError("", "Failed to retrieve seller ID. Please try again.");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid email or password. Please try again.";
                    return RedirectToAction("Login");
                    //ModelState.AddModelError("", "Invalid email or password. Please try again.");
                }
            }
            return View(model);
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadProfilePicture(HttpPostedFileBase profilePicture)
        {
            if (profilePicture != null && profilePicture.ContentLength > 0)
            {
                // Generate a unique file name
                var fileName = Path.GetFileNameWithoutExtension(profilePicture.FileName);
                var extension = Path.GetExtension(profilePicture.FileName);
                var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                // Define the path where the file will be saved
                var path = Path.Combine(Server.MapPath("~/Content/ProfilePictures/"), uniqueFileName);

                // Save the file
                profilePicture.SaveAs(path);

                // Update the user's profile picture in the database
                string email = Session["Email"]?.ToString(); // Or however you identify the user
                if (Database_helper.UpdateUserProfilePicture(email, uniqueFileName))
                {
                    // Update the session or ViewBag with the new profile picture path
                    Session["ProfilePicture"] = uniqueFileName;
                    ViewBag.ProfilePicture = uniqueFileName;
                }
                else
                {
                    ModelState.AddModelError("", "Failed to update profile picture.");
                }
            }
            return RedirectToAction("UserDashboard");
        }







    }
}