using MNS.Core.Entities;
using MNS.Services.Customer.Services;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MNS.Services.Customer.Infrastructure.Services
{
    /// <summary>
    /// Mobile Plan Service Interface
    /// </summary>
    public class MobilePlanService : IMobilePlanService
    {
        private readonly HttpClient httpClient;

        /// <summary>
        /// Mobile Plan Service Ctor
        /// </summary>
        /// <param name="httpClient"></param>
        public MobilePlanService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Get the mobile plan by Id.
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public async Task<MobilePlan> GetMobilePlan(int planId)
        {
            var response = await httpClient.GetAsync($"/plans/{planId}");
            return await response.ReadContentAs<MobilePlan>();
        }
    }
}
