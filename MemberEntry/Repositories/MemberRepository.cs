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
                    .Include(m => m.PassportType)
                    .OrderByDescending(m => m.MemberId)
                    .ToListAsync();
            }

            public async Task<MemberBasicInfoModel> GetByIdAsync(int id)
            {
                return await _context.Members
                    .Where(m => m.MemberId == id)
                    .FirstOrDefaultAsync();
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
            member.LastModifiedDate = DateTime.Now;
            if (existingMember != null)
            {
                var originalCreateDate = existingMember.CreatedDate;
                var originalImagePath = existingMember.ImagePath;

                _context.Entry(existingMember).CurrentValues.SetValues(member);

                existingMember.CreatedDate = originalCreateDate;
                if (string.IsNullOrEmpty(member.ImagePath))
                {
                    existingMember.ImagePath = originalImagePath;
                }

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


