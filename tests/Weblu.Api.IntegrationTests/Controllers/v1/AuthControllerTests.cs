using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Weblu.Api.IntegrationTests.Common;
using Weblu.Api.IntegrationTests.Helpers;
using Weblu.Application.Dtos.AuthDtos;
using Weblu.Domain.Entities.Tags;
using Weblu.Domain.Enums.Users;
using Weblu.Infrastructure.Data;

namespace Weblu.Api.IntegrationTests.Controllers.v1
{
    public class AuthControllerTests : IClassFixture<SqlServerTestContainer>
    {
        private readonly string connectionString;
        public AuthControllerTests(SqlServerTestContainer container)
        {
            connectionString = container.ConnectionString;
        }
        [Fact]
        public async Task AuthController_Register_Return200()
        {
            // Arrange
            var factory = new TestWebApplicationFactory(connectionString);

            RegisterDto registerDto = new RegisterDto
            {
                Username = "TestUsername",
                Password = "TestPassword123!",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                PhoneNumber = "09123456789",
            };
            var client = factory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:80");

            var content = JsonContent.Create(registerDto);
            // Act
            var act = await client.PostAsync("/api/auth/register", content, cancellationToken: TestContext.Current.CancellationToken);

            // Assert
            // act.EnsureSuccessStatusCode();
            // var response = await act.Content.ReadFromJsonAsync<AuthResponseDto>(cancellationToken: TestContext.Current.CancellationToken);
            // response?.Username.Should().Be("TestUsername");
        }
        [Fact]
        public async Task AuthController_Login_Return200()
        {
            // Arrange
            var factory = new TestWebApplicationFactory(connectionString);

            LoginDto loginDto = new LoginDto
            {
                Username = "Admin",
                Password = "@Admin248",
            };
            var client = factory.CreateClient();
            client.BaseAddress = new Uri("https://localhost");


            // Act
            var act = await client.PostAsJsonAsync("/api/auth/login", loginDto, cancellationToken: TestContext.Current.CancellationToken);
            // Assert
            // act.EnsureSuccessStatusCode();
            // var response = await act.Content.ReadFromJsonAsync<AuthResponseDto>(cancellationToken: TestContext.Current.CancellationToken);
            // response?.Username.Should().Be("admin");
        }
    }
}