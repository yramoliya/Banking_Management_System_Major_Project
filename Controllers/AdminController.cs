using Microsoft.AspNetCore.Mvc;

namespace Banking_Management_System_Major_Project.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
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
            return RedirectToAction("Login", "Account");
        }
    }
}
