using AutoMapper;
using Weblu.Domain.Common.Models;
using Weblu.Application.Common.Models;

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