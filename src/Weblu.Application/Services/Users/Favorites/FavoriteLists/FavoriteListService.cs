using AutoMapper;
using Weblu.Application.DTOs.Users.Favorites.FavoriteListDTOs;
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
        public async Task<FavoriteListDTO> CreateAsync(string userId, CreateFavoriteListDTO createFavoriteListDTO)
        {
            bool userExists = await _userRepository.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            FavoriteList favoriteList = _mapper.Map<FavoriteList>(createFavoriteListDTO);
            favoriteList.UserId = userId;

            _favoriteListRepository.Add(favoriteList);
            await _unitOfWork.CommitAsync();

            FavoriteListDTO favoriteListDTO = _mapper.Map<FavoriteListDTO>(favoriteList);
            return favoriteListDTO;
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
        public async Task<List<FavoriteListDTO>> GetAllAsync(string userId, FavoriteListParameters favoriteListParameters)
        {
            IReadOnlyList<FavoriteList> favoriteLists = await _favoriteListRepository.GetAllByUserIdAsync(userId, favoriteListParameters);
            List<FavoriteListDTO> favoriteListDTOs = _mapper.Map<List<FavoriteListDTO>>(favoriteLists);
            return favoriteListDTOs;
        }

        public async Task<FavoriteListDTO> GetByIdAsync(int favoriteListId)
        {
            FavoriteList favoriteList = await _favoriteListRepository.GetByIdAsync(favoriteListId) ?? throw new NotFoundException(FavoriteListErrorCodes.NotFound);
            FavoriteListDTO favoriteListDTO = _mapper.Map<FavoriteListDTO>(favoriteList);
            return favoriteListDTO;
        }

        public async Task<FavoriteListDTO> UpdateAsync(string userId, int favoriteListId, UpdateFavoriteListDTO updateFavoriteListDTO)
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
            favoriteList = _mapper.Map(updateFavoriteListDTO, favoriteList);

            favoriteList.MarkUpdated();
            _favoriteListRepository.Update(favoriteList);
            await _unitOfWork.CommitAsync();

            FavoriteListDTO favoriteListDTO = _mapper.Map<FavoriteListDTO>(favoriteList);
            return favoriteListDTO;
        }
    }
}