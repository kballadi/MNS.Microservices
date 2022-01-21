using MNS.Services.MobilePlan.Core.Entities;
using System.Net.Http;
using System.Threading.Tasks;

namespace MNS.Services.Utilization.Infrastructure.Services
{
    public class MobilePlanService : IMobilePlanService
    {
        private readonly HttpClient httpClient;

        public MobilePlanService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Plan> GetMobilePlan(int planId)
        {
            var response = await httpClient.GetAsync($"/plans/{planId}");
            return await response.ReadContentAs<Plan>();
        }
    }
}
