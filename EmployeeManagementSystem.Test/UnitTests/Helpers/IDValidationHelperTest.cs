using Bogus;
using Employee_Management_System_API.Helpers;
using FluentAssertions;

namespace EmployeeManagementSystem.Test.UnitTests.Helpers
{
    
    public class IDValidationHelperTest
    {
        [Fact]
        public void isRegexMatchTest()
        {
            string id = "2025-2233";
            var result = ValidationHelper.isRegexMatch(id);

            result.Should().BeTrue();
        }
    }
}
