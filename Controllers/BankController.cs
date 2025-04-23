using Banking_Management_System_Major_Project.Models;
using Banking_Management_System_Major_Project.Models.AdminModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using System.Net;

namespace Banking_Management_System_Major_Project.Controllers
{
    public class BankController : Controller
    {
        private readonly string connectionString = @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BMS;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
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
        [HttpGet]
        public IActionResult ForgotPassword() => View();
        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!ForgotPasswordModel.IsEmailRegistered(model.Email, connectionString))
            {
                ModelState.AddModelError("Email", "Email is not registered.");
                return View(model);
            }

            string otp = new Random().Next(100000, 999999).ToString();
            TempData["OTP"] = otp;
            TempData["Email"] = model.Email;

            SendEmail(model.Email, otp);
            return RedirectToAction("VerifyOTP");
        }
        private void SendEmail(string toEmail, string otp)
        {
            var fromAddress = new MailAddress("parthtank2231@gmail.com", "Bank Support");
            var toAddress = new MailAddress(toEmail);
            const string fromPass = "vqys xoon mbam mbkj";
            string subject = "Your OTP Code";
            string body = $"Your OTP for password reset is: {otp}";

            using var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(fromAddress.Address, fromPass)
            };

            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };
            smtp.Send(message);
        }
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
        [HttpGet]
        public IActionResult VerifyOtp() => View();

        [HttpPost]
        public IActionResult VerifyOtp(ForgotPasswordModel model)
        {
            string sentOtp = TempData["OTP"]?.ToString();
            string email = TempData["Email"]?.ToString();

            if (model.OTP != sentOtp)
            {
                ModelState.AddModelError("OTP", "Invalid OTP.");
                return View(model);
            }

            TempData["Email"] = email;
            return RedirectToAction("ResetPassword");
        }

        [HttpGet]
        public IActionResult ResetPassword() => View();

        [HttpPost]
        public IActionResult ResetPassword(string newPassword)
        {
            string email = TempData["Email"]?.ToString();
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("ForgotPassword");
            }

            bool success = ForgotPasswordModel.UpdatePassword(email, newPassword, connectionString);
            if (success)
                return RedirectToAction("Login");

            ViewBag.Error = "Failed to reset password.";
            return View();
        }
    }
}