using MNS.Services.MobilePlan.Core.Entities;
using System.Collections.Generic;

namespace MNS.Services.MobilePlan.Infrastructure.Repos
{
    /// <summary>
    /// Mobile Repo Interface
    /// 
    /// </summary>
    public interface IMobilePlanRepository
    {
        /// <summary>
        /// Add Method
        /// </summary>
        /// <param name="mobilePlan"></param>
        public void AddPlan(Plan mobilePlan);

        /// <summary>
        /// Get Mobile Plans
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Plan> GetMobilePlans();

        /// <summary>
        /// Get Mobile plan by Id
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public Plan GetMobilePlan(int planId);

    }
}
