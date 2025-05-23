using MemberEntry.Models;

namespace MemberEntry.Interfaces
{
    public interface IMemberRepository
    {
        Task<IEnumerable<MemberBasicInfoModel>> GetAllAsync();
        Task<MemberBasicInfoModel> GetByIdAsync(int id);
        Task AddAsync(MemberBasicInfoModel member);
        Task UpdateAsync(MemberBasicInfoModel member);
        Task DeleteAsync(int id);
    }
}
