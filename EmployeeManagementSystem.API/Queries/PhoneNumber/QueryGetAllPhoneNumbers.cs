using Employee_Management_System_API.Queries.Base;
using static Employee_Management_System_API.Domain.Enums.PhoneNumberCategories;

namespace Employee_Management_System_API.Queries.PhoneNumber
{
    public class QueryGetAllPhoneNumbers : QuerySortingAndPaginationBase
    {
        /// <summary>
        /// Phone number public id filter for phone number records.
        /// </summary>
        public string? PhoneNumberPub_ID { get; set; }

        /// <summary>
        /// Phone number value filter for phone number records.
        /// </summary>
        public string? PhoneNumberValue { get; set; }

        /// <summary>
        /// Sort by filter for phone number records.
        /// </summary>
        public SortGetAllPhoneNumber? Sortby { get; set; }
    }
}
