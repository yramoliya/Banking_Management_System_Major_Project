using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Banking_Management_System_Major_Project.Models.AdminModel
{
    public class CardDetails
    {
        [Key]
        public int CardId { get; set; }

        [Required]
        [ForeignKey("AccountDetails")]
        public int AccountId { get; set; } // Foreign Key referencing AccountDetails

        [Required(ErrorMessage = "Please enter the card number.")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Card number must be exactly 16 digits.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Please enter the cardholder's name.")]
        [StringLength(50, ErrorMessage = "Cardholder name cannot exceed 50 characters.")]
        public string CardHolderName { get; set; }

        [Required(ErrorMessage = "Please enter the CVV.")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "CVV must be exactly 3 digits.")]
        public string CVV { get; set; }

        [Required(ErrorMessage = "Please enter the expiration date.")]
        [DataType(DataType.Date)]
        [Display(Name = "Expiration Date")]
        public DateTime ExpiryDate { get; set; }

        [Required(ErrorMessage = "Please select the card type.")]
        public CardType CardType { get; set; } // Enum for different card types

        // Navigation Property
        public virtual AccountDetails Account { get; set; }
    }

    public enum CardType
    {
        Debit,
        Credit,
    }
}
