# Security Architecture Recommendations

This document provides an overview of the recommended security architecture for the ghagent-showcase application.

---

## Current Architecture (Insecure)

```
┌─────────────────────────────────────────────────────────────┐
│                         Browser                             │
│                    http://localhost:5173                    │
└──────────────────────┬──────────────────────────────────────┘
                       │
                       │ HTTP (unencrypted)
                       │ CORS: AllowAnyMethod, AllowAnyHeader
                       │ No rate limiting
                       ▼
┌─────────────────────────────────────────────────────────────┐
│                    Backend API                              │
│                 http://localhost:5000                       │
│                                                             │
│  ❌ No HTTPS                                                │
│  ❌ No Security Headers                                     │
│  ❌ No Rate Limiting                                        │
│  ❌ Overly Permissive CORS                                  │
│  ❌ Hardcoded URLs                                          │
└─────────────────────────────────────────────────────────────┘

Issues:
- All traffic in plaintext (vulnerable to MITM attacks)
- No protection against DoS
- Too many allowed HTTP methods
- Configuration not environment-aware
```

---

## Recommended Architecture (Secure)

```
┌─────────────────────────────────────────────────────────────┐
│                         Browser                             │
│                   https://localhost:5173                    │
│                                                             │
│  ✅ CSP Headers Enforced                                    │
│  ✅ Secure Cookies (httpOnly, secure, sameSite)             │
│  ✅ Environment-based Config                                │
└──────────────────────┬──────────────────────────────────────┘
                       │
                       │ HTTPS (TLS 1.2+)
                       │ Encrypted traffic
                       ▼
┌─────────────────────────────────────────────────────────────┐
│                     Security Layer                          │
│                                                             │
│  ✅ Rate Limiter (100 req/min per IP)                       │
│  ✅ CORS Policy (Specific methods/headers only)             │
│  ✅ HTTPS Redirection                                       │
│  ✅ Security Headers Middleware                             │
└──────────────────────┬──────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────┐
│                    Backend API                              │
│                https://localhost:5001                       │
│                                                             │
│  ┌─────────────────────────────────────────────┐           │
│  │            Request Pipeline                 │           │
│  │                                             │           │
│  │  1. HTTPS Redirection                       │           │
│  │  2. HSTS Header                             │           │
│  │  3. Security Headers                        │           │
│  │  4. CORS Policy Check                       │           │
│  │  5. Rate Limiting                           │           │
│  │  6. Input Validation (FluentValidation)     │           │
│  │  7. Controller → Service → Repository       │           │
│  │  8. Structured Logging (Serilog)            │           │
│  │  9. Exception Handling Middleware           │           │
│  └─────────────────────────────────────────────┘           │
│                                                             │
└─────────────────────────────────────────────────────────────┘
```

---

## Security Layers Explained

### 1️⃣ Transport Security

```
┌──────────────────────────────────────┐
│         TLS/HTTPS Layer              │
├──────────────────────────────────────┤
│ • TLS 1.2 or higher                  │
│ • Strong cipher suites               │
│ • HSTS enabled (max-age: 31536000)   │
│ • Automatic HTTP → HTTPS redirect    │
└──────────────────────────────────────┘
```

**Implementation:**
- Configure HTTPS in `launchSettings.json`
- Add `app.UseHttpsRedirection()` and `app.UseHsts()`
- Use valid certificates (self-signed for dev, CA-signed for prod)

---

### 2️⃣ Request Filtering

```
┌──────────────────────────────────────┐
│      CORS & Rate Limiting            │
├──────────────────────────────────────┤
│ CORS:                                │
│  ✓ Allowed Origins: Whitelist only   │
│  ✓ Methods: GET, POST, OPTIONS       │
│  ✓ Headers: Content-Type, Accept     │
│                                      │
│ Rate Limiting:                       │
│  ✓ 100 requests per minute per IP    │
│  ✓ Returns 429 when exceeded         │
│  ✓ Sliding/Fixed window algorithm    │
└──────────────────────────────────────┘
```

**Implementation:**
- Restrict CORS to specific methods and headers
- Use ASP.NET Core rate limiter middleware
- Configure per-endpoint limits if needed

---

### 3️⃣ Security Headers

```
┌─────────────────────────────────────────────────────┐
│                Security Headers                     │
├─────────────────────────────────────────────────────┤
│ X-Content-Type-Options: nosniff                     │
│   → Prevents MIME-sniffing attacks                  │
│                                                     │
│ X-Frame-Options: DENY                               │
│   → Prevents clickjacking                           │
│                                                     │
│ X-XSS-Protection: 1; mode=block                     │
│   → Enables browser XSS filter                      │
│                                                     │
│ Referrer-Policy: strict-origin-when-cross-origin    │
│   → Controls referrer information                   │
│                                                     │
│ Content-Security-Policy: ...                        │
│   → Controls resource loading                       │
│   → Prevents XSS and data injection                 │
└─────────────────────────────────────────────────────┘
```

---

### 4️⃣ Input Validation

```
┌──────────────────────────────────────┐
│        Validation Layer              │
├──────────────────────────────────────┤
│ Client (TypeScript):                 │
│  • Type checking                     │
│  • Runtime validation                │
│  • Schema validation                 │
│                                      │
│ Server (FluentValidation):           │
│  • DTO validation                    │
│  • Business rules                    │
│  • SQL injection prevention          │
│  • XSS prevention                    │
└──────────────────────────────────────┘
```

**Defense in Depth:** Never trust client-side validation alone

---

### 5️⃣ Logging & Monitoring

