using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MNS.Integration.MessageBus;
using MNS.Services.Customer.Api.Messaging;
using MNS.Services.Customer.Core.DomainExceptions;
using MNS.Services.Customer.Core.Entities;
using MNS.Services.Customer.Dtos;
using MNS.Services.Customer.Infrastructure.Repos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MNS.Customer.Service.Controllers
{
    /// <summary>
    /// Customer API Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> logger;
        private readonly IMapper mapper;
        private readonly ICustomerRepository customerRepo;
        private readonly IMessageBus messageBus;

        /// <summary>
        /// Constructor of Customer API Controller
        /// </summary>
        /// <param name="logger">Logger to Log</param>
        /// <param name="mapper">Automapper</param>
        /// <param name="customerRepository">Customer Repository</param>
        public CustomersController(ILogger<CustomersController> logger, IMapper mapper, ICustomerRepository customerRepository, IMessageBus messageBus)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.customerRepo = customerRepository;
            this.messageBus = messageBus;
        }

        /// <summary>
        /// Get the list of customers 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<User>> GetCustomers()
        {
            logger.LogInformation("Getting Customers from the system");
            var customers = customerRepo.GetCustomers();
            if (customers is null)
                return NotFound();
            return Ok(mapper.Map<IEnumerable<CustomerReadDto>>(customers));
        }

        /// <summary>
        /// Get the customer details 
        /// </summary>
        /// <param name="emailId">Registered Email Id</param>
        /// <returns></returns>
        [HttpGet("{emailId}", Name = "GetCustomerByEmailId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> GetCustomerByEmailId(string emailId)
        {
            logger.LogInformation($"Getting Customer from the system with Email Id : {emailId}");
            var customer = customerRepo.GetCustomer(emailId);
            if (customer is null)
                return NotFound();
            return Ok(mapper.Map<CustomerReadDto>(customer));
        }

        /// <summary>
        /// Registering Customer 
        /// </summary>
        /// <param name="customerCreateDto">DTO object of Customer</param>
        /// <returns>Customer</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerCreateDto>> AddCustomer(CustomerCreateDto customerCreateDto)
        {
            logger.LogInformation("Registering Customer in the system");
            var customerModel = mapper.Map<User>(customerCreateDto);
            if (!TryValidateModel(customerModel))
            {
                logger.LogError($"Adding mobile plan in the system failed {ModelState}.");
                return ValidationProblem(ModelState);
            }

            var emailId = customerRepo.GetCustomer(customerCreateDto.EmailId)?.EmailId;
            if (emailId != null)
                throw new DomainException($"Customer with Email Id '{emailId}' already exists.");
            else
            {
                customerRepo.RegisterCustomer(customerModel);
                var customerReadDto = mapper.Map<CustomerReadDto>(customerModel);
                try
                {
                    var customerRegisteredMessage = new CustomerRegisteredMessage()
                    {
                        Customer_Id = customerReadDto.Customer_Id,
                        Plan_Id = customerReadDto.Plan_Id,
                        EmailId = customerReadDto.EmailId,
                        IsVerified = customerReadDto.IsVerified,
                        Name = customerReadDto.Name
                    };
                    await messageBus.PublishMessage(customerRegisteredMessage, "customerregisteredmessage");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                return CreatedAtRoute(nameof(GetCustomerByEmailId), new { EmailId = customerModel.EmailId }, customerReadDto);
            }
        }

        /// <summary>
        /// Buying a Plan
        /// </summary>
        /// <param name="mobilePlanId">Mobile Plan</param>
        /// <returns>Mobile Plan</returns>
        [HttpPost("/mobileplan")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MobilePlan>> AddMobilePlan(int mobilePlanId)
        {
            logger.LogInformation("Adding mobile plan in the system");
            var mobilePlan = customerRepo.BuyPlan(mobilePlanId);
            if (mobilePlan != null)
            {
                try
                {
                    var mobilePlanBoughtMessage = new MobilePlanBoughtMessage()
                    {
                        Plan_ID = mobilePlan.Result.Plan_ID,
                        ValidityPeriod = mobilePlan.Result.ValidityPeriod,
                        //CustomerType = mobilePlan.Result.CustomerType,
                        CustomerAge = mobilePlan.Result.CustomerAge,
                        Amount = mobilePlan.Result.Amount
                    };
                    await messageBus.PublishMessage(mobilePlanBoughtMessage, "mobileplanboughtmessage");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                return Ok(mobilePlan);
            }
            return BadRequest();
        }
    }
}
