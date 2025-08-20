using Employee_Management_System_API.Domain.Entities;

namespace Employee_Management_System_API.Interfaces.Services
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken?> GetRefreshToken(Guid id);
        Task<RefreshToken?> GetRefreshToken(string id);
        Task<bool> Revoked(RefreshToken refreshToken, bool value);
        Task<bool> CreateRefreshToken(RefreshToken refreshToken);
        Task<bool> UpdateRefreshToken(string newHashedToken, RefreshToken refreshToken);
        Task<bool> DeleteRefreshToken(RefreshToken refreshToken);
        Task<bool> IsRefreshTokenExists(Guid id);
    }
}
