using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.DTOs.Request
{
    public class UpsertPhoneNumberRequest
    {
        [Required, MaxLength(10)]
        [DisplayName("Phonenumber ID")]
        public string PhoneNumberPub_ID { get; set; } = default!;

        [Required, MaxLength(20)]
        [DisplayName("Phone number")]
        public string PhoneNumberValue { get; set; } = default!;

        [Required, MaxLength(10)]
        [DisplayName("Employee ID")]
        public string EmployeePub_ID { get; set; } = default!;
    }
}
