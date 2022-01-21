using System.ComponentModel.DataAnnotations;

namespace MNS.Utilization.Service.Api.Dtos
{
    public class UtilizationUpdateDto
    {
        [Required]
        public int Plan_Id { get; set; }

        [Required]
        public int BillingCycle { get; set; }
    }
}
