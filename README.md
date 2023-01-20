 # ASP.NET Core WebApi + Svelte - Clean Architecture
 ![Azure Static Web Apps CI/CD](https://github.com/LongLongDragonTeam/LongLongDragonWebAppTemplate/workflows/Azure%20Static%20Web%20Apps%20CI/CD/badge.svg?branch=senpai)
 
Implementation of onion architecture using ASP.NET Core and Svelte, it's database independent but right now we are constrained by SQLKata and FluentMigrator implementation, but as long we can use dapper, we can create more database backends, we are using MVC for the web projects

## Technologies
- ASP.NET Core 3.1 WebApi
- REST Standards
- .NET Core 3.1 / Standard 2.1 Libraries
- Svelte + Snowpack

## Features Webapi
- [x] Onion Architecture
- [x] CQRS with MediatR Library
- [x] Entity Framework Core - Code First
- [x] Repository Pattern - Generic
- [x] MediatR Pipeline Logging & Validation
- [x] Serilog
- [x] Swagger UI
- [x] Response Wrappers
- [x] Healthchecks
- [x] Pagination
- [-] In-Memory Caching (Per project)
- [-] Redis Caching (Per Project)
- [x] In-Memory Database
- [x] Microsoft Identity with JWT Authentication
- [x] Role based Authorization
- [x] Identity Seeding
- [x] Database Seeding
- [x] Custom Exception Handling Middlewares
- [x] API Versioning
- [x] Fluent Validation
- [x] Automapper
- [x] SMTP / Mailkit / Sendgrid Email Service
- [x] Complete User Management Module (Register / Generate Token / Forgot Password / Confirmation Mail)
- [x] User Auditing
- [x] File Implementation to Azure/AWS/File System and in database link

## Features Svelte 
- [X] Generic Service Creation
- [X] Generic Model for WebApi Consumption
- [WIP] Generic Components we might use (Some but hey)
- [X] Generic Authorization Flow
- [X] Enviroment Variables
- [WIP] Image Minifier (Need to make our own stuff)
## Contribute

We like Early PR and Branches in situ, open a branch with [WIP] at the start to know it's being implemented, we accept both from forks and from branches in the repository
