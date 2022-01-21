using System.ComponentModel.DataAnnotations;

namespace MNS.Utilization.Service.Api.Dtos
{
    public class UtilizationReadDto
    {
        [Required]
        public int Customer_Id { get; set; }

        [Required]
        public int Plan_Id { get; set; }

        [Required]
        public int BillingCycle { get; set; }
    }
}
