using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MNS.Services.MobilePlan.Core.DomainExceptions;
using MNS.Services.MobilePlan.Core.Entities;
using MNS.Services.MobilePlan.Dtos;
using MNS.Services.MobilePlan.Infrastructure.Repos;
using System;
using System.Collections.Generic;

namespace MNS.MobilePlan.Service.Controllers
{
    /// <summary>
    /// Mobile Plans Service
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MobilePlansController : ControllerBase
    {
        private readonly ILogger<MobilePlansController> logger;
        private readonly IMapper mapper;
        private readonly IMobilePlanRepository mobilePlanRepository;

        /// <summary>
        /// Mobile Plan Ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="mobilePlanRepository"></param>
        public MobilePlansController(ILogger<MobilePlansController> logger, IMapper mapper, IMobilePlanRepository mobilePlanRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.mobilePlanRepository = mobilePlanRepository;
        }

        /// <summary>
        /// Get the Mobile plans from the system
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<MobilePlanReadDto>> GetMobilePlans()
        {
            logger.LogInformation("Getting Mobile Plans");
            var mobilePlans = mobilePlanRepository.GetMobilePlans();
            if (mobilePlans is null)
            {
                logger.LogError("No Plans found!");
                return NotFound();
            }
            return Ok(mapper.Map<IEnumerable<MobilePlanReadDto>>(mobilePlans));
        }

        /// <summary>
        /// Get the mobile plan by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetMobilePlanById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<MobilePlanReadDto> GetMobilePlanById(int id)
        {
            logger.LogInformation("Getting Mobile Plan with Id");
            var mobilePlan = mobilePlanRepository.GetMobilePlan(id);
            if (mobilePlan is null)
            {
                logger.LogError($"No mobile plan was found with  Id : {id}");
                return NotFound();
            }
            return Ok(mapper.Map<MobilePlanReadDto>(mobilePlan));
        }

        /// <summary>
        /// Add mobile plan to the system
        /// </summary>
        /// <param name="mobilePlanCreateDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<MobilePlanCreateDto> AddMobilePlans(MobilePlanCreateDto mobilePlanCreateDto)
        {
            logger.LogInformation($"Adding mobile plan in the system.");
            var mobilePlanModel = mapper.Map<Services.MobilePlan.Core.Entities.Plan>(mobilePlanCreateDto);
            if (!TryValidateModel(mobilePlanModel))
            {
                logger.LogError($"Adding mobile plan in the system failed {ModelState}.");
                return ValidationProblem(ModelState);
            }
            var planId = mobilePlanRepository.GetMobilePlan(mobilePlanCreateDto.PlanId)?.Plan_ID;
            if (planId != null)
                throw new DomainException($"Moile Plan with planId '{planId}' already added.");
            else if (Enum.TryParse(typeof(CustomerType), mobilePlanCreateDto.CustomerType.ToString(), out object cutomerType))
            {
                mobilePlanRepository.AddPlan(mobilePlanModel);
                var mobilePlanReadDto = mapper.Map<MobilePlanReadDto>(mobilePlanModel);
                return CreatedAtRoute(nameof(GetMobilePlanById), new { PlanId = mobilePlanModel.Plan_ID }, mobilePlanReadDto);
            }
            return BadRequest();
        }
    }
}
