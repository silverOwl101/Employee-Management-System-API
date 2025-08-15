using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Repositories;
using Microsoft.Identity.Client;

namespace Employee_Management_System_API.Services
{    
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _repo;
        public RefreshTokenService(IRefreshTokenRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> CreateRefreshToken(RefreshToken refreshToken)
        {
            return await _repo.CreateAsync(refreshToken);
        }

        public async Task<bool> DeleteRefreshToken(RefreshToken refreshToken)
        {
            return await _repo.DeleteAsync(refreshToken);
        }

        public async Task<RefreshToken?> GetRefreshToken(Guid id)
        {
            return await _repo.GetRefreshToken(id);
        }

        public async Task<RefreshToken?> GetRefreshToken(string id)
        {
            return await _repo.GetRefreshAndAppUserToken(id);
        }

        public async Task<bool> IsRefreshTokenExists(Guid id)
        {
            return await _repo.IsExistsAsync(id);
        }

        public async Task<bool> Revoked(RefreshToken refreshToken, bool value)
        {
            return await _repo.Revoked(refreshToken, value);
        }

        public async Task<bool> UpdateRefreshToken(string newHashedToken, RefreshToken refreshToken)
        {
            return await _repo.UpdateAsync(newHashedToken, refreshToken);
        }
    }
}
