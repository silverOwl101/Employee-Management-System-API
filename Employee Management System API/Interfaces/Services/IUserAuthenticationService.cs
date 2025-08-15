using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Domain.Enums;
using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Employee_Management_System_API.Interfaces.Services
{
    public interface IUserAuthenticationService
    {
        public Task<IdentityResult> CreateAccount(AppUser user, string password, InitEmployee createEmployee);
        public Task<InitEmployee> InitEmployee(InsertEmployeeRequest createEmployee);
        public Task<IdentityResult> AddAccountToRole(AppUser user, Categories.OrganizationRoles role);        
        public Task<IdentityRole<Guid>?> FindAccountRole(string role);
        public Task<IEnumerable<Claim>> GetAccountRoleClaims(IdentityRole<Guid> user);
        public Task<IEnumerable<string>> GetAccountRole(AppUser user);
        public Task<IEnumerable<Claim>> GetAccountClaims(AppUser user);
        public Task<SignInResult> AccountSignIn(AppUser user, string password, bool accountLockOnFailure);
        public Task<AppUser?> GetAccountbyUserName(string username);
        public Task<AppUser?> FindById(string userId);
    }
}
