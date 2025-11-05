# Security Fixes - Quick Start Guide

**⏱️ Total Time to Implement Critical Fixes:** ~2 hours  
**📋 Full Details:** See `SECURITY_FIXES_CHECKLIST.md`

This guide provides the absolute minimum changes to improve security quickly.

---

## 🚀 30-Minute Critical Fixes

### Fix 1: Update CORS (5 minutes)

**File:** `backend/src/Api/Program.cs`

**Find lines 17-24 and replace with:**
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder
            .WithOrigins("http://localhost:5173", "https://localhost:5173")
            .WithMethods("GET", "POST", "OPTIONS")
            .WithHeaders("Content-Type", "Accept"));
});
```

**Test:**
```bash
cd backend/src/Api && dotnet run
# In browser: http://localhost:5173 should still work
```

---

### Fix 2: Update Vulnerable Dependency (10 minutes)

**Commands:**
```bash
cd frontend
npm install vite@latest
npm audit
```

**Expected:** "found 0 vulnerabilities"

**Test:**
```bash
npm run dev
# Should start without errors
```

---

### Fix 3: Add Environment Variables (15 minutes)

**Step 1:** Create `frontend/.env.example`
```env
VITE_API_URL=http://localhost:5000
```

**Step 2:** Update `frontend/src/services/healthService.ts` line 3:
```typescript
const API_BASE_URL = import.meta.env.VITE_API_URL || "http://localhost:5000";
```

**Step 3:** Create `frontend/.env` (for local development)
```env
VITE_API_URL=http://localhost:5000
```

**Test:**
```bash
npm run dev
# Should still connect to backend
```

---

## 📊 Results After 30 Minutes

✅ **CORS** now restricts methods and headers  
✅ **Vite vulnerability** patched  
✅ **API URL** can be configured per environment  

**Security Improvement:** 🔴 → 🟠 (from Critical to Moderate)

---

## 🛡️ 1-Hour Bonus: Add Security Headers

**File:** `backend/src/Api/Program.cs`

**Add after line 33 (after `app.UseCors("AllowFrontend");`):**

```csharp
// Security headers
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Append("X-Frame-Options", "DENY");
    context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
    await next();
});
```

**Test:**
```bash
curl -I http://localhost:5000/api/health | grep "X-"
# Should show security headers
```

---

## 🔒 Complete Security (2-3 hours more)

See `SECURITY_FIXES_CHECKLIST.md` for:
- HTTPS enforcement
- Rate limiting
- Input validation patterns
- Structured logging

---

## Verification Commands

```bash
# Backend builds
cd backend/src/Api && dotnet build

# Frontend builds
cd frontend && npm run build

# No vulnerabilities
cd frontend && npm audit

# Check security headers (after adding them)
curl -I http://localhost:5000/api/health

# Test application
# 1. Start backend: cd backend/src/Api && dotnet run
# 2. Start frontend: cd frontend && npm run dev
# 3. Open browser: http://localhost:5173
# 4. Click "Refresh" - should see "Backend Status: OK"
```

---

## Commit Your Changes

```bash
git add .
git commit -m "Security improvements: CORS restrictions, dependency updates, environment config"
git push
```

---

## Next Steps

1. ✅ Implement these quick fixes (30 min - 1 hour)
2. 📖 Read `SECURITY_REVIEW.md` for detailed analysis  
3. 📋 Follow `SECURITY_FIXES_CHECKLIST.md` for remaining fixes
4. 🧪 Test thoroughly before production
5. 🔄 Schedule regular security reviews

---

**Questions?** Check the full documentation:
- `SECURITY_SUMMARY.md` - Executive overview
- `SECURITY_REVIEW.md` - Detailed findings (428 lines)
- `SECURITY_FIXES_CHECKLIST.md` - Complete remediation (493 lines)

---

**Remember:** These are showcase/development fixes. Before production:
- Enable HTTPS everywhere
- Add rate limiting
- Implement authentication
- Set up monitoring
- Review all MEDIUM/LOW priority items
