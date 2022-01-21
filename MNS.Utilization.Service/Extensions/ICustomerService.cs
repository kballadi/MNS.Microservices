using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MNS.Services.Utilization.Services
{
    public interface ICustomerService
    {
        public Task<Core.Entities.Customer> GetCustomer(int customerId);
    }
}
