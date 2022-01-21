using System.ComponentModel.DataAnnotations;

namespace MNS.Services.Customer.Core.Entities
{
    public class MobilePlan
    {
        [Key]
        public int Plan_ID { get; set; }

        [Required]
        [MaxLength(100)]
        public int CustomerAge { get; set; }

        [Required]
        public CustomerType CustomerType { get; set; }

        [Required]
        [MinLength(299)]
        public int Amount { get; set; }

        [Required]
        [MaxLength(365)]
        public int ValidityPeriod { get; set; }
    }

    public enum CustomerType
    {
        Student,
        SeniorCitizen,
        ExServiceMen
    }
}
