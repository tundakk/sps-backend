# API Documentation

> **REST API** documentation for the SPS Backend - comprehensive endpoints, authentication, and integration guides.

## 🌐 API Overview

The SPS Backend provides a comprehensive REST API for managing student support applications, with full CRUD operations, authentication, and real-time capabilities.

**Base URL**: `https://localhost:5001/api` (Development)  
**API Version**: v1  
**Documentation**: [Swagger UI](https://localhost:5001/swagger)

## 📋 **API Documentation**

### 🔑 **Authentication**
- [**Authentication Guide**](authentication.md) - JWT token authentication
- [**Authorization**](authorization.md) - Role-based access control
- [**API Keys**](api-keys.md) - Service-to-service authentication

### 📖 **API Reference**
- [**Endpoints Overview**](endpoints.md) - Complete endpoint reference
- [**Data Models**](models.md) - Request/response schemas
- [**Error Handling**](errors.md) - Error codes and responses
- [**Status Codes**](status-codes.md) - HTTP status code meanings

### 🔍 **Specific Endpoints**
- [**Authentication Endpoints**](endpoints/auth.md) - Login, register, token management
- [**Student Management**](endpoints/students.md) - Student CRUD operations
- [**SPSA Cases**](endpoints/spsa-cases.md) - Support case management
- [**Education**](endpoints/education.md) - Educational programs and periods
- [**Payments**](endpoints/payments.md) - Student and teacher payments
- [**Reports**](endpoints/reports.md) - Analytics and reporting

### 📊 **Data & Schema**
- [**Database Schema**](schema.md) - Entity relationships
- [**DTO Reference**](dtos.md) - Data transfer objects
- [**Validation Rules**](validation.md) - Input validation requirements

### 🔧 **Integration**
- [**Client Libraries**](clients.md) - SDKs and client examples
- [**Webhooks**](webhooks.md) - Event notifications
- [**Rate Limiting**](rate-limiting.md) - API usage limits

## 🚀 **Quick Start**

### 1. **Authentication**
```bash
# Login to get JWT token
curl -X POST "https://localhost:5001/api/Auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "password123"
  }'
```

### 2. **Use Token**
```bash
# Make authenticated request
curl -X GET "https://localhost:5001/api/Students" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN"
```

### 3. **Check Rate Limits**
```bash
# Monitor rate limit headers
curl -v "https://localhost:5001/api/Students" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN"
  
# Response headers:
# X-RateLimit-Limit: 500
# X-RateLimit-Remaining: 499
# X-RateLimit-Window: 60
```

## 📊 **API Status**

### ✅ **Available Endpoints**

| Category | Endpoints | Status | Rate Limit |
|----------|-----------|---------|------------|
| **Authentication** | `/api/Auth/*` | ✅ Active | 50/5min |
| **Students** | `/api/Students/*` | ✅ Active | 500/min |
| **SPSA Cases** | `/api/SpsaCases/*` | ✅ Active | 500/min |
| **Education** | `/api/Education*/*` | ✅ Active | 500/min |
| **Payments** | `/api/*Payments/*` | ✅ Active | 500/min |
| **Reports** | `/api/Reports/*` | 🚧 Planned | TBD |

### 🛡️ **Security Features**

- ✅ **JWT Authentication** - Required for all protected endpoints
- ✅ **Rate Limiting** - Global and endpoint-specific limits
- ✅ **Input Validation** - Comprehensive request validation
- ✅ **HTTPS Only** - All communications encrypted
- ✅ **CORS Protection** - Controlled cross-origin access

## 📝 **API Examples**

### **Authentication Flow**
```javascript
// 1. Login
const response = await fetch('/api/Auth/login', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    email: 'user@example.com',
    password: 'password123'
  })
});

const { token } = await response.json();

// 2. Use token for subsequent requests
const studentsResponse = await fetch('/api/Students', {
  headers: { 'Authorization': \`Bearer \${token}\` }
});
```

### **Student Management**
```javascript
// Create a new student
const newStudent = await fetch('/api/Students', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json',
    'Authorization': \`Bearer \${token}\`
  },
  body: JSON.stringify({
    firstName: 'John',
    lastName: 'Doe',
    email: 'john.doe@example.com',
    cprNumber: '1234567890'
  })
});
```

### **Error Handling**
```javascript
try {
  const response = await fetch('/api/Students', {
    headers: { 'Authorization': \`Bearer \${token}\` }
  });
  
  if (!response.ok) {
    const error = await response.json();
    console.error('API Error:', error);
    // Handle different error types
    switch (response.status) {
      case 401: // Unauthorized
      case 403: // Forbidden
      case 429: // Rate Limited
      case 500: // Server Error
    }
  }
} catch (error) {
  console.error('Network Error:', error);
}
```

## 🔧 **Development Tools**

### **Swagger/OpenAPI**
- **Interactive Documentation**: `https://localhost:5001/swagger`
- **API Schema**: `https://localhost:5001/swagger/v1/swagger.json`
- **Try It Out**: Test endpoints directly in browser

### **Postman Collection**
```bash
# Import Postman collection (when available)
# Collection includes all endpoints with examples
```

### **Testing**
```bash
# Run API tests
dotnet test sps.API.Tests

# Integration tests
dotnet test --filter Category=Integration
```

## 📈 **API Metrics**

### **Rate Limiting**
Monitor API usage via response headers:
- `X-RateLimit-Limit`: Maximum requests allowed
- `X-RateLimit-Remaining`: Requests remaining in window
- `X-RateLimit-Window`: Time window in seconds

### **Performance**
- **Average Response Time**: < 200ms
- **Error Rate**: < 1%
- **Uptime**: 99.9%

## 🚨 **Common Issues**

### **401 Unauthorized**
```json
{
  "message": "Authentication required",
  "code": "UNAUTHORIZED"
}
```
**Solution**: Include valid JWT token in Authorization header

### **429 Too Many Requests**
```json
{
  "message": "Rate limit exceeded",
  "code": "RATE_LIMIT_EXCEEDED"
}
```
**Solution**: Reduce request frequency, check rate limit headers

### **400 Bad Request**
```json
{
  "message": "Validation failed",
  "code": "VALIDATION_ERROR",
  "errors": {
    "email": ["Invalid email format"]
  }
}
```
**Solution**: Check request format and required fields

---

## 🔗 **Related Documentation**

- [**Security**](../security/README.md) - API security features
- [**Integration**](../integration/README.md) - Frontend integration guides
- [**Development**](../development/README.md) - API development guidelines

---

**📖 For interactive API documentation, visit the [Swagger UI](https://localhost:5001/swagger) when the application is running.**
