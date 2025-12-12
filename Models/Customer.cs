using System.ComponentModel.DataAnnotations;

namespace RealEstateManagement.Models
{
    public class Customer
    {
        [Key]
        public int Customer_ID { get; set; }

        [Required(ErrorMessage = "الاسم مطلوب")]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        // Navigation Property - العلاقة مع العقود
        public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    }
}