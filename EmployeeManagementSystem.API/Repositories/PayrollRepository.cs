using Employee_Management_System_API.Data;
using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Helpers.DataManipulators;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Queries.Payroll;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System_API.Repositories
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly ApplicationDBContext _context;
        public PayrollRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Payroll> CreateAsync(Payroll payroll)
        {
            await _context.Payrolls.AddAsync(payroll);
            await _context.SaveChangesAsync();
            return await Task.FromResult(payroll);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var exist = await _context.Payrolls.FirstOrDefaultAsync(e => e.PayrollUID == id);
            if (exist != null)
                _context.Payrolls.Remove(exist);
            return await _context.SaveChangesAsync() > -1 ? true : false;
        }

        public async Task<IEnumerable<Payroll>> GetAllAsync(QueryGetAllPayroll query)
        {
            var payroll = _context.Payrolls.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.PayrollPub_ID))
                payroll = payroll.Where(q => q.PayrollPub_ID == query.PayrollPub_ID);

            if (query.PayDate.HasValue)
                payroll = payroll.Where(q => q.PayDate == query.PayDate);

            if (query.Sortby.HasValue)
                payroll = EmployeeSorters.Sort(payroll, query.Sortby.ToString() ?? "", query.IsDecsending).AsQueryable();

            return await EmployeePagination.Pagination(payroll, query.PageNumber, query.PageSize).ToListAsync();
        }

        public async Task<Payroll?> GetByIdAsync(string id)
        {
            return await _context.Payrolls.FirstOrDefaultAsync(e => e.PayrollPub_ID == id);
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _context.Payrolls.AnyAsync(e => e.PayrollUID == id);
        }

        public async Task<Payroll?> UpdateAsync(Guid id, Payroll payroll)
        {
            var existingPayroll = await _context.Payrolls.FirstOrDefaultAsync(e => e.PayrollUID == id);
            if (existingPayroll != null)
            {
                existingPayroll.PayrollPub_ID = payroll.PayrollPub_ID;
                existingPayroll.PayDate = payroll.PayDate;
                existingPayroll.BasicSalary = payroll.BasicSalary;
                existingPayroll.Allowances = payroll.Allowances;
                existingPayroll.Deductions = payroll.Deductions;
                existingPayroll.NetSalary = payroll.NetSalary;
                _context.Payrolls.Update(existingPayroll);
                await _context.SaveChangesAsync();
                return existingPayroll;
            }
            return null;
        }
    }
}
