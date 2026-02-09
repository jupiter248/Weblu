using AutoMapper;
using Weblu.Application.DTOs.Common.ContributorDTOs;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Common.Contributors;

namespace Weblu.Application.Mappers.Common
{
    public class ContributorProfile : Profile
    {
        public ContributorProfile()
        {
            CreateMap<Contributor, ContributorDTO>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
            CreateMap<CreateContributorDTO, Contributor>();
            CreateMap<UpdateContributorDTO, Contributor>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
        }
    }
}