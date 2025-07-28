using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Employee_Management_System_API.Migrations
{
    /// <inheritdoc />
    public partial class Add_other_roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5162774b-9a1e-4773-a73c-de1963d40a58"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6a609f34-2104-4dc9-8fff-7f37f1f65d64"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("189f9797-aeec-481f-9154-4fc630590017"), null, "PayrollAssistant", "PAYROLLASSISTANT" },
                    { new Guid("18db8396-7473-4f53-9b1e-1155e65047ff"), null, "PayrollOfficer", "PAYROLLOFFICER" },
                    { new Guid("337ca893-79ce-4d45-a1af-8d335f76cec7"), null, "ProjectManager", "PROJECTMANAGER" },
                    { new Guid("349ad8bb-5d46-43f4-b21c-aafc6db97a94"), null, "Employee", "EMPLOYEE" },
                    { new Guid("34a71f5e-b93d-410b-a9b5-695bbad9c407"), null, "LeaveCoordinator", "LEAVECOORDINATOR" },
                    { new Guid("376e5a0e-dd14-4394-8ab9-0d02889972c5"), null, "HRManager", "HRMANAGER" },
                    { new Guid("415a4bd3-44c1-4725-b66f-362ed30a7b9d"), null, "Intern", "INTERN" },
                    { new Guid("543737ed-030e-47b2-9808-881ee43d2568"), null, "ProjectLead", "PROJECTLEAD" },
                    { new Guid("6ceb9ca1-4f8d-43e0-8d57-0f8d0205801c"), null, "TeamLead", "TEAMLEAD" },
                    { new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6"), null, "SuperAdmin", "SUPERADMIN" },
                    { new Guid("6eb74c93-108c-4481-9662-cfa244899f46"), null, "DepartmentHead", "DEPARTMENTHEAD" },
                    { new Guid("701747f8-ce70-4bf8-8b7c-e09c28d27cfe"), null, "HRAssistant", "HRASSISTANT" },
                    { new Guid("a9e1d827-52d6-40d9-9f00-616e19212417"), null, "ProjectCoordinator", "PROJECTCOORDINATOR" },
                    { new Guid("b7f60b18-dc58-4d0d-9d3e-d4b3c8ee9a3a"), null, "LeaveApprover", "LEAVEAPPROVER" },
                    { new Guid("ed02f158-8363-434f-9b2e-254c77c45c64"), null, "HRStaff", "HRSTAFF" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("189f9797-aeec-481f-9154-4fc630590017"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("18db8396-7473-4f53-9b1e-1155e65047ff"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("337ca893-79ce-4d45-a1af-8d335f76cec7"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("349ad8bb-5d46-43f4-b21c-aafc6db97a94"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("34a71f5e-b93d-410b-a9b5-695bbad9c407"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("376e5a0e-dd14-4394-8ab9-0d02889972c5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("415a4bd3-44c1-4725-b66f-362ed30a7b9d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("543737ed-030e-47b2-9808-881ee43d2568"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6ceb9ca1-4f8d-43e0-8d57-0f8d0205801c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6eb74c93-108c-4481-9662-cfa244899f46"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("701747f8-ce70-4bf8-8b7c-e09c28d27cfe"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a9e1d827-52d6-40d9-9f00-616e19212417"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b7f60b18-dc58-4d0d-9d3e-d4b3c8ee9a3a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ed02f158-8363-434f-9b2e-254c77c45c64"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("5162774b-9a1e-4773-a73c-de1963d40a58"), null, "Admin", "ADMIN" },
                    { new Guid("6a609f34-2104-4dc9-8fff-7f37f1f65d64"), null, "User", "USER" }
                });
        }
    }
}
