using AutoMapper;
using MNS.Services.Customer.Core.Entities;
using MNS.Services.Customer.Dtos;

namespace MNS.Services.Customer
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
            CreateMap<User, CustomerReadDto>();
            CreateMap<CustomerCreateDto, User>();
            CreateMap<CustomerUpdateDto, User>();
            CreateMap<User, CustomerUpdateDto>();
        }
    }
}
