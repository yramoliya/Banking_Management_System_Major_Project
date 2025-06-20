using System.ComponentModel.DataAnnotations;
namespace Banking_Management_System_Major_Project.Models.UserModel
{
    public class ApplyCardViewModel
    {
        public string DebitNumber { get; set; }
        public string FirstNumber { get; set; }
        public string SecondNumber { get; set; }
        public string ThirdNumber { get; set; }
        public string FourthNumber { get; set; }
        public string UniqueNumber { get; set; }
        public string ValidUpto { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string AcNumber { get; set; }
        public string CvvNumber { get; set; }
        public string Status { get; set; }
        public string CardType { get; set; }
        public string Random_Pin { get; set; }
        public string Pin { get; set; }
    }
}
