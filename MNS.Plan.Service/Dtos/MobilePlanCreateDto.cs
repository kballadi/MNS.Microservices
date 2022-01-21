using MNS.Services.MobilePlan.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace MNS.Services.MobilePlan.Dtos
{
    /// <summary>
    /// Mobile Plan Dto
    /// </summary>
    public class MobilePlanCreateDto
    {
        [Key]
        [Required]
        public int PlanId { get; set; }

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
}
