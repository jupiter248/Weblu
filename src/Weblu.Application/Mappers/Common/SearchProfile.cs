using AutoMapper;
using Weblu.Application.DTOs.Common.SearchDTOs;
using Weblu.Domain.Entities.Common.Search;

namespace Weblu.Application.Mappers.Common
{
    public class SearchProfile : Profile
    {
        public SearchProfile()
        {
            CreateMap<SearchItem, SearchItemDTO>();
        }
    }
}