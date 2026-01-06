using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Common.Pagination;
using Weblu.Application.Common.Responses;

namespace Weblu.Application.Common.Mappers
{
    public class PagedResponseProfile : Profile
    {
        public PagedResponseProfile()
        {
            CreateMap(typeof(PagedList<>), typeof(PagedResponse<>));
        }
    }
}