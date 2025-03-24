using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking_Management_System_Major_Project.Models.AdminModel
{
    public class AccountHistory
    {
        [Key]
        public int HistoryId { get; set; }

        [Required]
        [ForeignKey("AccountDetails")]
        public int AccountId { get; set; } // Foreign Key linking to AccountDetails

        [Required(ErrorMessage = "Transaction type is required.")]
        public TransactionType TransactionType { get; set; } // Enum for transaction type

        [Required(ErrorMessage = "Transaction date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Amount is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public double Amount { get; set; }

        [StringLength(250, ErrorMessage = "Remarks cannot exceed 250 characters.")]
        public string Remarks { get; set; }

        // Navigation Property
        public virtual AccountDetails Account { get; set; }
    }

    public enum TransactionType
    {
        Deposit,
        Withdrawal,
        Transfer,
        Payment
    }
}
