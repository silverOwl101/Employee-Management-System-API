using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Helpers;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Mappings;
using Employee_Management_System_API.Queries.Payroll;

namespace Employee_Management_System_API.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly IPayrollRepository _payrollRepo;
        private readonly IEmployeeRepository _employeeRepo;
        public PayrollService(IPayrollRepository payrollRepo, IEmployeeRepository employeeRepo)
        {
            _payrollRepo = payrollRepo;
            _employeeRepo = employeeRepo;
        }

        public async Task<PayrollResponse> CreatePayrollAsync(UpsertPayrollRequest payroll)
        {
            var existingEmployee = await _employeeRepo.GetByIdAsync(payroll.EmployeePub_ID);
            if (existingEmployee != null)
            {
                if (!ValidationHelper.isRegexMatch(payroll.PayrollPub_ID))
                    throw new InvalidOperationException($"Payroll ID must be in the format 0000-0000 using only digits.");
                var initPayroll = new Payroll
                {
                    PayrollPub_ID = payroll.PayrollPub_ID,
                    PayDate = payroll.PayDate,
                    BasicSalary = payroll.BasicSalary,
                    Allowances = payroll.Allowances,
                    Deductions = payroll.Deductions,
                    NetSalary = payroll.NetSalary,
                    EmployeeUID = existingEmployee.EmployeeUID
                };

                var createdPayroll = await _payrollRepo.CreateAsync(initPayroll);
                return createdPayroll.ToPayrollDto();
            }
            throw new KeyNotFoundException("Employee not found!");
        }

        public async Task<bool> DeletePayrollAsync(string id)
        {
            var existing = await _payrollRepo.GetByIdAsync(id);
            var result = existing != null ? await _payrollRepo.DeleteAsync(existing.PayrollUID) : false;
            return result;
        }

        public async Task<IEnumerable<PayrollResponse>> GetAllPayrollAsync(QueryGetAllPayroll query)
        {
            var list = await _payrollRepo.GetAllAsync(query);
            return list.Select(e => e.ToPayrollDto()).ToList();
        }

        public async Task<PayrollResponse?> GetPayrollByIdAsync(string id)
        {
            var exist = await _payrollRepo.GetByIdAsync(id);
            if (exist != null)
                return exist.ToPayrollDto();
            throw new KeyNotFoundException("No records found.");
        }

        public async Task<bool> IsPayrollExistsAsync(Guid id)
        {
            return await _payrollRepo.IsExistsAsync(id);
        }

        public async Task<PayrollResponse?> UpdatePayrollAsync(string id, UpsertPayrollRequest payroll)
        {
            var existing = await _payrollRepo.GetByIdAsync(id);
            if (existing != null)
            {
                var updatedPayroll = new Payroll
                {
                    PayrollPub_ID = payroll.PayrollPub_ID,
                    PayDate = payroll.PayDate,
                    BasicSalary = payroll.BasicSalary,
                    Allowances = payroll.Allowances,
                    Deductions = payroll.Deductions,
                    NetSalary = payroll.NetSalary
                };
                var newPayroll = await _payrollRepo.UpdateAsync(existing.PayrollUID, updatedPayroll);
                if (newPayroll != null)
                    return newPayroll.ToPayrollDto();
                throw new InvalidOperationException("Payroll cannot be updated!");
            }
            throw new InvalidOperationException("Payroll cannot be updated!");
        }
    }
}
