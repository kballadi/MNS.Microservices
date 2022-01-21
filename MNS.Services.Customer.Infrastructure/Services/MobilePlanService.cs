using MNS.Services.Customer.Core.Entities;
using MNS.Services.Customer.Infrastructure.Services;
using System.Net.Http;
using System.Threading.Tasks;

namespace MNS.Services.Customer.Services
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
