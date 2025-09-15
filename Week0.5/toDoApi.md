# Task Management API - Project Requirements

## Overview
Build a RESTful Task Management API using ASP.NET Core Minimal APIs. This project will help you practice CRUD operations, data validation, middleware, authentication, and database integration.

## Core Requirements (Must Have)

### 1. Task Model
Create a `Task` entity with the following properties:
- `Id` (int, primary key, auto-generated)
- `Title` (string, required, max 100 characters)
- `Description` (string, optional, max 500 characters)
- `IsCompleted` (bool, default false)
- `Priority` (enum: Low, Medium, High, Critical)
- `DueDate` (DateTime, optional)
- `CreatedAt` (DateTime, auto-generated)
- `UpdatedAt` (DateTime, auto-updated)

### 2. API Endpoints

#### Tasks
- `GET /api/tasks` - Get all tasks with optional filtering
  - Query parameters: `isCompleted`, `priority`, `dueBefore`
- `GET /api/tasks/{id}` - Get specific task by ID
- `POST /api/tasks` - Create new task
- `PUT /api/tasks/{id}` - Update existing task
- `DELETE /api/tasks/{id}` - Delete task

### 3. Data Validation
- Implement input validation for all endpoints
- Return appropriate error messages for validation failures
- Use Data Annotations or FluentValidation

### 4. Error Handling
- Custom exception handling middleware
- Proper HTTP status codes (200, 201, 400, 404, 500)
- Consistent error response format

### 5. Data Storage
Choose one:
- **Option A**: In-memory storage using `List<Task>`
- **Option B**: Serialized file storage

## Intermediate Requirements (Should Have)

### 6. Authentication & Authorization
- JWT token-based authentication
- User registration and login endpoints
- Associate tasks with users (add `UserId` to Task model)
- Users can only access their own tasks

### 7. Advanced Filtering & Sorting
- Sort tasks by: `createdAt`, `dueDate`, `priority`
- Search tasks by title/description
- Pagination support (`page`, `pageSize` parameters)

### 8. Request Logging Middleware
- Log all HTTP requests (method, path, response time)
- Include correlation ID for request tracking

### 9. API Documentation
- Swagger/OpenAPI documentation
- Add XML comments to endpoints
- Include example requests/responses

## Advanced Requirements (Nice to Have)

### 10. Categories System
- Add `Category` entity (Id, Name, Color)
- Associate tasks with categories
- CRUD operations for categories

### 11. File Attachments
- Add file upload capability to tasks
- Store files in `wwwroot/uploads`
- Return file URLs in task responses

### 12. Task Statistics
- `GET /api/tasks/statistics` endpoint
- Return: total tasks, completed tasks, overdue tasks, tasks by priority

### 13. Unit Testing
- Write unit tests for endpoints
- Use xUnit and ASP.NET Core Test Host
- Achieve 80%+ code coverage

### 14. Performance Features
- Implement response caching
- Add compression middleware
- Database query optimization (if using EF Core)

## Technical Specifications

### Required NuGet Packages
```xml
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="7.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="7.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="7.0.0" />
<PackageReference Include="FluentValidation.AspNetCore" Version="11.3.0" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
```

### Response Format Standards

#### Success Response
```json
{
  "success": true,
  "data": { ... },
  "message": "Operation completed successfully"
}
```

#### Error Response
```json
{
  "success": false,
  "errors": ["Error message 1", "Error message 2"],
  "message": "Operation failed"
}
```

#### Paginated Response
```json
{
  "success": true,
  "data": [...],
  "pagination": {
    "page": 1,
    "pageSize": 10,
    "totalItems": 50,
    "totalPages": 5
  }
}
```

## Delivery Milestones

### Day 1: Foundation
- [ ] Project setup and basic CRUD endpoints
- [ ] Task model and in-memory storage
- [ ] Basic validation

### Day 2: Enhancement
- [ ] Error handling middleware
- [ ] Filtering and search functionality
- [ ] Swagger documentation

### Day 3: Security
- [ ] JWT authentication implementation
- [ ] User management endpoints
- [ ] Authorization for task operations

### Day 4: Advanced Features
- [ ] Database integration (if chosen)
- [ ] File upload functionality
- [ ] Request logging middleware

### Day 5: Polish & Testing
- [ ] Unit tests
- [ ] Performance optimizations
- [ ] Final documentation and cleanup

## Criteria

### Code Quality
- Clean, readable code structure
- Proper separation of concerns
- Consistent naming conventions
- Appropriate use of async/await

### Functionality
- All core requirements implemented
- Proper HTTP status codes
- Correct API behavior

### Error Handling
- Graceful error responses
- Input validation
- Proper exception handling

### Documentation
- Clear API documentation
- Code comments where necessary
- README with setup instructions


## Resources

- [ASP.NET Core Minimal APIs Documentation](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis)
- [JWT Authentication in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/jwt-authn)

---

**Good luck! Remember to focus on clean, maintainable code and don't hesitate to ask questions during development.**