﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net.Mail;
using System.Net;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace Banking_Management_System_Major_Project.Models.AdminModel
{
    public enum gender
    {
        Male,
        Female,
        Other
    }
    public enum AccountType
    {
        Saving,
        Current,
        Other
    }

    public class AccountDetails
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BMS;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

        [Key]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]{2,50}$", ErrorMessage = "First name must be alphabetic and 2-50 characters long.")]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Middle name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]{2,50}$", ErrorMessage = "Middle name must be alphabetic and 2-50 characters long.")]
        [Display(Name = "Middle Name")]
        public string Middlename { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]{2,50}$", ErrorMessage = "Last name must be alphabetic and 2-50 characters long.")]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? Dob { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [RegularExpression(@"^(Male|Female|Other)$", ErrorMessage = "Gender must be Male, Female, or Other.")]
        public gender Gender { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [Display(Name = "Mobile Number")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter a valid 10-digit mobile number starting with 6-9.")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Email ID is required.")]
        [Display(Name = "Email ID")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string Email { get; set; }

        [Display(Name = "Alternate Number")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter a valid 10-digit mobile number starting with 6-9.")]
        public string AlternateNumber { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [RegularExpression(@"^[a-zA-Z\s]{2,50}$", ErrorMessage = "Country name must be alphabetic.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [RegularExpression(@"^[a-zA-Z\s]{2,50}$", ErrorMessage = "State name must be alphabetic.")]
        public string State { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [RegularExpression(@"^[a-zA-Z\s]{2,50}$", ErrorMessage = "City name must be alphabetic.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Pincode is required.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Enter a valid 6-digit pincode.")]
        public string Pincode { get; set; }

        [Required(ErrorMessage = "Account type is required.")]
        [Display(Name = "Account Type")]
        public AccountType? AcType { get; set; }

        [Display(Name = "Account Number")]
        //[RegularExpression(@"^\d{9}$", ErrorMessage = "Enter a valid 12-digit Account number.")]
        public string AccountNumber { get; set; } // Autogenerated

        [Display(Name = "IFSC Number")]
        public string Ifsc { get; set; } // Autogenerated

        [Display(Name = "PAN Number")]
        [RegularExpression(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$", ErrorMessage = "Enter a valid PAN number.")]
        public string PanNumber { get; set; }

        [Display(Name = "Aadhaar Number")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Enter a valid 12-digit Aadhaar number.")]
        public string AddharNumber { get; set; }

        [Display(Name = "Qualification")]
        [RegularExpression(@"^[a-zA-Z\s]{2,50}$", ErrorMessage = "Qualification must be alphabetic.")]
        public string Qulification { get; set; }

        [Display(Name = "Account Balance")]
        [RegularExpression(@"^\d{1,10}(\.\d{1,2})?$", ErrorMessage = "Enter a valid amount (up to 2 decimals).")]
        public string AcBalance { get; set; }

        public AccountDetails()
        {
            GenerateAccountNumber();
        }

        private void GenerateAccountNumber()
        {
            Random random = new Random();
            AccountNumber = "365" + random.Next(100000000, 999999999);
            Ifsc = "BMSN0001"; // Or generate a random IFSC if needed
        }
        public List<AccountDetails> getData()
        {
            List<AccountDetails> accounts = new List<AccountDetails>();
            SqlDataAdapter sdap = new SqlDataAdapter("SELECT * FROM AccountDetails", con);
            DataSet ds = new DataSet();
            sdap.Fill(ds);

            foreach (DataRow reader in ds.Tables[0].Rows)
            {
                accounts.Add(new AccountDetails
                {
                    AccountId = Convert.ToInt32(reader["AccountId"]),
                    Firstname = reader["Firstname"].ToString(),
                    Middlename = reader["Middlename"].ToString(),
                    Lastname = reader["Lastname"].ToString(),
                    Dob = Convert.ToDateTime(reader["Dob"]),
                    Gender = Enum.Parse<gender>(reader["Gender"].ToString()),
                    MobileNumber = reader["MobileNumber"].ToString(),
                    Email = reader["Email"].ToString(),
                    AlternateNumber = reader["AlternateNumber"].ToString(),
                    Country = reader["Country"].ToString(),
                    State = reader["State"].ToString(),
                    City = reader["City"].ToString(),
                    Pincode = reader["Pincode"].ToString(),
                    AcType = Enum.Parse<AccountType>(reader["AcType"].ToString()),
                    AccountNumber = reader["AccountNumber"].ToString(),
                    Ifsc = reader["Ifsc"].ToString(),
                    PanNumber = reader["PanNumber"].ToString(),
                    AddharNumber = reader["AddharNumber"].ToString(),
                    Qulification = reader["Qulification"].ToString(),
                    AcBalance = reader["AcBalance"].ToString()
                });
            }
            return accounts;
        }
        public AccountDetails getData(string Id)
        {
            AccountDetails acc = new AccountDetails();
            SqlCommand cmd = new SqlCommand("SELECT * FROM AccountDetails WHERE AccountId='" + Id + "'", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    acc.Firstname = reader["Firstname"].ToString();
                    acc.Middlename = reader["Middlename"].ToString();
                    acc.Lastname = reader["Lastname"].ToString();
                    acc.Dob = Convert.ToDateTime(reader["Dob"]);
                    acc.Gender = Enum.Parse<gender>(reader["Gender"].ToString());
                    acc.MobileNumber = reader["MobileNumber"].ToString();
                    acc.Email = reader["Email"].ToString();
                    acc.AlternateNumber = reader["AlternateNumber"].ToString();
                    acc.Country = reader["Country"].ToString();
                    acc.State = reader["State"].ToString();
                    acc.City = reader["City"].ToString();
                    acc.Pincode = reader["Pincode"].ToString();
                    acc.AcType = Enum.Parse<AccountType>(reader["AcType"].ToString());
                    acc.AccountNumber = reader["AccountNumber"].ToString();
                    acc.Ifsc = reader["Ifsc"].ToString();
                    acc.PanNumber = reader["PanNumber"].ToString();
                    acc.AddharNumber = reader["AddharNumber"].ToString();
                    acc.Qulification = reader["Qulification"].ToString();
                    acc.AcBalance = reader["AcBalance"].ToString();
                    acc.AccountId = Convert.ToInt32(reader["AccountId"]);
                }
            }
            con.Close();
            return acc;
        }
        public bool Insert(AccountDetails model, out string message)
        {
            message = string.Empty;

            try
            {
                using (con)
                {
                    string query = @"INSERT INTO AccountDetails 
                                    (Firstname, Middlename, Lastname, Dob, Gender, MobileNumber, Email, AlternateNumber, Country, State, City, Pincode, AcType, AccountNumber, Ifsc, PanNumber, AddharNumber, Qulification, AcBalance)
                                    VALUES 
                                    (@Firstname, @Middlename, @Lastname, @Dob, @Gender, @MobileNumber, @Email, @AlternateNumber, @Country, @State, @City, @Pincode, @AcType, @AccountNumber, @Ifsc, @PanNumber, @AddharNumber, @Qulification, @AcBalance)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Firstname", model.Firstname);
                        cmd.Parameters.AddWithValue("@Middlename", model.Middlename);
                        cmd.Parameters.AddWithValue("@Lastname", model.Lastname);
                        cmd.Parameters.AddWithValue("@Dob", model.Dob);
                        cmd.Parameters.AddWithValue("@Gender", model.Gender.ToString());
                        cmd.Parameters.AddWithValue("@MobileNumber", model.MobileNumber);
                        cmd.Parameters.AddWithValue("@Email", model.Email);
                        cmd.Parameters.AddWithValue("@AlternateNumber", model.AlternateNumber);
                        cmd.Parameters.AddWithValue("@Country", model.Country);
                        cmd.Parameters.AddWithValue("@State", model.State);
                        cmd.Parameters.AddWithValue("@City", model.City);
                        cmd.Parameters.AddWithValue("@Pincode", model.Pincode);
                        cmd.Parameters.AddWithValue("@AcType", model.AcType.ToString());
                        cmd.Parameters.AddWithValue("@AccountNumber", model.AccountNumber);
                        cmd.Parameters.AddWithValue("@Ifsc", model.Ifsc);
                        cmd.Parameters.AddWithValue("@PanNumber", model.PanNumber);
                        cmd.Parameters.AddWithValue("@AddharNumber", model.AddharNumber);
                        cmd.Parameters.AddWithValue("@Qulification", model.Qulification);
                        cmd.Parameters.AddWithValue("@AcBalance", model.AcBalance);

                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        con.Close();

                        if (result > 0)
                        {
                            SendEmail(model.Email, model.Firstname, false);
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

        public bool update(AccountDetails model, out string message)
        {
            message = string.Empty;

            try
            {
                using (con)
                {

                    string query = @"UPDATE AccountDetails
                             SET Firstname=@Firstname, Middlename=@Middlename, Lastname=@Lastname, Dob=@Dob, 
                                 Gender=@Gender, MobileNumber=@MobileNumber, Email=@Email, AlternateNumber=@AlternateNumber, 
                                 Country=@Country, State=@State, City=@City, Pincode=@Pincode, AcType=@AcType, 
                                 AccountNumber=@AccountNumber, Ifsc=@Ifsc, PanNumber=@PanNumber, AddharNumber=@AddharNumber, 
                                 Qulification=@Qulification, AcBalance=@AcBalance
                             WHERE AccountId=@AccountId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@AccountId", model.AccountId);
                        cmd.Parameters.AddWithValue("@Firstname", model.Firstname);
                        cmd.Parameters.AddWithValue("@Middlename", model.Middlename);
                        cmd.Parameters.AddWithValue("@Lastname", model.Lastname);
                        cmd.Parameters.AddWithValue("@Dob", model.Dob);
                        cmd.Parameters.AddWithValue("@Gender", model.Gender.ToString());
                        cmd.Parameters.AddWithValue("@MobileNumber", model.MobileNumber);
                        cmd.Parameters.AddWithValue("@Email", model.Email);
                        cmd.Parameters.AddWithValue("@AlternateNumber", model.AlternateNumber);
                        cmd.Parameters.AddWithValue("@Country", model.Country);
                        cmd.Parameters.AddWithValue("@State", model.State);
                        cmd.Parameters.AddWithValue("@City", model.City);
                        cmd.Parameters.AddWithValue("@Pincode", model.Pincode);
                        cmd.Parameters.AddWithValue("@AcType", model.AcType.ToString());
                        cmd.Parameters.AddWithValue("@AccountNumber", model.AccountNumber);
                        cmd.Parameters.AddWithValue("@Ifsc", model.Ifsc);
                        cmd.Parameters.AddWithValue("@PanNumber", model.PanNumber);
                        cmd.Parameters.AddWithValue("@AddharNumber", model.AddharNumber);
                        cmd.Parameters.AddWithValue("@Qulification", model.Qulification);
                        cmd.Parameters.AddWithValue("@AcBalance", model.AcBalance);

                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        con.Close();

                        if (result > 0)
                        {
                            SendEmail(model.Email, model.Firstname, true);
                            message = "Account Update successful! Confirmation email sent.";
                            return true;
                        }
                        else
                        {
                            message = "Account Update failed. No rows affected.";
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
        public bool delete(AccountDetails model, out string message)
        {
            try
            {
                message = string.Empty;
                using (con)
                {
                    string query = @"DELETE FROM AccountDetails WHERE AccountId=@AccountId";


                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@AccountId", model.AccountId);

                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        con.Close();

                        if (result > 0)
                        {
                            //SendEmail(model.Email, model.Firstname, true);
                            message = "Account Delete successful! Confirmation email sent.";
                            return true;
                        }
                        else
                        {
                            message = "Account Delete failed. No rows affected.";
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
        public bool ShowBalance(AccountDetails model, out string message)
        {
            message = string.Empty;

            try
            {
                using (con)// Replace with your connection string
                {
                    string query = "SELECT * FROM AccountDetails WHERE AccountNumber = @AccountNumber";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@AccountNumber", model.AccountNumber);
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                model.Firstname = reader["Firstname"].ToString();
                                model.Middlename = reader["Middlename"].ToString();
                                model.Lastname = reader["Lastname"].ToString();
                                model.Dob = Convert.ToDateTime(reader["Dob"]);
                                model.Gender = Enum.Parse<gender>(reader["Gender"].ToString());
                                model.MobileNumber = reader["MobileNumber"].ToString();
                                model.Email = reader["Email"].ToString();
                                model.AlternateNumber = reader["AlternateNumber"].ToString();
                                model.Country = reader["Country"].ToString();
                                model.State = reader["State"].ToString();
                                model.City = reader["City"].ToString();
                                model.Pincode = reader["Pincode"].ToString();
                                model.AcType = Enum.Parse<AccountType>(reader["AcType"].ToString());
                                model.AccountNumber = reader["AccountNumber"].ToString();
                                model.Ifsc = reader["Ifsc"].ToString();
                                model.PanNumber = reader["PanNumber"].ToString();
                                model.AddharNumber = reader["AddharNumber"].ToString();
                                model.Qulification = reader["Qulification"].ToString();
                                model.AcBalance = reader["AcBalance"].ToString();

                                return true; // success
                            }
                            else
                            {
                                message = "Account not found.";
                                return false;
                            }
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
            ? "🔁 Account Updated Successfully - Banking System"
            : "✅ Welcome to Banking System - Account Create Successful";

                string body = isUpdate
                    ? $@"
                <html>
                <body style='font-family: Arial, sans-serif;'>
                    <h2 style='color: #2e6c80;'>Hello {firstName},</h2>
                    <p>Your <strong>Account has been successfully updated</strong> in the Banking System.</p>
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
