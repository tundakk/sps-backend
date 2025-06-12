# AI Model Instructions for SPS Backend

> **📖 Source of Truth Protocol** - This document provides essential instructions for AI models working with the SPS Backend codebase to maintain documentation accuracy and consistency.

## 🎯 **Core Principle: Documentation as Source of Truth**

The `docs/` folder contains the **authoritative documentation** for this project. All AI models must:

1. **📚 ALWAYS consult relevant documentation first** before making code changes
2. **✏️ UPDATE documentation immediately** when making changes that affect documented functionality
3. **🔄 MAINTAIN consistency** between code and documentation at all times
4. **📝 DOCUMENT new features** following the established structure

---

## 📂 **Documentation Structure Reference**

```
docs/
├── README.md                           # 📖 Main documentation index
├── api/README.md                       # 🌐 API documentation and standards
├── architecture/README.md              # 🏗️ System design and patterns
├── deployment/README.md                # 🚀 Production deployment guides
├── development/README.md               # 💻 Development workflow and standards
├── features/README.md                  # ⚡ Feature capabilities and usage
├── integration/README.md               # 🔗 External integrations
├── integration/nextauth-integration.md # 🔑 NextAuth setup guide
└── security/
    ├── README.md                       # 🛡️ Security overview
    ├── rate-limiting.md                # 🚫 Rate limiting implementation
    ├── rate-limiting-status.md         # 📊 Rate limiting status
    └── rate-limiting-deployment.md     # 🚀 Rate limiting deployment
```

---

## ⚠️ **MANDATORY PROTOCOL: Before Making Changes**

### 1. **📖 DOCUMENTATION REVIEW (REQUIRED)**
Before modifying ANY code, you MUST:

```markdown
1. Read relevant documentation sections:
   - docs/architecture/README.md (for system design changes)
   - docs/features/README.md (for feature modifications)
   - docs/security/README.md (for security-related changes)
   - docs/api/README.md (for API endpoint changes)
   - docs/development/README.md (for development patterns)

2. Understand existing implementation patterns
3. Check if changes align with documented architecture
4. Verify security implications are documented
```

### 2. **🔍 SPECIFIC DOCUMENTATION AREAS TO CHECK**

| Change Type | Required Documentation Review |
|-------------|------------------------------|
| **API Endpoints** | `docs/api/README.md`, `docs/features/README.md` |
| **Authentication** | `docs/security/README.md`, `docs/integration/nextauth-integration.md` |
| **Rate Limiting** | `docs/security/rate-limiting.md`, `docs/security/rate-limiting-status.md` |
| **Database Changes** | `docs/architecture/README.md`, `docs/development/README.md` |
| **External Services** | `docs/integration/README.md` |
| **Security Features** | `docs/security/README.md`, `docs/deployment/README.md` |
| **Business Logic** | `docs/features/README.md`, `docs/architecture/README.md` |
| **Deployment** | `docs/deployment/README.md` |

---

## ✅ **MANDATORY PROTOCOL: After Making Changes**

### 1. **📝 DOCUMENTATION UPDATE (REQUIRED)**

When you make code changes, you MUST update documentation:

```markdown
✅ UPDATE these files when changes affect them:
- docs/features/README.md → Add/modify feature descriptions
- docs/api/README.md → Update API endpoint documentation
- docs/security/README.md → Update security implementation details
- docs/architecture/README.md → Update system design information
- docs/integration/README.md → Update integration examples
- docs/deployment/README.md → Update deployment procedures
```

### 2. **🔄 DOCUMENTATION UPDATE EXAMPLES**

#### Example 1: Adding New API Endpoint
```markdown
REQUIRED UPDATES:
1. docs/api/README.md → Add endpoint documentation
2. docs/features/README.md → Add feature capability description
3. docs/security/README.md → Update if endpoint has security implications
```

#### Example 2: Modifying Rate Limiting
```markdown
REQUIRED UPDATES:
1. docs/security/rate-limiting.md → Update implementation details
2. docs/security/rate-limiting-status.md → Update current status
3. docs/deployment/README.md → Update if deployment changes needed
```

#### Example 3: Adding New External Service
```markdown
REQUIRED UPDATES:
1. docs/integration/README.md → Add integration guide
2. docs/deployment/README.md → Add configuration requirements
3. docs/security/README.md → Add security considerations
```

