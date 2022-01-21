using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MNS.Services.Customer.Dtos
{
    /// <summary>
    /// Created Dto for Customer
    /// </summary>
    public class CustomerCreateDto
    {
        /// <summary>
        /// Customer Name 
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Customer Email Id
        /// </summary>
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string EmailId { get; set; }

        /// <summary>
        /// Mobile Plan Id
        /// </summary>
        [Required]
        public int Plan_Id { get; set; }

        /// <summary>
        /// Whether customer is verified or not
        /// </summary>
        [Required]
        public bool IsVerified { get; set; }
    }
}
