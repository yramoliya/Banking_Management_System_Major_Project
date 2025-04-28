using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Banking_Management_System_Major_Project.Models.AdminModel
{
    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public enum Role
    {
        Admin,
        User
    }

    public class UserRegistration
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BMS;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

        [Key]
        public int User_Id { get; set; }

        [Required(ErrorMessage = "Please enter first name.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "First Name must contain only letters")]
        public string Fname { get; set; }

        [Required(ErrorMessage = "Please enter middle name.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Middle Name must contain only letters")]
        public string Mname { get; set; }

        [Required(ErrorMessage = "Please enter last name.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Last Name must contain only letters")]
        public string Lname { get; set; }

        [Required(ErrorMessage = "Please enter a valid email.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Please enter a valid mobile number.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be 10 digits.")]
        public string Mobile { get; set; } // Changed to string to prevent leading zero issues

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
        ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]

        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public Role Role { get; set; } = Role.User; // Default value is User

        public List<UserRegistration> getData()
        {
            List<UserRegistration> lstureg = new List<UserRegistration>();
            SqlDataAdapter sdap = new SqlDataAdapter("SELECT * FROM UserRegistrations", con);
            DataSet ds = new DataSet();
            sdap.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lstureg.Add(new UserRegistration
                {
                    User_Id = Convert.ToInt32(dr["User_Id"].ToString()),
                    Fname = dr["Fname"].ToString(),
                    Mname = dr["Mname"].ToString(),
                    Lname = dr["Lname"].ToString(),
                    Email = dr["Email"].ToString(),
                    Gender = (Gender)Enum.Parse(typeof(Gender), dr["Gender"].ToString()),
                    Mobile = dr["Mobile"].ToString(),
                    DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]),
                    Password = dr["Password"].ToString(),
                    Role = (Role)Enum.Parse(typeof(Role), dr["Role"].ToString())
                });
            }
            return lstureg;

        }

        public UserRegistration getData(string Id)
        {
            UserRegistration emp = new UserRegistration();
            SqlCommand cmd = new SqlCommand("SELECT * FROM UserRegistrations WHERE User_Id='"+Id+"'", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    emp.User_Id = Convert.ToInt32(dr["User_Id"].ToString());
                    emp.Fname = dr["Fname"].ToString();
                    emp.Mname = dr["Mname"].ToString();
                    emp.Lname = dr["Lname"].ToString();
                    emp.Email = dr["Email"].ToString();
                    emp.Gender = (Gender)Enum.Parse(typeof(Gender), dr["Gender"].ToString());
                    emp.Mobile = dr["Mobile"].ToString();
                    emp.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
                    emp.Password = dr["Password"].ToString();
                    emp.Role = (Role)Enum.Parse(typeof(Role), dr["Role"].ToString());
                }
            }
            con.Close();
            return emp;
        }

        public bool Insert(UserRegistration Ureg, out string message)
        {
            message = string.Empty;

            try
            {
                using (con)
                {
                    string query = @"INSERT INTO UserRegistrations 
                            (Fname, Mname, Lname, Email, Gender, Mobile, DateOfBirth, Password, Role) 
                            VALUES 
                            (@Fname, @Mname, @Lname, @Email, @Gender, @Mobile, @DateOfBirth, @Password, @Role)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Fname", Ureg.Fname);
                        cmd.Parameters.AddWithValue("@Mname", Ureg.Mname);
                        cmd.Parameters.AddWithValue("@Lname", Ureg.Lname);
                        cmd.Parameters.AddWithValue("@Email", Ureg.Email);
                        cmd.Parameters.AddWithValue("@Gender", Ureg.Gender.ToString());
                        cmd.Parameters.AddWithValue("@Mobile", Ureg.Mobile);
                        cmd.Parameters.AddWithValue("@DateOfBirth", Ureg.DateOfBirth);
                        cmd.Parameters.AddWithValue("@Password", Ureg.Password); // You may hash this
                        cmd.Parameters.AddWithValue("@Role", Ureg.Role.ToString());

                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        con.Close();

                        if (result > 0)
                        {
                            SendEmail(Ureg.Email, Ureg.Fname, false);
                            message = "Registration successful! Confirmation email sent.";
                            return true;
                        }
                        else
                        {
                            message = "Registration failed. No rows affected.";
                            return false;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                message = "SQL Error: " + ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                message = "Unexpected Error: " + ex.Message;
                return false;
            }
        }

        public bool Update(UserRegistration Ureg, out string message)
        {
            try
            {
                message = string.Empty;
                using (con)
                {
                    string query = @"UPDATE UserRegistrations SET Fname=@Fname, Mname=@Mname, Lname=@Lname, Email=@Email, Gender=@Gender, Mobile=@Mobile, DateOfBirth=@DateOfBirth, Password=@Password, Role=@Role WHERE User_Id=@Id";


                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Fname", Ureg.Fname);
                        cmd.Parameters.AddWithValue("@Mname", Ureg.Mname);
                        cmd.Parameters.AddWithValue("@Lname", Ureg.Lname);
                        cmd.Parameters.AddWithValue("@Email", Ureg.Email);
                        cmd.Parameters.AddWithValue("@Gender", Ureg.Gender.ToString());
                        cmd.Parameters.AddWithValue("@Mobile", Ureg.Mobile);
                        cmd.Parameters.AddWithValue("@DateOfBirth", Ureg.DateOfBirth);
                        cmd.Parameters.AddWithValue("@Password", Ureg.Password); // You may hash this
                        cmd.Parameters.AddWithValue("@Role", Ureg.Role.ToString());
                        cmd.Parameters.AddWithValue("@Id", Ureg.User_Id);                        

                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        con.Close();

                        if (result > 0)
                        {
                            SendEmail(Ureg.Email, Ureg.Fname, true);
                            message = "Profile Update successful! Confirmation email sent.";
                            return true;
                        }
                        else
                        {
                            message = "Profile Update failed. No rows affected.";
                            return false;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                message = "SQL Error: " + ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                message = "Unexpected Error: " + ex.Message;
                return false;
            }
        }
        public bool Delete(UserRegistration Ureg, out string message)
        {
            try
            {
                message = string.Empty;
                using (con)
                {
                    string query = @"DELETE FROM UserRegistrations WHERE User_Id=@Id";


                    using (SqlCommand cmd = new SqlCommand(query, con))
                    { 
                        cmd.Parameters.AddWithValue("@Id", Ureg.User_Id);

                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        con.Close();

                        if (result > 0)
                        {
                            SendEmail(Ureg.Email, Ureg.Fname, true);
                            message = "Profile Delete successful! Confirmation email sent.";
                            return true;
                        }
                        else
                        {
                            message = "Profile Delete failed. No rows affected.";
                            return false;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                message = "SQL Error: " + ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                message = "Unexpected Error: " + ex.Message;
                return false;
            }
        }
        private void SendEmail(string userEmail, string firstName, bool isUpdate = false)
        {
            try
            {
                string senderEmail = "parthtank2231@gmail.com"; // ✅ Use your real domain-based email here (not free Gmail if possible)
                string senderPassword = "vqys xoon mbam mbkj"; // Use App Password or SMTP credentials
                string subject = isUpdate
            ? "🔁 Profile Updated Successfully - Banking System"
            : "✅ Welcome to Banking System - Registration Successful";

                string body = isUpdate
                    ? $@"
                <html>
                <body style='font-family: Arial, sans-serif;'>
                    <h2 style='color: #2e6c80;'>Hello {firstName},</h2>
                    <p>Your <strong>profile has been successfully updated</strong> in the Banking System.</p>
                    <p>If you did not make this change, please contact support immediately.</p>
                    <br/>
                    <p style='color: #555;'>Best Regards,<br/><strong>Banking System Team</strong></p>
                </body>
                </html>"
                    : $@"
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
                Console.WriteLine("Email send failed: " + ex.Message);
            }
        }
    }
}
