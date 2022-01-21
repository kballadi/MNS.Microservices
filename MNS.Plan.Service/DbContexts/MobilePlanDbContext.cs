using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MNS.Services.MobilePlan.Data
{
    /// <summary>
    /// DbContext for Mobile Plan
    /// </summary>
    public class MobilePlanDbContext : DbContext
    {
        /// <summary>
        /// Ctor of DBContext
        /// </summary>
        /// <param name="options"></param>
        public MobilePlanDbContext(DbContextOptions<MobilePlanDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// Table of MobilePlans
        /// </summary>
        public DbSet<MobilePlan> MobilePlans { get; set; }
    }
}
