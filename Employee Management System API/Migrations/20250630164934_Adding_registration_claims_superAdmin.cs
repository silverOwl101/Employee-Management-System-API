using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee_Management_System_API.Migrations
{
    /// <inheritdoc />
    public partial class Adding_registration_claims_superAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { 100, "Account.Register", "true", new Guid("6e70c831-1f14-44e8-ac9e-f02825bd65d6") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 100);
        }
    }
}
