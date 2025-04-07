﻿using Microsoft.Data.SqlClient;
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
}
