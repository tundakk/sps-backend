---
applyTo: '**'
---

# SPS Backend - GitHub Copilot Instructions

## 🎯 Project Overview
SPS Backend is a comprehensive API for managing student support applications (SPSA cases) at Københavns Professionshøjskole. This transforms Excel-based processes into a modern, scalable .NET 8.0 web application with robust security and data management.

## 📖 CRITICAL: Documentation as Source of Truth
**BEFORE making ANY code changes, you MUST:**
1. Read relevant documentation in `docs/` folder
2. Follow established patterns and architectures
3. Update documentation when making changes

**Quick Reference Files:**
- `AI_INSTRUCTIONS.md` - Comprehensive guidelines
- `AI_QUICK_REFERENCE.md` - Fast lookup reference
- `docs/README.md` - Complete documentation index

## 🏗️ Architecture & Patterns

### Multi-Layer Architecture
```
Frontend (Next.js) → sps.API → sps.BLL → sps.DAL → Database
```

**Layer Responsibilities:**
- `sps.API`: Controllers, middleware, configuration, attributes
- `sps.BLL`: Business logic, services, email/SMS, validation
- `sps.DAL`: Repository pattern, EF configurations, migrations
- `sps.Domain.Model`: Entities, DTOs, interfaces, enums

### Design Patterns to Follow
- **Repository Pattern**: All data access through repositories
- **Dependency Injection**: Use ASP.NET Core DI container
- **Middleware Pipeline**: Cross-cutting concerns (auth, rate limiting, exceptions)
- **DTO Pattern**: Never expose entities directly in API

## 🔒 Security Requirements

### Authentication & Authorization
- **JWT Tokens**: All API endpoints require valid JWT
- **Role-Based Access**: Admin, Staff, Teacher, Student roles
- **NextAuth Compatible**: Frontend integration ready

### Rate Limiting (ACTIVE SYSTEM)
- **General API**: 500 requests/minute
- **Auth Endpoints**: 50 requests/5 minutes
- **IP-Based Tracking**: Sliding window algorithm
- **Headers**: X-RateLimit-Limit, X-RateLimit-Remaining, X-RateLimit-Window

### Data Protection
- **Sensitive Data**: AES encryption for PII
- **HTTPS Only**: Enforce secure connections
- **Input Validation**: All inputs validated and sanitized
- **CORS**: Configured for specific origins only

## 💻 Coding Standards

### Naming Conventions
```csharp
// Classes: PascalCase
public class StudentService

// Methods: PascalCase
public async Task<Student> GetStudentByIdAsync(string id)

// Variables: camelCase
var studentId = "123";

// Constants: UPPER_SNAKE_CASE
public const int MAX_RETRY_COUNT = 3;

// Interfaces: IPascalCase
public interface IStudentService
```

### File Organization
- One class per file
- File names match class names exactly
- Use meaningful namespace hierarchies
- Group related classes in appropriate folders

### Async/Await Patterns
```csharp
// All database operations must be async
public async Task<Student> GetStudentAsync(string id)
{
    return await _studentRepository.GetByIdAsync(id);
}

// Use ConfigureAwait(false) in libraries
var result = await SomeAsyncMethod().ConfigureAwait(false);
```

### Error Handling
```csharp
// Use structured exception handling
try 
{
    var result = await _service.ProcessAsync(data);
    return Ok(new ApiResponse<Student> { Status = "success", Data = result });
}
catch (ValidationException ex)
{
    return BadRequest(new ApiResponse { Status = "error", Message = ex.Message });
}
catch (Exception ex)
{
    _logger.LogError(ex, "Unexpected error processing student data");
    return StatusCode(500, new ApiResponse { Status = "error", Message = "Internal server error" });
}
```

## 📊 Database Patterns

### Entity Framework Usage
```csharp
// Always use async methods
var students = await _context.Students
    .Where(s => s.Status == StudentStatus.Active)
    .ToListAsync();

// Use projections for performance
var studentDtos = await _context.Students
    .Select(s => new StudentDto { Id = s.Id, Name = s.Name })
    .ToListAsync();

// Include related data explicitly
var student = await _context.Students
    .Include(s => s.SPSACases)
    .FirstOrDefaultAsync(s => s.Id == id);
```

### Migration Naming
```bash
# Format: YYYYMMDD_DescriptiveName
dotnet ef migrations add 20250612_AddStudentStatusIndex
```

## 🌐 API Design

### Controller Structure
```csharp
[ApiController]
[Route("api/[controller]")]
[Authorize] // All controllers require authentication
public class StudentsController : BaseController
{
    [HttpGet]
    [RateLimit(Limit = 100, Window = "00:01:00")] // Optional override
    public async Task<ActionResult<ApiResponse<PaginatedResponse<StudentDto>>>> GetStudents(
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 10)
    {
        // Implementation
    }
}
```

