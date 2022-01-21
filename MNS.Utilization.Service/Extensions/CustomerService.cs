using MNS.Core.Entities;
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
        public async Task<Customer> GetCustomer(int customerId)
        {
            var response = await httpClient.GetAsync($"/customer/{customerId}");
            return await response.ReadContentAs<Core.Entities.Customer>();
        }
    }
}
