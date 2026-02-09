using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Weblu.Application.DTOs.Users.UserDTOs;
using Weblu.Domain.Enums.Users;
using Weblu.Infrastructure.Identity.Entities;
using Weblu.Infrastructure.Identity.Services;
using Weblu.Infrastructure.UnitTests.Helpers;

namespace Weblu.Infrastructure.UnitTests.Identity.Services
{
    public class UserServiceTests
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly AppUser _user;
        public UserServiceTests()
        {
            _httpContextAccessor = A.Fake<IHttpContextAccessor>();
            _userManager = A.Fake<UserManager<AppUser>>();
            _mapper = A.Fake<IMapper>();
            _user = new AppUser()
            {
                FirstName = "Mohammad",
                LastName = "Azimifar",
                PhoneNumber = "989031883414",
                UserName = "TestUsername",
            };
        }
        [Fact]
        public async Task UserService_ChangeUserPasswordAsync_ChangeAndCommitPassword()
        {
            // Arrange 
            ChangePasswordDTO changePasswordDTO = new ChangePasswordDTO()
            {
                OldPassword = "OldTestPassword",
                NewPassword = "NewTestPassword",
            };
            var user = ClaimsPrincipalTestHelper.CreateUser(_user.Id, UserType.User);
            HttpContext httpContent = new DefaultHttpContext()
            {
                User = user,
            };
            A.CallTo(() => _httpContextAccessor.HttpContext).Returns(httpContent);
            A.CallTo(() => _userManager.FindByIdAsync(_user.Id)).Returns(_user);
            A.CallTo(() => _userManager.CheckPasswordAsync(A<AppUser>._, changePasswordDTO.OldPassword)).Returns(true);
            A.CallTo(() => _userManager.ChangePasswordAsync(A<AppUser>._, changePasswordDTO.OldPassword, changePasswordDTO.NewPassword)).Returns(IdentityResult.Success);

            var userService = new UserService(_userManager, _mapper, _httpContextAccessor);

            // Act

            await userService.ChangePasswordAsync(_user.Id, changePasswordDTO);

            // Assert
            A.CallTo(() => _userManager.ChangePasswordAsync(
                    _user,
                    changePasswordDTO.OldPassword,
                    changePasswordDTO.NewPassword))
                .MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async Task UserService_DeleteUserAsync_DeleteAndCommitUser()
        {
            // Arrange
            var user = ClaimsPrincipalTestHelper.CreateUser(_user.Id, UserType.User);
            HttpContext httpContent = new DefaultHttpContext()
            {
                User = user,
            };
            A.CallTo(() => _httpContextAccessor.HttpContext).Returns(httpContent);
            A.CallTo(() => _userManager.FindByIdAsync(_user.Id)).Returns(_user);

            var userService = new UserService(_userManager, _mapper, _httpContextAccessor);

            // Act

            _user.Delete();

            // Assert
            _user.IsDeleted.Should().Be(true);
        }
        [Fact]
        public async Task UserService_UpdateUserAsync_ReturnUserDTO()
        {
            UpdateUserDTO updateUserDTO = new UpdateUserDTO()
            {
                FirstName = "Mohammad Mahdi",
                LastName = "Azimi",
                PhoneNumber = "989931883414",
                UserName = "TestUsername1",
            };
            UserDTO userDTO = new UserDTO()
            {
                FirstName = "Mohammad Mahdi",
                LastName = "Azimi",
                PhoneNumber = "989931883414",
                UserName = "TestUsername1",
                FullName = "Mohammad Mahdi Azimi",
            };
            var user = ClaimsPrincipalTestHelper.CreateUser(_user.Id, UserType.User);
            HttpContext httpContent = new DefaultHttpContext()
            {
                User = user,
            };
            A.CallTo(() => _httpContextAccessor.HttpContext).Returns(httpContent);
            A.CallTo(() => _userManager.FindByIdAsync(_user.Id)).Returns(_user);
            A.CallTo(() => _mapper.Map<UserDTO>(_user)).Returns(userDTO);
            A.CallTo(() => _mapper.Map(updateUserDTO, _user)).Returns(_user);

            A.CallTo(() => _userManager.UpdateAsync(_user)).Returns(IdentityResult.Success);


            var userService = new UserService(_userManager, _mapper, _httpContextAccessor);

            // Act
            var act = await userService.UpdateAsync(_user.Id, updateUserDTO);

            // Assert
            act.Should().BeOfType<UserDTO>();
            A.CallTo(() => _userManager.UpdateAsync(_user))
                .MustHaveHappenedOnceExactly();
        }

    }
}