using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking_Management_System_Major_Project.Models.AdminModel
{
    public class AccountDetails
    {
        [Key]
        public int AccountId { get; set; }

        [Required]
        [ForeignKey("UserRegistration")]
        public int User_Id { get; set; }

        [Required(ErrorMessage = "Please enter the account type.")]
        public string AccountType { get; set; }

        [Required(ErrorMessage = "Balance is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Balance must be a positive value.")]
        public double Balance { get; set; }

        [Required(ErrorMessage = "Please enter Aadhar Card Number.")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Aadhar Number must be exactly 12 digits.")]
        public string AadharCardNumber { get; set; }

        [Required(ErrorMessage = "Please enter PAN Card Number.")]
        [RegularExpression(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$", ErrorMessage = "Invalid PAN Card format.")]
        public string PanCardNumber { get; set; }

        [Required]
        public string AccountNumber { get; private set; } // Auto-generated Account Number

        // Navigation Properties
        public virtual UserRegistration User { get; set; }
        public virtual ICollection<CardDetails> Cards { get; set; } = new List<CardDetails>();

        // One Account can have multiple AccountHistory records
        public virtual ICollection<AccountHistory> AccountHistories { get; set; } = new List<AccountHistory>();

        // Constructor to Generate Account Number
        public AccountDetails()
        {
            GenerateAccountNumber();
        }

        private void GenerateAccountNumber()
        {
            Random random = new Random();
            AccountNumber = "BA" + random.Next(100000000, 999999999); // Generates a 9-digit unique number with "BA" prefix
        }
    }
}
