using MNS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MNS.Services.Utilization.Infrastructure.Repositories
{
    public interface IUtilizationRepository
    {
        public Task<Customer> GetCustomer(int custmerId);
        public Task<MobilePlan> GetMobilePlan(int mobilePlanId);
        public void AddBilling(Core.Entities.Utilization utilization);
        public Core.Entities.Utilization GetBilling(int customerId);
    }
}
