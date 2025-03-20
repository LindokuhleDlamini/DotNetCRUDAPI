# DotNet CRUD API

This is a .NET Core 7 CRUD API built with Clean Architecture, Entity Framework Core, and JWT authentication. It provides endpoints for managing users, teams, and leagues, with integration to an external sports API for fetching data.

## Features

User Management: Create, read, update, and delete users.

Team and League Data: Fetch team and league data from an external API (TheSportsDB).

JWT Authentication: Secure endpoints with JSON Web Tokens (JWT).

Entity Framework Core: Database operations using EF Core with Code First Migrations.

Clean Architecture: Separation of concerns with Domain, Application, Infrastructure, and Presentation layers.

Integration Testing: Includes tests for external API integration.

## Technologies Used
.NET Core 7

Entity Framework Core

JWT Authentication

Swagger/OpenAPI for API documentation

xUnit for unit and integration testing

TheSportsDB API for external data


## API Endpoints
### User Management
GET /api/user: Get all users.

GET /api/user/{id}: Get a user by ID.

POST /api/user: Create a new user.

PUT /api/user/{id}: Update a user.

DELETE /api/user/{id}: Delete a user.

### Team and League Data
GET /api/team: Get all teams.

GET /api/team/{id}: Get a team by ID.

GET /api/league: Get all leagues.

GET /api/league/{id}: Get a league by ID.

### Authentication
POST /api/auth/login: Authenticate and receive a JWT token.

## Testing
### Unit Tests
Run unit tests using the following command:
dotnet test

### Integration Tests
Integration tests are included for external API calls. Run them with:
dotnet test
