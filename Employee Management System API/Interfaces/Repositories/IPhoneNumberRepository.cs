using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Queries.PhoneNumber;

namespace Employee_Management_System_API.Interfaces.Repositories
{
    public interface IPhoneNumberRepository
    {
        Task<IEnumerable<PhoneNumber>> GetAllAsync(QueryGetAllPhoneNumbers query);
        Task<PhoneNumber?> GetByIdAsync(string id);
        Task<PhoneNumber> CreateAsync(PhoneNumber phoneNumber);
        Task<PhoneNumber?> UpdateAsync(Guid id, PhoneNumber phoneNumber);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> IsIdExistsAsync(string id);
        Task<bool> IsPhoneNumberExistsAsync(string id);
    }
}
