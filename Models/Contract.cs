using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateManagement.Models
{
    public class Contract
    {
        [Key]
        public int Contract_ID { get; set; }

        [Required(ErrorMessage = "يجب تحديد العقار")]
        public int Property_ID { get; set; }

        [Required(ErrorMessage = "يجب تحديد العميل")]
        public int Customer_ID { get; set; }

        [Required(ErrorMessage = "يجب تحديد الوكيل")]
        public int Agent_ID { get; set; }

        [Required(ErrorMessage = "نوع العقد مطلوب")]
        [StringLength(50)]
        public string ContractType { get; set; } // مثل: Sale, Rent

        [Required]
        public DateTime ContractDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        // Foreign Keys
        [ForeignKey("Property_ID")]
        public Property? Property { get; set; }

        [ForeignKey("Customer_ID")]
        public Customer? Customer { get; set; }

        [ForeignKey("Agent_ID")]
        public Agent? Agent { get; set; }

        // Navigation Property - العلاقة مع الدفعات
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
