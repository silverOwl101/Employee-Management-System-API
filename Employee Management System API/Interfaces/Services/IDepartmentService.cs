using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Queries.Department;

namespace Employee_Management_System_API.Interfaces.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentResponse>> GetAllDepartment(QueryGetAllDepartment query);
        Task<DepartmentResponse> CreateDepartmentAsync(UpsertDepartmentRequest department);
        Task<DepartmentResponse> UpdateDepartmentAsync(string id, UpsertDepartmentRequest department);
        Task<bool> DeleteDepartmentAsync(string id);
        Task<DepartmentResponse> GetDepartmentById(string id);        
    }
}
