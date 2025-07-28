using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Response;

namespace Employee_Management_System_API.Mappings
{
    public static class PhoneNumberMappers
    {
        public static PhoneNumberResponse ToPhoneNumberResponse(this PhoneNumber phoneNumber)
        {
            return new PhoneNumberResponse
            {
                PhoneNumberPub_ID = phoneNumber.PhoneNumberPub_ID,
                PhoneNumberValue = phoneNumber.PhoneNumberValue
            };
        }
    }
}
