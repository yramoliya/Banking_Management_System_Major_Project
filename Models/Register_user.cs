using System.ComponentModel.DataAnnotations;

namespace Banking_Management_System_Major_Project.Models
{
    public class Register_user
    {
        //public string? Title { get; set; }
        //public string? FirstName { get; set; }
        //public string? MiddleName { get; set; }
        //public string? LastName { get; set; }
        //public DateTime DateOfBirth { get; set; }
        //public string? Gender { get; set; }
        //public string? ParentName { get; set; }
        //public string? Nationality { get; set; }
        //public string? Address { get; set; }
        //public int ZipCode { get; set; }
        //public string? Area { get; set; }
        //public string? Country { get; set; }
        //public int PhoneCode { get; set; }
        //public int PhoneNumber { get; set; }
        //public string? Email { get; set; }
        //public bool TermsAccepted { get; set; }
        //public int AadhaarCardNumber { get; set; }
        //public int PanCardNumber { get; set; }

        // Acccount Credentials
        //[Required]
        //public string Username { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Passwords do not match.")]
        //public string ConfirmPassword { get; set; }
        // General Information
        //[Required(ErrorMessage = "Title is required.")]
        //public string? Title { get; set; }

        //[Required(ErrorMessage = "First name is required.")]
        //public string FirstName { get; set; }

        //public string? MiddleName { get; set; }

        //[Required(ErrorMessage = "Last name is required.")]
        //public string LastName { get; set; }

        //[Required(ErrorMessage = "Date of birth is required.")]
        //[DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        //public DateTime DateOfBirth { get; set; }

        //[Required(ErrorMessage = "Gender is required.")]
        //public string? Gender { get; set; }

        //[Required(ErrorMessage = "Parent's name is required.")]
        //public string ParentName { get; set; }

        //[Required(ErrorMessage = "Nationality is required.")]
        //public string Nationality { get; set; }

        //// Contact Details
        //[Required(ErrorMessage = "Address is required.")]
        //public string? Address { get; set; }

        //[Required(ErrorMessage = "Zip code is required.")]
        //public string? ZipCode { get; set; }

        //public string? Area { get; set; }

        //[Required(ErrorMessage = "Country is required.")]
        //public string? Country { get; set; }

        //[Required(ErrorMessage = "Phone code is required.")]
        //public string? PhoneCode { get; set; }

        //[Required(ErrorMessage = "Phone number is required.")]
        //public string? PhoneNumber { get; set; }

        //[Required(ErrorMessage = "Email is required.")]
        //[EmailAddress(ErrorMessage = "Invalid email format.")]
        //public string Email { get; set; }

        //[Required(ErrorMessage = "You must accept the terms and conditions.")]
        //public bool TermsAccepted { get; set; }

        //// Identity Verification (KYC)
        //[Required(ErrorMessage = "Aadhaar card number is required.")]
        //[RegularExpression(@"^\d{12}$", ErrorMessage = "Aadhaar number must be exactly 12 digits.")]
        //public string? AadhaarCardNumber { get; set; }

        //[Required(ErrorMessage = "PAN card number is required.")]
        //[RegularExpression(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$", ErrorMessage = "Invalid PAN card format (e.g., ABCDE1234F).")]
        //public string? PanCardNumber { get; set; }

        //public int Id { get; set; }
        //public string Title { get; set; }
        //public string FirstName { get; set; }
        //public string MiddleName { get; set; }
        //public string LastName { get; set; }
        //public DateTime DateOfBirth { get; set; }
        //public string Gender { get; set; }
        //public string Nationality { get; set; }
        //public string AadhaarCardNumber { get; set; }
        //public string PanCardNumber { get; set; }
        //public string Email { get; set; }
        //public string MobileNumber { get; set; }
        //public string Address { get; set; }
        //public string ZipCode { get; set; }
        //public string BranchName { get; set; }
        //public string AccountType { get; set; }
        //public string AccountNumber { get; set; }
        //public string IFSCCode { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "ID Number is required")]
        public string IdNumber { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }
    }
}
