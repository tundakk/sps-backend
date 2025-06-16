# SPS Backend Documentation

[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE.txt)
[![API](https://img.shields.io/badge/API-REST-orange.svg)](docs/api/README.md)
[![Security](https://img.shields.io/badge/security-JWT-red.svg)](docs/security/README.md)

> **SPS Backend** is a comprehensive API solution for managing student support applications (SPSA cases) at Københavns Professionshøjskole, transforming Excel-based processes into a modern, scalable web application.

## 🤖 **For AI Models**

**⚠️ CRITICAL**: This documentation serves as the **source of truth** for this project.

- **📖 [AI Instructions](../AI_INSTRUCTIONS.md)** - Mandatory guidelines for AI models
- **⚡ [AI Quick Reference](../AI_QUICK_REFERENCE.md)** - Fast lookup for documentation requirements

**Before making ANY code changes, read relevant documentation and update it accordingly.**

---

## 📖 Documentation Structure

### 🏗️ **Architecture & Design**
- [**System Architecture**](./architecture/README.md) - Multi-layer architecture overview, design patterns, and security architecture

### 💻 **Development**
- [**Development Guide**](./development/README.md) - Setup, workflow, testing, and coding standards
- [**Tools & Monitoring**](./development/tools-and-monitoring.md) - Performance monitoring and diagnostic tools

### ⚡ **Features**
- [**Features Overview**](./features/README.md) - Complete feature documentation including core functionality, security, and communication

### 🔗 **Integration**
- [**Integration Guide**](./integration/README.md) - Frontend integration, external services, and API guidelines

### 🚢 **Deployment**
- [**Deployment Guide**](./deployment/README.md) - Production deployment, environment setup, and monitoring
- [**Rate Limiting Deployment**](./deployment/rate-limiting-deployment.md) - Rate limiting implementation deployment details

### 🛡️ **Security**
- [**Security Overview**](./security/README.md) - Security architecture and implementation
- [**Rate Limiting**](./security/rate-limiting.md) - Comprehensive rate limiting implementation guide
- [**Rate Limiting Status**](./security/rate-limiting-status.md) - Current implementation status and testing results

### 🌐 **API Documentation**
- [**API Overview**](./api/README.md) - REST API introduction and reference

## 🛠️ **Tech Stack**

| Component | Technology | Version |
|-----------|------------|---------|
| **Framework** | .NET | 8.0 |
| **Database** | SQL Server | Latest |
| **ORM** | Entity Framework Core | 8.0 |
| **Authentication** | JWT / Identity | Core |
| **API Documentation** | Swagger/OpenAPI | 3.0 |
| **Email Service** | Brevo (SendInBlue) | - |
| **SMS Service** | Twilio | - |
| **Testing** | NUnit | Latest |
| **Rate Limiting** | Custom Middleware | - |

## 🚀 **Quick Start**

```bash
# Clone the repository
git clone https://github.com/tundakk/sps-backend.git
cd sps-backend

# Setup database
dotnet ef database update

# Run the application
dotnet run --project sps.API

# Access API documentation
# Navigate to: https://localhost:5001/swagger
```

## 🏗️ **Project Structure**

```
sps-backend/
├── docs/                    # 📖 Documentation (this folder)
├── sps.API/                 # 🌐 API Layer (Controllers, Middleware)
├── sps.BLL/                 # 💼 Business Logic Layer
├── sps.DAL/                 # 🗄️ Data Access Layer
├── sps.Domain.Model/        # 📋 Shared Models & DTOs
├── sps.*.Tests/             # 🧪 Test Projects
└── NextAuthIntegration/     # 🔗 Frontend Integration Examples
```

## 🔑 **Key Features**

- ✅ **Complete Student Management** - Full CRUD operations for students
- ✅ **SPSA Case Processing** - Support case workflows and tracking
- ✅ **JWT Authentication** - Secure API access with NextAuth compatibility
- ✅ **Rate Limiting** - API protection against abuse and attacks
- ✅ **Multi-layer Architecture** - Clean separation of concerns
- ✅ **Comprehensive Testing** - Unit and integration test coverage
- ✅ **API Documentation** - Interactive Swagger documentation
- ✅ **External Integrations** - Email (Brevo) and SMS (Twilio) services

## 📞 **Support & Contact**

For questions, issues, or contributions:

- **Issues**: [GitHub Issues](https://github.com/tundakk/sps-backend/issues)
- **Documentation**: [Full Documentation](docs/)
- **API Reference**: [Swagger UI](https://localhost:5001/swagger) (when running locally)

---

## 📄 **License**

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.

---

**[📖 View Complete Documentation →](docs/)**
