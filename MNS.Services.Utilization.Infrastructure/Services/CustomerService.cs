using MNS.Services.MobilePlan.Core.Entities;
using MNS.Services.Utilization.Infrastructure.Services;
using System.Net.Http;
using System.Threading.Tasks;

namespace MNS.Services.Utilization.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient httpClient;

        public CustomerService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Plan> GetCustomerMobilePlan(int customerId)
        {
            var response = await httpClient.GetAsync($"/customers/{customerId}");
            return await response.ReadContentAs<Plan>();
        }
    }
}
