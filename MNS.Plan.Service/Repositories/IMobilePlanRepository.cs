using System.Collections.Generic;

namespace MNS.Services.MobilePlan.Infrastructure.Repositories
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
        public void AddPlan(MobilePlan mobilePlan);

        /// <summary>
        /// Get Mobile Plans
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MobilePlan> GetMobilePlans();

        /// <summary>
        /// Get Mobile plan by Id
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public MobilePlan GetMobilePlan(int planId);

    }
}