---

## 🛡️ **SECURITY DOCUMENTATION REQUIREMENTS**

For ANY security-related changes:

### ⚠️ **CRITICAL: Security Changes Must Update Documentation**

```markdown
MANDATORY SECURITY DOCUMENTATION UPDATES:
1. docs/security/README.md → Update security overview
2. Relevant specific security files (rate-limiting.md, etc.)
3. docs/deployment/README.md → Update production security
4. docs/api/README.md → Update authentication requirements
```

### 🔒 **Security Documentation Standards**

```markdown
When documenting security features:
✅ Include configuration examples
✅ Document security implications
✅ Provide implementation status
✅ Include monitoring/testing procedures
✅ Document threat mitigation
```

---

## 📋 **DOCUMENTATION QUALITY STANDARDS**

### 1. **📝 Writing Standards**
```markdown
✅ Use clear, concise language
✅ Include code examples where relevant
✅ Provide configuration snippets
✅ Add implementation status badges
✅ Cross-reference related documentation
✅ Include troubleshooting guidance
```

### 2. **🔗 Link Maintenance**
```markdown
✅ Verify all internal links work
✅ Update cross-references when moving content
✅ Use relative paths for internal links
✅ Keep link structure consistent
```

### 3. **📊 Status Indicators**
```markdown
Use consistent status indicators:
✅ Active/Implemented
🚧 In Progress
🔲 Planned
⚠️ Needs Attention
❌ Deprecated
```

---

## 🔄 **DOCUMENTATION MAINTENANCE WORKFLOW**

### When Making Changes:

1. **BEFORE CODING:**
   ```markdown
   1. Read relevant docs sections
   2. Understand current implementation
   3. Plan documentation updates needed
   ```

2. **DURING DEVELOPMENT:**
   ```markdown
   1. Code according to documented patterns
   2. Note documentation changes needed
   3. Test implementation against documented behavior
   ```

3. **AFTER IMPLEMENTATION:**
   ```markdown
   1. Update all affected documentation
   2. Verify documentation accuracy
   3. Commit code AND documentation together
   4. Ensure links and cross-references work
   ```

---

## 🚨 **CRITICAL REMINDERS**

### ⚠️ **NEVER:**
- ❌ Make code changes without reading relevant documentation
- ❌ Leave documentation outdated after code changes
- ❌ Add features without documenting them
- ❌ Change security implementations without updating security docs
- ❌ Modify APIs without updating API documentation

### ✅ **ALWAYS:**
- ✅ Read documentation before coding
- ✅ Update documentation with code changes
- ✅ Maintain consistency between code and docs
- ✅ Document new features thoroughly
- ✅ Update security documentation for security changes
- ✅ Verify documentation accuracy after changes

---

## 📞 **Documentation Help**

### 🔍 **Finding Relevant Documentation**
```markdown
Use this decision tree:
1. API changes? → docs/api/README.md
2. Security changes? → docs/security/README.md + specific security files
3. New features? → docs/features/README.md
4. Architecture changes? → docs/architecture/README.md
5. Integration changes? → docs/integration/README.md
6. Deployment changes? → docs/deployment/README.md
```

### 📝 **Documentation Templates**
Follow existing patterns in the documentation for:
- Feature descriptions
- API endpoint documentation
- Security implementation guides
- Configuration examples
- Integration instructions

---

## 🎯 **SUCCESS CRITERIA**

Your implementation is successful when:

✅ **Code works correctly**  
✅ **Documentation is updated and accurate**  
✅ **Documentation reflects all changes made**  
✅ **Security implications are documented**  
✅ **Integration examples are current**  
✅ **Links and cross-references work**  

---

**📖 Remember: Documentation is not optional - it's the source of truth for this project. Keeping it accurate and current is as important as writing correct code.**

---

## 🔗 **Quick Documentation Links**

- [📖 Main Documentation](docs/README.md)
- [🏗️ Architecture Guide](docs/architecture/README.md)
- [🛡️ Security Documentation](docs/security/README.md)
- [🌐 API Documentation](docs/api/README.md)
- [⚡ Features Overview](docs/features/README.md)
- [🔗 Integration Guide](docs/integration/README.md)
- [🚀 Deployment Guide](docs/deployment/README.md)
- [💻 Development Guide](docs/development/README.md)
