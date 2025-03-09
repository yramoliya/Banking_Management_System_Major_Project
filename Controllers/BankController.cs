using Banking_Management_System_Major_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Banking_Management_System_Major_Project.Controllers
{
    public class BankController : Controller
    {
        public IActionResult Login()
        {
            return View();
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
