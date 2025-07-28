using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee_Management_System_API.Migrations
{
    /// <inheritdoc />
    public partial class renamerolestoemployeerolesv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "EmployeeRoles"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "EmployeeRoles",
                newName: "Roles"
            );
        }
    }
}
