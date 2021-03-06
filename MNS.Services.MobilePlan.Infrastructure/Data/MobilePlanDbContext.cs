using Microsoft.EntityFrameworkCore;
using MNS.Services.MobilePlan.Core.Entities;

namespace MNS.Services.MobilePlan.Infrastructure.Data
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
        public DbSet<Plan> MobilePlans { get; set; }
    }
}