### Response Format
```csharp
// Success response
return Ok(new ApiResponse<T> 
{ 
    Status = "success", 
    Data = result 
});

// Error response
return BadRequest(new ApiResponse 
{ 
    Status = "error", 
    Message = "Validation failed",
    Errors = new[] { "Email is required", "Password too short" }
});
```

### Swagger Documentation
```csharp
[HttpPost]
[ProducesResponseType(typeof(ApiResponse<StudentDto>), 201)]
[ProducesResponseType(typeof(ApiResponse), 400)]
[ProducesResponseType(401)]
[SwaggerOperation(Summary = "Create new student", Description = "Creates a new student record")]
public async Task<ActionResult> CreateStudent([FromBody] CreateStudentDto dto)
```

## 🧪 Testing Standards

### Unit Test Structure (AAA Pattern)
```csharp
[Test]
public async Task GetStudentById_WithValidId_ReturnsStudent()
{
    // Arrange
    var studentId = "123";
    var expectedStudent = new Student { Id = studentId, Name = "Test" };
    _mockRepo.Setup(r => r.GetByIdAsync(studentId)).ReturnsAsync(expectedStudent);

    // Act
    var result = await _service.GetStudentByIdAsync(studentId);

    // Assert
    Assert.That(result, Is.Not.Null);
    Assert.That(result.Id, Is.EqualTo(studentId));
}
```

### Mock Dependencies
```csharp
// Use Moq for mocking
var mockRepo = new Mock<IStudentRepository>();
var mockLogger = new Mock<ILogger<StudentService>>();
var service = new StudentService(mockRepo.Object, mockLogger.Object);
```

## 📧 External Integrations

### Email Service (Brevo/SendInBlue)
```csharp
public class BrevoEmailSender : IExtendedEmailSender
{
    public async Task SendEmailAsync(string to, string subject, string htmlMessage)
    {
        // Use Brevo API for email sending
    }
}
```

### SMS Service (Twilio)
```csharp
public class TwilioSmsService : ISmsService
{
    public async Task SendSmsAsync(string to, string message)
    {
        // Use Twilio API for SMS
    }
}
```

## 📝 Logging Patterns

### Structured Logging
```csharp
_logger.LogInformation("User {UserId} created SPSA case {CaseId}", userId, caseId);
_logger.LogWarning("Rate limit exceeded for IP {IPAddress}", ipAddress);
_logger.LogError(exception, "Failed to process payment {PaymentId}", paymentId);
```

### Log Levels
- **Trace**: Detailed flow tracing
- **Debug**: Development debugging info
- **Information**: General application flow
- **Warning**: Unexpected but handled situations
- **Error**: Errors and exceptions
- **Critical**: Critical failures

## 🚀 Performance Guidelines

### Database Optimization
```csharp
// Use async for all DB operations
await _context.SaveChangesAsync();

// Implement proper indexing
[Index(nameof(Email), IsUnique = true)]
public class Student

// Avoid N+1 queries
var students = await _context.Students
    .Include(s => s.SPSACases)
    .ToListAsync();
```

### Caching Strategies
```csharp
// Use memory cache for frequently accessed data
var students = await _cache.GetOrCreateAsync("all-students", async entry =>
{
    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
    return await _studentRepository.GetAllAsync();
});
```

## 🔧 Configuration Management

### appsettings.json Structure
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "connection-string"
  },
  "JwtSettings": {
    "SecretKey": "secret",
    "Issuer": "sps-api",
    "Audience": "sps-client",
    "ExpirationInMinutes": 60
  },
  "RateLimiting": {
    "Rules": [
      {
        "EndpointPattern": "api/",
        "Limit": 500,
        "Window": "00:01:00"
      }
    ]
  }
}
```

### Dependency Injection Registration
```csharp
// Services registration in Program.cs
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
```

## ⚠️ Critical Reminders

### Security
- Never expose internal IDs in URLs (use GUIDs)
- Always validate user permissions for resources
- Log security events (failed auth, rate limiting)
- Use HTTPS in production configurations

### Data Integrity
- Always validate input data
- Use transactions for multi-step operations
- Implement soft deletes for important entities
- Maintain audit trails for sensitive operations

### Performance
- Use pagination for list endpoints
- Implement proper caching strategies
- Monitor database query performance
- Use async/await throughout the stack

## 📚 Domain Knowledge

### Core Entities
- **Student**: Core user entity with personal information
- **SPSACase**: Support application cases (main business entity)
- **Teacher**: Faculty members with system access
- **Payment**: Financial transactions and tracking
- **Education**: Academic programs and courses

### Business Rules
- Students can have multiple SPSA cases
- SPSA cases go through approval workflows
- Teachers can be assigned to students
- Payments are linked to approved SPSA cases
- Rate limiting protects against abuse

### Status Workflows
```
SPSA Case: Pending → UnderReview → Approved/Rejected → Completed
Student: Active → Inactive → Graduated
Payment: Pending → Completed/Failed
```

Remember: This is a production system handling sensitive student data. Security, performance, and data integrity are paramount.