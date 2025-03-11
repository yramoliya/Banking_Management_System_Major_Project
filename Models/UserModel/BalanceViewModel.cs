using System.ComponentModel.DataAnnotations;

namespace Banking_Management_System_Major_Project.Models.UserModel
{
    public class BalanceViewModel
    {
        [Required, Display(Name = "Account Number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Account number must be 10 digits.")]
        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }
    }
}
