using System.ComponentModel.DataAnnotations;

namespace Banking_Management_System_Major_Project.Models.UserModel
{
    public class KYCViewModel
    {
        [Required, Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required, Display(Name = "Aadhar Number")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Aadhar number must be 12 digits.")]
        public string AadharNumber { get; set; }

        [Required, Display(Name = "PAN Number")]
        [RegularExpression(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$", ErrorMessage = "Invalid PAN number format.")]
        public string PANNumber { get; set; }

        [Required, Display(Name = "Address Proof (PDF)")]
        public string AddressProof { get; set; }

        [Required, Display(Name = "ID Proof (PDF)")]
        public string IDProof { get; set; }
    }
}
