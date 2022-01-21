using MNS.Services.MobilePlan.Core.Entities;
using System.Threading.Tasks;

namespace MNS.Services.Utilization.Infrastructure.Services
{
    public interface IMobilePlanService
    {
        public Task<Plan> GetMobilePlan(int planId);
    }
}
