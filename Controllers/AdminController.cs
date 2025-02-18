using Microsoft.AspNetCore.Mvc;

namespace Banking_Management_System_Major_Project.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult User_Manage()
        {
            return View();
        }
    }
}