```
┌──────────────────────────────────────┐
│      Observability Layer             │
├──────────────────────────────────────┤
│ Structured Logging (Serilog):       │
│  • Request/Response logging          │
│  • Error tracking                    │
│  • Security events                   │
│  • Performance metrics               │
│                                      │
│ What to Log:                         │
│  ✓ Failed auth attempts              │
│  ✓ Rate limit violations             │
│  ✓ Validation failures               │
│  ✓ Unhandled exceptions              │
│  ✗ Sensitive data (PII, passwords)   │
└──────────────────────────────────────┘
```

---

## Request Flow (Secure)

```
1. Browser Request
   │
   ├─→ HTTPS/TLS Encryption
   │
2. API Gateway/Load Balancer (Production)
   │
   ├─→ SSL Termination
   ├─→ DDoS Protection
   │
3. Application Server
   │
   ├─→ HTTPS Redirection Middleware
   ├─→ HSTS Middleware
   ├─→ Security Headers Middleware
   ├─→ CORS Middleware
   ├─→ Rate Limiting Middleware
   ├─→ Exception Handling Middleware
   │
4. Controller
   │
   ├─→ Model Binding
   ├─→ Input Validation (FluentValidation)
   ├─→ Authorization Check (when implemented)
   │
5. Service Layer
   │
   ├─→ Business Logic
   ├─→ Structured Logging
   │
6. Repository/Data Layer
   │
   ├─→ Parameterized Queries (EF Core)
   ├─→ Data Access
   │
7. Response
   │
   ├─→ Serialization
   ├─→ Security Headers Added
   ├─→ HTTPS Encryption
   │
8. Browser
   └─→ Response Received & Validated
```

---

## Environment-Specific Configuration

### Development
```
Frontend:
- URL: http://localhost:5173
- API: http://localhost:5000 or https://localhost:5001
- HTTPS: Self-signed certificate OK
- CORS: localhost allowed
- Rate Limit: Relaxed (1000/min)
- Logging: Verbose/Debug
- Error Details: Shown
```

### Production
```
Frontend:
- URL: https://app.yourdomain.com
- API: https://api.yourdomain.com
- HTTPS: Valid CA certificate REQUIRED
- CORS: Production domain only
- Rate Limit: Strict (100/min)
- Logging: Info/Warning/Error only
- Error Details: Hidden (generic messages)
```

**Implementation:** Use environment variables and configuration files

---

## Threat Model & Mitigations

| Threat | Mitigation | Status |
|--------|-----------|---------|
| **Man-in-the-Middle** | HTTPS + HSTS | 🟡 Pending |
| **Cross-Site Scripting** | CSP headers + React auto-escaping | 🟢 Partial |
| **Clickjacking** | X-Frame-Options: DENY | 🟡 Pending |
| **MIME Sniffing** | X-Content-Type-Options | 🟡 Pending |
| **CORS Misconfiguration** | Restrictive CORS policy | 🔴 Fix needed |
| **DoS/DDoS** | Rate limiting | 🔴 Not implemented |
| **SQL Injection** | Entity Framework (parameterized) | 🟢 Protected |
| **Vulnerable Dependencies** | npm audit, dotnet list package | 🔴 Vite vuln found |
| **Secrets Exposure** | .gitignore + env variables | 🟢 Good |
| **Session Hijacking** | Secure cookies (when auth added) | ⚪ Not applicable |

🔴 Critical | 🟡 In Progress | 🟢 Implemented | ⚪ Not Applicable

---

## Security Checklist for Production

### Before Deployment

- [ ] All HIGH severity issues fixed
- [ ] HTTPS configured with valid certificate
- [ ] Security headers implemented
- [ ] Rate limiting enabled
- [ ] CORS configured for production domain only
- [ ] No hardcoded secrets or URLs
- [ ] Dependencies updated (no known vulnerabilities)
- [ ] Error messages don't leak sensitive info
- [ ] Logging configured (no sensitive data logged)
- [ ] .env files not committed to git

### Testing

- [ ] Penetration testing completed
- [ ] OWASP ZAP or similar scan passed
- [ ] Security headers verified (securityheaders.com)
- [ ] SSL Labs test passed (A+ rating)
- [ ] Rate limiting tested
- [ ] CORS tested with different origins
- [ ] Error handling tested

### Monitoring

- [ ] Set up alerts for:
  - Unusual traffic patterns
  - Rate limit violations
  - Authentication failures (when implemented)
  - Application errors
  - Certificate expiration

---

## Tools & Resources

### Testing Tools
- **OWASP ZAP** - Vulnerability scanning
- **Burp Suite** - Security testing
- **SecurityHeaders.com** - Header analysis
- **SSL Labs** - SSL/TLS testing
- **npm audit** - Dependency scanning
- **dotnet list package --vulnerable** - .NET dependency check

### Libraries
- **Serilog** - Structured logging
- **FluentValidation** - Input validation
- **NetEscapades.AspNetCore.SecurityHeaders** - Security headers
- **AspNetCoreRateLimit** - Rate limiting (pre-.NET 7)

### Documentation
- [OWASP Top 10](https://owasp.org/Top10/)
- [ASP.NET Core Security](https://docs.microsoft.com/en-us/aspnet/core/security/)
- [React Security](https://reactjs.org/docs/dom-elements.html#dangerouslysetinnerhtml)
- [MDN Web Security](https://developer.mozilla.org/en-US/docs/Web/Security)

---

## Summary

**Current State:** 🔴 Development-ready, ⚠️ Not production-ready

**After Implementing Fixes:** 🟢 Production-ready with proper configuration

**Key Principle:** Defense in depth - multiple security layers protect against single point of failure

**Next Steps:**
1. Implement fixes from `SECURITY_FIXES_CHECKLIST.md`
2. Test each layer independently
3. Perform integrated security testing
4. Set up continuous security monitoring

---

**Last Updated:** 2025-11-05  
**Version:** 1.0
