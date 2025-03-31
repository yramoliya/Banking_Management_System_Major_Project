using Banking_Management_System_Major_Project.Models;
using Banking_Management_System_Major_Project.Models.AdminModel;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.AspNetCore.Http;


namespace Banking_Management_System_Major_Project.Controllers
{
    public class BankController : Controller
    {
        LoginModel login = new LoginModel();
        Register_user reg = new Register_user();

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel lm)
        {
           if (ModelState.IsValid)
            {
                if (login.ValidateUser(lm.Email, lm.Password, out int userId, out string role))
                {
                    // Store user details in session
                    HttpContext.Session.SetInt32("UserId", userId);
                    HttpContext.Session.SetString("UserEmail", lm.Email);
                    HttpContext.Session.SetString("UserRole", role);

                    TempData["SuccessMessage"] = "Login successful!";

                    // Redirect based on role
                    if (role == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "User");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid email or password.";
                    return View(lm);
                }
            }
            return View(lm);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Register_user reguser)
        {
            if (!ModelState.IsValid)
            {
                return View(reguser);
            }

            string message;
            bool res = reg.Insert(reguser, out message);

            TempData["msg"] = message;
            if (res)
            {
                return RedirectToAction("Login");
            }

            return View(reguser);
        }
        public IActionResult LoanService()
        {
            return View();
        }
        public IActionResult Transactions()
        {
            return View();
        }

        public IActionResult Fund() 
        {
            return View();
        }

        public IActionResult InvestmentPlan()
        {
            return View();
        }
        public IActionResult TermsCondition()
        {
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Contact_us()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact_us(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Process form data (e.g., save to database, send an email, etc.)
                ViewBag.Message = "Your message has been sent successfully!";
                return View();
            }

            return View(model);
        }

    }
}
