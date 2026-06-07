using AutoMapper;
using Weblu.Domain.Common.Models;
using Weblu.Application.Common.Models;
using Weblu.Application.DTOs.Common.SearchDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Domain.Interfaces.Repositories;
using Weblu.Domain.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Services.Common;
using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Common.Search;
using Weblu.Domain.Errors.Common;

namespace Weblu.Application.Services.Common
{
    public class SearchService : ISearchService
    {
        public readonly ISearchRepository _searchRepository;
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public SearchService(ISearchRepository searchRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _searchRepository = searchRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PagedResponse<SearchItemDTO>> SearchAsync(string text, SearchParameters searchParameters)
        {
            if (string.IsNullOrEmpty(text)) throw new BadRequestException(SearchErrorCodes.TextIsEmpty);

            PagedList<SearchItem> searchItems = await _searchRepository.SearchAsync(text, searchParameters);
            List<SearchItemDTO> searchItemDTOs = _mapper.Map<List<SearchItemDTO>>(searchItems);
            var pagedResponse = _mapper.Map<PagedResponse<SearchItemDTO>>(searchItems);
            pagedResponse.Items = searchItemDTOs;
            return pagedResponse;
        }
    }
}