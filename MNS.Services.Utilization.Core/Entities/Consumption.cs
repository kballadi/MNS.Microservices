using System;
using System.ComponentModel.DataAnnotations;

namespace MNS.Services.Utilization.Core.Entities
{
    public class Consumption
    {
        [Key]
        [Required]
        public Guid Billing_Id { get; set; }

        [Required]
        public int Customer_Id { get; set; }

        [Required]
        public int Plan_Id { get; set; }

        [Required]
        public int BillingCycle { get; set; }
    }
}
