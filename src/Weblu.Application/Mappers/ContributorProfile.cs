using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.ContributorDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Contributors;

namespace Weblu.Application.Mappers
{
    public class ContributorProfile : Profile
    {
        public ContributorProfile()
        {
            CreateMap<Contributor, ContributorDto>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
            CreateMap<AddContributorDto, Contributor>();
            CreateMap<UpdateContributorDto, Contributor>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
        }
    }
}