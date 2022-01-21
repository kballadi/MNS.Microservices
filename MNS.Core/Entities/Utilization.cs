using System.ComponentModel.DataAnnotations;

namespace MNS.Services.Utilization.Core
{
    public class Utilization
    {
        [Key]
        [Required]
        public int Billing_Id { get; set; }

        [Required]
        public int Customer_Id { get; set; }

        [Required]
        public int Plan_Id { get; set; }

        [Required]
        public int BillingCycle { get; set; }
    }
}
