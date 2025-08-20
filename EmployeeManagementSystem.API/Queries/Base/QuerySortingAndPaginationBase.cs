namespace Employee_Management_System_API.Queries.Base
{
    public class QuerySortingAndPaginationBase
    {
        /// <summary>
        /// Sets a value indicating whether the sorting order is descending.
        /// </summary>
        public bool IsDecsending { get; set; } = false;

        /// <summary>
        /// Sets what page number to display in a paginated list.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Sets the number of items to display per page in a paginated list.
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
