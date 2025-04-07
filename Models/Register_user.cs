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
        public string Fname { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string Lname { get; set; }

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
                    string query = "INSERT INTO UserRegistrations (Fname, Lname, Email, Password, Role) VALUES (@firstName, @lastName, @email, @password, @role)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@firstName", reguser.Fname);
                        cmd.Parameters.AddWithValue("@lastName", reguser.Lname);
                        cmd.Parameters.AddWithValue("@email", reguser.Email);
                        cmd.Parameters.AddWithValue("@password", reguser.Password);
                        cmd.Parameters.AddWithValue("@role", reguser.Role.ToString());

                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        con.Close();

                        if (i >= 1)
                        {
                            SendEmail(reguser.Email, reguser.Fname);
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
                string senderEmail = "parthtank2231@gmail.com"; // ✅ Use your real domain-based email here (not free Gmail if possible)
                string senderPassword = "vqys xoon mbam mbkj"; // Use App Password or SMTP credentials
                string subject = "✅ Welcome to Banking System - Registration Successful";
                string body = $@"
        <html>
        <body style='font-family: Arial, sans-serif;'>
            <h2 style='color: #2e6c80;'>Hello {firstName},</h2>
            <p>Thank you for registering with <strong>Banking System</strong>.</p>
            <p>Your registration was <strong>successful</strong>. You can now log in to your account.</p>
            <br/>
            <p style='color: #555;'>Best Regards,<br/><strong>Banking System Team</strong></p>
        </body>
        </html>";

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail, "Banking System Support"); // ✅ Friendly name
                mail.To.Add(userEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                // ✅ Set high priority and headers (optional but helps)
                mail.Priority = MailPriority.High;
                mail.Headers.Add("X-Priority", "1");
                mail.Headers.Add("X-MSMail-Priority", "High");
                mail.Headers.Add("X-Mailer", "Microsoft .NET");

                // ✅ Recommended: Use authenticated SMTP (prefer custom domain or services like SendGrid)
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587); // You can change to SendGrid, Mailgun, etc.
                smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                smtp.EnableSsl = true;

                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email sending failed: " + ex.Message);
            }
        }
    }
}
