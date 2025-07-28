using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Employee_Management_System_API.Migrations
{
    /// <inheritdoc />
    public partial class Adding_claims_SuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "Attendance.View", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 2, "Attendance.ById", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 3, "Attendance.Create", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 4, "Attendance.Update", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 5, "Attendance.Delete", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 6, "Department.View", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 7, "Department.ById", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 8, "Department.Create", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 9, "Department.Update", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 10, "Department.Delete", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 11, "Employee.View", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 12, "Employee.ById", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 13, "Employee.Attendance", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 14, "Employee.LeaveRequest", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 15, "Employee.Payroll", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 16, "Employee.PerformanceReview", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 17, "Employee.PhoneNumbers", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 18, "Employee.ProjectAssignment", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 19, "Employee.Create", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 20, "Employee.Update", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 21, "Employee.Delete", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 22, "LeaveRequest.View", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 23, "LeaveRequest.ById", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 24, "LeaveRequest.Create", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 25, "LeaveRequest.Update", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 26, "LeaveRequest.Delete", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 27, "Payroll.View", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 28, "Payroll.ById", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 29, "Payroll.Create", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 30, "Payroll.Update", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 31, "Payroll.Delete", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 32, "PerformanceReview.View", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 33, "PerformanceReview.ById", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 34, "PerformanceReview.Create", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 35, "PerformanceReview.Update", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 36, "PerformanceReview.Delete", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 37, "PhoneNumber.View", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 38, "PhoneNumber.ById", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 39, "PhoneNumber.Create", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 40, "PhoneNumber.Update", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 41, "PhoneNumber.Delete", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 42, "ProjectAssignment.View", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 43, "ProjectAssignment.ById", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 44, "ProjectAssignment.Create", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 45, "ProjectAssignment.Update", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 46, "ProjectAssignment.Delete", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 47, "Project.View", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 48, "Project.ById", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 49, "Project.GetEmployees", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 50, "Project.Create", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 51, "Project.Update", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 52, "Project.Delete", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 53, "Role.View", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 54, "Role.ById", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 55, "Role.Create", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 56, "Role.Update", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") },
                    { 57, "Role.Delete", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 57);
        }
    }
}
