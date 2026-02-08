using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Common.SearchDtos;
using Weblu.Application.Parameters.Common;

namespace Weblu.Application.Interfaces.Services.Common
{
    public interface ISearchService
    {
        Task<PagedResponse<SearchItemDto>> SearchAsync(string text, SearchParameters searchParameters);
    }
}