using MNS.Services.MobilePlan.Core.Entities;
using System.Threading.Tasks;

namespace MNS.Services.Utilization.Infrastructure.Services
{
    public interface ICustomerService
    {
        public Task<Plan> GetCustomerMobilePlan(int customerId);
    }
}
