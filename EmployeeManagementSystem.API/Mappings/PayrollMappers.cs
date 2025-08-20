using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Response;

namespace Employee_Management_System_API.Mappings
{
    public static class PayrollMappers
    {
        public static PayrollResponse ToPayrollDto(this Payroll payroll)
        {
            return new PayrollResponse
            {
                PayrollPub_ID = payroll.PayrollPub_ID,
                PayDate = payroll.PayDate,
                BasicSalary = payroll.BasicSalary,
                Allowances = payroll.Allowances,
                Deductions = payroll.Deductions,
                NetSalary = payroll.NetSalary
            };
        }
    }
}
