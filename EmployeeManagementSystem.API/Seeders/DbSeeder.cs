using Bogus;
using Employee_Management_System_API.Data;
using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.Helpers;
using Microsoft.AspNetCore.Identity;
using static Employee_Management_System_API.Domain.Enums.Categories;

namespace Employee_Management_System_API.Seeders
{
    public static class DbSeeder
    {
        public static async Task SeedSuperAdmin(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            var config = services.GetRequiredService<IConfiguration>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            var id = config["SeedSuperAdmin:SuperAdminGuid"];
            var userName = config["SeedSuperAdmin:SuperAdminUsername"];
            var password = config["SeedSuperAdmin:SuperAdminPassword"];
            var email = config["SeedSuperAdmin:SuperAdminEmail"];

            if (string.IsNullOrWhiteSpace(id) ||
                string.IsNullOrWhiteSpace(userName) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("SuperAdmin seeding skipped: Super admin credentials not found in configuration!");
                return;
            }


            var existing = await userManager.FindByNameAsync(userName);
            if (existing != null)
            {
                Console.WriteLine("SuperAdmin already exists.");
                return;
            }

            var superAdmin = new AppUser
            {
                Id = Guid.Parse(id),
                UserName = userName,
                Email = email
            };

            var result = await userManager.CreateAsync(superAdmin, password);

            if (!result.Succeeded)
            {
                Console.WriteLine("Failed to create SuperAdmin:");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description);
                }
                return;
            }

            await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");

