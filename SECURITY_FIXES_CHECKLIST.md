# Security Fixes Checklist

This checklist provides step-by-step instructions to remediate the security issues identified in the security review.

## 🔴 HIGH Priority (Do First)

### 1. Fix CORS Configuration

**File:** `backend/src/Api/Program.cs`

**Current code (lines 17-24):**
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder
            .WithOrigins("http://localhost:5173") // Vite default port
            .AllowAnyMethod()
            .AllowAnyHeader());
});
```

**Replace with:**
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder
            .WithOrigins("http://localhost:5173", "https://localhost:5173")
            .WithMethods("GET", "POST", "OPTIONS")
            .WithHeaders("Content-Type", "Accept")
            .SetIsOriginAllowedToAllowWildcardSubdomains());
});
```

- [ ] Update CORS policy in Program.cs
- [ ] Test that health endpoint still works
- [ ] Verify OPTIONS preflight requests work

---

### 2. Add HTTPS Support

**Step 1:** Update `backend/src/Api/Properties/launchSettings.json`

**Change line 7:**
```json
"applicationUrl": "https://localhost:5001;http://localhost:5000",
```

**Step 2:** Add HTTPS middleware to `backend/src/Api/Program.cs`

**Add after line 26 (after `var app = builder.Build();`):**
```csharp
// HTTPS redirection and security
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseHsts();
}
```

**Step 3:** Update frontend to support HTTPS

**File:** `frontend/src/services/healthService.ts`

**Add before line 3:**
```typescript
const API_BASE_URL = import.meta.env.VITE_API_URL || 
    (import.meta.env.DEV ? "http://localhost:5000" : "https://localhost:5001");
```

- [ ] Update launchSettings.json
- [ ] Add HTTPS middleware
- [ ] Update frontend API URL
- [ ] Test HTTPS locally (may need to trust dev certificate)
- [ ] Update CORS to include HTTPS origin

---

### 3. Environment-Based Configuration

**Step 1:** Create environment files

**Create:** `frontend/.env.example`
```env
# API Configuration
VITE_API_URL=http://localhost:5000

# Example for production:
# VITE_API_URL=https://api.yourdomain.com
```

**Create:** `frontend/.env.development` (optional, for local overrides)
```env
VITE_API_URL=http://localhost:5000
```

**Step 2:** Update service to use environment variable

**File:** `frontend/src/services/healthService.ts` (line 3)

**Replace:**
```typescript
const API_BASE_URL = "http://localhost:5000"; // Backend URL
```

**With:**
```typescript
const API_BASE_URL = import.meta.env.VITE_API_URL || "http://localhost:5000";
```

**Step 3:** Document in README

Add to `frontend/README.md`:
```markdown
## Environment Variables

Create a `.env` file in the frontend directory:

```env
VITE_API_URL=http://localhost:5000
```

For production, set `VITE_API_URL` to your production API URL.
```

- [ ] Create .env.example
- [ ] Update healthService.ts
- [ ] Test with environment variable
- [ ] Document in README

---

## 🟠 MEDIUM Priority (Do Soon)

### 4. Update Vite Dependency

**File:** `frontend/package.json`

**Commands:**
```bash
cd frontend
npm install vite@latest
npm audit fix
npm audit
```

**Verify in package.json:**
```json
"vite": "^7.1.11" // or later
```

- [ ] Run npm install vite@latest
- [ ] Run npm audit fix
- [ ] Verify no vulnerabilities remain
- [ ] Test that dev server still works
- [ ] Test production build

---

### 5. Add Security Headers

**File:** `backend/src/Api/Program.cs`

**Add after line 33 (after `app.UseCors("AllowFrontend");`):**

```csharp
// Security headers middleware
app.Use(async (context, next) =>
{
    // Prevent MIME-sniffing
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    
    // Prevent clickjacking
    context.Response.Headers.Append("X-Frame-Options", "DENY");
    
    // Enable XSS protection
    context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
    
    // Control referrer information
    context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
    
    // Content Security Policy
    context.Response.Headers.Append("Content-Security-Policy", 
        "default-src 'self'; " +
        "script-src 'self'; " +
        "style-src 'self' 'unsafe-inline'; " +
        "img-src 'self' data:; " +
        "font-src 'self'; " +
        "connect-src 'self' http://localhost:5173 https://localhost:5173;");
    
    await next();
});
```

**Alternative:** Use a NuGet package for cleaner implementation:

```bash
cd backend/src/Api
dotnet add package NetEscapades.AspNetCore.SecurityHeaders
```

Then in Program.cs:
```csharp
app.UseSecurityHeaders(policies => 
    policies.AddDefaultSecurityHeaders()
            .AddContentSecurityPolicy(builder =>
            {
                builder.AddDefaultSrc().Self();
                builder.AddScriptSrc().Self();
                builder.AddStyleSrc().Self().UnsafeInline();
                builder.AddImgSrc().Self().Data();
            }));
```

