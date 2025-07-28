using Employee_Management_System_API.Queries.Base;
using static Employee_Management_System_API.Domain.Enums.PhoneNumberCategories;

namespace Employee_Management_System_API.Queries.PhoneNumber
{
    public class QueryGetAllPhoneNumbers : QuerySortingAndPaginationBase
    {
        public string? PhoneNumberPub_ID { get; set; }

        public string? PhoneNumberValue { get; set; }

        public SortGetAllPhoneNumber? Sortby { get; set; }
    }
}
