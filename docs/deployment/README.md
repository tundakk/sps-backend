# Deployment Documentation

## Environment Setup

### Production Deployment

#### Prerequisites
- Windows Server 2019+ or Linux (Ubuntu 20.04+)
- .NET 8.0 Runtime
- SQL Server 2019+ (or Azure SQL Database)
- IIS (Windows) or Nginx/Apache (Linux)
- SSL Certificate

#### Deployment Steps

1. **Prepare Application**
   ```bash
   # Build for production
   dotnet publish -c Release -o ./publish
   
   # Copy files to server
   scp -r ./publish user@server:/var/www/sps-api
   ```

2. **Configure IIS (Windows)**
   ```xml
   <!-- web.config -->
   <?xml version="1.0" encoding="utf-8"?>
   <configuration>
     <system.webServer>
       <handlers>
         <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModuleV2" resourceType="Unspecified" />
       </handlers>
       <aspNetCore processPath="dotnet" arguments=".\sps.API.dll" stdoutLogEnabled="false" stdoutLogFile=".\logs\stdout" />
     </system.webServer>
   </configuration>
   ```

3. **Configure Nginx (Linux)**
   ```nginx
   server {
       listen 80;
       server_name yourdomain.com;
       location / {
           proxy_pass http://localhost:5000;
           proxy_http_version 1.1;
           proxy_set_header Upgrade $http_upgrade;
           proxy_set_header Connection keep-alive;
           proxy_set_header Host $host;
           proxy_cache_bypass $http_upgrade;
           proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
           proxy_set_header X-Forwarded-Proto $scheme;
       }
   }
   ```

4. **Database Setup**
   ```bash
   # Apply migrations
   dotnet ef database update --project sps.DAL --startup-project sps.API
   
   # Or use SQL script
   sqlcmd -S server -d database -i migration-script.sql
   ```

5. **Configure Application Settings**
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=prod-server;Database=SPSProd;Integrated Security=true;"
     },
     "JwtSettings": {
       "SecretKey": "production-secret-key-32-characters-minimum",
       "Issuer": "https://yourdomain.com",
       "Audience": "https://yourdomain.com",
       "ExpirationMinutes": 60
     },
     "Logging": {
       "LogLevel": {
         "Default": "Warning",
         "Microsoft.AspNetCore": "Warning"
       }
     }
   }
   ```

### Docker Deployment

#### Dockerfile
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["sps.API/sps.API.csproj", "sps.API/"]
COPY ["sps.BLL/sps.BLL.csproj", "sps.BLL/"]
COPY ["sps.DAL/sps.DAL.csproj", "sps.DAL/"]
COPY ["sps.Domain.Model/sps.Domain.Model.csproj", "sps.Domain.Model/"]
RUN dotnet restore "sps.API/sps.API.csproj"
COPY . .
WORKDIR "/src/sps.API"
RUN dotnet build "sps.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "sps.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sps.API.dll"]
```

#### Docker Compose
```yaml
version: '3.8'

services:
  api:
    build: .
    ports:
      - "5000:80"
      - "5001:443"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ConnectionStrings__DefaultConnection=Server=db;Database=SPSProd;User=sa;Password=YourPassword123!;
    depends_on:
      - db
    volumes:
      - ./logs:/app/logs

  db:
    image: mcr.microsoft.com/mssql/server:2019-latest
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourPassword123!
    ports:
      - "1433:1433"
    volumes:
      - sqlserver_data:/var/opt/mssql

volumes:
  sqlserver_data:
```

### Azure Deployment

#### App Service Configuration
```json
{
  "name": "sps-backend-api",
  "location": "West Europe",
  "sku": "B1",
  "runtime": ".NET 8",
  "connectionStrings": {
    "DefaultConnection": "Server=tcp:server.database.windows.net;Database=spsdb;Authentication=Active Directory Default;"
  }
}
```

