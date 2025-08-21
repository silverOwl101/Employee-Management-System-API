using Employee_Management_System_API.Authorization;
using Employee_Management_System_API.Data;
using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Global_Error;
using Employee_Management_System_API.Interfaces.Repositories;
using Employee_Management_System_API.Interfaces.Services;
using Employee_Management_System_API.Middleware;
using Employee_Management_System_API.Repositories;
using Employee_Management_System_API.Seeders;
using Employee_Management_System_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Net;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Employee_Management_System_API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            if (!builder.Environment.IsEnvironment("Test"))
                await LoadProductionEnvironment(builder);

            await TestEnvironment(builder);
        }
        private static async Task LoadProductionEnvironment(WebApplicationBuilder builder)
        {
            //Change allowedOrigins variable to AllowedProductionOrigins for Production Deployment.
            //Change allowedOrigins variable to AllowedDevelopmentOrigins for Development.
            var allowedOrigins = builder.Configuration.GetSection("AllowedDevelopmentOrigins").Get<string[]>();
            // Add services to the container.

            builder.Services.AddControllers(options =>
            {

            }).AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("DevCorsPolicy", policy =>
                {
                    policy.WithOrigins(allowedOrigins!)
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                });
            });

            //For 400 global model validation
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errorsMessage = context.ModelState.Where(ms => ms.Value?.Errors.Count > 0)
                    .ToDictionary(
                        kvp =>
                        {
                            var parameterType = context.ActionDescriptor.Parameters
                                                .FirstOrDefault()?.ParameterType;

                            var propInfo = parameterType?.GetProperty(kvp.Key);
                            var displayAttr = propInfo?.GetCustomAttribute<DisplayNameAttribute>();
                            return displayAttr?.DisplayName ?? kvp.Key;
                        },
                        kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                    var response = new ValidationErrorDetails
                    {
                        Status = 400,
                        Error = "BadRequest",
                        Messages = errorsMessage,
                        Path = context.HttpContext.Request.Path,
                        TraceId = context.HttpContext.TraceIdentifier
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSwaggerGen(option =>
            {
                option.CustomSchemaIds(type => type.Name);

                // This is for adding xml comments in every end points
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                option.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 12;
            }).AddEntityFrameworkStores<ApplicationDBContext>()
              .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme =
                options.DefaultScheme =
                options.DefaultSignInScheme =
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey
                                (System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]!))
                };

                // For 401 and 403 JWT Global error
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsJsonAsync(new ErrorDetail
                        {
                            Status = 401,
                            Error = "Unauthorized",
                            Message = "Authentication is required.",
                            Path = context.HttpContext.Request.Path,
                            TraceId = context.HttpContext.TraceIdentifier
                        });
                    },

                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsJsonAsync(new ErrorDetail
                        {
                            Status = 403,
                            Error = "Forbidden",
                            Message = "You are not allowed to access this resource.",
                            Path = context.HttpContext.Request.Path,
                            TraceId = context.HttpContext.TraceIdentifier
                        });
                    },

                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("___at"))
                        {
                            context.Token = context.Request.Cookies["___at"];
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                AuthorizationPolicies.SystemPolicies(options!);
            });

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
            builder.Services.AddScoped<IPhoneNumberService, PhoneNumberService>();
            builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            builder.Services.AddScoped<IAttendanceService, AttendanceService>();
            builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
            builder.Services.AddScoped<IPayrollRepository, PayrollRepository>();
            builder.Services.AddScoped<IPayrollService, PayrollService>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IProjectAssignmentRepository, ProjectAssignmentRepository>();
            builder.Services.AddScoped<IProjectAssignmentService, ProjectAssignmentService>();
            builder.Services.AddScoped<IPerformanceReviewRepository, PerformanceReviewRepository>();
            builder.Services.AddScoped<IPerformanceReviewService, PerformanceReviewService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserAuthenticationService, AccountService>();
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            var app = builder.Build();

            #region Seedings
            //Seeding the sa (Super Admin)
            await DbSeeder.SeedSuperAdmin(app.Services);

            //using var scope = app.Services.CreateScope();
            //var context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
            //await DbSeeder.SeedProjectAssignmentInfo(context);
            //await DbSeeder.SeedPerformanceReviewInfo(context);
            //await DbSeeder.SeedLeaveRequestInfo(context);
            //await DbSeeder.SeedPhoneNumberInfo(context);
            //await DbSeeder.SeedFakeAttendanceInfo(context);
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.InjectJavascript("/swagger-custom.js");
                });
            }

            app.UseHttpsRedirection();

            //For 404 global model validation
            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;

                if (response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    response.ContentType = "application/json";
                    await response.WriteAsJsonAsync(new ErrorDetail
                    {
                        Status = 404,
                        Error = "NotFound",
                        Message = "The requested resource was not found.",
                        Path = context.HttpContext.Request.Path,
                        TraceId = context.HttpContext.TraceIdentifier
                    });
                }
            });

            app.UseCors("DevCorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();

            app.MapControllers();

            await app.RunAsync();

        }
        private static async Task TestEnvironment(WebApplicationBuilder builder)
        {
            //Change allowedOrigins variable to AllowedProductionOrigins for Production Deployment.
            //Change allowedOrigins variable to AllowedDevelopmentOrigins for Development.
            var allowedOrigins = builder.Configuration.GetSection("AllowedDevelopmentOrigins").Get<string[]>();
            // Add services to the container.

            builder.Services.AddControllers(options =>
            {

            }).AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("DevCorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            //For 400 global model validation
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errorsMessage = context.ModelState.Where(ms => ms.Value?.Errors.Count > 0)
                    .ToDictionary(
                        kvp =>
                        {
                            var parameterType = context.ActionDescriptor.Parameters
                                                .FirstOrDefault()?.ParameterType;

                            var propInfo = parameterType?.GetProperty(kvp.Key);
                            var displayAttr = propInfo?.GetCustomAttribute<DisplayNameAttribute>();
                            return displayAttr?.DisplayName ?? kvp.Key;
                        },
                        kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                    var response = new ValidationErrorDetails
                    {
                        Status = 400,
                        Error = "BadRequest",
                        Messages = errorsMessage,
                        Path = context.HttpContext.Request.Path,
                        TraceId = context.HttpContext.TraceIdentifier
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSwaggerGen(option =>
            {
                option.CustomSchemaIds(type => type.Name);

                // This is for adding xml comments in every end points
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                option.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 12;
            }).AddEntityFrameworkStores<ApplicationDBContext>()
              .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme =
                options.DefaultScheme =
                options.DefaultSignInScheme =
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey
                                (System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]!))
                };

                // For 401 and 403 JWT Global error
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsJsonAsync(new ErrorDetail
                        {
                            Status = 401,
                            Error = "Unauthorized",
                            Message = "Authentication is required.",
                            Path = context.HttpContext.Request.Path,
                            TraceId = context.HttpContext.TraceIdentifier
                        });
                    },

                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsJsonAsync(new ErrorDetail
                        {
                            Status = 403,
                            Error = "Forbidden",
                            Message = "You are not allowed to access this resource.",
                            Path = context.HttpContext.Request.Path,
                            TraceId = context.HttpContext.TraceIdentifier
                        });
                    },

                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("___at"))
                        {
                            context.Token = context.Request.Cookies["___at"];
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                AuthorizationPolicies.SystemPolicies(options!);
            });

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
            builder.Services.AddScoped<IPhoneNumberService, PhoneNumberService>();
            builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            builder.Services.AddScoped<IAttendanceService, AttendanceService>();
            builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
            builder.Services.AddScoped<IPayrollRepository, PayrollRepository>();
            builder.Services.AddScoped<IPayrollService, PayrollService>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IProjectAssignmentRepository, ProjectAssignmentRepository>();
            builder.Services.AddScoped<IProjectAssignmentService, ProjectAssignmentService>();
            builder.Services.AddScoped<IPerformanceReviewRepository, PerformanceReviewRepository>();
            builder.Services.AddScoped<IPerformanceReviewService, PerformanceReviewService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserAuthenticationService, AccountService>();
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            var app = builder.Build();

            #region Seedings
            //Seeding the sa (Super Admin)
            await DbSeeder.SeedSuperAdmin(app.Services);

            //using var scope = app.Services.CreateScope();
            //var context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
            //await DbSeeder.SeedProjectAssignmentInfo(context);
            //await DbSeeder.SeedPerformanceReviewInfo(context);
            //await DbSeeder.SeedLeaveRequestInfo(context);
            //await DbSeeder.SeedPhoneNumberInfo(context);
            //await DbSeeder.SeedFakeAttendanceInfo(context);
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.InjectJavascript("/swagger-custom.js");
                });
            }

            app.UseHttpsRedirection();

            //For 404 global model validation
            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;

                if (response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    response.ContentType = "application/json";
                    await response.WriteAsJsonAsync(new ErrorDetail
                    {
                        Status = 404,
                        Error = "NotFound",
                        Message = "The requested resource was not found.",
                        Path = context.HttpContext.Request.Path,
                        TraceId = context.HttpContext.TraceIdentifier
                    });
                }
            });

            app.UseCors("DevCorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}
public partial class Program { }
