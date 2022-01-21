using MNS.Services.Utilization.Core.Entities;
using System.Threading.Tasks;

namespace MNS.Services.Utilization.Infrastructure.Repos
{
    public interface IUtilizationRepository
    {
        //public Task<Customer> GetCustomer(int custmerId);
        //public Task<MobilePlan> GetMobilePlan(int mobilePlanId);
        public Task AddBilling(Consumption utilization);
        public Consumption GetBilling(int customerId);
    }
}