            Console.WriteLine("SuperAdmin seeded successfully.");
        }
        public static async Task SeedFakeAttendanceInfo(ApplicationDBContext context)
        {
            var employeeFaker = new Faker<Attendance>()
                    .RuleFor(a => a.AttendancePub_ID, GeneratorHelpers.GenerateID)
                    .RuleFor(a => a.Date, f => DateOnly.FromDateTime(f.Date.Recent(30)))
                    .RuleFor(a => a.CheckInTime, f => f.Date.BetweenTimeOnly(TimeOnly.Parse("08:00"), TimeOnly.Parse("10:00")).ToTimeSpan())
                    .RuleFor(a => a.CheckOutTime, f => f.Date.BetweenTimeOnly(TimeOnly.Parse("16:00"), TimeOnly.Parse("18:00")).ToTimeSpan())
                    .RuleFor(a => a.Status, f => f.PickRandom<AttendanceStatus>())
                    .RuleFor(a => a.EmployeeUID, Guid.Parse("ACB9433E-F36B-1410-85CB-0084382A06A5"));

            var fakeEmployees = employeeFaker.Generate(100);
            await context.Attendances.AddRangeAsync(fakeEmployees);
            await context.SaveChangesAsync();
        }
        public static async Task SeedLeaveRequestInfo(ApplicationDBContext context)
        {
            var employeeFaker = new Faker<LeaveRequest>()
                    .RuleFor(lr => lr.LeavePub_ID, GeneratorHelpers.GenerateID)
                    .RuleFor(lr => lr.StartDate, f => DateOnly.FromDateTime(f.Date.Recent(60)))
                    .RuleFor(lr => lr.EndDate, (f, lr) =>
                    {
                        var start = lr.StartDate.ToDateTime(TimeOnly.MinValue);
                        var end = f.Date.Between(start, start.AddDays(5));
                        return DateOnly.FromDateTime(end);
                    })
                    .RuleFor(lr => lr.LeaveType, f => f.PickRandom<LeaveType>())
                    .RuleFor(lr => lr.Status, f => f.PickRandom<LeaveStatus>())
                    .RuleFor(lr => lr.Reason, f => f.Lorem.Sentence())
                    .RuleFor(lr => lr.EmployeeUID, Guid.Parse("ACB9433E-F36B-1410-85CB-0084382A06A5")); // Link to existing employee

            var fakeEmployees = employeeFaker.Generate(100);
            await context.LeaveRequests.AddRangeAsync(fakeEmployees);
            await context.SaveChangesAsync();
        }
        public static async Task SeedPayrollInfo(ApplicationDBContext context)
        {
            var employeeFaker = new Faker<Payroll>()
                    .RuleFor(lr => lr.PayrollPub_ID, GeneratorHelpers.GenerateID)
                    .RuleFor(lr => lr.PayDate, f => DateOnly.FromDateTime(f.Date.Recent(60)))
                    .RuleFor(p => p.BasicSalary, f => f.Finance.Amount(20000, 80000, 2))
                    .RuleFor(p => p.Allowances, (f, p) => f.Finance.Amount(1000, 5000, 2))
                    .RuleFor(p => p.Deductions, (f, p) => f.Finance.Amount(500, 3000, 2))
                    .RuleFor(p => p.NetSalary, (f, p) => Math.Round(p.BasicSalary + p.Allowances - p.Deductions, 2))
                    .RuleFor(lr => lr.EmployeeUID, Guid.Parse("ACB9433E-F36B-1410-85CB-0084382A06A5")); // Link to existing employee
            var fakeEmployees = employeeFaker.Generate(100);
            await context.Payrolls.AddRangeAsync(fakeEmployees);
            await context.SaveChangesAsync();
        }
        public static async Task SeedProjectAssignmentInfo(ApplicationDBContext context)
        {
            var employeeFaker = new Faker<ProjectAssignment>()
                    .RuleFor(pa => pa.AssignmentPub_ID, GeneratorHelpers.GenerateID)
                    .RuleFor(pa => pa.RoleInProject, f => f.Name.JobTitle()) // Generates roles like "Software Engineer"
                    .RuleFor(pa => pa.AssignedDate, f => DateOnly.FromDateTime(f.Date.Recent(90)))
                    .RuleFor(pa => pa.ProjectUID, Guid.Parse("98D9423E-F36B-1410-85C1-0084382A06A5"))
                    .RuleFor(pa => pa.EmployeeUID, Guid.Parse("ACB9433E-F36B-1410-85CB-0084382A06A5")); // replace with real UID or randomize            
            var fakeEmployees = employeeFaker.Generate(100);
            await context.EmployeeProjectAssignments.AddRangeAsync(fakeEmployees);
            await context.SaveChangesAsync();
        }
        public static async Task SeedPerformanceReviewInfo(ApplicationDBContext context)
        {
            var employeeFaker = new Faker<PerformanceReview>()
                    .RuleFor(pa => pa.ReviewPub_ID, GeneratorHelpers.GenerateID)
                    .RuleFor(pr => pr.ReviewDate, f => DateOnly.FromDateTime(f.Date.Recent(90)))
                    .RuleFor(pr => pr.Score, f => f.Random.Int(1, 10))
                    .RuleFor(pr => pr.Comments, f => f.Lorem.Sentence())
                    .RuleFor(pr => pr.EmployeeUID, Guid.Parse("ACB9433E-F36B-1410-85CB-0084382A06A5")); // Or randomize from real UIDs

            var fakeEmployees = employeeFaker.Generate(100);
            await context.PerformanceReviews.AddRangeAsync(fakeEmployees);
            await context.SaveChangesAsync();
        }
        public static async Task SeedPhoneNumberInfo(ApplicationDBContext context)
        {
            var employeeFaker = new Faker<PhoneNumber>()
                    .RuleFor(pa => pa.PhoneNumberPub_ID, GeneratorHelpers.GenerateID)
                    .RuleFor(p => p.PhoneNumberValue, f => f.Phone.PhoneNumber("09#########"))
                    .RuleFor(pa => pa.EmployeeUID, Guid.Parse("ACB9433E-F36B-1410-85CB-0084382A06A5")); // replace with real UID or randomize

            var fakeEmployees = employeeFaker.Generate(3);
            await context.PhoneNumbers.AddRangeAsync(fakeEmployees);
            await context.SaveChangesAsync();
        }
    }
}
