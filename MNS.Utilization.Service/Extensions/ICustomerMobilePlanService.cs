using System.Threading.Tasks;

namespace MNS.Services.Utilization.Services
{
    public interface ICustomerMobilePlanService
    {
        public Task<Core.Entities.Customer> GetCustomer(int customerId);
        public Task<Core.Entities.MobilePlan> GetMobilePlan(int planId);
    }
}
