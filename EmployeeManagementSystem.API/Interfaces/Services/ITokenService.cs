using Employee_Management_System_API.Domain.Entities;

namespace Employee_Management_System_API.Interfaces.Services
{
    public interface ITokenService
    {
        Task<(string token, string refreshToken)> CreateToken(AppUser user, string employeeId);
        Task<(string token, string refreshToken)> CreateToken(AppUser user);
        Task<string> CreateTokenOnly(AppUser user, string employeeId);
        Task<string> Refresh_Token(RefreshToken refresh);
    }
}
