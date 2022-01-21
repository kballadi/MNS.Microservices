using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MNS.Services.Customer.Infrastructure.Repositories
{
    /// <summary>
    /// Customer Repo Interfaces
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Get the list of Customers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomers();

        /// <summary>
        /// Get the customer with registered email id
        /// </summary>
        /// <param name="emailId">customer EmailId</param>
        /// <returns></returns>
        public Customer GetCustomer(string emailId);

        /// <summary>
        /// Register the customer with email id.
        /// </summary>
        /// <param name="customer">Customer Details</param>
        /// <returns></returns>
        public bool RegisterCustomer(Customer customer);

        /// <summary>
        /// Adds a plan in the Customer Repo
        /// </summary>
        /// <param name="planId">Mobile Plan Id</param>
        /// <returns></returns>
        public Task<MobilePlan> BuyPlan(int planId);
    }
}
