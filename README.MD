# Weblu - Web Development Services Backend API

[![.NET Version](https://img.shields.io/badge/.NET-8.0-purple)](https://dotnet.microsoft.com/)
[![Architecture](https://img.shields.io/badge/Architecture-Clean_Architecture-blue)](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
[![Docker](https://img.shields.io/badge/Docker-Ready-2496ED)](https://docker.com/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-2019-CC2927)](https://www.microsoft.com/sql-server)
[![Tests](https://img.shields.io/badge/Tests-Unit_&_Integration-green)](./tests)
[![License](https://img.shields.io/badge/License-MIT-yellow)](LICENSE)

## 📋 What is Weblu?

**Weblu** is a backend API platform that connects clients with web development services. Customers can request custom websites (shop websites, portfolios, blogs, etc.) built with their preferred technology stack - whether it's React + WebAPI, WordPress, or other methods. Weblu also provides informative articles about web development.

## ✨ Features

- 🛒 **Service Requests** - Clients can request website development services
- 🎨 **Multiple Tech Stacks** - Choose between React+WebAPI, WordPress, or custom methods
- 📝 **Article Management** - Browse and read web development articles
- 🧱 **Clean Architecture** - Separation of concerns (Domain, Application, Infrastructure, API layers)
- 🐳 **Docker Ready** - One-command setup with Docker Compose
- ✅ **Comprehensive Testing** - Unit tests + Integration tests included

## 🛠️ Tech Stack

| Layer        | Technology                   |
| ------------ | ---------------------------- |
| Backend      | ASP.NET Core 9 WebAPI        |
| Architecture | Clean Architecture           |
| Database     | SQL Server                   |
| ORM          | Entity Framework Core        |
| Testing      | xUnit, Moq, FluentAssertions |
| Container    | Docker & Docker Compose      |

## 🏗️ Project Structure

Weblu/src
├── Weblu.Domain/ # Entities, value objects, interfaces
├── Weblu.Application/ # Use cases, DTOs, business logic
├── Weblu.Infrastructure/ # Data access, repositories, EF Core
├── Weblu.API/ # Controllers, middleware, startup

Weblu/tests
└── Weblu.Tests/ # Unit & integration tests

## 🚀 Quick Start

### Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (v20.10 or later)
- Git

This project **requires** a `.env` file to run. The `.env` file contains sensitive configuration like database passwords and secret keys.

### Step 1: Create the `.env` file

In the root directory of the project, create a file named `.env` with the following content:

````env
# Database Configuration
DefaultConnection=Server=datatbase-service-name-in-docker-compose;Database=Weblu;User Id=sa;Password=your-strong-password;TrustServerCertificate=True;Encrypt=True;
ACCEPT_EULA=Y
MSSQL_SA_PASSWORD=your-strong-password

# JWT Authentication
JWT_SECRET_KEY=your-super-secret-key-minimum-32-characters-long
JWT_ISSUER=WebluAPI
JWT_AUDIENCE=WebluClients
JWT_ExpiryMinutes=30

# Admin Configuration
ADMIN_USERNAME=admin
ADMIN_PASSWORD=@Admin248

# Application Settings
ASPNETCORE_ENVIRONMENT=Development

### Run with Docker (Recommended)

```bash
# Clone the repository
git clone https://github.com/jupiter248/Weblu.git
cd Weblu

# Build and run
docker compose up --build
````

The API will be available at: http://localhost:5000

Swagger UI: http://localhost:5000/swagger

### Stop the application

```bash
docker compose down
```

🧪 Running Tests

```bash
# Run all tests
dotnet test Weblu/tests/

```
