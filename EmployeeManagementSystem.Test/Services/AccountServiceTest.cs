using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace EmployeeManagementSystem.Test.Services
{
    public class AccountServiceTest
    {
        private readonly Mock<UserManager<AppUser>> _userManagerMock;
        private readonly Mock<SignInManager<AppUser>> _signInManagerMock;
        private readonly Mock<RoleManager<IdentityRole<Guid>>> _roleManagerMock;
        private readonly Mock<IEmployeeService> _employeeServiceMock;
        private readonly AccountService _accountService;

        public AccountServiceTest()
        {
            // Setup mocks for UserManager
            var userStoreMock = new Mock<IUserStore<AppUser>>();
            _userManagerMock = new Mock<UserManager<AppUser>>(
                userStoreMock.Object,
                null!, null!, null!, null!, null!, null!, null!, null!
            );

            // Setup mocks for SignInManager
            var contextAccessor = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<AppUser>>();
            _signInManagerMock = new Mock<SignInManager<AppUser>>(
                _userManagerMock.Object,
                contextAccessor.Object,
                claimsFactory.Object,
                null!, null!, null!, null!
            );

            // Setup mocks for RoleManager
            var roleStoreMock = new Mock<IRoleStore<IdentityRole<Guid>>>();
            _roleManagerMock = new Mock<RoleManager<IdentityRole<Guid>>>(
                roleStoreMock.Object,
                null!, null!, null!, null!
            );

            _employeeServiceMock = new Mock<IEmployeeService>();

            // Inject mocks into AccountService
            _accountService = new AccountService(
                _userManagerMock.Object,
                _signInManagerMock.Object,
                _roleManagerMock.Object,
                _employeeServiceMock.Object
            );
        }

        [Fact]
        public async Task CreateAccount_ShouldThrow_WhenEmailAlreadyExists()
        {
            // Arrange
            var user = new AppUser { Email = "test@email.com", UserName = "testuser" };
            var employee = new InitEmployee { Email = "test@email.com" };

            _userManagerMock
                .Setup(u => u.FindByEmailAsync(user.Email))
                .ReturnsAsync(new AppUser()); // Simulate existing user

            // Act
            Func<Task> act = async () => await _accountService.CreateAccount(user, "P@ssw0rd!", employee);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                     .WithMessage("Email already exists!");
        }

        [Fact]
        public async Task CreateAccount_ShouldThrow_WhenUsernameIsEmpty()
        {
            // Arrange
            var user = new AppUser { UserName = "", Email = "valid@email.com" };
            var employee = new InitEmployee { Email = "valid@email.com" };

            // Act
            Func<Task> act = async () => await _accountService.CreateAccount(user, "P@ssw0rd!", employee);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                     .WithMessage("Enter a username");
        }

        [Fact]
        public async Task CreateAccount_ShouldThrow_WhenPasswordIsEmpty()
        {
            // Arrange
            var user = new AppUser { UserName = "testuser", Email = "valid@email.com" };
            var employee = new InitEmployee { Email = "valid@email.com" };

            // Act
            Func<Task> act = async () => await _accountService.CreateAccount(user, "", employee);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                     .WithMessage("Enter a password");
        }

        [Fact]
        public async Task CreateAccount_ShouldCreateUser_AndEmployee_WhenValid()
        {
            // Arrange
            var user = new AppUser { Id = Guid.NewGuid(), UserName = "testuser", Email = "valid@email.com" };
            var employee = new InitEmployee { Email = "valid@email.com" };

            _userManagerMock.Setup(u => u.FindByEmailAsync(user.Email)).ReturnsAsync((AppUser?)null);
            _userManagerMock.Setup(u => u.FindByNameAsync(user.UserName)).ReturnsAsync((AppUser?)null);
            _userManagerMock.Setup(u => u.CheckPasswordAsync(user, "P@ssw0rd!")).ReturnsAsync(false);

            _userManagerMock.Setup(u => u.CreateAsync(user, "P@ssw0rd!")).ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(u => u.FindByIdAsync(user.Id.ToString())).ReturnsAsync(user);

            // Act
            var result = await _accountService.CreateAccount(user, "P@ssw0rd!", employee);

            // Assert
            result.Succeeded.Should().BeTrue();
            _employeeServiceMock.Verify(e => e.CreateEmployeeAsync(It.IsAny<UpsertEmployeeRequest>()), Times.Once);
        }

    }
}
