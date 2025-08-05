# Employee Management System API

A .NET Core Web API for managing employees, attendance, payroll, departments, and more.

---

## Table of Contents

* [Introduction](#introduction)
* [Technologies Used](#technologies-used)
* [Key Features](#key-features)
* [Getting Started](#getting-started) **🚧 Note:** Under construction and will be updated soon.
* [Project Structure](#project-structure) **🚧 Note:** Under construction and will be updated soon.
* [Authorization (RBAC)](#authorization-rbac) **🚧 Note:** Under construction and will be updated soon.
* [Authentication (JWT)](#authentication-jwt) **🚧 Note:** Under construction and will be updated soon.
* [API Endpoints](#api-endpoints) **🚧 Note:** Under construction and will be updated soon.
* [.http Files for Testing](#http-files-for-testing) **🚧 Note:** Under construction and will be updated soon.
* [Skills Demonstrated](#skills-demonstrated)
* [License](#license)

---

<h2 id="introduction">📖 Introduction</h2>

This project provides a fully featured REST API for managing employee-related operations in a business setting, including user authentication, CRUD operations, attendance logging, leave requests, and payroll processing.

---

<h2 id="technologies-used">🔧 Technologies Used</h2>

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

To use this API, you need to follow these steps:

1. Clone the repository or you can [fork](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/working-with-forks/fork-a-repo) the repository if you want.

```bash
git clone https://github.com/silverOwl101/Employee-Management-System-API.git
```
2. After you clone the repository, open the solution file and build it.
3. Download and install [Docker desktop](https://www.docker.com/products/docker-desktop/).
4. Run the MSSQL Server via Docker      
      1. Make sure Docker Desktop is installed and running.
      2. Open PowerShell or your terminal of choice.
      3. Run the following command:
      ```bash
      docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong!Pass123" -p 1433:1433 --name mssql -d mcr.microsoft.com/mssql/server:2022-latest      
      ```
5. Set up [User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-9.0&tabs=windows).
      1. Make sure the solution file is open.      
      2. Open Developer PowerShell or your terminal of choice.
      2. Run the following command.
      ```bash
      # Assign your values here
      $superAdminGuid = "YOUR_GUID_HERE"
      $superAdminUsername = "sa"
      $superAdminEmail = "your_email@example.com"
      $superAdminPassword = "YOUR_STRONG_PASSWORD"
      $DataBasePassword = "YOUR_STRONG_DATABASE_PASSWORD"
      $databaseName = "YOUR_DATABASE_NAME"
      $jwtSigningKey = "YOUR_JWT:SigningKey"

      # Clear and init user-secrets
      dotnet user-secrets clear
      dotnet user-secrets init

      # Set secrets
      dotnet user-secrets set "SeedSuperAdmin:SuperAdminGuid" $superAdminGuid
      dotnet user-secrets set "SeedSuperAdmin:SuperAdminUsername" $superAdminUsername
      dotnet user-secrets set "SeedSuperAdmin:SuperAdminEmail" $superAdminEmail
      dotnet user-secrets set "SeedSuperAdmin:SuperAdminPassword" $superAdminPassword

      # Connection string (replace "localhost,1433" if your server is different)
      $connectionString = "Server=localhost,1433;Database=$databaseName;User Id=sa;Password=$DataBasePassword;Integrated Security=False;TrustServerCertificate=True;"
      dotnet user-secrets set "ConnectionStrings:default" $connectionString

      # JWT Signing Key
      dotnet user-secrets set "JWT:SigningKey" $jwtSigningKey

      ```
6. Set up [Entity Framework Core migrations](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli).
      1. Open Package Manager Console `Tools -> NuGet Package Manager -> Package Manager Console`.
      2. Create your first migration
      ```bash
      Add-Migration InitialCreate
      ```
      3. Create your database and schema
      ```bash
      Update-Database
      ```
7. Build and run the API project `(e.g., Employee_Management_System_API)` to launch the application.

> **🚧 Note:** This section is currently under construction and will be updated soon.
---

<h2 id="project-structure">📂 Project Structure</h2>

```bash
.
├── Controllers
├── Services
├── Repositories
├── Models
├── DTOs
├── Data
├── Middleware
├── Migrations
└── wwwroot
```
> **🚧 Note:** This section is currently under construction and will be updated soon.
---

<h2 id="authorization-rbac">🛂 Authorization (RBAC)</h2>

> **🚧 Note:** This section is currently under construction and will be updated soon.

---

<h2 id="authentication-jwt">🔐 Authentication (JWT)</h2>

* Token is issued after a successful login
* Tokens include roles and policy-based claims
* Secure endpoints with `[Authorize(Policy = "...")]`

> **🚧 Note:** This section is currently under construction and will be updated soon.
---

<h2 id="api-endpoints">📫 API Endpoints</h2>



All endpoints follow REST conventions: 

* `GET /api/employees`
* `POST /api/employees`
* `PUT /api/employees/{id}`

> **🚧 Note:** This section is currently under construction and will be updated soon.
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
> **🚧 Note:** This section is currently under construction and will be updated soon.
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