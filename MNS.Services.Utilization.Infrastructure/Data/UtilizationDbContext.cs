using Microsoft.EntityFrameworkCore;
using MNS.Services.Utilization.Core.Entities;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace MNS.Services.Utilization.Infrastructre.Data
{
    public class UtilizationDbContext : DbContext
    {
        public UtilizationDbContext(DbContextOptions<UtilizationDbContext> options) : base(options)
        {

        }
        public DbSet<Consumption> Utilizations { get; set; }
    }
}
