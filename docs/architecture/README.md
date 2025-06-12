# Architecture Documentation

## System Architecture

### Multi-Layer Architecture
The SPS Backend follows a clean, multi-layered architecture pattern:

```
┌─────────────────┐
│   Frontend      │ ← Next.js / React
│   (External)    │
└─────────────────┘
         │
┌─────────────────┐
│   sps.API       │ ← Controllers, Middleware, Configuration
└─────────────────┘
         │
┌─────────────────┐
│   sps.BLL       │ ← Business Logic, Services
└─────────────────┘
         │
┌─────────────────┐
│   sps.DAL       │ ← Data Access, Repositories, EF Context
└─────────────────┘
         │
┌─────────────────┐
│ sps.Domain.Model│ ← Entities, DTOs, Models
└─────────────────┘
         │
┌─────────────────┐
│   Database      │ ← SQL Server
└─────────────────┘
```

## Project Details

### sps.API (Presentation Layer)
- **Purpose**: HTTP API endpoints and request handling
- **Components**:
  - Controllers (Base and Implementations)
  - Middleware (Authentication, Rate Limiting, Exception Handling)
  - Configuration classes
  - Attributes for cross-cutting concerns

### sps.BLL (Business Logic Layer)
- **Purpose**: Core business rules and application logic
- **Components**:
  - Service interfaces and implementations
  - Business rule validation
  - Email and SMS services
  - Encryption and security utilities

### sps.DAL (Data Access Layer)
- **Purpose**: Database operations and data persistence
- **Components**:
  - Repository pattern implementations
  - Entity Framework configurations
  - Database migrations
  - Data model converters

### sps.Domain.Model (Domain Layer)
- **Purpose**: Shared contracts and data structures
- **Components**:
  - Entity models
  - Data Transfer Objects (DTOs)
  - Response models
  - Service interfaces
  - Value objects and enums

## Design Patterns

### Repository Pattern
- Abstracts data access logic
- Enables unit testing with mock implementations
- Centralizes database query logic

### Dependency Injection
- Promotes loose coupling
- Facilitates testing and maintenance
- Managed through ASP.NET Core's built-in container

### Middleware Pipeline
- Cross-cutting concerns handled declaratively
- Authentication, rate limiting, exception handling
- Configurable and composable

## Database Design

### Key Entities
- **Students**: Core student information
- **SPSA Cases**: Support application cases
- **Teachers**: Faculty and staff
- **Payments**: Financial tracking
- **Education**: Academic program information
- **Diagnosis**: Support type classification

### Relationships
- Students have multiple SPSA cases
- SPSA cases reference education programs
- Teachers can be associated with multiple students
- Payments are linked to SPSA cases

## Security Architecture

### Authentication Flow
1. User credentials validated against database
2. JWT token generated with user claims
3. Token included in subsequent requests
4. Middleware validates token on protected endpoints

### Rate Limiting
- IP-based request tracking
- Configurable rules per endpoint pattern
- Sliding window algorithm
- Headers provide client feedback

## Integration Points

### External Services
- **Brevo/SendInBlue**: Email notifications
- **Twilio**: SMS communications
- **Frontend Applications**: NextAuth integration

### API Contracts
- RESTful design principles
- Swagger/OpenAPI documentation
- Consistent response formats
- Proper HTTP status codes
