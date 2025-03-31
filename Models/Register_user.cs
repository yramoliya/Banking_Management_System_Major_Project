using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace Banking_Management_System_Major_Project.Models
{
    public enum UserRole
    {
        User,
        Admin
    }
    public class Register_user
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BMS;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public UserRole Role { get; set; } = UserRole.User;  // Default to User

        public bool Insert(Register_user reguser, out string message)
        {
            try
            {
                using (con)
                {
                    string query = "INSERT INTO Register (FirstName, LastName, Email, Password, Role) VALUES (@firstName, @lastName, @email, @password, @role)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@firstName", reguser.FirstName);
                        cmd.Parameters.AddWithValue("@lastName", reguser.LastName);
                        cmd.Parameters.AddWithValue("@email", reguser.Email);
                        cmd.Parameters.AddWithValue("@password", reguser.Password);
                        cmd.Parameters.AddWithValue("@role", reguser.Role.ToString());

                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        con.Close();

                        if (i >= 1)
                        {
                            SendEmail(reguser.Email, reguser.FirstName);
                            message = "Registration successful! Check your email.";
                            return true;
                        }
                        else
                        {
                            message = "Registration failed. Please try again.";
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = "Error: " + ex.Message;
                return false;
            }
        }

        private void SendEmail(string userEmail, string firstName)
        {
            try
            {
                string senderEmail = "parthtank2231@gmail.com";  // Your Gmail address
                string senderPassword = "ndqx tens vezx yako";  // Your Gmail App Password
                string subject = "Registration Successful";
                string body = $"Hello {firstName},\n\nWelcome to our system! Your registration was successful.\n\nBest Regards,\nYour Company Name";

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail);
                mail.To.Add(userEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                smtp.EnableSsl = true;

                smtp.Send(mail);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email sending failed: " + ex.Message);
            }
        }
    }
}
