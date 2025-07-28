using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.DTOs.Request
{
    public class UpsertPhoneNumberRequest
    {
        [Required, MaxLength(10)]
        public string PhoneNumberPub_ID { get; set; } = default!;

        [Required, MaxLength(20)]
        public string PhoneNumberValue { get; set; } = default!;

        [Required, MaxLength(10)]
        public string EmployeePub_ID { get; set; } = default!;
    }
}
