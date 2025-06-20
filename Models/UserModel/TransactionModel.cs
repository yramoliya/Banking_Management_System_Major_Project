using Banking_Management_System_Major_Project.Models.AdminModel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
namespace Banking_Management_System_Major_Project.Models.UserModel
{
    public class TransactionModel
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BMS;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
        public string Date { get; set; }
        public string Time { get; set; }
        public string Name { get; set; }
        public string Acnumber { get; set; }
        public string Trasaction { get; set; }
        public string Amount { get; set; }
        public string Email { get; set; }

        // READ
        public List<TransactionModel> GetAll()
        {
            List<TransactionModel> list = new List<TransactionModel>();
            SqlDataAdapter sdap = new SqlDataAdapter("SELECT * FROM Ac_History ORDER BY Date DESC, Time DESC", con);
            DataSet ds = new DataSet();
            sdap.Fill(ds);

            foreach (DataRow reader in ds.Tables[0].Rows)
            {
                list.Add(new TransactionModel
                {
                    Date = reader["Date"].ToString(),
                    Time = reader["Time"].ToString(),
                    Name = reader["Name"].ToString(),
                    Acnumber = reader["Acnumber"].ToString(),
                    Trasaction = reader["Trasaction"].ToString(),
                    Amount = reader["Amount"].ToString()
                });
            }
            return list;
        }
        public string AddTransaction(TransactionModel model)
        {
            using (con)
            {
                string query = @"INSERT INTO Ac_History ([Date], [Time], [Name], [Acnumber], [Trasaction], [Amount])
                             VALUES (@Date, @Time, @Name, @Acnumber, @Trasaction, @Amount)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Date", model.Date);
                cmd.Parameters.AddWithValue("@Time", model.Time);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Acnumber", model.Acnumber);
                cmd.Parameters.AddWithValue("@Trasaction", model.Trasaction);
                cmd.Parameters.AddWithValue("@Amount", model.Amount);
                con.Open();
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    SendEmail(model.Email, "Transaction Alert", $"Dear {model.Name}, your {model.Trasaction} of Rs.{model.Amount} was successful.");
                    return "Success";
                }
                return "Failed";
            }
        }

        // UPDATE
        public string UpdateTransaction(TransactionModel model)
        {
            using (con)
            {
                string query = @"UPDATE Ac_History SET 
                             [Date] = @Date, 
                             [Time] = @Time, 
                             [Name] = @Name, 
                             [Acnumber] = @Acnumber, 
                             [Trasaction] = @Trasaction, 
                             [Amount] = @Amount
                             WHERE [Acnumber] = @Acnumber";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Date", model.Date);
                cmd.Parameters.AddWithValue("@Time", model.Time);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Acnumber", model.Acnumber);
                cmd.Parameters.AddWithValue("@Trasaction", model.Trasaction);
                cmd.Parameters.AddWithValue("@Amount", model.Amount);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0 ? "Updated" : "Failed";
            }
        }

        // DELETE
        public string DeleteTransaction(string acNumber)
        {
            using (con)
            {
                string query = "DELETE FROM Ac_History WHERE [Acnumber] = @Acnumber";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Acnumber", acNumber);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0 ? "Deleted" : "Failed";
            }
        }

        // EMAIL
        private void SendEmail(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient("smtp.yourserver.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("parthtank2231@gmail.com", "vqys xoon mbam mbkj"),
                EnableSsl = true
            };

            var message = new MailMessage
            {
                From = new MailAddress("parthtank2231@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(toEmail);
            smtpClient.Send(message);
        }
    }
}
