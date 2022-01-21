using System.ComponentModel.DataAnnotations;

namespace MNS.Services.Customer.Core
{
    public class Customer
    {
        [Key]
        public int Customer_Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string EmailId { get; set; }

        [Required]
        public int Plan_Id { get; set; }

        [Required]
        public bool IsVerified { get; set; }
    }
}
