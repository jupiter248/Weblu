using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.ServiceDtos;
using Weblu.Domain.Entities;

namespace Weblu.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Service, ServiceDto>();
            CreateMap<ServiceDto, AddServiceDto>();
            CreateMap<ServiceDto, UpdateServiceDto>();
        }
    }
}