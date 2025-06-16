# Database Performance Monitoring Documentation

## Overview

This document describes the implementation of database performance monitoring in the SPS Backend solution using MiniProfiler, a lightweight profiling tool that helps identify and resolve performance issues in SQL queries and database operations.

## Implementation Details

### Packages Used

The implementation uses the following NuGet packages:

- **MiniProfiler.AspNetCore.Mvc**: For ASP.NET Core integration, including UI and middleware
- **MiniProfiler.EntityFrameworkCore**: For Entity Framework Core profiling

### Configuration

Database monitoring is configured in `appsettings.json` under the `DatabaseMonitoring` section:

```json
"DatabaseMonitoring": {
  "Enabled": true,
  "RetentionDays": 7,
  "MinimumSqlLength": 100,
  "ResultsAuthorized": true,
  "ShowParameters": true,
  "TrackConnectionOpenClose": true
}
```

#### Configuration Options

- **Enabled**: Toggle to enable/disable database profiling
- **RetentionDays**: Number of days to retain profiling data
- **ResultsAuthorized**: When true, only admins or development environments can access profiler results
- **ShowParameters**: Show SQL parameters in profiling output
- **TrackConnectionOpenClose**: Track database connection open/close events

### Integration Points

1. **Program.cs**: 
   - Configures MiniProfiler services with `.AddMiniProfiler().AddEntityFramework()`
   - Sets up authorization and retention policies
   - Adds MiniProfiler middleware with `app.UseMiniProfiler()`
   - Utilizes MiniProfiler's native Entity Framework Core integration (no custom interceptors needed)

2. **Controllers/Implementations/MonitoringController.cs**:
   - Provides endpoints to access performance metrics
   - Restricts access to admin users only

### How It Works

1. When a request arrives, MiniProfiler begins timing the request
2. EF Core database calls are automatically intercepted and profiled using MiniProfiler's built-in Entity Framework Core integration
3. Timing information is collected and stored in memory cache
4. The profiler UI can be accessed at `/profiler/results-index`

## Usage

### Accessing the Profiler UI

In development mode, the profiler UI can be accessed at:
- `/profiler/results-index` - List of recent profile sessions
- `/profiler/results?id={guid}` - Details for a specific profile session

In production, only users with the Admin role can access these endpoints.

### Interpreting Results

The profiler shows:
- Total request time
- Database query time
- Individual SQL queries
- Time spent per query
- Call stack for each query

### Monitoring API

The MonitoringController provides endpoints to programmatically access profiling data:
- `GET /api/Monitoring/database/summary` - Get summary stats for the current request
- `GET /api/Monitoring/database/dashboard` - Get URL to the profiler dashboard

## Best Practices

1. Use MiniProfiler in development to identify slow queries
2. Look for N+1 query problems in related entity loading
3. Optimize identified slow queries using proper LINQ constructs
4. Consider adding indexes for frequently queried fields
5. Use profiler results to guide performance optimizations

## References

- [MiniProfiler Documentation](https://miniprofiler.com/dotnet/)
- [Entity Framework Core Profiling](https://miniprofiler.com/dotnet/HowTo/ProfileEFCore)
