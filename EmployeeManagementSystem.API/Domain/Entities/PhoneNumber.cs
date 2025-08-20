using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_API.Domain.Entities
{
    public class PhoneNumber
    {
        [Key]
        public Guid PhoneNumberUID { get; set; }

        [Required, MaxLength(10)]
        public string PhoneNumberPub_ID { get; set; } = default!;

        [Required, MaxLength(20)]
        public string PhoneNumberValue { get; set; } = default!;

        public Guid EmployeeUID { get; set; }
        public Employee Employee { get; set; } = default!;

    }
}
