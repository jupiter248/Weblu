using Weblu.Application.Common.Pagination;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.SearchDtos;
using Weblu.Application.Parameters;
using Weblu.Domain.Enums.Common.Search;

namespace Weblu.Application.Interfaces.Services
{
    public interface ISearchService
    {
        Task<PagedResponse<SearchItemDto>> SearchAsync(string text, SearchParameters searchParameters);
    }
}