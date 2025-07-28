using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Queries.PhoneNumber;

namespace Employee_Management_System_API.Interfaces.Services
{
    public interface IPhoneNumberService
    {
        Task<IEnumerable<PhoneNumberResponse>> GetPhoneNumbersAsync(QueryGetAllPhoneNumbers query);
        Task<PhoneNumberResponse?> GetPhoneNumberByIdAsync(string id);
        Task<PhoneNumberResponse> AddPhoneNumberAsync(UpsertPhoneNumberRequest phoneNumber);
        Task<PhoneNumberResponse?> UpdatePhoneNumberAsync(string id, UpsertPhoneNumberRequest phoneNumber);
        Task<bool> DeletePhoneNumberAsync(string id);
        Task<bool> IsPhoneNumberIdExistsAsync(string id);
        Task<bool> IsPhoneNumberExistsAsync(string phoneNumber);
    }
}
