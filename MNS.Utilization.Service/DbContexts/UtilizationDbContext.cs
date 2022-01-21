using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MNS.Services.Utilization.Data
{
    public class UtilizationDbContext : DbContext
    {
        public UtilizationDbContext(DbContextOptions<UtilizationDbContext> options) : base(options)
        {

        }
        public DbSet<Utilization> Utilizations { get; set; }
    }
}
