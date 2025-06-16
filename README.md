# SPS Backend

## 🤖 For AI Models Working on This Project

**📖 IMPORTANT**: This project uses documentation as the source of truth. Before making any changes:

- **📚 READ**: [AI Instructions](AI_INSTRUCTIONS.md) - Comprehensive guidelines for maintaining documentation
- **⚡ QUICK REF**: [AI Quick Reference](AI_QUICK_REFERENCE.md) - Fast lookup for common tasks
- **📖 DOCS**: [Documentation](docs/README.md) - Complete project documentation

**⚠️ MANDATORY**: Always read relevant documentation before coding and update it when making changes.

---

## Overview
SPS Backend is a comprehensive API solution for managing student support applications (SPSA cases) at Københavns Professionshøjskole. This project transforms a previously complex Excel-based process into a modern, scalable web application with robust security and data management capabilities.

## Technical Architecture

### Core Technologies
- **Framework**: .NET 8.0
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: JWT token authentication (NextAuth compatible)
- **API Documentation**: Swagger/OpenAPI
- **Communication**: Email (Brevo/SendInBlue) and SMS (Twilio)
- **Testing**: NUnit

### Project Structure
The solution follows a multi-layered architecture:

- **sps.API**: API controllers, middleware, and configuration
- **sps.BLL**: Business logic layer with services
- **sps.DAL**: Data access layer with repositories and EF configurations
- **sps.Domain.Model**: Shared models, entities, and DTOs
- **sps.*.Tests**: Corresponding test projects

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server (local or remote instance)
- Visual Studio 2022 or Visual Studio Code

### Setup
1. Clone the repository
2. Update connection strings in `appsettings.json`
3. Apply migrations:
4. Run the project:
5. Access Swagger documentation at `https://localhost:5001/swagger`

### Configuration
Key configuration areas in `appsettings.json`:

- Database connection strings
- JWT authentication settings
- CORS allowed origins
- Email and SMS service credentials
- Encryption keys

## API Features
- Complete student management
- SPSA case processing
- Education tracking
- Teacher and payment management
- Role-based security
- File and document handling
- Diagnosis and support type management
- Comprehensive reporting capabilities

## Security
- JWT token authentication
- Identity framework for user management
- Sensitive data encryption (AES)
- HTTPS enforcement
- CORS protection
- Input validation

## Testing
The codebase includes comprehensive unit tests:

- **sps.BLL.Tests**: Service layer tests
- **sps.DAL.Tests**: Repository and data access tests
- **sps.Base.Tests**: Core functionality tests

Run tests with:

## Frontend Integration
The API is designed to work with a modern frontend application:

- NextAuth integration available in the `NextAuthIntegration` folder
- CORS configured for cross-domain requests
- JWT token handling with cookie support

## Development Tools
The project includes development utilities in the `dev/` folder:

- **Python Scripts**: Code generation utilities for controllers, services, and repositories
- **Utilities**: Text replacement and refactoring tools
- **Documentation**: Usage guides and safety instructions

See [`dev/README.md`](dev/README.md) for detailed information about available development tools.

## Development Guidelines
- Follow existing code style and patterns
- Use dependency injection for all services
- Write unit tests for new functionality
- Use DTOs for all API communication
- Document new endpoints in Swagger
- Use development tools in `dev/` folder for code generation when needed