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
    }
}
