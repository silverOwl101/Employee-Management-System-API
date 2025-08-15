using Employee_Management_System_API.Data;
using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System_API.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDBContext _context;
        public RefreshTokenRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
            return await _context.SaveChangesAsync() > -1;
        }

        public async Task<bool> DeleteAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Remove(refreshToken);
            return await _context.SaveChangesAsync() > -1;
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.UserId == id);
            return refreshToken is not null;
        }

        public async Task<RefreshToken?> GetRefreshToken(Guid id)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.UserId == id);
            return refreshToken;
        }

        public async Task<bool> UpdateAsync(string newHashedToken, RefreshToken refreshToken)
        {
            refreshToken.Token = newHashedToken;
            refreshToken.Expires = DateTime.UtcNow.AddDays(7);
            refreshToken.IsRevoked = false;

            return await _context.SaveChangesAsync() > -1;
        }

        public async Task<RefreshToken?> GetRefreshAndAppUserToken(string hashId)
        {
            return await _context.RefreshTokens.Include(e => e.AppUser)
                                               .FirstOrDefaultAsync(t => t.Token == hashId);
        }

        public async Task<bool> Revoked(RefreshToken tokenInformation, bool setRevokeValue)
        {
            tokenInformation.IsRevoked = setRevokeValue;
            return await _context.SaveChangesAsync() > -1;
        }
    }
}
