using System.ComponentModel.DataAnnotations;

namespace RealEstateManagement.Models
{
    public class Agent
    {
        [Key]
        public int Agent_ID { get; set; }

        [Required(ErrorMessage = "اسم الوكيل مطلوب")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100)]
        public string RegionCovered { get; set; }

        // Navigation Property - العلاقة مع العقارات
        public ICollection<Property> Properties { get; set; } = new List<Property>();

        // Navigation Property - العلاقة مع العقود
        public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    }
}