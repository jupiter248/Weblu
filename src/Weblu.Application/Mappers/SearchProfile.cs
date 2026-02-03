using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.SearchDtos;
using Weblu.Domain.Entities.Search;

namespace Weblu.Application.Mappers
{
    public class SearchProfile : Profile
    {
        public SearchProfile()
        {
            CreateMap<SearchItem , SearchItemDto>();
        }
    }
}