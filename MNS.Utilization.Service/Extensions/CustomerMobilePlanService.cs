using MNS.Core.Entities;
using System.Net.Http;
using System.Threading.Tasks;

namespace MNS.Services.Utilization.Services
{
    /// <summary>
    /// Customer Mobile Services
    /// </summary>
    public class CustomerMobilePlanService : ICustomerMobilePlanService
    {
        private readonly HttpClient httpClient;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="httpClient">Http Client</param>
        public CustomerMobilePlanService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Get the Customer details
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <returns></returns>
        public async Task<Customer> GetCustomer(int customerId)
        {
            var response = await httpClient.GetAsync($"/customer/{customerId}");
            return await response.ReadContentAs<Customer>();
        }

        /// <summary>
        /// Get Mobile Plan details
        /// </summary>
        /// <param name="planId">Mobile Plan Id</param>
        /// <returns></returns>
        public async Task<MobilePlan> GetMobilePlan(int planId)
        {
            var response = await httpClient.GetAsync($"/plans/{planId}");
            return await response.ReadContentAs<MobilePlan>();
        }
    }
}
