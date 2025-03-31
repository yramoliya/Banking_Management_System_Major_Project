using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace Banking_Management_System_Major_Project.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not admin
            }
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

        // Manage Accounts
        public IActionResult Accounts()
        {
            return View();
        }

        // Manage Transactions
        public IActionResult Transactions()
        {
            return View();
        }

        // Loan Approvals
        public IActionResult Loans()
        {
            return View();
        }

        // Reports & Analytics
        public IActionResult Reports()
        {
            return View();
        }

        // Settings Page
        public IActionResult Settings()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Bank");
        }
    }
}
