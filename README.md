# Employee Management System API

A .NET Core Web API for managing employees, attendance, payroll, departments, and more.

---

## Table of Contents

* [Introduction](#introduction)
* [Technology Stack](#technologies-used)
* [Key Features](#key-features)
* [Getting Started](#getting-started)
* [Project Structure](#project-structure) 
* [Authorization (RBAC)](#authorization-rbac)
* [Authentication (JWT)](#authentication-jwt)
* [CORS Configuration](#cors-configuration)
* [API Endpoints](#api-endpoints)
* [.http Files for Testing](#http-files-for-testing)
* [Skills Demonstrated](#skills-demonstrated)
* [License](#license)

---

<h2 id="introduction">📖 Introduction</h2>

This project provides a fully featured REST API for managing employee-related operations in a business setting, including user authentication, CRUD operations, attendance logging, leave requests, and payroll processing.

---

<h2 id="technologies-used">🔧 Technology Stack</h2>

| Layer                 | Technology                                |
| ------------------    | ----------------------------------------- |
| **API**               | ASP.NET Core Web API (Repository Pattern) |
| **ORM**               | Entity Framework Core (Code First)        |
| **Authentication**    | [JWT](https://www.jwt.io/) (JSON Web Tokens)|
| **Authorization**     | Role-Based Access Control (RBAC)          |
| **Database**          | MSSQL (via Docker container)                              |
| **Development Env**   | [Docker](https://www.docker.com/)(for containerized MSSQL Server)|
| **Documentation**     | [Swagger](https://swagger.io/) / [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore?tab=readme-ov-file)|
| **Test Data Seeding** | [Bogus for .NET](https://github.com/bchavez/Bogus) (C# fake data generator)|

---

<h2 id="key-features">🔑 Key Features</h2>

| Feature               | Description                                                                 |
| --------------------- | --------------------------------------------------------------------------- |
| User Management       | Register, update, and delete users with role-based access control           |
| Attendance System     | Clock-in/clock-out functionality with employee attendance tracking          |
| Payroll               | Automated salary computation with support for deductions and bonuses        |
| Authentication        | Secure JWT-based authentication stored in HttpOnly cookies with claims and policy validation |
| Leave Management      | Submit, track, and manage employee leave requests                           |
| Project Assignment    | Assign employees to projects and monitor participation and progress         |


---

<h2 id="getting-started">🚀 Getting Started</h2>

To use this API, follow the steps below:


### 1. Clone or Fork the Repository

You can clone the repository or [fork](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/working-with-forks/fork-a-repo) it if you want to contribute:

```bash
git clone https://github.com/silverOwl101/Employee-Management-System-API.git
````

### 2. Build the Solution

After cloning, open the solution file in Visual Studio and build the project.

### 3. Install Docker Desktop

Download and install [Docker Desktop](https://www.docker.com/products/docker-desktop) for your operating system.

### 4. Run Microsoft SQL Server via Docker

1. Make sure Docker Desktop is installed and running.

2. Open PowerShell or your preferred terminal.

3. Run the following command:

      ```bash
      docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong!Pass123" -p 1433:1433 --name mssql -d mcr.microsoft.com/mssql/server:2022-latest
      ```

### 5. Set Up User Secrets

Follow the [official Microsoft guide](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-9.0&tabs=windows) for detailed instructions.

1. Open the solution in Visual Studio.

2. Open Developer PowerShell or your preferred terminal.

3. Run the following commands, replacing variables with your actual values:

      ```bash
      # Assign your values here
      $superAdminGuid = "YOUR_GUID_HERE"
      $superAdminUsername = "sa"
      $superAdminEmail = "your_email@example.com"
      $superAdminPassword = "YOUR_STRONG_PASSWORD"
      $DataBasePassword = "YOUR_STRONG_DATABASE_PASSWORD"
      $databaseName = "YOUR_DATABASE_NAME"
      $jwtSigningKey = "YOUR_JWT:SigningKey"

      # Clear and initialize user secrets
      dotnet user-secrets clear
      dotnet user-secrets init

      # Set user secrets
      dotnet user-secrets set "SeedSuperAdmin:SuperAdminGuid" $superAdminGuid
      dotnet user-secrets set "SeedSuperAdmin:SuperAdminUsername" $superAdminUsername
      dotnet user-secrets set "SeedSuperAdmin:SuperAdminEmail" $superAdminEmail
      dotnet user-secrets set "SeedSuperAdmin:SuperAdminPassword" $superAdminPassword

      # Connection string (adjust if your server differs)
      $connectionString = "Server=localhost,1433;Database=$databaseName;User Id=sa;Password=$DataBasePassword;Integrated Security=False;TrustServerCertificate=True;"
      dotnet user-secrets set "ConnectionStrings:default" $connectionString

      # JWT Signing Key
      dotnet user-secrets set "JWT:SigningKey" $jwtSigningKey
      ```

### 6. Set Up Entity Framework Core Migrations

Follow the [EF Core migration guide](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli).

1. Open the **Package Manager Console**:
   `Tools → NuGet Package Manager → Package Manager Console`

2. Create the initial migration:

      ```bash
      Add-Migration InitialCreate
      ```
3. Review the generated migration file to ensure it matches your `ApplicationDbContext`.

4. Apply the migration to create the database:

      ```bash
      Update-Database
      ```

### 7. Enable Super Admin Seeding

To seed the default `sa` (Super Admin) account, open `Program.cs` and **locate the following line of code**:

```csharp
await DbSeeder.SeedSuperAdmin(app.Services);
```

You can quickly find it by pressing **`Ctrl + F`** in Visual Studio 2022 and searching for `DbSeeder.SeedSuperAdmin`. Then, uncomment the code to seed the Super Admin.

📝 **Note:** Once the Super Admin is created, you may comment this line again. Leaving it uncommented is safe, as the code prevents duplicate creation.

### 8. Build and Run the API

Set the `Employee_Management_System_API` project as the startup project, then build and run it.

### 9. Verify Swagger Launch

If the Swagger UI appears after the project launches, your API setup is complete.

> **🎉 Congratulations!** You can now use the API for your project.

---

<h2 id="project-structure">📂 Project Structure</h2>

```bash
Employee Management System API/
├── EmployeeManagementSystem.API/
└── EmployeeManagementSystem.Test/
```
---

<h2 id="authorization-rbac">🛂 Authorization (Role-Based Access Control - RBAC)</h2>

This API uses **Role-Based Access Control (RBAC)** with **ASP.NET Core Authorization Policies**. The policies are centrally defined in: `EmployeeManagementSystem.API\Authorization\AuthorizationPolicies.cs`

### `AuthorizationPolicies.cs` Overview

This file centralizes **authorization policy definitions** for the API. Each policy maps to a specific claim that determines whether a user is permitted to perform certain actions (e.g., view, create, update, or delete resources).

Example excerpt:

```csharp
namespace EmployeeManagementSystem.API.Authorization
{
    public static class AuthorizationPolicies
    {
        public static void Register(AuthorizationOptions options)
        {
            // Employee policies
            options.AddPolicy("Employee.View", policy =>
                policy.RequireClaim("Employee.View", "true"));

            options.AddPolicy("Employee.Create", policy =>
                policy.RequireClaim("Employee.Create", "true"));

            options.AddPolicy("Employee.Update", policy =>
                policy.RequireClaim("Employee.Update", "true"));

            options.AddPolicy("Employee.Delete", policy =>
                policy.RequireClaim("Employee.Delete", "true"));

            // Department policies
            options.AddPolicy("Department.View", policy =>
                policy.RequireClaim("Department.View", "true"));
            
            // ... other resources
        }
    }
}
```

Each policy enforces **fine-grained access control**, ensuring that only users with the required claims can access protected endpoints.

###  Policy Registration

In `Program.cs`, the policies from `AuthorizationPolicies.cs` are registered like this:

```csharp
builder.Services.AddAuthorization(options =>
{
    AuthorizationPolicies.SystemPolicies(options);
});
```

Each endpoint requires a specific claim with value `"true"` in the user’s token.

### Policy Format

```
<Resource>.<Action>
```

Example:

* **`Employee.View`** – Grants permission to view all employee records.
* **`Employee.Create`** – Grants permission to create new employee records.

### Example Usage

```csharp
[Authorize(Policy = "Employee.Create")]
[HttpPost]
public async Task<IActionResult> CreateEmployee(EmployeeDto dto)
{
    ...
}
```

### 📋 Available Policies

| **Resource**          | **View** | **ById** | **Create** | **Update** | **Delete** | **Other Actions**                                                                     |
| --------------------- | -------- | -------- | ---------- | ---------- | ---------- | ------------------------------------------------------------------------------------- |
| **Account**           | –        | –        | ✅ Register | –          | –          | –                                                                                     |
| **Attendance**        | ✅ View   | ✅ ById   | ✅ Create   | ✅ Update   | ✅ Delete   | –                                                                                     |
| **Department**        | ✅ View   | ✅ ById   | ✅ Create   | ✅ Update   | ✅ Delete   | –                                                                                     |
| **Employee**          | ✅ View   | ✅ ById   | ✅ Create   | ✅ Update   | ✅ Delete   | Attendance, LeaveRequest, Payroll, PerformanceReview, PhoneNumbers, ProjectAssignment |
| **LeaveRequest**      | ✅ View   | ✅ ById   | ✅ Create   | ✅ Update   | ✅ Delete   | –                                                                                     |
| **Payroll**           | ✅ View   | ✅ ById   | ✅ Create   | ✅ Update   | ✅ Delete   | –                                                                                     |
| **PerformanceReview** | ✅ View   | ✅ ById   | ✅ Create   | ✅ Update   | ✅ Delete   | –                                                                                     |
| **PhoneNumber**       | ✅ View   | ✅ ById   | ✅ Create   | ✅ Update   | ✅ Delete   | –                                                                                     |
| **ProjectAssignment** | ✅ View   | ✅ ById   | ✅ Create   | ✅ Update   | ✅ Delete   | –                                                                                     |
| **Project**           | ✅ View   | ✅ ById   | ✅ Create   | ✅ Update   | ✅ Delete   | GetEmployees                                                                          |
| **Role**              | ✅ View   | ✅ ById   | ✅ Create   | ✅ Update   | ✅ Delete   | –                                                                                     |

---

<h2 id="authentication-jwt">🔐 Authentication (JSON Web Tokens - JWT)</h2>

This API uses **JSON Web Tokens (JWT)** with **HttpOnly cookies** for authentication and session management.  

### How It Works
1. **Login (`/api/Account/Login`)**  
   - User provides `username` and `password`.  
   - If valid, the API generates:
     - **Access Token** (`___at`) → short-lived (15 minutes).  
     - **Refresh Token** (`___rt`) → longer-lived (7 days).  
   - Both tokens are stored securely in **HttpOnly cookies**.  

2. **Accessing Protected Endpoints**  
   - Each request automatically includes the `___at` cookie.  
   - The API validates the JWT using `JwtBearer` authentication.  

3. **Token Refresh (`/api/Account/Refresh-Token`)**  
   - When the access token expires, the client can call this endpoint.  
   - The server validates the refresh token and issues a new access + refresh token pair.  

4. **Logout (`/api/Account/Logout`)**  
   - Refresh token is revoked.  
   - Cookies (`___at` and `___rt`) are cleared.  


### JWT Configuration

Configured in `Program.cs`:

```csharp
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]!))
    };
});
```

### Example Requests

**Login**
```http
POST /api/Account/Login
Content-Type: application/json

{
  "userName": "johndoe",
  "password": "P@ssw0rd!"
}
```
Response includes:
* `___at` cookie (access token, 15 minutes).
* `___rt` cookie (refresh token, 7 days).

**Refresh Token**
```http
POST /api/Account/Refresh-Token
Content-Type: application/json

{
  "refreshToken": "<old-refresh-token>",
  "employeeId": "[EMPLOYEE PUBLIC ID HERE]"
}
```

**Logout**
```http
POST /api/Account/Logout
Content-Type: application/json

{
  "refreshToken": "<refresh-token>"
}
```

### Security Notes

* **HttpOnly cookies** prevent JavaScript access (mitigating XSS).  
* **Short-lived access tokens** (15 minutes) reduce the risk if compromised.  
* **Refresh tokens** (7 days) are hashed and stored in the database for added safety.  
* **Claims included in JWT**: `email`, `username`, `employeeId`, `departmentId`, `roleId`, and `role claims`.  

---

<h2 id="cors-configuration">🌐 Cross-Origin Resource Sharing (CORS) Configuration</h2>

This project uses **CORS policies** to restrict which front-end applications can call the API.

In `Program.cs`:

```csharp
// Use different origins depending on the environment
// Production → AllowedProductionOrigins
// Development → AllowedDevelopmentOrigins

var allowedOrigins = builder.Configuration
    .GetSection("AllowedDevelopmentOrigins")
    .Get<string[]>();

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
```

### AppSettings Setup

Create two configuration files in your project root:

**`appsettings.Development.json`**
```json
{
  "AllowedDevelopmentOrigins": [
    "https://localhost:7235"
  ]
}
```

**`appsettings.Production.json`**
```json
{
  "AllowedProductionOrigins": [
    "https://your-production-frontend.com"
  ]
}
```

- Add your frontend production URL(s) inside `AllowedProductionOrigins`.  
- For local development, the default origin is `https://localhost:7235`.  

### Summary

- Local development uses `AllowedDevelopmentOrigins`.  
- Production uses `AllowedProductionOrigins`.  
- This ensures your API only accepts requests from **trusted domains**.

---

<h2 id="api-endpoints">📫 API Endpoints</h2>

### These are the following API endpoints:

### Account Controller
| Endpoint                          | Description          |
| --------------------------------- | -------------------- |
| `POST /api/Account/Register`      | Register new account |
| `POST /api/Account/Login`         | Log-in account       |
| `POST /api/Account/Refresh-Token` | Token refresh        |
| `POST /api/Account/Logout`        | Log-out account      |

### Attendance Controller
| Endpoint                      | Description                                      |
| ----------------------------- | ------------------------------------------------ |
| `GET /api/Attendance`         | Get all attendance records                       |
| `POST /api/Attendance`        | Create a new attendance record                   |
| `GET /api/Attendance/{id}`    | Get attendance record using attendance public id |
| `PUT /api/Attendance/{id}`    | Update an attendance record                      |
| `DELETE /api/Attendance/{id}` | Delete an attendance record                      |

### Department Controller
| Endpoint                      | Description                                      |
| ----------------------------- | ------------------------------------------------ |
| `GET /api/Department`         | Get all department records                       |
| `POST /api/Department`        | Create new department record                     |
| `GET /api/Department/{id}`    | Get department record using department public id |
| `PUT /api/Department/{id}`    | Update a department record                       |
| `DELETE /api/Department/{id}` | Delete a department record                       |

### Employee Controller
| Endpoint                       | Description                                              |
| ------------------------------ | -------------------------------------------------------- |
| `GET /api/Employee`            | Get all employee records                                 |
| `POST /api/Employee`           | Create new employee record                               |
| `GET /api/Employee/{id}`       | Get the employee record details using employee public id |
| `PUT /api/Employee/{id}`       | Update an employee record                                |
| `DELETE /api/Employee/{id}`    | Delete an employee record                                |
| `GET /api/Employee/attendance` | Get the attendance of the employee                       |
| `GET /api/Employee/leave`      | Get the leave request of the employee                    |
| `GET /api/Employee/payroll`    | Get the payroll of the employee                          |
| `GET /api/Employee/appraisal`  | Get the performance review of the employee               |
| `GET /api/Employee/contact`    | Get the contact number of the employee                   |
| `GET /api/Employee/task`       | Get the project assignment of the employee               |

### LeaveRequest Controller
| Endpoint                   | Description                                            |
| -------------------------- | ------------------------------------------------------ |
| `GET /api/Timeoff`         | Get all leave request records                          |
| `POST /api/Timeoff`        | Create new leave request record                        |
| `GET /api/Timeoff/{id}`    | Get leave request record using leave request public id |
| `PUT /api/Timeoff/{id}`    | Update a leave request record                          |
| `DELETE /api/Timeoff/{id}` | Delete a leave request record                          |

### Payroll Controller
| Endpoint                   | Description                                 |
| -------------------------- | ------------------------------------------- |
| `GET /api/Payroll`         | Get all payroll records                     |
| `POST /api/Payroll`        | Create new payroll record                   |
| `GET /api/Payroll/{id}`    | Get payroll records using payroll public id |
| `PUT /api/Payroll/{id}`    | Update a payroll record                     |
| `DELETE /api/Payroll/{id}` | Delete a payroll record                     |

### PerformanceReview Controller
| Endpoint                  | Description                                                      |
| ------------------------- | ---------------------------------------------------------------- |
| `GET /api/Review`         | Get all performance review records                               |
| `POST /api/Review`        | Create new performance review record                             |
| `GET /api/Review/{id}`    | Get performance review record using performance review public id |
| `PUT /api/Review/{id}`    | Update a performance review record                               |
| `DELETE /api/Review/{id}` | Delete a performance review record                               |

### PhoneNumber Controller
| Endpoint                        | Description                                          |
| ------------------------------- | ---------------------------------------------------- |
| `GET /api/Phone-number`         | Get all phone number records                         |
| `POST /api/Phone-number`        | Create new phone number record                       |
| `GET /api/Phone-number/{id}`    | Get phone number record using phone number public id |
| `PUT /api/Phone-number/{id}`    | Update a phone number record                         |
| `DELETE /api/Phone-number/{id}` | Delete a phone number record                         |

### Project Controller
| Endpoint                                   | Description                                       |
| ------------------------------------------ | ------------------------------------------------- |
| `GET /api/Project`                         | Get all project records                           |
| `POST /api/Project`                        | Create new project record                         |
| `GET /api/Project/{id}`                    | Get project record using project public id        |
| `PUT /api/Project/{id}`                    | Update a project record                           |
| `DELETE /api/Project/{id}`                 | Delete a project record                           |
| `GET /api/Project/{id}/employees-assigned` | Get the list of employees assigned in the project |

### ProjectAssignment Controller
| Endpoint                       | Description                                                      |
| ------------------------------ | ---------------------------------------------------------------- |
| `GET /api/Assignments`         | Get all project assignment records                               |
| `POST /api/Assignments`        | Create new project assignment record                             |
| `GET /api/Assignments/{id}`    | Get project assignment record using project assignment public id |
| `PUT /api/Assignments/{id}`    | Update a project assignment record                               |
| `DELETE /api/Assignments/{id}` | Delete a project assignment record                               |

### Role Controller
| Endpoint                | Description                                       |
| ----------------------- | ------------------------------------------------- |
| `GET /api/Role`         | Get all employee job role records                 |
| `POST /api/Role`        | Create new employee job role record               |
| `GET /api/Role/{id}`    | Get employee job role record using role public id |
| `PUT /api/Role/{id}`    | Update role record                                |
| `DELETE /api/Role/{id}` | Delete a role record                              |

---

<h2 id="http-files-for-testing">📄 .http Files for Testing</h2>

HTTP files are located in the `Http/` folder inside the `EmployeeManagementSystem.API` project.

To use the HTTP files, you need to set up the [environment file](https://learn.microsoft.com/en-us/aspnet/core/test/http-files?view=aspnetcore-9.0#environment-files).

First, run the `Login Request` inside the `Account.http` file to get the refresh token and the JWT token.

```http
### Login Request
POST {{baseUrl}}/api/account/login
Content-Type: application/json

{
  "userName": "[USER NAME HERE]",
  "password": "[PASSWORD HERE]"
}
```

**Note:** When you click the `Headers` in the `HTTP Response Window`, the refresh token is named `___rt` while the JWT is named `___at`. To decode the value of `___rt`, use `[System.Net.WebUtility]::UrlDecode("[___rt HERE]")` in PowerShell or your preferred terminal.

Open the `httpenv.json` file and replace the variables with your actual values.

```json
{
  "development": {
    "https": "[HTTPS HERE]",
    "http": "[HTTP HERE]",
    "refreshToken": [___rt HERE],
    "token": [___at HERE]
  }  
}
```

You can now use the following HTTP files:

#### Account.http

```http
@baseUrl = {{https}}
@employeeID = [ID HERE]

### Login Request
POST {{baseUrl}}/api/account/login
Content-Type: application/json

{
  "userName": "[USER NAME HERE]",
  "password": "[PASSWORD HERE]"
}

### Register Request (requires bearer token)
POST {{baseUrl}}/api/account/register
Authorization: Bearer {{token}}
Content-Type: application/json

{ 
  "firstName": "testing",
  "middleName": "test",
  "lastName": "testingting",
  "email": "test@email.com",
  "dateOfBirth": "2025-07-22",
  "hireDate": "2025-07-22",
  "address": "test",
  "status": "Active",
  "departmentPub_ID": "2025-1122",
  "rolePub_ID": "2025-1222",
  "userName": "testingUser",
  "orgRole": "HRStaff",
  "password": "user_Password101"
}

### Token Refresh
POST {{baseUrl}}/api/Account/Refresh-Token
Content-Type: application/json

{
   "refreshToken": "{{refreshToken}}",
   "employeeId": "{{employeeID}}"
}

### Logout Request
POST {{baseUrl}}/api/Account/Logout
Content-Type: application/json
Authorization: Bearer {{token}}

{
   "refreshToken": "{{refreshToken}}"
}
```

#### Attendance.http

```http
@baseUrl = {{https}}
@attendanceId = [ATTENDANCE ID HERE]

### Get All Attendance
GET {{baseUrl}}/api/attendance?PageNumber=1&PageSize=3
# Authorization: Bearer {{token}}

### Get Attendance by ID
GET {{baseUrl}}/api/attendance/{{attendanceId}}
Authorization: Bearer {{token}}

### Create Attendance
POST {{baseUrl}}/api/attendance
Content-Type: application/json
Authorization: Bearer {{token}}

{
  "attendancePub_ID": "string",
  "date": "2025-07-23",
  "checkInTime": "string",
  "checkOutTime": "string",
  "status": "Present",
  "employeePub_ID": "string"
}

### Update Attendance
PUT {{baseUrl}}/api/attendance/{{attendanceId}}
Content-Type: application/json
Authorization: Bearer {{token}}

{
  "attendancePub_ID": "{{attendanceId}}",
  "date": "2025-07-23",
  "checkInTime": ""09:46:30",
  "checkOutTime": "16:33:20",
  "status": "Present",
  "employeePub_ID": "string"
}

### Delete Attendance
DELETE {{baseUrl}}/api/attendance/{{attendanceId}}
Authorization: Bearer {{token}}
```

#### Department.http

```http
@baseUrl = {{http}}
@departmentId = [DEPARTMENT ID HERE]

### Get all departments (with optional query parameters)
GET {{baseUrl}}/api/department?PageNumber=1&PageSize=10
Authorization: Bearer {{token}}

### Get department by ID
GET {{baseUrl}}/api/department/{{departmentId}}
Authorization: Bearer {{token}}

### Create a new department
POST {{baseUrl}}/api/department
Content-Type: application/json
Authorization: Bearer {{token}}

{
  "departmentPub_ID": "[New ID]",
  "departmentName": "Hr Department",
  "description": "test"
}

### Update an existing department
PUT {{baseUrl}}/api/department/{{departmentId}}
Content-Type: application/json
Authorization: Bearer {{token}}

{
  "departmentPub_ID": "{{departmentId}}",
  "departmentName": "Hr Department",
  "description": "test"
}

### Delete a department
DELETE {{baseUrl}}/api/department/{{departmentId}}
Authorization: Bearer {{token}}
```

#### Employee.http

```http
@baseUrl = {{http}}
@employeeId = [EMPLOYEE ID HERE]

### Get All Employees
GET {{baseUrl}}/api/employee?PageNumber=1&PageSize=10
Authorization: Bearer {{token}}

### Get Employee By ID
GET {{baseUrl}}/api/employee/{{employeeId}}
Authorization: Bearer {{token}}

### Get Employee Attendance
GET {{baseUrl}}/api/employee/attendance?EmployeePub_ID={{employeeId}}
Authorization: Bearer {{token}}

### Get Employee Leave Request
GET {{baseUrl}}/api/employee/leave?EmployeePub_ID={{employeeId}}
Authorization: Bearer {{token}}

### Get Employee Payroll
GET {{baseUrl}}/api/employee/payroll?EmployeePub_ID={{employeeId}}
Authorization: Bearer {{token}}

### Get Employee Performance Review
GET {{baseUrl}}/api/employee/appraisal?EmployeePub_ID={{employeeId}}
Authorization: Bearer {{token}}

### Get Employee Contact Info
GET {{baseUrl}}/api/employee/contact?EmployeePub_ID={{employeeId}}
Authorization: Bearer {{token}}

### Get Employee Project Assignment
GET {{baseUrl}}/api/employee/task?EmployeePub_ID={{employeeId}}
Authorization: Bearer {{token}}

### Create New Employee: in order to create an Employee you must create and account
POST {{baseUrl}}/api/employee
Content-Type: application/json
Authorization: Bearer {{token}}

{
  "employeePub_ID": "2025-1155",
  "firstName": "John",
  "middleName": "D",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "dateOfBirth": "1995-01-01",
  "hireDate": "2023-05-01",
  "address": "123 Sample Street",
  "status": "Active",
  "departmentPub_ID": "2025-1122",
  "rolePub_ID": "2025-1222",
  "appUserId": "e86b2ad3-4e7c-497b-a760-1b6dcfe5938f"
}

### Update Employee: in order to update an Employee you must create an account
PUT {{baseUrl}}/api/employee/{{employeeId}}
Content-Type: application/json
Authorization: Bearer {{token}}

{
  "employeePub_ID": "{{employeeId}}",
  "firstName": "John",
  "middleName": "Updated",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "dateOfBirth": "1995-01-01",
  "hireDate": "2023-05-01",
  "address": "456 New Address",
  "status": "Active",
  "departmentPub_ID": "2025-1122",
  "rolePub_ID": "2025-1222",
  "appUserId": "e86b2ad3-4e7c-497b-a760-1b6dcfe5938f"
}

### Delete Employee
DELETE {{baseUrl}}/api/employee/{{employeeId}}
Authorization: Bearer {{token}}
```

#### LeaveRequest.http

```http

@baseUrl = {{http}}
@leaveRequestId = [LEAVE REQUEST ID HERE]

### Get All Leave request
GET {{baseUrl}}/api/timeoff?PageNumber=1&PageSize=10
Authorization: Bearer {{token}}

### Get Leave request by ID
GET {{baseUrl}}/api/timeoff/{{leaveRequestId}}
Authorization: Bearer {{token}}

### Create Leave request: To create a leave request you must have an employee id
POST {{baseUrl}}/api/timeoff
Content-Type: application/json
Authorization: Bearer {{token}}

{
  "leavePub_ID": "string",
  "startDate": "2025-07-25",
  "endDate": "2025-07-25",
  "leaveType": "Sick",
  "status": "Pending",
  "reason": "string",
  "employeePub_ID": "string"
}


### Update Leave request
PUT {{baseUrl}}/api/timeoff/{{leaveRequestId}}
Content-Type: application/json
Authorization: Bearer {{token}}

{
  "leavePub_ID": "{{leaveRequestId}}",
  "startDate": "2025-07-25",
  "endDate": "2025-07-25",
  "leaveType": "Sick",
  "status": "Pending",
  "reason": "string",
  "employeePub_ID": "string"
}

### Delete Leave request
DELETE {{baseUrl}}/api/timeoff/{{leaveRequestId}}
Authorization: Bearer {{token}}      
```

#### Payroll.http

```http
@baseUrl = {{http}}
@payrollId = [PAYROLL ID HERE]

### Get All Payroll
GET {{baseUrl}}/api/payroll?PageNumber=1&PageSize=10
Authorization: Bearer {{token}}

### Get Payroll by ID
GET {{baseUrl}}/api/payroll/{{payrollId}}
Authorization: Bearer {{token}}

### Create Payroll: Need Employee ID
POST {{baseUrl}}/api/payroll
Content-Type: application/json
Authorization: Bearer {{token}}

{
  "payrollPub_ID": "string",
  "payDate": "2025-07-25",
  "basicSalary": 0,
  "allowances": 0,
  "deductions": 0,
  "netSalary": 0,
  "employeePub_ID": "string"
}

### Update Payroll
PUT {{baseUrl}}/api/payroll/{{payrollId}}
Content-Type: application/json
Authorization: Bearer {{token}}

{
  "payrollPub_ID": "string",
  "payDate": "2025-07-25",
  "basicSalary": 0,
  "allowances": 0,
  "deductions": 0,
  "netSalary": 0,
  "employeePub_ID": "string"
}

### Delete Payroll
DELETE {{baseUrl}}/api/payroll/{{payrollId}}
Authorization: Bearer {{token}}
```

#### PhoneNumber.http

```http

@baseUrl = {{http}}
@phoneNumberId = [PHONE NUMBER ID HERE]

### Get All Phone numbers
GET {{baseUrl}}/api/phone-number?PageNumber=1&PageSize=10
Authorization: Bearer {{token}}

### Get Phone numbers by ID
GET {{baseUrl}}/api/phone-number/{{phoneNumberId}}
Authorization: Bearer {{token}}

### Create Phone number record: To create a phone number record you must have an employeePub_ID
POST {{baseUrl}}/api/phone-number
Content-Type: application/json
Authorization: Bearer {{token}}

{
  "phoneNumberPub_ID": "string",
  "phoneNumberValue": "string",
  "employeePub_ID": "string"
}

### Update Phone number
PUT {{baseUrl}}/api/phone-number/{{phoneNumberId}}
Content-Type: application/json
Authorization: Bearer {{token}}

{
  "phoneNumberPub_ID": "{{phoneNumberId}}",
  "phoneNumberValue": "string",
  "employeePub_ID": "string"
}

### Delete Phone number
DELETE {{baseUrl}}/api/phone-number/{{phoneNumberId}}
Authorization: Bearer {{token}}
```

#### Project.http

```http

@baseUrl = {{http}}
@projectId = [PROJECT ID HERE]

### Get All Project
GET {{baseUrl}}/api/project?PageNumber=1&PageSize=3
Authorization: Bearer {{token}}

### Get Project by ID
GET {{baseUrl}}/api/project/{{projectId}}
Authorization: Bearer {{token}}

### Create Project
POST {{baseUrl}}/api/project
Content-Type: application/json
Authorization: Bearer {{token}}

{
  "projectPub_ID": "2026-5566",
  "projectName": "sample project",
  "description": "aaaa",
  "startDate": "2025-07-26",
  "endDate": "2025-07-30",
  "status": "Ongoing"
}

### Update Project
PUT {{baseUrl}}/api/project/{{projectId}}
Content-Type: application/json
Authorization: Bearer {{token}}

{
  "projectPub_ID": "{{projectId}}",
  "projectName": "sample project",
  "description": "aaaa",
  "startDate": "2025-07-26",
  "endDate": "2025-08-26",
  "status": "Ongoing"
}

### Delete Project
DELETE {{baseUrl}}/api/project/{{projectId}}
Authorization: Bearer {{token}}
```

#### ProjectAssignment.http

```http

@baseUrl = {{http}}
@projectAssignment = [PROJECT ASSIGNMENT ID HERE]

### Get All Project assignment
GET {{baseUrl}}/api/assignments?PageNumber=1&PageSize=10
Authorization: Bearer {{token}}

### Get Project assignment by ID
GET {{baseUrl}}/api/assignments/{{projectAssignment}}
Authorization: Bearer {{token}}

### Create Project assignment
POST {{baseUrl}}/api/assignments
Content-Type: application/json
Authorization: Bearer {{token}}

{
  "assignmentPub_ID": "string",
  "roleInProject": "string",
  "assignedDate": "2025-07-26",
  "employeePub_ID": "string",
  "projectPub_ID": "string"
}

### Update Project assignment
PUT {{baseUrl}}/api/assignments/{{projectAssignment}}
Content-Type: application/json
Authorization: Bearer {{token}}

{
  "assignmentPub_ID": "{{projectAssignment}}",
  "roleInProject": "string",
  "assignedDate": "2025-07-26",
  "employeePub_ID": "string",
  "projectPub_ID": "string"
}

### Delete Project assignment
DELETE {{baseUrl}}/api/assignments/{{projectAssignment}}
Authorization: Bearer {{token}}
```

#### Role.http

```http

@baseUrl = {{http}}
@roleId = [ROLE ID HERE]

### Get All Roles
GET {{baseUrl}}/api/role?PageNumber=1&PageSize=10
Authorization: Bearer {{token}}

### Get Role by ID
GET {{baseUrl}}/api/role/{{roleId}}
Authorization: Bearer {{token}}

### Create Role
POST {{baseUrl}}/api/role
Content-Type: application/json
Authorization: Bearer {{token}}

{
  "rolePub_ID": "2026-5565",
  "roleName": "Qa",
  "description": "check check",
  "salary": 100000
}

### Update Role
PUT {{baseUrl}}/api/role/{{roleId}}
Content-Type: application/json
Authorization: Bearer {{token}}

{
  "rolePub_ID": "{{roleId}}",
  "roleName": "Programmers",
  "description": "test test",
  "salary": 12000
}

### Delete Role
DELETE {{baseUrl}}/api/role/{{roleId}}
Authorization: Bearer {{token}}
```
---

<h2 id="skills-demonstrated">📚 Skills Demonstrated </h2>

* RESTful API Design
* Repository & Service Layer Pattern
* JWT Authentication & Authorization
* Global Error Handling with Middleware
* Pagination, Filtering and Sorting
* Secure Secret Management
* Entity Framework Code-First Migrations
* Version Control Best Practices with `.gitignore`

---

<h2 id="license">📌 License</h2>

This project is licensed under the [MIT License](https://github.com/silverOwl101/Employee-Management-System-API/blob/main/LICENSE).

---

> Made by John Harold P. Paluca