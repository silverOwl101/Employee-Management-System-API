using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Helpers;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Mappings;
using Employee_Management_System_API.Queries.PhoneNumber;

namespace Employee_Management_System_API.Services
{
    public class PhoneNumberService : IPhoneNumberService
    {
        private readonly IPhoneNumberRepository _phoneNumberRepo;
        private readonly IEmployeeRepository _employeeRepo;
        public PhoneNumberService(IPhoneNumberRepository phoneNumberRepo, IEmployeeRepository employeeRepo)
        {
            _phoneNumberRepo = phoneNumberRepo;
            _employeeRepo = employeeRepo;
        }

        public async Task<PhoneNumberResponse> AddPhoneNumberAsync(UpsertPhoneNumberRequest phoneNumber)
        {
            var existingEmployee = await _employeeRepo.GetByIdAsync(phoneNumber.EmployeePub_ID);

            if (existingEmployee != null)
            {
                if (!ValidationHelper.isRegexMatch(phoneNumber.PhoneNumberPub_ID))
                    throw new InvalidOperationException($"PhoneNumber ID must be in the format 0000-0000 using only digits.");

                var newPhoneNumber = new PhoneNumber
                {
                    PhoneNumberPub_ID = phoneNumber.PhoneNumberPub_ID,
                    PhoneNumberValue = phoneNumber.PhoneNumberValue,
                    EmployeeUID = existingEmployee.EmployeeUID
                };
                await _phoneNumberRepo.CreateAsync(newPhoneNumber);

                return new PhoneNumberResponse
                {
                    PhoneNumberPub_ID = newPhoneNumber.PhoneNumberPub_ID,
                    PhoneNumberValue = newPhoneNumber.PhoneNumberValue
                };
            }
            throw new KeyNotFoundException("Employee not found!");
        }

        public async Task<bool> DeletePhoneNumberAsync(string id)
        {
            var existing = await _phoneNumberRepo.GetByIdAsync(id);
            if (existing != null)
                return await _phoneNumberRepo.DeleteAsync(existing.PhoneNumberUID);
            return false;
        }

        public async Task<PhoneNumberResponse?> GetPhoneNumberByIdAsync(string id)
        {
            var existing = await _phoneNumberRepo.GetByIdAsync(id);
            if (existing != null)
                return existing.ToPhoneNumberResponse();
            throw new KeyNotFoundException("No records found.");
        }

        public async Task<IEnumerable<PhoneNumberResponse>> GetPhoneNumbersAsync(QueryGetAllPhoneNumbers query)
        {
            var phoneNumbers = await _phoneNumberRepo.GetAllAsync(query);
            return phoneNumbers.Select(e => new PhoneNumberResponse
            {
                PhoneNumberPub_ID = e.PhoneNumberPub_ID,
                PhoneNumberValue = e.PhoneNumberValue
            }).ToList();
        }

        public async Task<bool> IsPhoneNumberExistsAsync(string phoneNumber)
        {
            return await _phoneNumberRepo.IsPhoneNumberExistsAsync(phoneNumber);
        }

        public async Task<bool> IsPhoneNumberIdExistsAsync(string id)
        {
            return await _phoneNumberRepo.IsIdExistsAsync(id);
        }

        public async Task<PhoneNumberResponse?> UpdatePhoneNumberAsync(string id, UpsertPhoneNumberRequest phoneNumber)
        {
            var existing = await _phoneNumberRepo.GetByIdAsync(id);

            if (existing != null)
            {
                var updated = new PhoneNumber
                {
                    PhoneNumberPub_ID = phoneNumber.PhoneNumberPub_ID,
                    PhoneNumberValue = phoneNumber.PhoneNumberValue
                };
                var newPhoneValue = await _phoneNumberRepo.UpdateAsync(existing.PhoneNumberUID, updated);
                if (newPhoneValue != null)
                    return newPhoneValue.ToPhoneNumberResponse();
                throw new InvalidOperationException("Phone number cannot be updated!");
            }
            throw new InvalidOperationException("Phone number cannot be updated!");
        }
    }
}
