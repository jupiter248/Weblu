using AutoMapper;
using Weblu.Application.Dtos.Common.SearchDtos;
using Weblu.Domain.Entities.Common.Search;

namespace Weblu.Application.Mappers.Common
{
    public class SearchProfile : Profile
    {
        public SearchProfile()
        {
            CreateMap<SearchItem , SearchItemDto>();
        }
    }
}