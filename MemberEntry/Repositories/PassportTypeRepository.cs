using MemberEntry.Data;
using MemberEntry.Interfaces;
using MemberEntry.Models;
using Microsoft.EntityFrameworkCore;

namespace MemberEntry.Repositories
{
    public class PassportTypeRepository:IPassprtTypeRepository
    {

        private readonly AppDbContext _context;

        public PassportTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PassportType passportType)
        {
            await _context.PassportTypes.AddAsync(passportType);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PassportType>> GetAllAsync()
        {
            return await _context.PassportTypes
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }
    }
}
