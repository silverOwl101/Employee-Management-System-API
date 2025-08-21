using Employee_Management_System_API;
using Employee_Management_System_API.DTOs.Request;
using EmployeeManagementSystem.Test.Classes;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace EmployeeManagementSystem.Test.IntegrationTests
{
    public class AccountControllerIntegrationTesting : IClassFixture<VirtualWebApplicationDB<Program>>
    {
        private readonly HttpClient _client;

        
        
        public AccountControllerIntegrationTesting(VirtualWebApplicationDB<Program> factory)
        {
            _client = factory.CreateClient();            
        }

        [Fact]
        public async Task Register_ShouldCreateUser_AndReturnOk()
        {
            TestTokenContainer.LoadTokens();
            // Arrange
            var request = new InsertAccountRequest
            {
                UserName = "johndoe",
                Email = "john@example.com",
                Password = "Password123!",
                OrgRole = Employee_Management_System_API.Domain.Enums.Categories.OrganizationRoles.HRManager,
                FirstName = "John",
                MiddleName = "test",
                LastName = "Doe",
                DepartmentPub_ID = "2025-2255",
                RolePub_ID = "2025-2255",
                Address = "123 Street",
                DateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-25)),
                HireDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Status = Employee_Management_System_API.Domain.Enums.Categories.EmployeeStatus.Active
            };
            // Act
            var httprequest = new HttpRequestMessage(HttpMethod.Post, "/api/Account/Register")
            {
                Content = JsonContent.Create(request)
            };
            
            httprequest.Headers.Add("Cookie", $"___at={TestTokenContainer.AccessToken};" +
                                 $" ___rt={TestTokenContainer.RefreshToken}");

            var response = await _client.SendAsync(httprequest);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var accountResponse = await response.Content.ReadFromJsonAsync<Employee_Management_System_API.DTOs.Response.AccountResponse>();
            accountResponse.Should().NotBeNull();
            accountResponse!.UserName.Should().Be("johndoe");
            accountResponse.Email.Should().Be("john@example.com");
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenCredentialsAreValid()
        {
            // First, register a user
            var registerRequest = new InsertAccountRequest
            {
                UserName = "janedoe",
                Email = "jane@example.com",
                Password = "Password123!",
                OrgRole = Employee_Management_System_API.Domain.Enums.Categories.OrganizationRoles.HRManager,
                FirstName = "Jane",
                LastName = "Doe",
                DepartmentPub_ID = "Dept01",
                RolePub_ID = "Role01",
                Address = "123 Street",
                DateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)),
                HireDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Status = Employee_Management_System_API.Domain.Enums.Categories.EmployeeStatus.Active
            };

            await _client.PostAsJsonAsync("/api/Account/Register", registerRequest);

            var loginRequest = new LogInRequest
            {
                UserName = "janedoe",
                PassWord = "Password123!"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Account/Login", loginRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Headers.Contains("Set-Cookie").Should().BeTrue(); // Token cookies set
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenPasswordIsWrong()
        {
            var loginRequest = new LogInRequest
            {
                UserName = "nonexistent",
                PassWord = "WrongPass123"
            };

            var response = await _client.PostAsJsonAsync("/api/Account/Login", loginRequest);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenSA()
        {
            
            var loginRequest = new LogInRequest
            {
                UserName = "sa",
                PassWord = "company_Password101"
            };

            var response = await _client.PostAsJsonAsync("/api/Account/Login", loginRequest);            
            response.EnsureSuccessStatusCode();

            var _accessToken = response.Headers
                                                      .GetValues("Set-Cookie")
                                                      .FirstOrDefault(h => h.StartsWith("___at="))?
                                                      .Split(';')[0]
                                                      .Split('=')[1];

            var _refreshToken = response.Headers
                                                       .GetValues("Set-Cookie")
                                                       .FirstOrDefault(h => h.StartsWith("___rt="))?
                                                       .Split(';')[0]
                                                       .Split('=')[1];

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            _accessToken.Should().NotBeNull();
            _refreshToken.Should().NotBeNull();

            TestTokenContainer.SaveTokens(_accessToken, _refreshToken);
        }        
    }
}
