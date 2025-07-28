using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee_Management_System_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppUserEmployeeRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_AppUserId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_AppUserId",
                table: "Employees");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppUserId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWSEQUENTIALID()");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AppUserId",
                table: "Employees",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_AppUserId",
                table: "Employees",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_AppUserId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_AppUserId",
                table: "Employees");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppUserId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AppUserId",
                table: "Employees",
                column: "AppUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_AppUserId",
                table: "Employees",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
