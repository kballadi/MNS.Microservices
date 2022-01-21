using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MNS.Services.Customer.Data
{
    /// <summary>
    /// Customer Db Context
    /// </summary>
    public class CustomerDbContext : DbContext
    {
        /// <summary>
        /// Ctor of Db Context
        /// </summary>
        /// <param name="options">Db context options</param>
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// Customers Db set as Table
        /// </summary>
        public DbSet<Customer> Customers { get; set; }
    }
}
