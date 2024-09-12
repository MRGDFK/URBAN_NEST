using System;
using System.Collections.Generic;
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
                        Session["JoinedDate"] = userDetails.JoinedDate.ToString("MMMM dd, yyyy");


                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to retrieve seller ID. Please try again.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password. Please try again.");
                }
            }
            return View(model);
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }



    




    }
}