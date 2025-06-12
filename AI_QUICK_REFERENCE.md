# 🤖 AI Quick Reference Card

> **⚡ Quick lookup for AI models working with SPS Backend**

## 🚨 BEFORE ANY CODE CHANGES

### 1. **📖 READ DOCUMENTATION FIRST**
```markdown
Check these docs based on your task:
├── API changes         → docs/api/README.md
├── Security changes    → docs/security/README.md
├── New features       → docs/features/README.md
├── Architecture       → docs/architecture/README.md
├── Integrations       → docs/integration/README.md
└── Deployment         → docs/deployment/README.md
```

### 2. **🔍 UNDERSTAND CURRENT STATE**
- Read relevant documentation sections completely
- Understand existing patterns and conventions
- Check security implications
- Review integration requirements

## ✅ AFTER CODE CHANGES

### **📝 UPDATE DOCUMENTATION (MANDATORY)**

| Change Type | Update These Docs |
|-------------|------------------|
| **API Endpoint** | `docs/api/README.md` + `docs/features/README.md` |
| **Security Feature** | `docs/security/README.md` + specific security files |
| **Rate Limiting** | `docs/security/rate-limiting*.md` |
| **Authentication** | `docs/security/README.md` + `docs/integration/nextauth-integration.md` |
| **External Service** | `docs/integration/README.md` |
| **Database Change** | `docs/architecture/README.md` + `docs/development/README.md` |
| **Business Logic** | `docs/features/README.md` |
| **Deployment** | `docs/deployment/README.md` |

## 🛡️ SECURITY RULE

**ANY security-related change MUST update:**
1. `docs/security/README.md`
2. Relevant specific security documentation
3. `docs/deployment/README.md` (if production impact)

## 📋 QUALITY CHECKLIST

```markdown
✅ Read relevant documentation before coding
✅ Code follows documented patterns
✅ Updated all affected documentation
✅ Security implications documented
✅ Examples and configurations updated
✅ Links and cross-references work
✅ Commit message mentions doc updates
```

## 🚨 NEVER DO

❌ Code without reading docs first  
❌ Leave documentation outdated  
❌ Skip security documentation updates  
❌ Break documentation links  
❌ Add features without documenting them  

---

**📖 Documentation = Source of Truth. Keep it current!**