#### GitHub Actions Workflow
```yaml
name: Deploy to Azure

on:
  push:
    branches: [ main ]

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v2
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: '8.0.x'
        
    - name: Restore dependencies
      run: dotnet restore
      
    - name: Build
      run: dotnet build --no-restore -c Release
      
    - name: Test
      run: dotnet test --no-build --verbosity normal
      
    - name: Publish
      run: dotnet publish -c Release -o ./publish
      
    - name: Deploy to Azure
      uses: azure/webapps-deploy@v2
      with:
        app-name: 'sps-backend-api'
        publish-profile: ${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
        package: './publish'
```

## Environment Configuration

### Development
```json
{
  "Environment": "Development",
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Information"
    }
  },
  "AllowedHosts": "*",
  "CORS": {
    "AllowedOrigins": [ "http://localhost:3000", "http://localhost:3001" ]
  }
}
```

### Staging
```json
{
  "Environment": "Staging",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "staging.yourdomain.com",
  "CORS": {
    "AllowedOrigins": [ "https://staging-frontend.yourdomain.com" ]
  }
}
```

### Production
```json
{
  "Environment": "Production",
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Error"
    }
  },
  "AllowedHosts": "yourdomain.com",
  "CORS": {
    "AllowedOrigins": [ "https://frontend.yourdomain.com" ]
  }
}
```

## Security Considerations

### Production Security Checklist
- [ ] HTTPS enforced
- [ ] Secure JWT secret keys
- [ ] Database credentials encrypted
- [ ] CORS properly configured
- [ ] Rate limiting enabled
- [ ] Security headers configured
- [ ] Application insights/monitoring enabled
- [ ] Error details hidden from clients
- [ ] Input validation implemented
- [ ] SQL injection protection verified

### Environment Variables
```bash
# Sensitive configuration via environment variables
export ConnectionStrings__DefaultConnection="Server=...;Database=...;"
export JwtSettings__SecretKey="your-secure-secret-key"
export EmailSettings__ApiKey="your-email-api-key"
export TwilioSettings__AuthToken="your-twilio-token"
```

## Monitoring and Logging

### Application Insights (Azure)
```json
{
  "ApplicationInsights": {
    "InstrumentationKey": "your-instrumentation-key",
    "EnableAdaptiveSampling": true
  }
}
```

### Structured Logging
```csharp
logger.LogInformation("User {UserId} created SPSA case {CaseId}", 
    userId, caseId);
    
logger.LogWarning("Rate limit exceeded for IP {IPAddress}, endpoint {Endpoint}", 
    ipAddress, endpoint);
    
logger.LogError(exception, "Failed to process payment {PaymentId}", 
    paymentId);
```

### Health Checks
```csharp
builder.Services.AddHealthChecks()
    .AddSqlServer(connectionString)
    .AddCheck<EmailServiceHealthCheck>("email")
    .AddCheck<SmsServiceHealthCheck>("sms");

app.MapHealthChecks("/health");
```

## Backup and Recovery

### Database Backup
```sql
-- Full backup
BACKUP DATABASE SPSProd TO DISK = 'C:\Backups\SPSProd_Full.bak'

-- Differential backup
BACKUP DATABASE SPSProd TO DISK = 'C:\Backups\SPSProd_Diff.bak'
WITH DIFFERENTIAL

-- Transaction log backup
BACKUP LOG SPSProd TO DISK = 'C:\Backups\SPSProd_Log.trn'
```

### Application Files Backup
```bash
# Backup application files
tar -czf sps-api-backup-$(date +%Y%m%d).tar.gz /var/www/sps-api

# Backup configuration
cp /var/www/sps-api/appsettings.Production.json ./config-backup/
```

## Performance Optimization

### Production Settings
```json
{
  "Kestrel": {
    "Limits": {
      "MaxConcurrentConnections": 100,
      "MaxRequestBodySize": 10485760
    }
  },
  "ResponseCaching": {
    "Enabled": true,
    "DefaultExpirationMinutes": 30
  }
}
```

### Database Optimization
- Implement proper indexing
- Use connection pooling
- Enable query plan caching
- Monitor slow queries
- Implement read replicas for reporting
