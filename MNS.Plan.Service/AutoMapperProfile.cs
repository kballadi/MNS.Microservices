using AutoMapper;
using MNS.Services.MobilePlan.Core.Entities;
using MNS.Services.MobilePlan.Dtos;

namespace MNS.Services.MobilePlan
{
    /// <summary>
    /// Automapper Profile
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Ctor Auto mapper
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<Plan, MobilePlanCreateDto>();
            CreateMap<MobilePlanCreateDto, Plan>();
            CreateMap<MobilePlanUpdateDto, Plan>();
            CreateMap<Plan, MobilePlanUpdateDto>();
        }
    }
}
