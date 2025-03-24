using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Key]
        public int User_Id { get; set; }

        [Required(ErrorMessage = "Please enter first name.")]
        public string Fname { get; set; }

        [Required(ErrorMessage = "Please enter middle name.")]
        public string Mname { get; set; }

        [Required(ErrorMessage = "Please enter last name.")]
        public string Lname { get; set; }

        [Required(ErrorMessage = "Please enter a valid email.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Please enter a valid mobile number.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be 10 digits.")]
        public string Mobile { get; set; } // Changed to string to prevent leading zero issues

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public Role Role { get; set; } = Role.User; // Default value is User

        // Foreign Key for AccountDetails
        [ForeignKey("AccountDetails")]
        public int AccountId { get; set; }

        // Navigation Property
        public virtual AccountDetails AccountDetails { get; set; }
    }
}
