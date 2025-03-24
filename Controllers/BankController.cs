using Banking_Management_System_Major_Project.Models;
using Banking_Management_System_Major_Project.Models.AdminModel;
using Microsoft.AspNetCore.Mvc;


namespace Banking_Management_System_Major_Project.Controllers
{
    public class BankController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IActionResult Login()
        {
            return View();
        }

        // POST: Handle Login
        [HttpPost]
        public IActionResult Login(UserRegistration model)
        {
            if (ModelState.IsValid)
            {
                // Check if user exists in the database
                var user = _context.UserRegistrations
                    .FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

                if (user != null)
                {
                    // Store user info in session
                    HttpContext.Session.SetInt32("UserId", user.User_Id);
                    HttpContext.Session.SetString("Username", user.Username);
                    HttpContext.Session.SetString("Role", user.Role.ToString());

                    TempData["SuccessMessage"] = "Login successful!";

                    // Redirect based on role
                    if (user.Role == Role.Admin)
                    {
                        return RedirectToAction("AdminDashboard", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("UserDashboard", "User");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid Username or Password.";
                }
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
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
