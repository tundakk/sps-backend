# Tools & Monitoring

This section describes the available tools and monitoring capabilities in the SPS Backend solution.

## Database Performance Monitoring

The system includes integrated database performance monitoring using MiniProfiler. This tool helps identify slow SQL queries, inefficient data access patterns, and potential optimization opportunities.

- [Database Monitoring Documentation](./database-monitoring.md)

### Key Features

- Real-time SQL query profiling
- Query execution time tracking
- Query parameter visualization
- Connection tracking
- Administrative dashboard

## Logging & Diagnostics

The application uses structured logging to capture important events, errors, and diagnostic information.

### Log Levels

- **Trace**: Detailed flow tracing (development only)
- **Debug**: Development debugging info
- **Information**: General application flow
- **Warning**: Unexpected but handled situations
- **Error**: Errors and exceptions
- **Critical**: Critical failures

### Accessing Logs

Logs are written to:
- Console (development)
- Application log files
- Azure Application Insights (production)
