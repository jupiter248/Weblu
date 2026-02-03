using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Common.Pagination;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.SearchDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Search;
using Weblu.Domain.Enums.Common.Search;
using Weblu.Domain.Errors.Search;

namespace Weblu.Application.Services
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
        public async Task<PagedResponse<SearchItemDto>> SearchAsync(string text, SearchParameters searchParameters)
        {
            if (string.IsNullOrEmpty(text)) throw new BadRequestException(SearchErrorCodes.TextIsEmpty);

            PagedList<SearchItem> searchItems = await _searchRepository.SearchAsync(text, searchParameters);
            List<SearchItemDto> searchItemDtos = _mapper.Map<List<SearchItemDto>>(searchItems);
            var pagedResponse = _mapper.Map<PagedResponse<SearchItemDto>>(searchItems);
            pagedResponse.Items = searchItemDtos;
            return pagedResponse;
        }
    }
}