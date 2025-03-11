using System.ComponentModel.DataAnnotations;

namespace Banking_Management_System_Major_Project.Models.UserModel
{
    public class LoanApplicationViewModel
    {
        [Required, Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required, Display(Name = "Account Number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Account number must be 10 digits.")]
        public string AccountNumber { get; set; }

        [Required, Display(Name = "Loan Type")]
        public string LoanType { get; set; }

        [Required, Range(1000, 1000000, ErrorMessage = "Loan amount must be between $1,000 and $1,000,000")]
        public decimal LoanAmount { get; set; }

        [Required, Display(Name = "Monthly Income")]
        public decimal MonthlyIncome { get; set; }

        [Required, Display(Name = "Employment Type")]
        public string EmploymentType { get; set; }

        [Required, Range(1, 30, ErrorMessage = "Loan term must be between 1 to 30 years.")]
        public int LoanTerm { get; set; }
    }
}
