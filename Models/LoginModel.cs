using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Banking_Management_System_Major_Project.Models
{
    public class LoginModel
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BMS;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool ValidateUser(string email, string password, out string FirstName, out int userId, out string role)
        {
            userId = 0;
            role = "";
            FirstName = "";
            string query = "SELECT User_Id,Fname, Role from UserRegistrations WHERE Email = @Email AND Password = @Password";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                userId = (int)reader["User_Id"];
                role = reader["Role"].ToString(); // Get role from DB
                FirstName = reader["Fname"].ToString();
                return true;
            }
            return false;
        }
    }
    public class ForgotPasswordModel
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BMS;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a valid OTP.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "OTP number must be 6 digits.")]
        public string OTP { get; set; } // Changed to string to prevent leading zero issues
        public static bool IsEmailRegistered(string email, string connectionString)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "SELECT COUNT(*) FROM UserRegistrations WHERE Email = @Email";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", email);
            int result = (int)cmd.ExecuteScalar();
            return result > 0;
        }

        public static bool UpdatePassword(string email, string newPassword, string connectionString)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "UPDATE UserRegistrations SET Password = @Password WHERE Email = @Email";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", newPassword); // In real app, use hashed password
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }
    }
}
