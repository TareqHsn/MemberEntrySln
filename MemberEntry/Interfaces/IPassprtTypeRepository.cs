using MemberEntry.Models;

namespace MemberEntry.Interfaces
{
    public interface IPassprtTypeRepository
    {
        Task<IEnumerable<PassportType>> GetAllAsync();
        Task AddAsync(PassportType passportType);


    }
}
