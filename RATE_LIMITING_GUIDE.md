# Rate Limiting Implementation Guide

## Overview
This document outlines the comprehensive rate limiting implementation for the ASP.NET Core API, featuring both middleware-based global rate limiting and attribute-based granular rate limiting.

## Implementation Components

### 1. Core Rate Limiting Middleware
**File**: `sps.API/Middleware/RateLimitingMiddleware.cs`
- Implements IP-based rate limiting using sliding window algorithm
- Configurable rate limit rules via appsettings.json
- Automatic rate limit headers (X-RateLimit-Limit, X-RateLimit-Remaining, X-RateLimit-Window)
- Supports multiple rate limit rules for different endpoint patterns

**Configuration**: `sps.API/appsettings.json`
```json
{
  "RateLimiting": {
    "Rules": [
      {
        "EndpointPattern": "api/",
        "Limit": 500,
        "Window": "00:01:00"
      },
      {
        "EndpointPattern": "api/Auth/",
        "Limit": 50,
        "Window": "00:05:00"
      }
    ]
  }
}
```

### 2. Attribute-Based Rate Limiting
**File**: `sps.API/Attributes/RateLimitAttribute.cs`
- Custom ActionFilterAttribute for controller/action-level rate limiting
- Overrides middleware defaults when applied
- Supports IP-based and user-based tracking
- Configurable limit and time window parameters

**Usage Examples**:
```csharp
[RateLimit(5, 5)]    // 5 requests per 5 minutes
[RateLimit(100, 1)]  // 100 requests per minute
[RateLimit(10, 60)]  // 10 requests per hour
```

### 3. Enhanced BaseController
**File**: `sps.API/Controllers/Base/BaseController.cs`
- Ready for default rate limiting attributes
- All API controllers inherit rate limiting capabilities
- Can be overridden at controller or action level

### 4. Demo Controller
**File**: `sps.API/Controllers/Implementations/RateLimitDemoController.cs`
- Comprehensive example showing different rate limiting scenarios
- Controller-level rate limiting (50 requests/minute)
- Action-specific overrides:
  - High-frequency endpoint: 200 requests/minute
  - Sensitive endpoint: 5 requests/5 minutes
  - Bulk operations: 10 requests/hour

## Rate Limiting Strategies

### Authentication Endpoints
Recommended rate limits for security-sensitive endpoints:

1. **Login**: 5 attempts per 5 minutes per IP
   ```csharp
   [HttpPost("login")]
   [RateLimit(5, 5)]
   public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
   ```

2. **Registration**: 10 registrations per hour per IP
   ```csharp
   [HttpPost("register")]
   [RateLimit(10, 60)]
   public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
   ```

3. **Password Reset**: 3 attempts per 10 minutes per IP
   ```csharp
   [HttpPost("reset-password")]
   [RateLimit(3, 10)]
   public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
   ```

4. **Token Refresh**: 100 requests per minute (normal rate)
   ```csharp
   [HttpPost("refresh")]
   // Uses controller-level or default rate limiting
   public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
   ```

### General API Endpoints
- **Read operations**: 100-200 requests per minute
- **Write operations**: 50-100 requests per minute
- **Bulk operations**: 10-20 requests per hour
- **Search/filter**: 50-100 requests per minute

## Testing the Implementation

### 1. Test Rate Limiting Middleware
```bash
# Test general API rate limiting
for i in {1..10}; do curl -v http://localhost:5000/api/RateLimitDemo/status; done

# Check rate limit headers
curl -v http://localhost:5000/api/RateLimitDemo/status | grep -i x-ratelimit
```

### 2. Test Attribute-Based Rate Limiting
```bash
# Test strict rate limiting (5 requests per 5 minutes)
for i in {1..6}; do curl -X POST http://localhost:5000/api/RateLimitDemo/sensitive -H "Content-Type: application/json" -d '{"data":"test"}'; done

# Test high-frequency endpoint (200 requests per minute)
for i in {1..10}; do curl http://localhost:5000/api/RateLimitDemo/high-frequency; done
```

### 3. Monitor Rate Limit Headers
```bash
curl -v http://localhost:5000/api/RateLimitDemo/rate-limit-info
# Look for headers:
# X-RateLimit-Limit: 50
# X-RateLimit-Remaining: 49
# X-RateLimit-Window: 60
```

## Dual Protection Architecture

The implementation provides dual protection:

1. **Middleware Layer**: Global protection for all API endpoints
2. **Attribute Layer**: Granular control for specific actions

### Protection Flow:
1. Request hits rate limiting middleware (checks general API limits)
2. If middleware passes, request continues to controller
3. Controller action filter checks attribute-based limits
4. If both pass, request proceeds to business logic

### Benefits:
- **Defense in Depth**: Multiple layers of protection
- **Flexibility**: Global defaults with specific overrides
- **Performance**: Efficient caching and sliding window algorithm
- **Monitoring**: Comprehensive rate limit headers
- **Security**: Prevents brute force attacks and API abuse

## Configuration Best Practices

### Development Environment
```json
{
  "RateLimiting": {
    "Rules": [
      {
        "EndpointPattern": "api/",
        "Limit": 1000,
        "Window": "00:01:00"
      },
      {
        "EndpointPattern": "api/Auth/login",
        "Limit": 20,
        "Window": "00:05:00"
      }
    ]
  }
}
```

### Production Environment
```json
{
  "RateLimiting": {
    "Rules": [
      {
        "EndpointPattern": "api/",
        "Limit": 500,
        "Window": "00:01:00"
      },
      {
        "EndpointPattern": "api/Auth/",
        "Limit": 50,
        "Window": "00:05:00"
      }
    ]
  }
}
```

## Monitoring and Alerting

### Key Metrics to Monitor:
1. Rate limit violations per endpoint
2. IP addresses hitting rate limits frequently
3. Rate limit header values in responses
4. Performance impact of rate limiting middleware

### Logging Integration:
The rate limiting system logs important events:
- Rate limit violations (Warning level)
- Rate limit checks (Debug level)
- Configuration loading (Information level)

## Future Enhancements

1. **Database-backed Storage**: Replace memory cache with Redis for distributed scenarios
2. **User-based Rate Limiting**: Extend to include authenticated user limits
3. **Dynamic Rate Limits**: Admin interface to modify limits without restart
4. **Whitelist/Blacklist**: IP-based allow/deny lists
5. **Advanced Algorithms**: Token bucket, fixed window, etc.
6. **Analytics Dashboard**: Real-time rate limiting metrics and visualization

## Security Considerations

1. **IP Spoofing**: Validate X-Forwarded-For headers carefully
2. **Distributed Attacks**: Consider user-based limits for authenticated endpoints
3. **Performance Impact**: Monitor middleware performance in high-traffic scenarios
4. **Configuration Security**: Protect rate limiting configuration from unauthorized changes

## Conclusion

This implementation provides a robust, flexible, and scalable rate limiting solution that protects the API from abuse while maintaining excellent performance and user experience. The dual-layer approach ensures comprehensive protection with fine-grained control where needed.
