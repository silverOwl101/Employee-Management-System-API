using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee_Management_System_API.Migrations
{
    /// <inheritdoc />
    public partial class EnforcePubIdDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Departments_DepartmentPub_ID",
                table: "Departments",
                column: "DepartmentPub_ID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Departments_DepartmentPub_ID",
                table: "Departments");
        }
    }
}
