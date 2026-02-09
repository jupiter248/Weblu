using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Common.SearchDTOs;
using Weblu.Application.Parameters.Common;

namespace Weblu.Application.Interfaces.Services.Common
{
    public interface ISearchService
    {
        Task<PagedResponse<SearchItemDTO>> SearchAsync(string text, SearchParameters searchParameters);
    }
}