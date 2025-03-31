using Banking_Management_System_Major_Project.Models.UserModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Banking_Management_System_Major_Project.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "User")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not user
            }
            return View();
        }
        public IActionResult ApplyCard()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ShowBalance()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ShowBalance(BalanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Mocking balance retrieval
                model.Balance = 5000.75m;
                return View(model);
            }
            return View(model);
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult ApplyLoan()
        {
            return View();
        }
        public IActionResult KYCConfirmation()
        {
            return View();
        }
        public IActionResult FundTransfer()
        {
            return View();
        }
        public IActionResult WithdrawMoney()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogoutConfirmed()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
