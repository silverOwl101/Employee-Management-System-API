namespace Employee_Management_System_API.Queries.Base
{
    public class QuerySortingAndPaginationBase
    {
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
