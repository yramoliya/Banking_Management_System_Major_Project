using System.ComponentModel.DataAnnotations;
namespace Banking_Management_System_Major_Project.Models.UserModel
{
    public class ApplyCardViewModel
    {   
        public int Id { get; set; }
        [Required, Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required, Display(Name = "Account Number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Account number must be 10 digits.")]
        public string AccountNumber { get; set; }

        [Required, Display(Name = "Card Type")]
        public string CardType { get; set; }

        [Required, Display(Name = "Card Variant")]
        public string CardVariant { get; set; }

        [Range(5000, 500000, ErrorMessage = "Credit limit should be between $5,000 and $500,000")]
        public int? CreditLimit { get; set; }
    }
}
