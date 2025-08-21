# Employee Management System API

A .NET Core Web API for managing employees, attendance, payroll, departments, and more.

---

## Table of Contents

* [Introduction](#introduction)
* [Technology Stack](#technologies-used)
* [Key Features](#key-features)
* [Getting Started](#getting-started)
* [Project Structure](#project-structure) 
* Authorization (RBAC)
* Authentication (JWT)
* API Endpoints
* .http Files for Testing
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
| **Development Env**   | [Docker](https://www.docker.com/) (used for MSSQL Server container)                              |
| **IDE**               | [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)                        |
| **Documentation**     | [Swagger](https://swagger.io/) / [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore?tab=readme-ov-file)|
| **Test Data Seeding** | [Bogus for .NET](https://github.com/bchavez/Bogus) (C# fake data generator)|

---

<h2 id="key-features">🔑 Key Features</h2>

| Feature               | Description                                             |
| --------------------- | ------------------------------------------------------- |
| 👤 User Management    | Register, update, delete users with role-based access   |
| 🕒 Attendance System  | Clock-in/clock-out, employee attendance tracking        |
| 💼 Payroll            | Salary computation, deductions, bonuses                 |
| 🔒 Authentication     | JWT-based login with claims and policy validation       |
| 🛆 Leave Management   | Track and manage leave requests                         |
| 📱 Project Assignment | Assign and monitor project-based employee participation |

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

To seed the default `sa` (Super Admin) account:

In `Program.cs`, **uncomment** line 458. To quickly go to a specific line number, press `Ctrl + G`, then enter `458`.

```csharp
await DbSeeder.SeedSuperAdmin(app.Services);
```

> **📝 Note:** After the Super Admin is created, you may comment this line again. However, leaving it uncommented is safe, duplicate creation is prevented.

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

<h2 id="authorization-rbac">🛂 Authorization (RBAC)</h2>

> **🚧 Note:** This section is currently under construction and will be updated soon. The API is fully functional and tested via Swagger with documentation available at `/swagger`.

---

<h2 id="authentication-jwt">🔐 Authentication (JWT)</h2>

> **🚧 Note:** This section is currently under construction and will be updated soon. The API is fully functional and tested via Swagger with documentation available at `/swagger`.
---

<h2 id="api-endpoints">📫 API Endpoints</h2>

All endpoints follow REST conventions: 

* `GET /api/employees`
* `POST /api/employees`
* `PUT /api/employees/{id}`

> **🚧 Note:** This section is currently under construction and will be updated soon. The API is fully functional and tested via Swagger with documentation available at `/swagger`.
---

<h2 id="http-files-for-testing">📄 .http Files for Testing</h2>

* Located in the `HttpRequests/` folder
* Easy to test endpoints using REST Client in VS Code or VS 2022
* Example:

```http
POST https://localhost:7235/api/account/login
Content-Type: application/json

{
  "userName": "admin",
  "password": "admin_password"
}
```
> **🚧 Note:** This section is currently under construction and will be updated soon. The API is fully functional and tested via Swagger with documentation available at `/swagger`.
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