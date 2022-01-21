using AutoMapper;
using MNS.Services.Utilization.Core.Entities;
using MNS.Utilization.Service.Api.Dtos;

namespace MNS.Utilization.Service.Api
{
    /// <summary>
    /// Auto mapper profile
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Ctor mapper
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<Consumption, UtilizationReadDto>();
            CreateMap<UtilizationCreateDto, Consumption>();
            CreateMap<UtilizationUpdateDto, Consumption>();
            CreateMap<Consumption, UtilizationUpdateDto>();
        }
    }
}
