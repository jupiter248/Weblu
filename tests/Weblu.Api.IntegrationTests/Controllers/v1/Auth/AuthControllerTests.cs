using System.Net.Http.Json;
using FluentAssertions;
using Weblu.Api.IntegrationTests.Common;
using Weblu.Api.IntegrationTests.Helpers;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Auth.AuthDtos;

namespace Weblu.Api.IntegrationTests.Controllers.v1.Auth
{
    public class AuthControllerTests : BaseTestIntegration
    {
        public AuthControllerTests(SqlServerTestContainer container) : base(container)
        {
        }
        [Fact]
        public async Task AuthController_Register_Return200()
        {
            // Arrange
            RegisterDto registerDto = new RegisterDto
            {
                Username = "testusername",
                Password = "TestPassword123!",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                PhoneNumber = "09123456789",
            };
            // Act
            var act = await _client.PostAsJsonAsync("api/auth/register", registerDto, cancellationToken: TestContext.Current.CancellationToken);

            // Assert
            act.EnsureSuccessStatusCode();

            var response = await act.Content.ReadFromJsonAsync<ApiResponse<AuthResponseDto>>(cancellationToken: TestContext.Current.CancellationToken);
            response?.Data.Should().NotBeNull();
            response?.Data.Should().BeOfType<AuthResponseDto>();
            response?.Data.Should().Match<AuthResponseDto>(x => x.Username == registerDto.Username);

        }
        [Fact]
        public async Task AuthController_Login_Return200()
        {
            // Arrange

            LoginDto loginDto = new LoginDto
            {
                Username = "user",
                Password = "@User248",
            };

            // Act
            var act = await _client.PostAsJsonAsync("api/auth/login", loginDto, cancellationToken: TestContext.Current.CancellationToken);
            // Assert
            act.EnsureSuccessStatusCode();

            var response = await act.Content.ReadFromJsonAsync<ApiResponse<AuthResponseDto>>(cancellationToken: TestContext.Current.CancellationToken);
            response?.Data.Should().NotBeNull();
            response?.Data.Should().BeOfType<AuthResponseDto>();
            response?.Data.Should().Match<AuthResponseDto>(x => x.Username == loginDto.Username);

        }
    }
}