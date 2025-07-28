using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Employee_Management_System_API.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Role_Claims : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 58, "Employee.View", "true", new Guid("376e5a0e-dd14-4394-8ab9-0d02889972c5") },
                    { 59, "Department.View", "true", new Guid("376e5a0e-dd14-4394-8ab9-0d02889972c5") },
                    { 60, "LeaveRequest.Approve", "true", new Guid("376e5a0e-dd14-4394-8ab9-0d02889972c5") },
                    { 61, "LeaveRequest.ById", "true", new Guid("376e5a0e-dd14-4394-8ab9-0d02889972c5") },
                    { 62, "Employee.View", "true", new Guid("ed02f158-8363-434f-9b2e-254c77c45c64") },
                    { 63, "LeaveRequest.View", "true", new Guid("ed02f158-8363-434f-9b2e-254c77c45c64") },
                    { 64, "Employee.View", "true", new Guid("701747f8-ce70-4bf8-8b7c-e09c28d27cfe") },
                    { 65, "Payroll.View", "true", new Guid("18db8396-7473-4f53-9b1e-1155e65047ff") },
                    { 66, "Payroll.Update", "true", new Guid("18db8396-7473-4f53-9b1e-1155e65047ff") },
                    { 67, "Payroll.ById", "true", new Guid("18db8396-7473-4f53-9b1e-1155e65047ff") },
                    { 68, "Payroll.Create", "true", new Guid("18db8396-7473-4f53-9b1e-1155e65047ff") },
                    { 69, "Payroll.View", "true", new Guid("189f9797-aeec-481f-9154-4fc630590017") },
                    { 70, "Payroll.ById", "true", new Guid("189f9797-aeec-481f-9154-4fc630590017") },
                    { 71, "LeaveRequest.View", "true", new Guid("b7f60b18-dc58-4d0d-9d3e-d4b3c8ee9a3a") },
                    { 72, "LeaveRequest.Update", "true", new Guid("b7f60b18-dc58-4d0d-9d3e-d4b3c8ee9a3a") },
                    { 73, "LeaveRequest.View", "true", new Guid("34a71f5e-b93d-410b-a9b5-695bbad9c407") },
                    { 74, "Project.View", "true", new Guid("337ca893-79ce-4d45-a1af-8d335f76cec7") },
                    { 75, "Project.Create", "true", new Guid("337ca893-79ce-4d45-a1af-8d335f76cec7") },
                    { 76, "Project.Update", "true", new Guid("337ca893-79ce-4d45-a1af-8d335f76cec7") },
                    { 77, "ProjectAssignment.Create", "true", new Guid("337ca893-79ce-4d45-a1af-8d335f76cec7") },
                    { 78, "ProjectAssignment.Update", "true", new Guid("337ca893-79ce-4d45-a1af-8d335f76cec7") },
                    { 79, "ProjectAssignment.Create", "true", new Guid("543737ed-030e-47b2-9808-881ee43d2568") },
                    { 80, "ProjectAssignment.Update", "true", new Guid("543737ed-030e-47b2-9808-881ee43d2568") },
                    { 81, "Project.View", "true", new Guid("543737ed-030e-47b2-9808-881ee43d2568") },
                    { 82, "Project.ById", "true", new Guid("543737ed-030e-47b2-9808-881ee43d2568") },
                    { 83, "Project.View", "true", new Guid("a9e1d827-52d6-40d9-9f00-616e19212417") },
                    { 84, "Project.ById", "true", new Guid("6ceb9ca1-4f8d-43e0-8d57-0f8d0205801c") },
                    { 85, "ProjectAssignment.View", "true", new Guid("6ceb9ca1-4f8d-43e0-8d57-0f8d0205801c") },
                    { 86, "PerformanceReview.View", "true", new Guid("6ceb9ca1-4f8d-43e0-8d57-0f8d0205801c") },
                    { 87, "PerformanceReview.Create", "true", new Guid("6ceb9ca1-4f8d-43e0-8d57-0f8d0205801c") },
                    { 88, "PerformanceReview.Update", "true", new Guid("6ceb9ca1-4f8d-43e0-8d57-0f8d0205801c") },
                    { 89, "Department.View", "true", new Guid("6eb74c93-108c-4481-9662-cfa244899f46") },
                    { 90, "PerformanceReview.View", "true", new Guid("6eb74c93-108c-4481-9662-cfa244899f46") },
                    { 91, "Employee.View", "true", new Guid("349ad8bb-5d46-43f4-b21c-aafc6db97a94") },
                    { 92, "Employee.Attendance", "true", new Guid("349ad8bb-5d46-43f4-b21c-aafc6db97a94") },
                    { 93, "Employee.LeaveRequest", "true", new Guid("349ad8bb-5d46-43f4-b21c-aafc6db97a94") },
                    { 94, "Employee.Payroll", "true", new Guid("349ad8bb-5d46-43f4-b21c-aafc6db97a94") },
                    { 95, "Employee.PerformanceReview", "true", new Guid("349ad8bb-5d46-43f4-b21c-aafc6db97a94") },
                    { 96, "Employee.PhoneNumbers", "true", new Guid("349ad8bb-5d46-43f4-b21c-aafc6db97a94") },
                    { 97, "Employee.ProjectAssignment", "true", new Guid("349ad8bb-5d46-43f4-b21c-aafc6db97a94") },
                    { 98, "LeaveRequest.Create", "true", new Guid("349ad8bb-5d46-43f4-b21c-aafc6db97a94") },
                    { 99, "Employee.View", "true", new Guid("415a4bd3-44c1-4725-b66f-362ed30a7b9d") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 99);
        }
    }
}
