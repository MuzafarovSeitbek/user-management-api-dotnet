# User Management API (.NET)

A RESTful API for managing users built with **ASP.NET Core 8.0** and **C#**. This project demonstrates CRUD operations, middleware implementation, data validation with Data Annotations, and API key authentication.

## 🚀 Features

✅ **CRUD Operations** - Create, Read, Update, and Delete users  
✅ **API Key Authentication** - Custom middleware for API key validation  
✅ **Request Logging** - Middleware that logs all HTTP requests with timestamps and response times  
✅ **Data Validation** - Built-in ASP.NET Core validation with Data Annotations  
✅ **DTOs (Data Transfer Objects)** - Separate models for requests and responses  
✅ **Error Handling** - Comprehensive error responses with proper HTTP status codes  
✅ **Swagger/OpenAPI** - Interactive API documentation  
✅ **Dependency Injection** - Following .NET best practices  
✅ **RESTful Design** - Follows REST API conventions  

## 📁 Project Structure

```
UserManagementAPI/
├── Controllers/
│   ├── UsersController.cs          # User CRUD endpoints
│   └── HealthController.cs         # Health check endpoint
├── Models/
│   ├── User.cs                     # User entity
│   ├── ApiResponse.cs              # Response wrapper
│   └── DTOs/
│       ├── CreateUserDto.cs        # DTO for creating users
│       └── UpdateUserDto.cs        # DTO for updating users
├── Services/
│   ├── IUserService.cs             # User service interface
│   └── UserService.cs              # User service implementation (in-memory)
├── Middleware/
│   ├── RequestLoggingMiddleware.cs # Request/response logging
│   └── ApiKeyAuthenticationMiddleware.cs # API key validation
├── Properties/
│   └── launchSettings.json         # Launch configuration
├── appsettings.json                # Application settings
├── appsettings.Development.json    # Development settings
├── Program.cs                      # Application entry point
├── UserManagementAPI.csproj        # Project file
├── .gitignore                      # Git ignore rules
└── README.md                       # This file
```

## 🔧 Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- A code editor (Visual Studio, VS Code, or JetBrains Rider)
- (Optional) Postman or similar tool for testing

## 📦 Installation

### 1. Clone the Repository

```bash
git clone <your-repository-url>
cd UserManagementAPI
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Configure API Key

Edit `appsettings.Development.json` to set your API key:

```json
{
  "ApiSettings": {
    "ApiKey": "dev-api-key-12345"
  }
}
```

### 4. Build the Project

```bash
dotnet build
```

### 5. Run the Application

```bash
dotnet run
```

The API will start on:
- **HTTPS**: `https://localhost:5001`
- **HTTP**: `http://localhost:5000`
- **Swagger UI**: `https://localhost:5001/swagger`

## 🌐 API Endpoints

All `/api/users` endpoints require authentication via the `X-API-Key` header.

### Base URL
```
https://localhost:5001
```

### Authentication

Include this header in all requests to `/api/users`:

```http
X-API-Key: dev-api-key-12345
```

