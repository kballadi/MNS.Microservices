using MNS.Services.MobilePlan.Core.Entities;
using System.Threading.Tasks;

namespace MNS.Services.Utilization.Infrastructure.Services
{
    public interface ICustomerMobilePlanService
    {
        //public Task<Customer> GetCustomer(int customerId);
        public Task<Plan> GetMobilePlan(int planId);
    }
}
