using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Helpers;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Mappings;
using Employee_Management_System_API.Queries.Department;

namespace Employee_Management_System_API.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repo;
        public DepartmentService(IDepartmentRepository repo)
        {
            _repo = repo;
        }
        public async Task<DepartmentResponse> CreateDepartmentAsync(UpsertDepartmentRequest department)
        {
            if (await _repo.IsIdExistsAsync(department.DepartmentPub_ID) ||
                await _repo.IsNameExistsAsync(department.DepartmentName))
                throw new InvalidOperationException($"Department existed");

            if (!ValidationHelper.isRegexMatch(department.DepartmentPub_ID))
                throw new InvalidOperationException($"Department ID must be in the format 0000-0000 using only digits.");

            var dept = new Department
            {
                DepartmentPub_ID = department.DepartmentPub_ID,
                DepartmentName = department.DepartmentName,
                Description = department.Description
            };

            await _repo.CreateAsync(dept);

            return new DepartmentResponse
            {
                DepartmentPub_ID = dept.DepartmentPub_ID,
                DepartmentName = dept.DepartmentName,
                Description = dept.Description
            };
        }
        public async Task<IEnumerable<DepartmentResponse>> GetAllDepartment(QueryGetAllDepartment query)
        {
            var departments = await _repo.GetAllAsync(query);
            return departments.Select(e => e.ToDepartmentDto()).ToList();
        }
        public async Task<DepartmentResponse> GetDepartmentById(string id)
        {
            var department = await _repo.GetByIdAsync(id);
            if (department is not null)
                return department.ToDepartmentDto();
            throw new KeyNotFoundException("Department not found!");
        }

        public async Task<DepartmentResponse> UpdateDepartmentAsync(string id, UpsertDepartmentRequest department)
        {
            var existingDepartment = await _repo.GetByIdAsync(id);
            if (existingDepartment != null)
            {
                var dept = new Department
                {
                    DepartmentPub_ID = department.DepartmentPub_ID,
                    DepartmentName = department.DepartmentName,
                    Description = department.Description
                };

                var newDeptValues = await _repo.UpdateAsync(existingDepartment.DepartmentUID, dept);
                return newDeptValues != null ?
                new DepartmentResponse
                {
                    DepartmentPub_ID = newDeptValues.DepartmentPub_ID,
                    DepartmentName = newDeptValues.DepartmentName,
                    Description = newDeptValues.Description
                } : throw new InvalidOperationException($"Department with ID {id} could not be updated.");
            }
            throw new KeyNotFoundException($"Department with ID {id} not found.");
        }

        public async Task<bool> DeleteDepartmentAsync(string id)
        {
            var existingDepartment = await _repo.GetByIdAsync(id);
            if (existingDepartment == null)
                throw new KeyNotFoundException($"Department with ID {id} not found.");
            return await _repo.DeleteAsync(existingDepartment.DepartmentUID);
        }
    }
}