### Endpoints Overview

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/health` | API health check | ❌ |
| GET | `/api/users` | Get all users | ✅ |
| GET | `/api/users/{id}` | Get user by ID | ✅ |
| POST | `/api/users` | Create new user | ✅ |
| PUT | `/api/users/{id}` | Update user | ✅ |
| DELETE | `/api/users/{id}` | Delete user | ✅ |

---

### 1️⃣ Health Check

**GET** `/api/health`

No authentication required.

**Response (200 OK):**
```json
{
  "message": "Welcome to User Management API",
  "version": "1.0.0",
  "status": "healthy",
  "timestamp": "2024-01-19T12:00:00Z",
  "endpoints": {
    "users": "/api/users",
    "swagger": "/swagger",
    "health": "/api/health"
  }
}
```

---

### 2️⃣ Get All Users

**GET** `/api/users`

**Headers:**
```
X-API-Key: dev-api-key-12345
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": null,
  "data": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "username": "johndoe",
      "email": "john@example.com",
      "firstName": "John",
      "lastName": "Doe",
      "age": 30,
      "createdAt": "2024-01-19T12:00:00Z",
      "updatedAt": null
    }
  ],
  "count": 2
}
```

---

### 3️⃣ Get User by ID

**GET** `/api/users/{id}`

**Headers:**
```
X-API-Key: dev-api-key-12345
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": null,
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "username": "johndoe",
    "email": "john@example.com",
    "firstName": "John",
    "lastName": "Doe",
    "age": 30,
    "createdAt": "2024-01-19T12:00:00Z",
    "updatedAt": null
  },
  "count": null
}
```

**Response (404 Not Found):**
```json
{
  "error": "Not Found",
  "message": "User with ID 3fa85f64-5717-4562-b3fc-2c963f66afa6 not found",
  "details": null
}
```

---

### 4️⃣ Create New User

**POST** `/api/users`

**Headers:**
```
X-API-Key: dev-api-key-12345
Content-Type: application/json
```

**Request Body:**
```json
{
  "username": "bobsmith",
  "email": "bob@example.com",
  "firstName": "Bob",
  "lastName": "Smith",
  "age": 35
}
```

**Validation Rules:**

| Field | Required | Rules |
|-------|----------|-------|
| username | ✅ Yes | 3-20 characters, alphanumeric and underscores only |
| email | ✅ Yes | Valid email format |
| firstName | ✅ Yes | Letters, spaces, hyphens, apostrophes only |
| lastName | ✅ Yes | Letters, spaces, hyphens, apostrophes only |
| age | ❌ No | 0-150 |

**Response (201 Created):**
```json
{
  "success": true,
  "message": "User created successfully",
  "data": {
    "id": "7c9e6679-7425-40de-944b-e07fc1f90ae7",
    "username": "bobsmith",
    "email": "bob@example.com",
    "firstName": "Bob",
    "lastName": "Smith",
    "age": 35,
    "createdAt": "2024-01-19T13:00:00Z",
    "updatedAt": null
  },
  "count": null
}
```

**Response (400 Bad Request):**
```json
{
  "error": "Validation Error",
  "message": "Invalid user data",
  "details": [
    "Username must be between 3 and 20 characters",
    "Invalid email format"
  ]
}
```

**Response (409 Conflict):**
```json
{
  "error": "Conflict",
  "message": "Username already exists",
  "details": null
}
```

---

### 5️⃣ Update User

**PUT** `/api/users/{id}`

**Headers:**
```
X-API-Key: dev-api-key-12345
Content-Type: application/json
```

**Request Body** (all fields optional):
```json
{
  "age": 36
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "User updated successfully",
  "data": {
    "id": "7c9e6679-7425-40de-944b-e07fc1f90ae7",
    "username": "bobsmith",
    "email": "bob@example.com",
    "firstName": "Bob",
    "lastName": "Smith",
    "age": 36,
    "createdAt": "2024-01-19T13:00:00Z",
    "updatedAt": "2024-01-19T14:30:00Z"
  },
  "count": null
}
```

---

### 6️⃣ Delete User

**DELETE** `/api/users/{id}`

**Headers:**
```
X-API-Key: dev-api-key-12345
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "User deleted successfully",
  "data": {
    "id": "7c9e6679-7425-40de-944b-e07fc1f90ae7",
    "username": "bobsmith",
    "email": "bob@example.com",
    "firstName": "Bob",
    "lastName": "Smith",
    "age": 36,
    "createdAt": "2024-01-19T13:00:00Z",
    "updatedAt": "2024-01-19T14:30:00Z"
  },
  "count": null
}
```

---

## 🧪 Testing with cURL

### Get all users:
```bash
curl -X GET https://localhost:5001/api/users \
  -H "X-API-Key: dev-api-key-12345" \
  --insecure
```

### Create a new user:
```bash
curl -X POST https://localhost:5001/api/users \
  -H "X-API-Key: dev-api-key-12345" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "testuser",
    "email": "test@example.com",
    "firstName": "Test",
    "lastName": "User",
    "age": 25
  }' \
  --insecure
```

### Update a user:
```bash
curl -X PUT https://localhost:5001/api/users/{user-id} \
  -H "X-API-Key: dev-api-key-12345" \
  -H "Content-Type: application/json" \
  -d '{"age": 26}' \
  --insecure
```

### Delete a user:
```bash
curl -X DELETE https://localhost:5001/api/users/{user-id} \
  -H "X-API-Key: dev-api-key-12345" \
  --insecure
