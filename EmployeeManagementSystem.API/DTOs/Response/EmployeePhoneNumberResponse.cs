namespace Employee_Management_System_API.DTOs.Response
{
    public class EmployeePhoneNumberResponse
    {
        public string EmployeePub_ID { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string MiddleName { get; set; } = default!;

        public string LastName { get; set; } = default!;
        public  IEnumerable<PhoneNumberResponse> PhoneNumbers { get; set; } = new List<PhoneNumberResponse>();
    }
}
