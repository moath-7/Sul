using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateManagement.Models
{
    public class Payment
    {
        [Key]
        public int Payment_ID { get; set; }

        [Required(ErrorMessage = "يجب تحديد العقد")]
        public int Contract_ID { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountPaid { get; set; }

        [Required(ErrorMessage = "طريقة الدفع مطلوبة")]
        [StringLength(50)]
        public string PaymentMethod { get; set; } // مثل: Cash, Bank Transfer, Check

        // Foreign Key
        [ForeignKey("Contract_ID")]
        public Contract? Contract { get; set; }
    }
}
