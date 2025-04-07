using Banking_Management_System_Major_Project.Models;
using Banking_Management_System_Major_Project.Models.AdminModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Banking_Management_System_Major_Project.Controllers
{
    public class BankController : Controller
    {
        private readonly LoginModel login = new LoginModel();
        private readonly Register_user reg = new Register_user();

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel lm)
        {
            if (ModelState.IsValid)
            {
                if (login.ValidateUser(lm.Email, lm.Password, out string FirstName, out int userId, out string role))
                {
                    // Store user details in session
                    HttpContext.Session.SetInt32("UserId", userId);
                    HttpContext.Session.SetString("UserEmail", lm.Email);
                    HttpContext.Session.SetString("UserFirstName", FirstName);
                    HttpContext.Session.SetString("UserRole", role);

                    TempData["SuccessMessage"] = "Login successful!";

                    // Redirect based on role
                    return role == "Admin"
                        ? RedirectToAction("Index", "Admin")
                        : RedirectToAction("Index", "User");
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid email or password.";
                    return View(lm);
                }
            }
            return View(lm);
        }

        // Logout function
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear session on logout
            TempData["LogoutMessage"] = "You have been logged out successfully!";
            return RedirectToAction("Login");
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
            return res ? RedirectToAction("Login") : View(reguser);
        }

        // Protect dashboard pages with session validation
        public IActionResult Dashboard()
        {
            if (!IsUserLoggedIn()) return RedirectToAction("Login");

            ViewBag.UserFirstName = HttpContext.Session.GetString("UserFirstName");
            return View();
        }

        public IActionResult LoanService() => IsUserLoggedIn() ? View() : RedirectToLogin();
        public IActionResult Transactions() => IsUserLoggedIn() ? View() : RedirectToLogin();
        public IActionResult Fund() => IsUserLoggedIn() ? View() : RedirectToLogin();
        public IActionResult InvestmentPlan() => IsUserLoggedIn() ? View() : RedirectToLogin();
        public IActionResult TermsCondition() => IsUserLoggedIn() ? View() : RedirectToLogin();
        public IActionResult ForgotPassword() => View();
        public IActionResult AboutUs() => View();
        public IActionResult Contact_us() => View();

        [HttpPost]
        public IActionResult Contact_us(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Your message has been sent successfully!";
                return View();
            }
            return View(model);
        }

        // Helper method to check if user session exists
        private bool IsUserLoggedIn()
        {
            return HttpContext.Session.GetInt32("UserId") != null;
        }

        // Redirect helper function
        private IActionResult RedirectToLogin()
        {
            return RedirectToAction("Login");
        }
    }
}