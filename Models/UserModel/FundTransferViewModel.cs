using System.ComponentModel.DataAnnotations;

namespace Banking_Management_System_Major_Project.Models.UserModel
{
    public class FundTransferViewModel
    {
        [Required, Display(Name = "Sender Account Number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Account number must be 10 digits.")]
        public string SenderAccount { get; set; }

        [Required, Display(Name = "Recipient Account Number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Account number must be 10 digits.")]
        public string RecipientAccount { get; set; }

        [Required, Range(1, 1000000, ErrorMessage = "Amount must be at least $1.")]
        public decimal Amount { get; set; }

        [Required, Display(Name = "Transfer Type")]
        public string TransferType { get; set; }
    }
}