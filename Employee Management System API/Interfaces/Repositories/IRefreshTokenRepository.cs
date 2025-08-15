using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Queries.Project;

namespace Employee_Management_System_API.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetRefreshToken(Guid id);
        Task<RefreshToken?> GetRefreshAndAppUserToken(string hashId);
        Task<bool> Revoked(RefreshToken tokenInformation, bool setRevokeValue);
        Task<bool> CreateAsync(RefreshToken refreshToken);
        Task<bool> UpdateAsync(string newHashedToken, RefreshToken refreshToken);
        Task<bool> DeleteAsync(RefreshToken refreshToken);
        Task<bool> IsExistsAsync(Guid id);        
    }
}
