using Employee_Management_System_API.Domain.Entities;

namespace Employee_Management_System_API.Interfaces.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user, string employeeId);
        Task<string> CreateToken(AppUser user);
    }
}