```

**Note:** `--insecure` flag is used for development with self-signed certificates.

---

## 📖 Using Swagger UI

1. Start the application: `dotnet run`
2. Open your browser and navigate to: `https://localhost:5001/swagger`
3. Click on "Authorize" button (top right)
4. Enter your API key: `dev-api-key-12345`
5. Click "Authorize" and then "Close"
6. Now you can test all endpoints directly from Swagger UI!

---

## 🏗️ Architecture & Design Patterns

### 1. **Dependency Injection**
All services are registered in `Program.cs` and injected where needed.

```csharp
builder.Services.AddSingleton<IUserService, UserService>();
```

### 2. **Repository Pattern**
`UserService` acts as an in-memory repository for user data.

### 3. **DTO Pattern**
Separate DTOs for input (`CreateUserDto`, `UpdateUserDto`) and output (`User`).

### 4. **Middleware Pipeline**
Custom middleware for logging and authentication.

### 5. **RESTful API Design**
- Proper HTTP verbs (GET, POST, PUT, DELETE)
- Meaningful status codes (200, 201, 400, 401, 403, 404, 409, 500)
- Resource-based URLs

---

## 🔐 Middleware

### 1. Request Logging Middleware
Logs all incoming requests with:
- Timestamp
- HTTP method and path
- IP address
- Response status code and duration

### 2. API Key Authentication Middleware
- Validates `X-API-Key` header
- Returns 401 if missing
- Returns 403 if invalid
- Skips authentication for `/`, `/swagger`, and `/api/health`

---

## ✅ Data Validation

Validation is handled using **Data Annotations** in DTOs:

```csharp
[Required(ErrorMessage = "Username is required")]
[StringLength(20, MinimumLength = 3)]
[RegularExpression(@"^[a-zA-Z0-9_]+$")]
public string Username { get; set; }
```

ASP.NET Core automatically validates the model and returns 400 Bad Request with validation errors.

---

## 🛠️ Technologies Used

- **ASP.NET Core 8.0** - Web framework
- **C#** - Programming language
- **Swagger/OpenAPI** - API documentation
- **Dependency Injection** - Built-in DI container
- **LINQ** - Data querying
- **Data Annotations** - Model validation

---

## 📝 Development Notes

### In-Memory Storage
The current implementation uses an in-memory `List<User>` for storage. Data will be lost when the application restarts.

**For production**, consider integrating:
- **Entity Framework Core** with SQL Server, PostgreSQL, or MySQL
- **Dapper** for lightweight data access
- **MongoDB** for NoSQL storage

### Example with Entity Framework Core:
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
```

---

## 🚀 Deployment

### Deploy to Azure App Service:
```bash
# Publish the app
dotnet publish -c Release -o ./publish

# Deploy to Azure (requires Azure CLI)
az webapp up --name your-app-name --resource-group your-rg
```

### Docker Deployment:
Create a `Dockerfile`:
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["UserManagementAPI.csproj", "./"]
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagementAPI.dll"]
```

Build and run:
```bash
docker build -t user-management-api .
docker run -p 8080:80 user-management-api
```

---

## 📚 Further Improvements

- [ ] Add database integration (Entity Framework Core)
- [ ] Implement JWT authentication
- [ ] Add pagination and filtering for GET endpoints
- [ ] Add unit tests (xUnit, NUnit)
- [ ] Add integration tests
- [ ] Implement CQRS pattern
- [ ] Add caching (Redis)
- [ ] Add rate limiting
- [ ] Implement versioning (v1, v2)

---

## 📄 License

This project is licensed under the MIT License.

---

## 👤 Author

Created as part of a course assignment on **Back-End Development with .NET**.

---

## 🆘 Troubleshooting

### Problem: "Unable to configure HTTPS endpoint"
**Solution:** Trust the development certificate:
```bash
dotnet dev-certs https --trust
```

### Problem: "401 Unauthorized"
**Solution:** Make sure you're including the `X-API-Key` header with the correct value from `appsettings.Development.json`.

### Problem: Port already in use
**Solution:** Change the port in `Properties/launchSettings.json` or stop the process using the port.

---

## 📞 Support

For questions or issues:
1. Check the Swagger documentation at `/swagger`
2. Review the logs in the console
3. Ensure all dependencies are restored: `dotnet restore`
4. Verify your API key matches the configuration

