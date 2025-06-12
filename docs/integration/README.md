# Integration Documentation

## Frontend Integration

### NextAuth.js Integration
Complete integration examples for Next.js applications using NextAuth.

**📁 Integration Files:**
- [`NextAuthIntegration/next-auth-examples.ts`](../../NextAuthIntegration/next-auth-examples.ts) - Complete NextAuth configuration examples
- [`NextAuthIntegration/api-client.ts`](../../NextAuthIntegration/api-client.ts) - HTTP client with authentication
- [`NextAuthIntegration/api-types.ts`](../../NextAuthIntegration/api-types.ts) - TypeScript type definitions

**📖 Documentation:**
- [**NextAuth Integration Guide**](./nextauth-integration.md) - Complete setup and implementation guide

#### Setup Instructions

1. **Install Dependencies**
   ```bash
   npm install next-auth
   npm install axios  # or your preferred HTTP client
   ```

2. **Configure NextAuth**
   ```typescript
   // pages/api/auth/[...nextauth].ts
   import NextAuth from 'next-auth'
   import CredentialsProvider from 'next-auth/providers/credentials'
   
   export default NextAuth({
     providers: [
       CredentialsProvider({
         name: 'credentials',
         credentials: {
           email: { label: 'Email', type: 'email' },
           password: { label: 'Password', type: 'password' }
         },
         async authorize(credentials) {
           // Use the provided API client
           const response = await fetch('https://your-api/api/auth/login', {
             method: 'POST',
             headers: { 'Content-Type': 'application/json' },
             body: JSON.stringify(credentials)
           })
           
           if (response.ok) {
             const user = await response.json()
             return user.data
           }
           return null
         }
       })
     ],
     callbacks: {
       async jwt({ token, user }) {
         if (user) {
           token.accessToken = user.token
           token.roles = user.roles
         }
         return token
       },
       async session({ session, token }) {
         session.accessToken = token.accessToken
         session.user.roles = token.roles
         return session
       }
     }
   })
   ```

3. **API Client Configuration**
   ```typescript
   // lib/api-client.ts
   import { getSession } from 'next-auth/react'
   import axios from 'axios'
   
   const apiClient = axios.create({
     baseURL: process.env.NEXT_PUBLIC_API_URL,
     headers: {
       'Content-Type': 'application/json'
     }
   })
   
   apiClient.interceptors.request.use(async (config) => {
     const session = await getSession()
     if (session?.accessToken) {
       config.headers.Authorization = `Bearer ${session.accessToken}`
     }
     return config
   })
   
   export default apiClient
   ```

#### Authentication Flow
1. User submits credentials to NextAuth
2. NextAuth calls backend `/api/auth/login`
3. Backend validates credentials and returns JWT
4. NextAuth stores token in session
5. API client includes token in subsequent requests

---

## External Service Integrations

### Email Service (Brevo/SendInBlue)

**Configuration:**
```json
{
  "EmailSettings": {
    "Provider": "Brevo",
    "ApiKey": "your-brevo-api-key",
    "SenderEmail": "noreply@yourapp.com",
    "SenderName": "SPS System"
  }
}
```

**Usage:**
```csharp
public class EmailService : IExtendedEmailSender
{
    public async Task SendEmailAsync(string to, string subject, string htmlMessage)
    {
        // Brevo implementation
    }
    
    public async Task SendTemplateEmailAsync(string to, int templateId, object templateData)
    {
        // Template-based email sending
    }
}
```

**Available Templates:**
- Case status updates
- Payment confirmations
- Application notifications
- Welcome emails

### SMS Service (Twilio)

**Configuration:**
```json
{
  "TwilioSettings": {
    "AccountSid": "your-twilio-account-sid",
    "AuthToken": "your-twilio-auth-token",
    "FromNumber": "+1234567890"
  }
}
```

**Usage:**
```csharp
public class TwilioSmsService : ISmsService
{
    public async Task SendSmsAsync(string to, string message)
    {
        // Twilio SMS implementation
    }
}
```

---

## Database Integration

### Entity Framework Configuration

**Connection String Setup:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SPSDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

**Migration Commands:**
```bash
# Add migration
dotnet ef migrations add InitialCreate --project sps.DAL --startup-project sps.API

# Update database
dotnet ef database update --project sps.DAL --startup-project sps.API

# Generate SQL script
dotnet ef migrations script --project sps.DAL --startup-project sps.API
```

### Custom Database Providers
The system supports multiple database providers through Entity Framework:

- **SQL Server** (default)
- **PostgreSQL** (with Npgsql.EntityFrameworkCore.PostgreSQL)
- **SQLite** (for development/testing)
- **MySQL** (with Pomelo.EntityFrameworkCore.MySql)

---

## API Integration Guidelines

### Request/Response Format

**Standard Response Structure:**
```json
{
  "status": "success|error",
  "data": { ... },
  "message": "Optional message",
  "errors": ["Optional error array"]
}
```

**Error Response Example:**
```json
{
  "status": "error",
  "message": "Validation failed",
  "errors": [
    "Email is required",
    "Password must be at least 8 characters"
  ]
}
```

### Authentication Headers

**Required Headers:**
```http
Authorization: Bearer <jwt-token>
Content-Type: application/json
```

**Optional Headers:**
```http
X-Client-Version: 1.0.0
X-Request-ID: unique-request-identifier
```

### Rate Limiting Headers

**Response Headers:**
```http
X-RateLimit-Limit: 1000
X-RateLimit-Remaining: 999
X-RateLimit-Window: 60
```

---

## Third-Party Integrations

### Available Webhooks

**SPSA Case Updates:**
```http
POST /webhooks/spsa-case-updated
Content-Type: application/json

{
  "eventType": "case.updated",
  "caseId": "123",
  "status": "approved",
  "timestamp": "2025-06-12T10:30:00Z"
}
```

**Payment Notifications:**
```http
POST /webhooks/payment-completed
Content-Type: application/json

{
  "eventType": "payment.completed",
  "paymentId": "456",
  "amount": 1500.00,
  "currency": "DKK"
}
```

### Integration Best Practices

1. **Authentication**: Always use HTTPS and valid JWT tokens
2. **Error Handling**: Implement retry logic with exponential backoff
3. **Rate Limiting**: Respect rate limits and handle 429 responses
4. **Versioning**: Use API versioning for backward compatibility
5. **Monitoring**: Log integration events and monitor success rates

### Testing Integration

**Development Environment:**
- Use test credentials for external services
- Mock external API responses
- Validate webhook payloads

**Staging Environment:**
- Test with sandbox accounts
- Verify end-to-end workflows
- Performance testing with realistic data

**Production Environment:**
- Monitor integration health
- Set up alerting for failures
- Regular connectivity testing
