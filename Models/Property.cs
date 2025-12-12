using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateManagement.Models
{
    public class Property
    {
        [Key]
        public int Property_ID { get; set; }

        [Required(ErrorMessage = "العنوان مطلوب")]
        [StringLength(200)]
        public string Address { get; set; }

        [Required(ErrorMessage = "نوع العقار مطلوب")]
        [StringLength(50)]
        public string PropertyType { get; set; } // مثل: Villa, Apartment, Land

        [Required]
        public decimal Area { get; set; } // المساحة بالمتر المربع

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [StringLength(50)]
        public string Status { get; set; } // مثل: Available, Sold, Rented

        [Required(ErrorMessage = "يجب تحديد الوكيل")]
        public int Agent_ID { get; set; }

        // Foreign Key Relationship
        [ForeignKey("Agent_ID")]
        public Agent? Agent { get; set; }

        // Navigation Property - العلاقة مع العقود
        public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    }
}