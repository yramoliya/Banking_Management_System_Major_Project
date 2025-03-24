using Banking_Management_System_Major_Project.Models.AdminModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking_Management_System_Major_Project.Models
{
    public enum EmployeeRole
    {
        Manager,
        Cashier,
        LoanOfficer,
        CustomerService,
        Accountant
    }

    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Middle name cannot exceed 50 characters.")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1900-01-01", "2006-12-31", ErrorMessage = "Employee must be at least 18 years old.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Hire date is required.")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(10000, 500000, ErrorMessage = "Salary must be between 10,000 and 500,000.")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public EmployeeRole Role { get; set; }

        
        // Computed Property for Full Name
        public string FullName => $"{FirstName} {MiddleName} {LastName}".Trim();
    }
}
