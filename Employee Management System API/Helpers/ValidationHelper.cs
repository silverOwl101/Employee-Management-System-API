using System.Text.RegularExpressions;

namespace Employee_Management_System_API.Helpers
{
    public static class ValidationHelper
    {
        public static bool isRegexMatch(string id)
        {
            return Regex.IsMatch(id, @"^\d{4}-\d{4}$");
        }
    }
}
