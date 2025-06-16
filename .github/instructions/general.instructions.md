---
applyTo: '**'
---

# General AI Assistant Instructions for SPS Backend

## 🎯 Primary Directive
**DOCUMENTATION FIRST**: Always read relevant documentation in `docs/` before making changes and update it afterwards.

## 📖 Essential Reading
Before ANY code changes:
1. **`AI_INSTRUCTIONS.md`** - Comprehensive guidelines
2. **`AI_QUICK_REFERENCE.md`** - Fast lookup reference  
3. **`docs/README.md`** - Complete documentation structure
4. **Relevant `docs/` sections** - Based on change type

## 🏗️ Architecture Compliance
- **Multi-layer pattern**: API → BLL → DAL → Database
- **Repository pattern** for data access
- **Dependency injection** throughout
- **DTO pattern** for API communication
- **Async/await** for all I/O operations

## 🔒 Security First
- **JWT authentication** required for all endpoints
- **Rate limiting** is active (500/min general, 50/5min auth)
- **Input validation** on all user data
- **HTTPS enforcement** in production
- **Role-based authorization** (Admin, Staff, Teacher, Student)

## 📝 Code Quality
- **One class per file** with matching names
- **PascalCase** for classes and methods
- **camelCase** for variables
- **Comprehensive error handling** with proper logging
- **Unit tests** for all business logic (AAA pattern)

## 🌐 API Standards
- **RESTful design** principles
- **Consistent response format**: `{ status, data, message, errors }`
- **Proper HTTP status codes**
- **Swagger documentation** for all endpoints
- **Pagination** for list endpoints

## 💾 Database Best Practices
- **Entity Framework async methods** only
- **Include related data** explicitly
- **Use projections** for performance
- **Proper indexing** on frequently queried fields
- **Migrations** with descriptive names

## 🧪 Testing Requirements
- **AAA pattern** (Arrange, Act, Assert)
- **Mock external dependencies** with Moq
- **Test business logic** thoroughly
- **Cover error scenarios**
- **Integration tests** for critical workflows

## 📧 External Services
- **Brevo/SendInBlue** for email notifications
- **Twilio** for SMS communications
- **NextAuth integration** ready for frontend
- **Proper error handling** for service failures

## 🚨 Critical Rules
❌ **NEVER:**
- Make changes without reading relevant docs
- Leave documentation outdated after changes
- Expose entities directly in API responses
- Skip input validation
- Ignore security implications
- Use blocking I/O operations

✅ **ALWAYS:**
- Read documentation before coding
- Update documentation after changes
- Use async/await for I/O
- Validate user inputs
- Handle errors gracefully
- Log important events
- Follow established patterns

## 📊 Performance Considerations
- **Async operations** for all database calls
- **Caching** for frequently accessed data
- **Pagination** to limit response sizes
- **Query optimization** to avoid N+1 problems
- **Connection pooling** for database access

Remember: This is a production system handling sensitive student data. Quality, security, and documentation accuracy are essential.
