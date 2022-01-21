using MNS.Core.Entities;
using MNS.Services.Plan.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MNS.Services.Plan.Repositories
{
    public class MobilePlanRepository : IMobilePlanRepository
    {
        private readonly MobilePlanDbContext mobilePlanDbContext;

        public MobilePlanRepository(MobilePlanDbContext mobilePlanDbContext)
        {
            this.mobilePlanDbContext = mobilePlanDbContext;
        }

        public void AddPlan(MobilePlan mobilePlan)
        {
            mobilePlanDbContext.MobilePlans.Add(mobilePlan);
            SaveChanges();
        }

        public MobilePlan GetMobilePlan(int planId)
        {
            return mobilePlanDbContext.MobilePlans.FirstOrDefault(x => x.Plan_ID == planId);
        }

        public IEnumerable<MobilePlan> GetMobilePlans()
        {
            return mobilePlanDbContext.MobilePlans.ToList();
        }
        public bool SaveChanges()
        {
            return mobilePlanDbContext.SaveChanges() >= 0;
        }
    }
}
