using System.ComponentModel.DataAnnotations;

namespace Banking_Management_System_Major_Project.Models.UserModel
{
    public class WithdrawMoneyViewModel
    {
        [Required, Display(Name = "Account Number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Account number must be 10 digits.")]
        public string AccountNumber { get; set; }

        [Required, Range(10, 50000, ErrorMessage = "Minimum withdrawal amount is $10.")]
        public decimal Amount { get; set; }
    }

}
