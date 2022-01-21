using MNS.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MNS.Services.Utilization.Services
{
    public interface IMobilePlanService
    {
        public Task<MobilePlan> GetMobilePlan(int planId);
    }
}
