using Microsoft.EntityFrameworkCore;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Persistance.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _context;

    public RefreshTokenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken> GetByIdAsync(Guid id)
    {
        return await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.RefreshTokenId == id);
    }

    public async Task<IEnumerable<RefreshToken>> GetAllAsync()
    {
        return await _context.RefreshTokens.ToListAsync();
    }

    public async Task AddAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Update(refreshToken);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var refreshToken = await GetByIdAsync(id);
        if (refreshToken != null)
        {
            _context.RefreshTokens.Remove(refreshToken);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<RefreshToken> GetByUserIdAsync(Guid userId)
    {
        return await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.UserId == userId);
    }
}
