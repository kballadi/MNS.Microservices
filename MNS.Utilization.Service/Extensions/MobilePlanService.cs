using MNS.Core.Entities;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MNS.Services.Utilization.Services
{
    public class MobilePlanService : IMobilePlanService
    {
        private readonly HttpClient httpClient;

        public MobilePlanService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<MobilePlan> GetMobilePlan(int planId)
        {
            var response = await httpClient.GetAsync($"/plans/{planId}");
            return await response.ReadContentAs<MobilePlan>();
        }
    }
}
