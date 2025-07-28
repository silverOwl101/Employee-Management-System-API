using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Employee_Management_System_API.Migrations
{
    /// <inheritdoc />
    public partial class AddingRolesv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {            
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("5162774b-9a1e-4773-a73c-de1963d40a58"), null, "Admin", "ADMIN" },
                    { new Guid("6a609f34-2104-4dc9-8fff-7f37f1f65d64"), null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5162774b-9a1e-4773-a73c-de1963d40a58"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6a609f34-2104-4dc9-8fff-7f37f1f65d64"));                        
        }
    }
}
