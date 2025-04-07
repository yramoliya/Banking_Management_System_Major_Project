using Banking_Management_System_Major_Project.Models.UserModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Banking_Management_System_Major_Project.Controllers
{
    public class UserController : Controller
    {
        // Validate session before accessing user dashboard
        public IActionResult Index()
        {
            if (!IsUserLoggedIn()) return RedirectToAction("Login", "Bank");

            // Fetch User Name from Session
            ViewBag.UserFirstName = HttpContext.Session.GetString("UserFirstName");

            return View();
        }

        public IActionResult ApplyCard() //=> IsUserLoggedIn() ? View() : RedirectToLogin();
        {
            if (HttpContext.Session.GetString("UserRole") != "User")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not admin
            }
            return View();
        }
        public IActionResult ShowBalance() //=> IsUserLoggedIn() ? View() : RedirectToLogin();
        {
            if (HttpContext.Session.GetString("UserRole") != "User")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not admin
            }
            return View();
        }

        [HttpPost]
        public IActionResult ShowBalance(BalanceViewModel model)
        {
            if (!IsUserLoggedIn()) return RedirectToLogin();

            if (ModelState.IsValid)
            {
                // Mocking balance retrieval
                model.Balance = 5000.75m;
                return View(model);
            }
            return View(model);
        }

        public IActionResult Profile() //=> IsUserLoggedIn() ? View() : RedirectToLogin();
        {
            if (HttpContext.Session.GetString("UserRole") != "User")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not admin
            }
            return View();
        }
        public IActionResult ApplyLoan() //=> IsUserLoggedIn() ? View() : RedirectToLogin();
        {
            if (HttpContext.Session.GetString("UserRole") != "User")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not admin
            }
            return View();
        }
        public IActionResult KYCConfirmation() //=> IsUserLoggedIn() ? View() : RedirectToLogin();
        {
            if (HttpContext.Session.GetString("UserRole") != "User")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not admin
            }
            return View();
        }
        public IActionResult FundTransfer() //=> IsUserLoggedIn() ? View() : RedirectToLogin();
        {
            if (HttpContext.Session.GetString("UserRole") != "User")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not admin
            }
            return View();
        }
        public IActionResult WithdrawMoney() //=> IsUserLoggedIn() ? View() : RedirectToLogin();
        {
            if (HttpContext.Session.GetString("UserRole") != "User")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not admin
            }
            return View();
        }

        // Logout function with session clearing
        public IActionResult Logout()
        {
            if (!IsUserLoggedIn()) return RedirectToLogin();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogoutConfirmed()
        {
            HttpContext.Session.Clear(); // Clear session properly
            TempData["LogoutMessage"] = "You have been logged out successfully!";
            return RedirectToAction("Index", "Home");
        }

        // Helper method to check if user session exists
        private bool IsUserLoggedIn()
        {
            return HttpContext.Session.GetString("UserRole") == "User";
        }

        // Redirect helper function
        private IActionResult RedirectToLogin()
        {
            return RedirectToAction("Login", "Bank");
        }
    }
}