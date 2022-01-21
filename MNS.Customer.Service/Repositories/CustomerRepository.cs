using MNS.Core.Entities;
using MNS.Services.Customer.DbContexts;
using MNS.Services.Customer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MNS.Services.Customer.Repositories
{
    /// <summary>
    /// Customer Repo Class
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext customerDbcontext;
        private readonly IMobilePlanService mobilePlanService;

        /// <summary>
        /// Ctor Customer Repo
        /// </summary>
        /// <param name="customerDbcontext">DbContext</param>
        /// <param name="mobilePlanService">MobilePlan Service</param>
        public CustomerRepository(CustomerDbContext customerDbcontext, IMobilePlanService mobilePlanService)
        {
            this.customerDbcontext = customerDbcontext;
            this.mobilePlanService = mobilePlanService;
        }

        /// <summary>
        /// Gets the mobile plan from mobile plan service
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public async Task<Core.Entities.MobilePlan> BuyPlan(int planId)
        {
            return await mobilePlanService.GetMobilePlan(planId);
        }

        /// <summary>
        /// Get the registered customer from the System.
        /// </summary>
        /// <param name="emailId">Registered Email Id</param>
        /// <returns></returns>
        public Core.Entities.Customer GetCustomer(string emailId)
        {
            return customerDbcontext.Customers.FirstOrDefault(x => x.EmailId == emailId);
        }

        /// <summary>
        /// Gets the Customers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Core.Entities.Customer> GetCustomers()
        {
            return customerDbcontext.Customers.ToList();
        }

        /// <summary>
        /// Registers the customer by adding them in the Store,
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool RegisterCustomer(Core.Entities.Customer customer)
        {
            customerDbcontext.Customers.Add(customer);
            return SaveChanges();
        }

        private bool SaveChanges()
        {
            return customerDbcontext.SaveChanges() >= 0;
        }
    }
}
