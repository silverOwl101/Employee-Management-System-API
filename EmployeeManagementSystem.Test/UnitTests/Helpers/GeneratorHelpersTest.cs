using Employee_Management_System_API.Helpers;
using FluentAssertions;
using System.Text.RegularExpressions;

namespace EmployeeManagementSystem.Test.UnitTests.Helpers
{
    public class GeneratorHelpersTest
    {
        [Fact]
        public void GenerateIDTest()
        {
            //Arrange
            var result = GeneratorHelpers.GenerateID();
            //Act

            //Assert
            result.Should().NotBeNull();
            result.Should().StartWith(DateTime.UtcNow.Year.ToString());
            Regex.IsMatch(result, @"^\d{4}-\d{4}$").Should().BeTrue();


        }
    }
}
