using MNS.Services.Customer.Core.Entities;
using System.Threading.Tasks;

namespace MNS.Services.Customer.Infrastructure.Services
{
    public interface IMobilePlanService
    {
        public Task<MobilePlan> GetMobilePlan(int planId);
    }
}
