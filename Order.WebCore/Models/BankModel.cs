using System.ComponentModel.DataAnnotations;

namespace PaymentOrder.WebCore.Models
{
    public class BankModel : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "VÖEN 10 simvoldan ibarət olmalıdır")]
        public string VOEN { get; set; }

        [Required]
        public string CorrespondentAccount { get; set; }
        [Required]
        public string SWIFT { get; set; }
    }
}
