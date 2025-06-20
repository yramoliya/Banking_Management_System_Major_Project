using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Banking_Management_System_Major_Project.Models.AdminModel;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Banking_Management_System_Major_Project.Models.UserModel;
namespace Banking_Management_System_Major_Project.Controllers
{
    public class AdminController : Controller
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BMS;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        UserRegistration Ureg = new UserRegistration();
        AccountDetails acc = new AccountDetails();
        TransactionModel tc = new TransactionModel();
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

        [HttpGet]
        public IActionResult EditUser(string Id)
        {
            UserRegistration Urg = Ureg.getData(Id);
            return View(Urg);
        }

        [HttpPost]
        public IActionResult EditUser(UserRegistration ureg)
        {
            if (ModelState.IsValid)
            {
                UserRegistration reg = new UserRegistration();
                string message;
                bool isInserted = reg.Update(ureg, out message);
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
        [HttpGet]
        public IActionResult DeleteUser(string Id)
        {
            UserRegistration Urg = Ureg.getData(Id);
            return View(Urg);
        }
        [HttpPost]
        public IActionResult DeleteUser(UserRegistration ureg)
        {
            if (ModelState.IsValid)
            {
                UserRegistration reg = new UserRegistration();
                string message;
                bool isInserted = reg.Delete(ureg, out message);
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
            acc = new AccountDetails();
            List<AccountDetails> lst = acc.getData();
            return View(lst);
        }
        public IActionResult Cards()
        {
            List<ApplyCardViewModel> list = new();
            using SqlConnection con = new(connectionString);
            con.Open();
            string query = "SELECT * FROM Debit_Card_Apply";
            using SqlCommand cmd = new(query, con);
            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new ApplyCardViewModel
                {
                    DebitNumber = dr["DebitNumber"].ToString(),
                    FirstNumber = dr["FirstNumber"].ToString(),
                    SecondNumber = dr["SecondNumber"].ToString(),
                    ThirdNumber = dr["ThirdNumber"].ToString(),
                    FourthNumber = dr["FourthNumber"].ToString(),
                    UniqueNumber = dr["UniqueNumber"].ToString(),
                    ValidUpto = dr["ValidUpto"].ToString(),
                    Firstname = dr["Firstname"].ToString(),
                    Lastname = dr["Lastname"].ToString(),
                    AcNumber = dr["AcNumber"].ToString(),
                    CvvNumber = dr["CvvNumber"].ToString(),
                    Status = dr["Status"].ToString(),
                    CardType = dr["CardType"].ToString(),
                    Random_Pin = dr["Random_Pin"].ToString(),
                    Pin = dr["Pin"].ToString()
                });
            }
            return View(list);
        }
        public IActionResult Passbook(string Id)
        {
            //acc = new AccountDetails();
            //List<AccountDetails> lst = acc.getData();
            //return View(lst);
            AccountDetails Acc = acc.getData(Id);
            return View(Acc);
        }

        //Add Account From Admin
        public IActionResult Add_Accounts()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not admin
            }
            var model = new AccountDetails(); // This will auto-generate values
            return View(model);
        }

        [HttpPost]
        public IActionResult Add_Accounts(AccountDetails account)
        {

            if (ModelState.IsValid)
            {
                AccountDetails accountDetails = new AccountDetails();
                string message;
                bool isInserted = accountDetails.Insert(account, out message);
                if (isInserted)
                {
                    TempData["Message"] = message;
                    return RedirectToAction("Accounts", "Admin");
                }
                else
                {
                    TempData["Error"] = message;
                }
            }
            return View(account);
        }
        [HttpGet]
        public IActionResult Editaccount(string Id)
        {
            AccountDetails Acc = acc.getData(Id);
            return View(Acc);
        }

        [HttpPost]
        public IActionResult Editaccount(AccountDetails account)
        {
            if (ModelState.IsValid)
            {
                AccountDetails accountDetails = new AccountDetails();
                string message;
                bool isInserted = accountDetails.update(account, out message);
                if (isInserted)
                {
                    TempData["Message"] = message;
                    return RedirectToAction("Accounts", "Admin");
                }
                else
                {
                    TempData["Error"] = message;
                }
            }
            return View(account);
        }
        [HttpGet]
        public IActionResult Deleteaccount(string Id)
        {
            AccountDetails Acc = acc.getData(Id);
            return View(Acc);
        }
        [HttpPost]
        public IActionResult Deleteaccount(AccountDetails account)
        {
            if (ModelState.IsValid)
            {
                AccountDetails accountDetails = new AccountDetails();
                string message;
                bool isInserted = accountDetails.delete(account, out message);
                if (isInserted)
                {
                    TempData["Message"] = message;
                    return RedirectToAction("Accounts", "Admin");
                }
                else
                {
                    TempData["Error"] = message;
                }
            }
            return View(account);
        }
        // Manage Transactions
        public IActionResult Transactions()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not admin
            }
            var data = tc.GetAll();
            return View(data);
        }
        public IActionResult CreateTransation()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Login", "Bank"); // Restrict access if not admin
            }
            var model1 = new TransactionModel(); // This will auto-generate values
            return View(model1);
        }
        [HttpPost]
        public IActionResult CreateTransation(TransactionModel model)
        {
            string result = model.AddTransaction(model);
            if (result == "Success")
                return RedirectToAction("Index");
            ViewBag.Error = "Transaction Failed";
            return View(model);
        }

        // Edit GET
        public IActionResult EditTransation(string acNumber)
        {
            var item = tc.GetAll().Find(x => x.Acnumber == acNumber);
            return View(item);
        }

        // Edit POST
        [HttpPost]
        public IActionResult EditTransation(TransactionModel model)
        {
            string result = model.UpdateTransaction(model);
            return RedirectToAction("Index");
        }

        // Delete
        public IActionResult DeleteTransation(string acNumber)
        {
            TransactionModel model = new TransactionModel();
            model.DeleteTransaction(acNumber);
            return RedirectToAction("Index");
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
