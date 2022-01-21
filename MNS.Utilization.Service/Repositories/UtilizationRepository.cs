using MNS.Core.Entities;
using MNS.Services.Utilization.DbContexts;
using MNS.Services.Utilization.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MNS.Services.Utilization.Infrastructure.Repositories
{
    /// <summary>
    /// Customer Mobile Plan Billing class
    /// </summary>
    public class UtilizationRepository : IUtilizationRepository
    {
        private readonly UtilizationDbContext utilizationDbContext;
        private readonly ICustomerMobilePlanService customerMobilePlanService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="utilizationDbContext">DbContext</param>
        /// <param name="customerMobilePlanService">Customer Mobile Plan Service</param>
        public UtilizationRepository(UtilizationDbContext utilizationDbContext, ICustomerMobilePlanService customerMobilePlanService)
        {
            this.utilizationDbContext = utilizationDbContext;
            this.customerMobilePlanService = customerMobilePlanService;
        }

        /// <summary>
        /// Adding Billing to the Customer
        /// </summary>
        /// <param name="utilization"></param>
        public void AddBilling(Core.Entities.Utilization utilization)
        {
            utilizationDbContext.Utilizations.Add(utilization);
            SaveChanges();
        }

        /// <summary>
        /// Getting Billing of the customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Core.Entities.Utilization GetBilling(int customerId)
        {
            return utilizationDbContext.Utilizations.FirstOrDefault(x => x.Customer_Id == customerId);
        }

        /// <summary>
        /// Get Customer details
        /// </summary>
        /// <param name="custmerId"></param>
        /// <returns></returns>
        public async Task<Customer> GetCustomer(int custmerId)
        {
            return await customerMobilePlanService.GetCustomer(custmerId);
        }

        /// <summary>
        /// Get the Mobile Plan
        /// </summary>
        /// <param name="mobilePlanId"></param>
        /// <returns></returns>
        public async Task<MobilePlan> GetMobilePlan(int mobilePlanId)
        {
            return await customerMobilePlanService.GetMobilePlan(mobilePlanId);
        }

        private bool SaveChanges()
        {
            return utilizationDbContext.SaveChanges() >= 0;
        }
    }
}
