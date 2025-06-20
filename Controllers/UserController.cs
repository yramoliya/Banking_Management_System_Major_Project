using Banking_Management_System_Major_Project.Models.UserModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Banking_Management_System_Major_Project.Models.AdminModel;
using Microsoft.Data.SqlClient;

namespace Banking_Management_System_Major_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BMS;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        AccountDetails acc = new AccountDetails();
        ProfileViewModel prof = new ProfileViewModel();
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
        [HttpPost]
        public IActionResult ApplyCard(ApplyCardViewModel model) //=> IsUserLoggedIn() ? View() : RedirectToLogin();
        {
            model.FirstNumber = GenerateFourDigit();
            model.SecondNumber = GenerateFourDigit();
            model.ThirdNumber = GenerateFourDigit();
            model.FourthNumber = GenerateFourDigit();
            model.DebitNumber = $"{model.FirstNumber}-{model.SecondNumber}-{model.ThirdNumber}-{model.FourthNumber}";
            model.UniqueNumber = Guid.NewGuid().ToString("N").Substring(0, 16);
            model.CvvNumber = new Random().Next(100, 999).ToString();
            model.Random_Pin = new Random().Next(1000, 9999).ToString();
            model.Pin = model.Random_Pin;
            model.ValidUpto = DateTime.Now.AddYears(5).ToString("MM/yy");

            using SqlConnection con = new(connectionString);
            con.Open();
            string query = @"INSERT INTO Debit_Card_Apply 
        (DebitNumber, FirstNumber, SecondNumber, ThirdNumber, FourthNumber, UniqueNumber, ValidUpto, Firstname, Lastname, AcNumber, CvvNumber, Status, CardType, Random_Pin, Pin)
        VALUES (@DebitNumber, @FirstNumber, @SecondNumber, @ThirdNumber, @FourthNumber, @UniqueNumber, @ValidUpto, @Firstname, @Lastname, @AcNumber, @CvvNumber, @Status, @CardType, @Random_Pin, @Pin)";
            using SqlCommand cmd = new(query, con);
            cmd.Parameters.AddWithValue("@DebitNumber", model.DebitNumber);
            cmd.Parameters.AddWithValue("@FirstNumber", model.FirstNumber);
            cmd.Parameters.AddWithValue("@SecondNumber", model.SecondNumber);
            cmd.Parameters.AddWithValue("@ThirdNumber", model.ThirdNumber);
            cmd.Parameters.AddWithValue("@FourthNumber", model.FourthNumber);
            cmd.Parameters.AddWithValue("@UniqueNumber", model.UniqueNumber);
            cmd.Parameters.AddWithValue("@ValidUpto", model.ValidUpto);
            cmd.Parameters.AddWithValue("@Firstname", model.Firstname);
            cmd.Parameters.AddWithValue("@Lastname", model.Lastname);
            cmd.Parameters.AddWithValue("@AcNumber", model.AcNumber);
            cmd.Parameters.AddWithValue("@CvvNumber", model.CvvNumber);
            cmd.Parameters.AddWithValue("@Status", model.Status);
            cmd.Parameters.AddWithValue("@CardType", model.CardType);
            cmd.Parameters.AddWithValue("@Random_Pin", model.Random_Pin);
            cmd.Parameters.AddWithValue("@Pin", model.Pin);
            cmd.ExecuteNonQuery();
            return RedirectToAction("Index");
        }

        private string GenerateFourDigit() => new Random().Next(1000, 9999).ToString();

        [HttpGet]
        public IActionResult ShowBalance() //=> IsUserLoggedIn() ? View() : RedirectToLogin();
        {
            if (HttpContext.Session.GetString("UserRole") != "User")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not admin
            }
            return View();
        }

        [HttpPost]
        public IActionResult ShowBalance(AccountDetails model,string Id)
        {
            if (ModelState.IsValid)
            {
                AccountDetails Acc = acc.getData(Id);
                return View(Acc);
                string message;

                bool isSuccess = acc.ShowBalance(model, out message);
                if (isSuccess)
                {
                    TempData["Message"] = "Balance fetched successfully!";
                    return View("ShowBalance", model); // ✅ send the populated model
                }
                else
                {
                    TempData["Error"] = message;
                    return View("ShowBalance"); // show empty or error message
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Profile(string Email) //=> IsUserLoggedIn() ? View() : RedirectToLogin();
        {
            if (HttpContext.Session.GetString("UserRole") != "User")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not admin
            }
            ProfileViewModel Acc = prof.getData(Email);
            return View(Acc);
        }
        [HttpPost]
        public IActionResult Profile(ProfileViewModel profile) //=> IsUserLoggedIn() ? View() : RedirectToLogin();
        {
            if (HttpContext.Session.GetString("UserRole") != "User")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not admin
            }
            if (ModelState.IsValid)
            {
                ProfileViewModel reg = new ProfileViewModel();
                string message;
                bool isInserted = reg.Update(profile, out message);
                if (isInserted)
                {
                    TempData["Message"] = message;
                    return RedirectToAction("Profile", "User");
                }
                else
                {
                    TempData["Error"] = message;
                }
            }
            return View(profile);
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