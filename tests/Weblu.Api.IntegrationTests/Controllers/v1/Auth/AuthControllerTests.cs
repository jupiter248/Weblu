using System.Net.Http.Json;
using FluentAssertions;
using Weblu.Api.IntegrationTests.Common;
using Weblu.Api.IntegrationTests.Helpers;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Auth.AuthDTOs;

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
            RegisterDTO registerDTO = new RegisterDTO
            {
                Username = "testusername",
                Password = "TestPassword123!",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                PhoneNumber = "09123456789",
            };
            // Act
            var act = await _client.PostAsJsonAsync("api/auth/register", registerDTO, cancellationToken: TestContext.Current.CancellationToken);

            // Assert
            act.EnsureSuccessStatusCode();

            var response = await act.Content.ReadFromJsonAsync<ApiResponse<AuthResponseDTO>>(cancellationToken: TestContext.Current.CancellationToken);
            response?.Data.Should().NotBeNull();
            response?.Data.Should().BeOfType<AuthResponseDTO>();
            response?.Data.Should().Match<AuthResponseDTO>(x => x.Username == registerDTO.Username);

        }
        [Fact]
        public async Task AuthController_Login_Return200()
        {
            // Arrange

            LoginDTO loginDTO = new LoginDTO
            {
                Username = "user",
                Password = "@User248",
            };

            // Act
            var act = await _client.PostAsJsonAsync("api/auth/login", loginDTO, cancellationToken: TestContext.Current.CancellationToken);
            // Assert
            act.EnsureSuccessStatusCode();

            var response = await act.Content.ReadFromJsonAsync<ApiResponse<AuthResponseDTO>>(cancellationToken: TestContext.Current.CancellationToken);
            response?.Data.Should().NotBeNull();
            response?.Data.Should().BeOfType<AuthResponseDTO>();
            response?.Data.Should().Match<AuthResponseDTO>(x => x.Username == loginDTO.Username);

        }
    }
}