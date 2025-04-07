using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Banking_Management_System_Major_Project.Models.AdminModel;
using System.Reflection;
namespace Banking_Management_System_Major_Project.Controllers
{
    public class AdminController : Controller
    {
        UserRegistration Ureg = new UserRegistration();

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not admin
            }
            return View();
        }

        public IActionResult Users(int page = 1, int pageSize = 5)
        {
            Ureg = new UserRegistration();
            List<UserRegistration> lst = Ureg.getData();
            int totalUsers = lst.Count;
            int totalPages = (int)Math.Ceiling(totalUsers / (double)pageSize);

            var pagedUsers = lst
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            return View(lst);
        }

        public IActionResult UserRegistration()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not admin
            }
            return View();
        }
        [HttpPost]
        public IActionResult UserRegistration(UserRegistration ureg)
        {

            if (ModelState.IsValid)
            {
                UserRegistration reg = new UserRegistration();
                string message;
                bool isInserted = reg.Insert(ureg, out message);
                if (isInserted)
                {
                    TempData["Message"] = message;
                    return RedirectToAction("Users", "Admin");
                }
                else
                {
                    TempData["Error"] = message;
                }
            }
            return View(ureg);
        }

        // Manage Accounts
        public IActionResult Accounts()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not admin
            }
            return View();
        }

        // Manage Transactions
        public IActionResult Transactions()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not admin
            }
            return View();
        }

        // Loan Approvals
        public IActionResult Loans()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not admin
            }
            return View();
        }

        // Reports & Analytics
        public IActionResult Reports()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not admin
            }
            return View();
        }

        // Settings Page
        public IActionResult Settings()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not admin
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Bank");
        }
    }
}