- [ ] Add security headers middleware
- [ ] Test headers with browser DevTools
- [ ] Verify CSP doesn't break frontend
- [ ] Test with securityheaders.com

---

### 6. Implement Rate Limiting

**File:** `backend/src/Api/Program.cs`

**Step 1:** Add required using statement at top:
```csharp
using System.Threading.RateLimiting;
```

**Step 2:** Add after line 14 (after swagger configuration):

```csharp
// Rate limiting
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 100, // 100 requests
                Window = TimeSpan.FromMinutes(1) // per minute
            }));
    
    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        await context.HttpContext.Response.WriteAsync(
            "Too many requests. Please try again later.", token);
    };
});
```

**Step 3:** Add middleware after CORS (around line 35):

```csharp
// Enable rate limiting
app.UseRateLimiter();
```

- [ ] Add rate limiting service
- [ ] Add rate limiter middleware
- [ ] Test rate limiting (make 100+ requests)
- [ ] Verify 429 status code is returned
- [ ] Adjust limits as needed

---

## 🟡 LOW Priority (Future Development)

### 7. Establish Input Validation Pattern

**When adding new endpoints that accept input:**

**Step 1:** Add FluentValidation package

```bash
cd backend/src/Application
dotnet add package FluentValidation
dotnet add package FluentValidation.AspNetCore
```

**Step 2:** Configure in Program.cs

```csharp
using FluentValidation;
using FluentValidation.AspNetCore;

// After AddControllers()
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
```

**Step 3:** Create validator examples

Create `Application/Validators/` folder and add validators:

```csharp
public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(255);
            
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100)
            .Matches("^[a-zA-Z0-9 ]*$").WithMessage("Name contains invalid characters");
    }
}
```

- [ ] Add FluentValidation package (when needed)
- [ ] Configure validation in Program.cs
- [ ] Create validator for first DTO that accepts input
- [ ] Document validation pattern for team

---

## Additional Security Enhancements

### Optional: Structured Logging

**Add Serilog for better logging:**

```bash
cd backend/src/Api
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.File
```

In Program.cs:
```csharp
using Serilog;

// Early in the file
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/api-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
```

---

### Optional: API Documentation Improvements

Enhance Swagger documentation with security information:

```csharp
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ghagent-showcase API",
        Version = "v1",
        Description = "Minimal scaffold API with health endpoint. Uses HTTPS and includes rate limiting.",
        Contact = new OpenApiContact
        {
            Name = "API Support",
            Email = "support@example.com"
        }
    });
    
    // Include XML comments if available
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        o.IncludeXmlComments(xmlPath);
    }
});
```

---

## Testing Your Fixes

### Test CORS
```bash
# From browser console on http://localhost:5173
fetch('http://localhost:5000/api/health', {
    method: 'GET',
    headers: { 'Content-Type': 'application/json' }
}).then(r => r.json()).then(console.log)
```

### Test Rate Limiting
```bash
# Run this script to test rate limit
for i in {1..110}; do
    curl -s -o /dev/null -w "%{http_code}\n" http://localhost:5000/api/health
done
# Should see 429 responses after 100 requests
```

### Test Security Headers
```bash
# Check headers
curl -I https://localhost:5001/api/health

# Or use online tool:
# Visit https://securityheaders.com/
```

### Test HTTPS
```bash
# Should redirect to HTTPS (in non-development)
curl -I http://localhost:5000/api/health

# HTTPS should work
curl -k https://localhost:5001/api/health
```

---

## Verification Checklist

After implementing fixes, verify:

- [ ] Application builds without errors
- [ ] Frontend can connect to backend
- [ ] CORS works for allowed origins
- [ ] CORS blocks disallowed origins
- [ ] HTTPS works locally
- [ ] Security headers are present (check in browser DevTools)
- [ ] Rate limiting triggers after limit reached
- [ ] No npm audit vulnerabilities
- [ ] Environment variables work
- [ ] Production build succeeds
- [ ] API documentation updated

---

## CI/CD Integration

Add to your CI/CD pipeline:

```yaml
# GitHub Actions example
- name: Security Checks
  run: |
    cd frontend
    npm audit --audit-level=moderate
    
    cd ../backend
    dotnet list package --vulnerable --include-transitive
    
- name: Build with security checks
  run: |
    cd backend/src/Api
    dotnet build --configuration Release
    
    cd ../../../frontend
    npm run build
```

---

## Documentation Updates

After implementing fixes, update:

- [ ] README.md - Add security section
- [ ] FRONTEND_SETUP.md - Add environment variables
- [ ] Create SECURITY.md - Security policy
- [ ] Update .env.example with all variables

---

## Questions or Issues?

If you encounter problems implementing these fixes:

1. Check the full security review in `SECURITY_REVIEW.md`
2. Consult the ASP.NET Core security documentation
3. Review OWASP best practices
4. Test changes incrementally

---

**Last Updated:** 2025-11-05
**Review Status:** Initial security fixes pending
