using Employee_Management_System_API.Data;
using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Helpers.DataManipulators;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Queries.PhoneNumber;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System_API.Repositories
{
    public class PhoneNumberRepository : IPhoneNumberRepository
    {
        private readonly ApplicationDBContext _context;
        public PhoneNumberRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<PhoneNumber> CreateAsync(PhoneNumber phoneNumber)
        {
            await _context.PhoneNumbers.AddAsync(phoneNumber);
            await _context.SaveChangesAsync();
            return await Task.FromResult(phoneNumber);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var phoneNumber = await _context.PhoneNumbers.FirstOrDefaultAsync(p => p.PhoneNumberUID == id);
            if (phoneNumber != null)
            {
                _context.PhoneNumbers.Remove(phoneNumber);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<PhoneNumber>> GetAllAsync(QueryGetAllPhoneNumbers query)
        {
            var phoneNumber = _context.PhoneNumbers.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.PhoneNumberPub_ID))
                phoneNumber = phoneNumber.Where(q => q.PhoneNumberPub_ID == query.PhoneNumberPub_ID);

            if (!string.IsNullOrEmpty(query.PhoneNumberValue))
                phoneNumber = phoneNumber.Where(q => q.PhoneNumberValue == query.PhoneNumberValue);

            if (query.Sortby.HasValue)
                phoneNumber = EmployeeSorters.Sort(phoneNumber, query.Sortby.ToString() ?? "", query.IsDecsending).AsQueryable();

            return await EmployeePagination.Pagination(phoneNumber, query.PageNumber, query.PageSize).ToListAsync();
        }

        public async Task<PhoneNumber?> GetByIdAsync(string id)
        {
            return await _context.PhoneNumbers.FirstOrDefaultAsync(p => p.PhoneNumberPub_ID == id);
        }

        public async Task<bool> IsIdExistsAsync(string id)
        {
            return await _context.PhoneNumbers.AnyAsync(d => d.PhoneNumberPub_ID == id);
        }

        public async Task<bool> IsPhoneNumberExistsAsync(string phoneNumber)
        {
            return await _context.PhoneNumbers.AnyAsync(d => d.PhoneNumberValue == phoneNumber);
        }

        public async Task<PhoneNumber?> UpdateAsync(Guid id, PhoneNumber phoneNumber)
        {
            var existingPhoneNumber = await _context.PhoneNumbers.FirstOrDefaultAsync(p => p.PhoneNumberUID == id);
            if (existingPhoneNumber != null)
            {
                existingPhoneNumber.PhoneNumberPub_ID = phoneNumber.PhoneNumberPub_ID;
                existingPhoneNumber.PhoneNumberValue = phoneNumber.PhoneNumberValue;
                _context.PhoneNumbers.Update(existingPhoneNumber);
                await _context.SaveChangesAsync();
                return existingPhoneNumber;
            }
            return null;
        }
    }
}
