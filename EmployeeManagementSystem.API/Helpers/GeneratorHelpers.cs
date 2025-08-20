using Employee_Management_System_API.Interfaces.Services;
using System.Text;

namespace Employee_Management_System_API.Helpers
{    
    public static class GeneratorHelpers
    {        
        public static string GenerateID()
        {
            var getYear = DateTime.UtcNow.Year;
            string publicId;

            var randomNumber = new Random().Next(1000, 9999);
            publicId = $"{getYear}-{randomNumber}";

            return publicId;                    
        }
    }
}
