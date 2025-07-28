using Employee_Management_System_API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Employee_Management_System_API.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser,
                                        IdentityRole<Guid>,
                                        Guid,
                                        IdentityUserClaim<Guid>,
                                        IdentityUserRole<Guid>,
                                        IdentityUserLogin<Guid>,
                                        IdentityRoleClaim<Guid>,
                                        IdentityUserToken<Guid>>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Role> EmployeeRoles => Set<Role>();
        public DbSet<PhoneNumber> PhoneNumbers => Set<PhoneNumber>();
        public DbSet<Attendance> Attendances => Set<Attendance>();
        public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();
        public DbSet<Payroll> Payrolls => Set<Payroll>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<ProjectAssignment> EmployeeProjectAssignments => Set<ProjectAssignment>();
        public DbSet<PerformanceReview> PerformanceReviews => Set<PerformanceReview>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Set default value for GUIDs
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entity.GetProperties().Where(p => p.ClrType == typeof(Guid)))
                {
                    property.SetDefaultValueSql("NEWSEQUENTIALID()");
                }
            }
            
            //Enum to string mappings
            modelBuilder.Entity<Employee>().Property(e => e.Status).HasConversion<string>();
            modelBuilder.Entity<Attendance>().Property(a => a.Status).HasConversion<string>();
            modelBuilder.Entity<LeaveRequest>().Property(l => l.LeaveType).HasConversion<string>();
            modelBuilder.Entity<LeaveRequest>().Property(l => l.Status).HasConversion<string>();
            modelBuilder.Entity<Project>().Property(p => p.Status).HasConversion<string>();            

            //Check constraint for PerformanceReview.Score
            modelBuilder.Entity<PerformanceReview>()
                .ToTable(tb => tb.HasCheckConstraint("CK_PerformanceReview_Score", "[Score] BETWEEN 1 AND 10"));

            //TimeSpan mapped to SQL Server time
            modelBuilder.Entity<Attendance>().Property(a => a.CheckInTime).HasColumnType("time");
            modelBuilder.Entity<Attendance>().Property(a => a.CheckOutTime).HasColumnType("time");

            //Unique constraints
            modelBuilder.Entity<Employee>().HasIndex(e => e.Email).IsUnique();
            modelBuilder.Entity<Department>().HasIndex(d => d.DepartmentName).IsUnique();
            
            //Seed Roles
            List<IdentityRole<Guid>> roles = new List<IdentityRole<Guid>>
            {

                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("6e70c831-1f14-44e8-ac9e-f02825bd65d6"),
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN"
                },

                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("376e5a0e-dd14-4394-8ab9-0d02889972c5"),
                    Name = "HRManager",
                    NormalizedName = "HRMANAGER"
                },

                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("ed02f158-8363-434f-9b2e-254c77c45c64"),
                    Name = "HRStaff",
                    NormalizedName = "HRSTAFF"
                },

                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("701747f8-ce70-4bf8-8b7c-e09c28d27cfe"),
                    Name = "HRAssistant",
                    NormalizedName = "HRASSISTANT"
                },

                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("18db8396-7473-4f53-9b1e-1155e65047ff"),
                    Name = "PayrollOfficer",
                    NormalizedName = "PAYROLLOFFICER"
                },

                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("189f9797-aeec-481f-9154-4fc630590017"),
                    Name = "PayrollAssistant",
                    NormalizedName = "PAYROLLASSISTANT"
                },

                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("b7f60b18-dc58-4d0d-9d3e-d4b3c8ee9a3a"),
                    Name = "LeaveApprover",
                    NormalizedName = "LEAVEAPPROVER"
                },

                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("34a71f5e-b93d-410b-a9b5-695bbad9c407"),
                    Name = "LeaveCoordinator",
                    NormalizedName = "LEAVECOORDINATOR"
                },

                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("337ca893-79ce-4d45-a1af-8d335f76cec7"),
                    Name = "ProjectManager",
                    NormalizedName = "PROJECTMANAGER"
                },

                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("543737ed-030e-47b2-9808-881ee43d2568"),
                    Name = "ProjectLead",
                    NormalizedName = "PROJECTLEAD"
                },

                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("a9e1d827-52d6-40d9-9f00-616e19212417"),
                    Name = "ProjectCoordinator",
                    NormalizedName = "PROJECTCOORDINATOR"
                },

                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("6ceb9ca1-4f8d-43e0-8d57-0f8d0205801c"),
                    Name = "TeamLead",
                    NormalizedName = "TEAMLEAD"
                },

                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("6eb74c93-108c-4481-9662-cfa244899f46"),
                    Name = "DepartmentHead",
                    NormalizedName = "DEPARTMENTHEAD"
                },

                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("349ad8bb-5d46-43f4-b21c-aafc6db97a94"),
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },

                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("415a4bd3-44c1-4725-b66f-362ed30a7b9d"),
                    Name = "Intern",
                    NormalizedName = "INTERN"
                }
            };
            modelBuilder.Entity<IdentityRole<Guid>>().HasData(roles);

            // Role Guids
            Guid superAdminRoleId = new Guid("6E70C831-1F14-44E8-AC9E-F02825BD65D6");
            Guid hrManagerRoleId = Guid.Parse("376e5a0e-dd14-4394-8ab9-0d02889972c5");
            Guid hrStaffRoleId = Guid.Parse("ed02f158-8363-434f-9b2e-254c77c45c64");
            Guid hrAssistantRoleId = Guid.Parse("701747f8-ce70-4bf8-8b7c-e09c28d27cfe");
            Guid payrollOfficerRoleId = Guid.Parse("18db8396-7473-4f53-9b1e-1155e65047ff");
            Guid payrollAssistantRoleId = Guid.Parse("189f9797-aeec-481f-9154-4fc630590017");
            Guid leaveApproverRoleId = Guid.Parse("b7f60b18-dc58-4d0d-9d3e-d4b3c8ee9a3a");
            Guid leaveCoordinatorRoleId = Guid.Parse("34a71f5e-b93d-410b-a9b5-695bbad9c407");
            Guid projectManagerRoleId = Guid.Parse("337ca893-79ce-4d45-a1af-8d335f76cec7");
            Guid projectLeadRoleId = Guid.Parse("543737ed-030e-47b2-9808-881ee43d2568");
            Guid projectCoordinatorRoleId = Guid.Parse("a9e1d827-52d6-40d9-9f00-616e19212417");
            Guid teamLeadRoleId = Guid.Parse("6ceb9ca1-4f8d-43e0-8d57-0f8d0205801c");
            Guid departmentHeadRoleId = Guid.Parse("6eb74c93-108c-4481-9662-cfa244899f46");
            Guid employeeRoleId = Guid.Parse("349ad8bb-5d46-43f4-b21c-aafc6db97a94");
            Guid internRoleId = Guid.Parse("415a4bd3-44c1-4725-b66f-362ed30a7b9d");


            modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(

            //SuperAdmin Role Claims
            new IdentityRoleClaim<Guid> { Id = 1, RoleId = superAdminRoleId, ClaimType = "Attendance.View", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 2, RoleId = superAdminRoleId, ClaimType = "Attendance.ById", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 3, RoleId = superAdminRoleId, ClaimType = "Attendance.Create", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 4, RoleId = superAdminRoleId, ClaimType = "Attendance.Update", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 5, RoleId = superAdminRoleId, ClaimType = "Attendance.Delete", ClaimValue = "true" },

            new IdentityRoleClaim<Guid> { Id = 6, RoleId = superAdminRoleId, ClaimType = "Department.View", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 7, RoleId = superAdminRoleId, ClaimType = "Department.ById", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 8, RoleId = superAdminRoleId, ClaimType = "Department.Create", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 9, RoleId = superAdminRoleId, ClaimType = "Department.Update", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 10, RoleId = superAdminRoleId, ClaimType = "Department.Delete", ClaimValue = "true" },

            new IdentityRoleClaim<Guid> { Id = 11, RoleId = superAdminRoleId, ClaimType = "Employee.View", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 12, RoleId = superAdminRoleId, ClaimType = "Employee.ById", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 13, RoleId = superAdminRoleId, ClaimType = "Employee.Attendance", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 14, RoleId = superAdminRoleId, ClaimType = "Employee.LeaveRequest", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 15, RoleId = superAdminRoleId, ClaimType = "Employee.Payroll", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 16, RoleId = superAdminRoleId, ClaimType = "Employee.PerformanceReview", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 17, RoleId = superAdminRoleId, ClaimType = "Employee.PhoneNumbers", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 18, RoleId = superAdminRoleId, ClaimType = "Employee.ProjectAssignment", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 19, RoleId = superAdminRoleId, ClaimType = "Employee.Create", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 20, RoleId = superAdminRoleId, ClaimType = "Employee.Update", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 21, RoleId = superAdminRoleId, ClaimType = "Employee.Delete", ClaimValue = "true" },

            new IdentityRoleClaim<Guid> { Id = 22, RoleId = superAdminRoleId, ClaimType = "LeaveRequest.View", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 23, RoleId = superAdminRoleId, ClaimType = "LeaveRequest.ById", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 24, RoleId = superAdminRoleId, ClaimType = "LeaveRequest.Create", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 25, RoleId = superAdminRoleId, ClaimType = "LeaveRequest.Update", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 26, RoleId = superAdminRoleId, ClaimType = "LeaveRequest.Delete", ClaimValue = "true" },

            new IdentityRoleClaim<Guid> { Id = 27, RoleId = superAdminRoleId, ClaimType = "Payroll.View", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 28, RoleId = superAdminRoleId, ClaimType = "Payroll.ById", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 29, RoleId = superAdminRoleId, ClaimType = "Payroll.Create", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 30, RoleId = superAdminRoleId, ClaimType = "Payroll.Update", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 31, RoleId = superAdminRoleId, ClaimType = "Payroll.Delete", ClaimValue = "true" },

            new IdentityRoleClaim<Guid> { Id = 32, RoleId = superAdminRoleId, ClaimType = "PerformanceReview.View", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 33, RoleId = superAdminRoleId, ClaimType = "PerformanceReview.ById", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 34, RoleId = superAdminRoleId, ClaimType = "PerformanceReview.Create", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 35, RoleId = superAdminRoleId, ClaimType = "PerformanceReview.Update", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 36, RoleId = superAdminRoleId, ClaimType = "PerformanceReview.Delete", ClaimValue = "true" },

            new IdentityRoleClaim<Guid> { Id = 37, RoleId = superAdminRoleId, ClaimType = "PhoneNumber.View", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 38, RoleId = superAdminRoleId, ClaimType = "PhoneNumber.ById", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 39, RoleId = superAdminRoleId, ClaimType = "PhoneNumber.Create", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 40, RoleId = superAdminRoleId, ClaimType = "PhoneNumber.Update", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 41, RoleId = superAdminRoleId, ClaimType = "PhoneNumber.Delete", ClaimValue = "true" },

            new IdentityRoleClaim<Guid> { Id = 42, RoleId = superAdminRoleId, ClaimType = "ProjectAssignment.View", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 43, RoleId = superAdminRoleId, ClaimType = "ProjectAssignment.ById", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 44, RoleId = superAdminRoleId, ClaimType = "ProjectAssignment.Create", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 45, RoleId = superAdminRoleId, ClaimType = "ProjectAssignment.Update", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 46, RoleId = superAdminRoleId, ClaimType = "ProjectAssignment.Delete", ClaimValue = "true" },

            new IdentityRoleClaim<Guid> { Id = 47, RoleId = superAdminRoleId, ClaimType = "Project.View", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 48, RoleId = superAdminRoleId, ClaimType = "Project.ById", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 49, RoleId = superAdminRoleId, ClaimType = "Project.GetEmployees", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 50, RoleId = superAdminRoleId, ClaimType = "Project.Create", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 51, RoleId = superAdminRoleId, ClaimType = "Project.Update", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 52, RoleId = superAdminRoleId, ClaimType = "Project.Delete", ClaimValue = "true" },

            new IdentityRoleClaim<Guid> { Id = 53, RoleId = superAdminRoleId, ClaimType = "Role.View", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 54, RoleId = superAdminRoleId, ClaimType = "Role.ById", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 55, RoleId = superAdminRoleId, ClaimType = "Role.Create", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 56, RoleId = superAdminRoleId, ClaimType = "Role.Update", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 57, RoleId = superAdminRoleId, ClaimType = "Role.Delete", ClaimValue = "true" },

            // HRManager Role Claims
            new IdentityRoleClaim<Guid> { Id = 58, RoleId = hrManagerRoleId, ClaimType = "Employee.View", ClaimValue = "true" },                        
            new IdentityRoleClaim<Guid> { Id = 59, RoleId = hrManagerRoleId, ClaimType = "Department.View", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 60, RoleId = hrManagerRoleId, ClaimType = "LeaveRequest.Approve", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 61, RoleId = hrManagerRoleId, ClaimType = "LeaveRequest.ById", ClaimValue = "true" },

            // HRStaff Role Claims
            new IdentityRoleClaim<Guid> { Id = 62, RoleId = hrStaffRoleId, ClaimType = "Employee.View", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 63, RoleId = hrStaffRoleId, ClaimType = "LeaveRequest.View", ClaimValue = "true" },

            // HRAssistant Role Claims
            new IdentityRoleClaim<Guid> { Id = 64, RoleId = hrAssistantRoleId, ClaimType = "Employee.View", ClaimValue = "true" },

            // PayrollOfficer Role Claims
            new IdentityRoleClaim<Guid> { Id = 65, RoleId = payrollOfficerRoleId, ClaimType = "Payroll.View", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 66, RoleId = payrollOfficerRoleId, ClaimType = "Payroll.Update", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 67, RoleId = payrollOfficerRoleId, ClaimType = "Payroll.ById", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 68, RoleId = payrollOfficerRoleId, ClaimType = "Payroll.Create", ClaimValue = "true" },

            // PayrollAssistant Role Claims
            new IdentityRoleClaim<Guid> { Id = 69, RoleId = payrollAssistantRoleId, ClaimType = "Payroll.View", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 70, RoleId = payrollAssistantRoleId, ClaimType = "Payroll.ById", ClaimValue = "true" },

            // LeaveApprover Role Claims
            new IdentityRoleClaim<Guid> { Id = 71, RoleId = leaveApproverRoleId, ClaimType = "LeaveRequest.View", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 72, RoleId = leaveApproverRoleId, ClaimType = "LeaveRequest.Update", ClaimValue = "true" },

            // LeaveCoordinator Role Claims
            new IdentityRoleClaim<Guid> { Id = 73, RoleId = leaveCoordinatorRoleId, ClaimType = "LeaveRequest.View", ClaimValue = "true" },            

            // ProjectManager Role Claims
            new IdentityRoleClaim<Guid> { Id = 74, RoleId = projectManagerRoleId, ClaimType = "Project.View", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 75, RoleId = projectManagerRoleId, ClaimType = "Project.Create", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 76, RoleId = projectManagerRoleId, ClaimType = "Project.Update", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 77, RoleId = projectManagerRoleId, ClaimType = "ProjectAssignment.Create", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 78, RoleId = projectManagerRoleId, ClaimType = "ProjectAssignment.Update", ClaimValue = "true" },

            // ProjectLead Role Claims
            new IdentityRoleClaim<Guid> { Id = 79, RoleId = projectLeadRoleId, ClaimType = "ProjectAssignment.Create", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 80, RoleId = projectLeadRoleId, ClaimType = "ProjectAssignment.Update", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 81, RoleId = projectLeadRoleId, ClaimType = "Project.View", ClaimValue = "true" },            
            new IdentityRoleClaim<Guid> { Id = 82, RoleId = projectLeadRoleId, ClaimType = "Project.ById", ClaimValue = "true" },            

            // ProjectCoordinator Role Claims
            new IdentityRoleClaim<Guid> { Id = 83, RoleId = projectCoordinatorRoleId, ClaimType = "Project.View", ClaimValue = "true" },

            // TeamLead Role Claims
            new IdentityRoleClaim<Guid> { Id = 84, RoleId = teamLeadRoleId, ClaimType = "Project.ById", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 85, RoleId = teamLeadRoleId, ClaimType = "ProjectAssignment.View", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 86, RoleId = teamLeadRoleId, ClaimType = "PerformanceReview.View", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 87, RoleId = teamLeadRoleId, ClaimType = "PerformanceReview.Create", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 88, RoleId = teamLeadRoleId, ClaimType = "PerformanceReview.Update", ClaimValue = "true" },

            // DepartmentHead Role Claims
            new IdentityRoleClaim<Guid> { Id = 89, RoleId = departmentHeadRoleId, ClaimType = "Department.View", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 90, RoleId = departmentHeadRoleId, ClaimType = "PerformanceReview.View", ClaimValue = "true" },

            // Employee Role Claims
            new IdentityRoleClaim<Guid> { Id = 91, RoleId = employeeRoleId, ClaimType = "Employee.View", ClaimValue = "true" },            
            new IdentityRoleClaim<Guid> { Id = 92, RoleId = employeeRoleId, ClaimType = "Employee.Attendance", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 93, RoleId = employeeRoleId, ClaimType = "Employee.LeaveRequest", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 94, RoleId = employeeRoleId, ClaimType = "Employee.Payroll", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 95, RoleId = employeeRoleId, ClaimType = "Employee.PerformanceReview", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 96, RoleId = employeeRoleId, ClaimType = "Employee.PhoneNumbers", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 97, RoleId = employeeRoleId, ClaimType = "Employee.ProjectAssignment", ClaimValue = "true" },
            new IdentityRoleClaim<Guid> { Id = 98, RoleId = employeeRoleId, ClaimType = "LeaveRequest.Create", ClaimValue = "true" },

            // Intern Role Claims
            new IdentityRoleClaim<Guid> { Id = 99, RoleId = internRoleId, ClaimType = "Employee.View", ClaimValue = "true" },

            new IdentityRoleClaim<Guid> { Id = 100, RoleId = superAdminRoleId, ClaimType = "Account.Register", ClaimValue = "true" }
            );
            
            //Relationships

            // Employee - Department (many-to-one)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentUID)
                .OnDelete(DeleteBehavior.Restrict);

            // Employee - Role (many-to-one)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Role)
                .WithMany(r => r.Employees)
                .HasForeignKey(e => e.RoleUID)
                .OnDelete(DeleteBehavior.Restrict);

            // Employee - AppUser (one-to-one)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.AppUser)
                .WithOne()
                .HasForeignKey<Employee>(e => e.AppUserId)
                .OnDelete(DeleteBehavior.SetNull);

            // PhoneNumber - Employee (many-to-one)
            modelBuilder.Entity<PhoneNumber>()
                .HasOne(p => p.Employee)
                .WithMany(e => e.PhoneNumbers)
                .HasForeignKey(p => p.EmployeeUID)
                .OnDelete(DeleteBehavior.Cascade);

            // Attendance - Employee
            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Attendances)
                .HasForeignKey(a => a.EmployeeUID)
                .OnDelete(DeleteBehavior.Cascade);

            // LeaveRequest - Employee
            modelBuilder.Entity<LeaveRequest>()
                .HasOne(l => l.Employee)
                .WithMany(e => e.LeaveRequests)
                .HasForeignKey(l => l.EmployeeUID)
                .OnDelete(DeleteBehavior.Cascade);

            // Payroll - Employee
            modelBuilder.Entity<Payroll>()
                .HasOne(p => p.Employee)
                .WithMany(e => e.Payrolls)
                .HasForeignKey(p => p.EmployeeUID)
                .OnDelete(DeleteBehavior.Cascade);

            // PerformanceReview - Employee
            modelBuilder.Entity<PerformanceReview>()
                .HasOne(pr => pr.Employee)
                .WithMany(e => e.PerformanceReviews)
                .HasForeignKey(pr => pr.EmployeeUID)
                .OnDelete(DeleteBehavior.Cascade);

            // ProjectAssignment - Employee
            modelBuilder.Entity<ProjectAssignment>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.ProjectAssignments)
                .HasForeignKey(a => a.EmployeeUID)
                .OnDelete(DeleteBehavior.Cascade);

            // ProjectAssignment - Project
            modelBuilder.Entity<ProjectAssignment>()
                .HasOne(a => a.Project)
                .WithMany(p => p.EmployeeAssignments)
                .HasForeignKey(a => a.ProjectUID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Department>()
                .HasIndex(d => d.DepartmentPub_ID)
                .IsUnique();
        }

    }
}
