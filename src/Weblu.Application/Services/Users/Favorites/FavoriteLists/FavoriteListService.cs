using AutoMapper;
using Weblu.Application.Dtos.Users.Favorites.FavoriteListDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Users;
using Weblu.Application.Interfaces.Repositories.Users.Favorites;
using Weblu.Application.Interfaces.Services.Users.UserFavorites.FavoriteLists;
using Weblu.Application.Parameters.Users;
using Weblu.Domain.Entities.Users.Favorites;
using Weblu.Domain.Errors.Users;
using Weblu.Domain.Errors.Users.Favorites;

namespace Weblu.Application.Services.Users.Favorites.FavoriteLists
{
    public class FavoriteListService : IFavoriteListService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFavoriteListRepository _favoriteListRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public FavoriteListService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUserRepository userRepository,
            IFavoriteListRepository favoriteListRepository
            )
        {
            _favoriteListRepository = favoriteListRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<FavoriteListDto> CreateAsync(string userId, CreateFavoriteListDto createFavoriteListDto)
        {
            bool userExists = await _userRepository.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            FavoriteList favoriteList = _mapper.Map<FavoriteList>(createFavoriteListDto);
            favoriteList.UserId = userId;

            _favoriteListRepository.Add(favoriteList);
            await _unitOfWork.CommitAsync();

            FavoriteListDto favoriteListDto = _mapper.Map<FavoriteListDto>(favoriteList);
            return favoriteListDto;
        }
        public async Task DeleteAsync(string userId, int favoriteListId)
        {
            bool userExists = await _userRepository.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            FavoriteList favoriteList = await _favoriteListRepository.GetByIdAsync(favoriteListId) ?? throw new NotFoundException(FavoriteListErrorCodes.NotFound);
            if (favoriteList.UserId != userId)
            {
                throw new UnauthorizedException(FavoriteListErrorCodes.DeleteForbidden);
            }
            _favoriteListRepository.Remove(favoriteList);
            await _unitOfWork.CommitAsync();
        }
        public async Task<List<FavoriteListDto>> GetAllAsync(string userId, FavoriteListParameters favoriteListParameters)
        {
            IReadOnlyList<FavoriteList> favoriteLists = await _favoriteListRepository.GetAllByUserIdAsync(userId, favoriteListParameters);
            List<FavoriteListDto> favoriteListDtos = _mapper.Map<List<FavoriteListDto>>(favoriteLists);
            return favoriteListDtos;
        }

        public async Task<FavoriteListDto> GetByIdAsync(int favoriteListId)
        {
            FavoriteList favoriteList = await _favoriteListRepository.GetByIdAsync(favoriteListId) ?? throw new NotFoundException(FavoriteListErrorCodes.NotFound);
            FavoriteListDto favoriteListDto = _mapper.Map<FavoriteListDto>(favoriteList);
            return favoriteListDto;
        }

        public async Task<FavoriteListDto> UpdateAsync(string userId, int favoriteListId, UpdateFavoriteListDto updateFavoriteListDto)
        {
            bool userExists = await _userRepository.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            FavoriteList favoriteList = await _favoriteListRepository.GetByIdAsync(favoriteListId) ?? throw new NotFoundException(FavoriteListErrorCodes.NotFound);
            if (favoriteList.UserId != userId)
            {
                throw new UnauthorizedException(FavoriteListErrorCodes.UpdateForbidden);
            }
            favoriteList = _mapper.Map(updateFavoriteListDto, favoriteList);
            
            favoriteList.MarkUpdated();
            _favoriteListRepository.Update(favoriteList);
            await _unitOfWork.CommitAsync();

            FavoriteListDto favoriteListDto = _mapper.Map<FavoriteListDto>(favoriteList);
            return favoriteListDto;
        }
    }
}