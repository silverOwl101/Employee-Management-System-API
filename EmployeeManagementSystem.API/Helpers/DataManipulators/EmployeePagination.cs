using Employee_Management_System_API.Domain.Entities;

namespace Employee_Management_System_API.Helpers.DataManipulators
{
    public static class EmployeePagination
    {

        public static IQueryable<T> Pagination<T>(IQueryable<T> query, int pageNumber, int pageSize)
        {            
            var pagination = (pageNumber - 1) * pageSize;
            return query.Skip(pagination).Take(pageSize);
        }        
    }
}
