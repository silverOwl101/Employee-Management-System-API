using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee_Management_System_API.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DepartmentPub_ID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentUID);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ProjectPub_ID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectUID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRoles",
                columns: table => new
                {
                    RoleUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    RolePub_ID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleUID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    EmployeePub_ID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    RoleUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeUID);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentUID",
                        column: x => x.DepartmentUID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentUID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Roles_RoleUID",
                        column: x => x.RoleUID,
                        principalTable: "EmployeeRoles",
                        principalColumn: "RoleUID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    AttendanceUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AttendancePub_ID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckInTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CheckOutTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.AttendanceUID);
                    table.ForeignKey(
                        name: "FK_Attendances_Employees_EmployeeUID",
                        column: x => x.EmployeeUID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProjectAssignments",
                columns: table => new
                {
                    AssignmentUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AssignmentPub_ID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RoleInProject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ProjectUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProjectAssignments", x => x.AssignmentUID);
                    table.ForeignKey(
                        name: "FK_EmployeeProjectAssignments_Employees_EmployeeUID",
                        column: x => x.EmployeeUID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeUID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeProjectAssignments_Projects_ProjectUID",
                        column: x => x.ProjectUID,
                        principalTable: "Projects",
                        principalColumn: "ProjectUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    LeaveUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    LeavePub_ID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.LeaveUID);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_Employees_EmployeeUID",
                        column: x => x.EmployeeUID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payrolls",
                columns: table => new
                {
                    PayrollUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PayrollPub_ID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PayDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BasicSalary = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Allowances = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Deductions = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    NetSalary = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    EmployeeUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payrolls", x => x.PayrollUID);
                    table.ForeignKey(
                        name: "FK_Payrolls_Employees_EmployeeUID",
                        column: x => x.EmployeeUID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceReviews",
                columns: table => new
                {
                    ReviewUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ReviewPub_ID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceReviews", x => x.ReviewUID);
                    table.CheckConstraint("CK_PerformanceReview_Score", "[Score] BETWEEN 1 AND 10");
                    table.ForeignKey(
                        name: "FK_PerformanceReviews_Employees_EmployeeUID",
                        column: x => x.EmployeeUID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    PhoneNumberUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PhoneNumberPub_ID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PhoneNumberValue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmployeeUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.PhoneNumberUID);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_Employees_EmployeeUID",
                        column: x => x.EmployeeUID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EmployeeUID",
                table: "Attendances",
                column: "EmployeeUID");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DepartmentName",
                table: "Departments",
                column: "DepartmentName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjectAssignments_EmployeeUID",
                table: "EmployeeProjectAssignments",
                column: "EmployeeUID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjectAssignments_ProjectUID",
                table: "EmployeeProjectAssignments",
                column: "ProjectUID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentUID",
                table: "Employees",
                column: "DepartmentUID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleUID",
                table: "Employees",
                column: "RoleUID");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_EmployeeUID",
                table: "LeaveRequests",
                column: "EmployeeUID");

            migrationBuilder.CreateIndex(
                name: "IX_Payrolls_EmployeeUID",
                table: "Payrolls",
                column: "EmployeeUID");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_EmployeeUID",
                table: "PerformanceReviews",
                column: "EmployeeUID");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_EmployeeUID",
                table: "PhoneNumbers",
                column: "EmployeeUID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "EmployeeProjectAssignments");

            migrationBuilder.DropTable(
                name: "LeaveRequests");

            migrationBuilder.DropTable(
                name: "Payrolls");

            migrationBuilder.DropTable(
                name: "PerformanceReviews");

            migrationBuilder.DropTable(
                name: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "EmployeeRoles");
        }
    }
}
