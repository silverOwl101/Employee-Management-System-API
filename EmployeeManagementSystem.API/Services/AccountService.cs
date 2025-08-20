using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Domain.Enums;
using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.Helpers;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Employee_Management_System_API.Services
{
    public class AccountService : IUserAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IEmployeeService _employeeService;        
        public AccountService(UserManager<AppUser> userManager,
                              SignInManager<AppUser> signinManager,
                              RoleManager<IdentityRole<Guid>> roleManager,
                              IEmployeeService employeeService
                              )
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _roleManager = roleManager;
            _employeeService = employeeService;            
        }

        public async Task<SignInResult> AccountSignIn(AppUser user, string password, bool accountLockOnFailure)
        {
            return await _signinManager.CheckPasswordSignInAsync(user, password, accountLockOnFailure);
        }

        public async Task<IdentityResult> AddAccountToRole(AppUser user, Categories.OrganizationRoles role)
        {
            return await _userManager.AddToRoleAsync(user, role.ToString());
        }

        public async Task<IdentityResult> CreateAccount(AppUser user, string password, InitEmployee createEmployee)
        {
            AppUser? getAppUserId;

            var newEmployeeRequest = new UpsertEmployeeRequest
            {
                EmployeePub_ID = createEmployee.EmployeePub_ID,
                FirstName = createEmployee.FirstName,
                MiddleName = createEmployee.MiddleName,
                LastName = createEmployee.LastName,
                Email = createEmployee.Email,
                DateOfBirth = createEmployee.DateOfBirth,
                HireDate = createEmployee.HireDate,
                Address = createEmployee.Address,
                Status = createEmployee.Status,
                DepartmentPub_ID = createEmployee.DepartmentPub_ID,
                RolePub_ID = createEmployee.RolePub_ID
            };

            if (user.Email is not null)
            {
                var checkUser = await _userManager.FindByEmailAsync(user.Email);
                if (checkUser is not null)
                    throw new InvalidOperationException("Email already exists!");
            }

            if (string.IsNullOrEmpty(user.UserName))
                throw new InvalidOperationException("Enter a username");
            if (string.IsNullOrEmpty(password))
                throw new InvalidOperationException("Enter a password");

            var isUserNameExist = await _userManager.FindByNameAsync(user.UserName);
            if (isUserNameExist is not null)
                throw new UnauthorizedAccessException("Invalid username and password!");

            var isPasswordExist = await _userManager.CheckPasswordAsync(user, password);
            if (isPasswordExist)
                throw new UnauthorizedAccessException("Invalid username and password!");

            var creatAccount = await _userManager.CreateAsync(user, password);
            if (creatAccount.Succeeded)
            {
                getAppUserId = await _userManager.FindByIdAsync(user.Id.ToString());
                if (getAppUserId is not null)
                {
                    newEmployeeRequest.AppUserId = getAppUserId.Id;
                    await _employeeService.CreateEmployeeAsync(newEmployeeRequest);
                }
            }

            return creatAccount;
        }
        public async Task<InitEmployee> InitEmployee(InsertEmployeeRequest createEmployee)
        {
            var employeeId = string.Empty;
            do
            {
                employeeId = GeneratorHelpers.GenerateID();
            } while (await _employeeService.isEmployeeExistsAsync(employeeId));

            var newEmployeeRequest = new InitEmployee
            {
                EmployeePub_ID = employeeId,
                FirstName = createEmployee.FirstName,
                MiddleName = createEmployee.MiddleName,
                LastName = createEmployee.LastName,
                Email = createEmployee.Email,
                DateOfBirth = createEmployee.DateOfBirth,
                HireDate = createEmployee.HireDate,
                Address = createEmployee.Address,
                Status = createEmployee.Status,
                DepartmentPub_ID = createEmployee.DepartmentPub_ID,
                RolePub_ID = createEmployee.RolePub_ID
            };

            return newEmployeeRequest;
        }

        public async Task<AppUser?> GetAccountbyUserName(string username)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username.ToLower());
        }

        public async Task<IEnumerable<string>> GetAccountRole(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IEnumerable<Claim>> GetAccountClaims(AppUser user)
        {
            return await _userManager.GetClaimsAsync(user);
        }

        public async Task<IdentityRole<Guid>?> FindAccountRole(string role)
        {
            return await _roleManager.FindByNameAsync(role);
        }

        public async Task<IEnumerable<Claim>> GetAccountRoleClaims(IdentityRole<Guid> user)
        {
            return await _roleManager.GetClaimsAsync(user);
        }
        public async Task<AppUser?> FindById(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
    }
}
