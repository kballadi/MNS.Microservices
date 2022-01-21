using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MNS.Services.Customer.Dtos
{
    public class CustomerUpdateDto
    {
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
