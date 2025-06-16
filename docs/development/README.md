# Development Documentation

## Getting Started

### Prerequisites
- **.NET 8.0 SDK**: [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server**: Local instance or connection to remote server
- **IDE**: Visual Studio 2022 (recommended) or Visual Studio Code
- **Git**: For version control

### Initial Setup

1. **Clone the Repository**
   ```bash
   git clone <repository-url>
   cd sps-backend
   ```

2. **Configure Database**
   - Update connection strings in `appsettings.json` and `appsettings.Development.json`
   - Ensure SQL Server is running and accessible

3. **Apply Database Migrations**
   ```bash
   dotnet ef database update --project sps.DAL --startup-project sps.API
   ```

4. **Install Dependencies**
   ```bash
   dotnet restore
   ```

5. **Run the Application**
   ```bash
   dotnet run --project sps.API
   ```

6. **Access Documentation**
   - Swagger UI: `https://localhost:5001/swagger`
   - API endpoints: `https://localhost:5001/api/`

## Development Workflow

### Branch Strategy
- **main**: Production-ready code
- **feature/***: New feature development
- **bugfix/***: Bug fixes
- **hotfix/***: Critical production fixes

### Code Standards

#### Naming Conventions
- **Classes**: PascalCase (`StudentService`)
- **Methods**: PascalCase (`GetStudentById`)
- **Variables**: camelCase (`studentId`)
- **Constants**: UPPER_SNAKE_CASE (`MAX_RETRY_COUNT`)
- **Interfaces**: IPascalCase (`IStudentService`)

#### File Organization
- One class per file
- File names match class names
- Group related classes in appropriate folders
- Use meaningful namespace hierarchies

### Testing Guidelines

#### Unit Tests
- Write tests for all business logic
- Use AAA pattern (Arrange, Act, Assert)
- Mock external dependencies
- Aim for high code coverage

#### Running Tests
```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test sps.BLL.Tests

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### Database Development

#### Entity Framework Migrations
```bash
# Add new migration
dotnet ef migrations add MigrationName --project sps.DAL --startup-project sps.API

# Update database
dotnet ef database update --project sps.DAL --startup-project sps.API

# Remove last migration (if not applied)
dotnet ef migrations remove --project sps.DAL --startup-project sps.API
```

#### Database Seeding
- Use `SeedData.cs` for initial data
- Include test data for development environment
- Ensure production seeds are safe to run multiple times

### Configuration Management

#### Environment-Specific Settings
- `appsettings.json`: Base configuration
- `appsettings.Development.json`: Development overrides
- `appsettings.Production.json`: Production settings
- Environment variables: Sensitive data in production

#### Adding New Configuration
1. Add properties to appropriate section in `appsettings.json`
2. Create strongly-typed configuration class
3. Register in `Program.cs` with dependency injection
4. Inject into services that need the configuration

### API Development

#### Creating New Controllers
1. Inherit from `BaseController`
2. Use appropriate HTTP method attributes
3. Return consistent response types
4. Add Swagger documentation attributes
5. Implement proper error handling

#### Adding New Endpoints
1. Define DTOs for request/response
2. Implement business logic in service layer
3. Add controller action
4. Write unit tests
5. Update API documentation

### Debugging

#### Common Issues
- **Connection String**: Verify database connectivity
- **Migrations**: Ensure migrations are applied
- **Dependencies**: Check NuGet package versions
- **CORS**: Verify frontend URL in allowed origins

#### Logging
- Use `ILogger<T>` for structured logging
- Log levels: Trace, Debug, Information, Warning, Error, Critical
- Include relevant context in log messages

### Performance Considerations

#### Database Queries
- Use async/await for database operations
- Implement proper indexing
- Avoid N+1 query problems
- Use projection for large datasets

#### Caching
- Implement response caching where appropriate
- Use memory cache for frequently accessed data
- Consider distributed caching for scalability

## Code Review Guidelines

### What to Look For
- Code follows established patterns
- Business logic is in appropriate layer
- Error handling is comprehensive
- Tests cover new functionality
- Documentation is updated
- Performance implications considered

### Review Process
1. Create pull request with descriptive title
2. Include summary of changes
3. Reference related issues/tickets
4. Ensure CI/CD checks pass
5. Address review feedback promptly

## Additional Resources

- [Tools & Monitoring](./tools-and-monitoring.md) - Performance monitoring and diagnostic tools
