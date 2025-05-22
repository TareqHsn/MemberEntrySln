using MemberEntry.Data;
using MemberEntry.Interfaces;
using MemberEntry.Models;
using Microsoft.EntityFrameworkCore;

namespace MemberEntry.Repositories
{

        public class MemberRepository : IMemberRepository
        {
            private readonly AppDbContext _context;

            public MemberRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<MemberBasicInfoModel>> GetAllAsync()
            {
                return await _context.Members
                    .OrderBy(m => m.NameInEnglish)
                    .ToListAsync();
            }

            public async Task<MemberBasicInfoModel> GetByIdAsync(int id)
            {
                return await _context.Members
                    .Where(m => m.MemberId == id)
                    .FirstOrDefaultAsync();
            }

            public async Task<IEnumerable<MemberBasicInfoModel>> SearchAsync(string searchTerm)
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return await GetAllAsync();

                searchTerm = searchTerm.ToLower();
                return await _context.Members
                    .Where(m => m.NameInEnglish.ToLower().Contains(searchTerm) ||
                               m.NameInBangla.ToLower().Contains(searchTerm) ||
                               m.IdentityNo.ToLower().Contains(searchTerm))
                    .OrderBy(m => m.NameInEnglish)
                    .ToListAsync();
            }

            public async Task AddAsync(MemberBasicInfoModel member)
            {
                await _context.Members.AddAsync(member);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(MemberBasicInfoModel member)
            {
                var existingMember = await _context.Members
                    .Where(m => m.MemberId == member.MemberId)
                    .FirstOrDefaultAsync();

                if (existingMember != null)
                {
                    _context.Entry(existingMember).CurrentValues.SetValues(member);
                    await _context.SaveChangesAsync();
                }
            }

            public async Task DeleteAsync(int id)
            {
                var member = await _context.Members
                    .Where(m => m.MemberId == id)
                    .FirstOrDefaultAsync();
                if (member != null)
                {
                    _context.Members.Remove(member);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }


