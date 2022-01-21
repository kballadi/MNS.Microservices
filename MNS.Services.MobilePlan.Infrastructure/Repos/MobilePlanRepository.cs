using MNS.Services.MobilePlan.Core.Entities;
using MNS.Services.MobilePlan.Infrastructure.Data;
using MNS.Services.MobilePlan.Infrastructure.Repos;
using System.Collections.Generic;
using System.Linq;

namespace MNS.Services.MobilePlan.Infrastructure.Repos
{
    public class MobilePlanRepository : IMobilePlanRepository
    {
        private readonly MobilePlanDbContext mobilePlanDbContext;

        public MobilePlanRepository(MobilePlanDbContext mobilePlanDbContext)
        {
            this.mobilePlanDbContext = mobilePlanDbContext;
        }

        public void AddPlan(Plan mobilePlan)
        {
            mobilePlanDbContext.MobilePlans.Add(mobilePlan);
            SaveChanges();
        }

        public Plan GetMobilePlan(int planId)
        {
            return mobilePlanDbContext.MobilePlans.FirstOrDefault(x => x.Plan_ID == planId);
        }

        public IEnumerable<Plan> GetMobilePlans()
        {
            return mobilePlanDbContext.MobilePlans.ToList();
        }
        public bool SaveChanges()
        {
            return mobilePlanDbContext.SaveChanges() >= 0;
        }
    }
}
