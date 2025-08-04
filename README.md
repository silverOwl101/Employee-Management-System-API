# Employee Management System API

A .NET Core Web API for managing employees, attendance, payroll, departments, and more.

---

## Table of Contents

* [Introduction](#introduction)
* [Technologies Used](#technologies-used) **🚧 Note:** Under construction and will be updated soon.
* [Key Features](#key-features) **🚧 Note:** Under construction and will be updated soon.
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
| **Database**          | MSSQL                                     |
| **IDE**               | [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)                        |
| **Documentation**     | [Swagger / Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore?tab=readme-ov-file)|
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

1. Clone the repository
2. Set up your SQL Server and update `appsettings.json`
3. Run database migrations: `dotnet ef database update`
4. Launch the API: `dotnet run`
5. Use Swagger or `.http` files to test endpoints

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