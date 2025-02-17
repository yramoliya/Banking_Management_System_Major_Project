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

    }
}
