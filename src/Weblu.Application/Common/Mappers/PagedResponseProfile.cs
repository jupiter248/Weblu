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