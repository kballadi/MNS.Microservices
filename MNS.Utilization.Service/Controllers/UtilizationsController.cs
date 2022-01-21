using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MNS.Services.Utilization.Core.Entities;
using MNS.Services.Utilization.Infrastructure.Repos;
using MNS.Utilization.Service.Api.Dtos;

namespace MNS.Utilization.Service.Api.Controllers
{
    /// <summary>
    /// Consumption Service
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UtilizationsController : ControllerBase
    {
        private readonly ILogger<UtilizationsController> logger;
        private readonly IMapper mapper;
        private readonly IUtilizationRepository utilizationRepo;

        /// <summary>
        /// Consumption Ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="utilizationRepo"></param>
        public UtilizationsController(ILogger<UtilizationsController> logger, IMapper mapper, IUtilizationRepository utilizationRepo)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.utilizationRepo = utilizationRepo;
        }

        /// <summary>
        /// Get the registered custome by Id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>        
        [HttpGet("{customerId}", Name = "GetCustomerBillingById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Consumption> GetCustomerBillingById(int customerId)
        {
            logger.LogInformation($"Getting Customer by Id {customerId}");
            var customer = utilizationRepo.GetBilling(customerId);
            if (customer is null)
            {
                return NotFound($"No customer found with Id {customerId}");
            }
            return Ok(customer);
        }

        ///// <summary>
        ///// Get the mobile plan of the customer
        ///// </summary>
        ///// <param name="mobilePlanId"></param>
        ///// <returns></returns>        
        //[HttpGet("{mobilePlanId}", Name = "GetMobilePlanById")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult<MobilePlan> GetMobilePlanById(int mobilePlanId)
        //{
        //    logger.LogInformation($"Getting Customer by Id {mobilePlanId}");
        //    var mobilePlan = utilizationRepo.GetMobilePlan(mobilePlanId);
        //    if (mobilePlan is null)
        //    {
        //        return NotFound($"No customer found with Id {mobilePlanId}");
        //    }
        //    return Ok(mobilePlan);
        //}

        /// <summary>
        /// Add billing to the customers
        /// </summary>
        /// <param name="utilizationCreateDto"></param>
        /// <returns></returns>
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public ActionResult<Consumption> AddBilling(UtilizationCreateDto utilizationCreateDto)
        //{
        //    logger.LogInformation($"Adding Billing to the registered customer");
        //    var mobilePlan = mapper.Map<Consumption>(utilizationCreateDto);
        //    if (!TryValidateModel(mobilePlan))
        //    {
        //        logger.LogError($"Adding Billing in the system failed {ModelState}.");
        //        return ValidationProblem(ModelState);
        //    }
        //    //var plan = utilizationRepo.GetMobilePlan(utilizationCreateDto.Plan_Id).Result;
        //    //var customer = utilizationRepo.GetCustomer(utilizationCreateDto.Customer_Id).Result;
        //    //if (plan is null || customer is null)
        //    //    throw new DomainException($"Moile Plan with planId '{plan?.Plan_ID}' not found.");
        //    //else if (Enum.TryParse(typeof(CustomerType), plan.CustomerType.ToString(), out object cutomerType))
        //    //{
        //    utilizationRepo.AddBilling(mobilePlan);
        //    var mobilePlanReadDto = mapper.Map<UtilizationReadDto>(mobilePlan);
        //    return CreatedAtRoute(nameof(GetCustomerConsumptionById), new { Customer_Id = mobilePlan.Customer_Id }, mobilePlanReadDto);
        //    //}
        //    //return BadRequest();
        //}
    }
}
