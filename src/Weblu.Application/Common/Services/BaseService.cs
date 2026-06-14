using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Domain.Errors.Users;
using Weblu.Domain.Interfaces.Repositories.Users;

namespace Weblu.Application.Common.Services;

public abstract class BaseService
{
    private readonly IUserRepository _userRepository;

    protected BaseService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected async Task EnsureUserExistsAsync(string userId)
    {
        var exists = await _userRepository.UserExistsAsync(userId);
        if (!exists) throw new NotFoundException(UserErrorCodes.UserNotFound);
    }
}