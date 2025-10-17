# 7. Deployment View

## 7.1 Development Environment Deployment

The application is designed for local development with a simple setup process.

### 7.1.1 Development Infrastructure

```mermaid
graph TB
    subgraph "Developer Machine"
        subgraph "Frontend"
            A[Vite Dev Server<br/>:5173]
            B[Node.js Runtime]
            C[npm Dependencies]
        end
        
        subgraph "Backend"
            D[Kestrel Server<br/>:5000]
            E[.NET 9.0 Runtime]
            F[NuGet Dependencies]
        end
        
        subgraph "Development Tools"
            G[VS Code]
            H[Git Repository]
            I[Browser DevTools]
        end
    end
    
    B --> A
    C --> A
    E --> D
    F --> D
    
    G --> A
    G --> D
    H --> G
    I --> A
    
    A -.->|HTTP| D
```

### 7.1.2 Development Deployment Process

| Step | Command | Purpose | Port |
|------|---------|---------|------|
| **1. Backend Start** | `cd backend/src/Api && dotnet run` | Start .NET API server | 5000 |
| **2. Frontend Start** | `cd frontend && npm run dev` | Start React dev server | 5173 |
| **3. Browser Access** | Open `http://localhost:5173` | Access application | 5173 |

### 7.1.3 Development Configuration

```mermaid
graph LR
    subgraph "Frontend Config"
        A[vite.config.ts]
        B[package.json]
        C[tsconfig.json]
    end
    
    subgraph "Backend Config"
        D[Program.cs]
        E[launchSettings.json]
        F[Api.csproj]
    end
    
    subgraph "Development Settings"
        G[CORS Policy]
        H[Hot Reload]
        I[Port Configuration]
    end
    
    A --> H
    B --> A
    C --> A
    
    D --> G
    E --> I
    F --> D
    
    G --> I
    H --> I
```

## 7.2 Local Development Setup

### 7.2.1 Prerequisites

| Requirement | Version | Purpose | Installation |
|-------------|---------|---------|--------------|
| **Node.js** | 18+ | Frontend development | [nodejs.org](https://nodejs.org) |
| **.NET SDK** | 9.0+ | Backend development | [dotnet.microsoft.com](https://dotnet.microsoft.com) |
| **Git** | Latest | Version control | [git-scm.com](https://git-scm.com) |
| **VS Code** | Latest | Development environment | [code.visualstudio.com](https://code.visualstudio.com) |

### 7.2.2 Installation Flow

```mermaid
flowchart TD
    A[Clone Repository] --> B[Install Frontend Dependencies]
    B --> C[Install Backend Dependencies]
    C --> D[Start Backend Server]
    D --> E[Start Frontend Server]
    E --> F[Open Browser]
    F --> G[Verify Health Check]
    
    B1[npm install] --> B
    C1[dotnet restore] --> C
    D1[dotnet run] --> D
    E1[npm run dev] --> E
    F1[http://localhost:5173] --> F
```

### 7.2.3 Development Scripts

```bash
# Backend Commands
cd backend/src/Api
dotnet restore          # Install dependencies
dotnet build           # Build application
dotnet run             # Start development server

# Frontend Commands
cd frontend
npm install            # Install dependencies
npm run dev           # Start development server
npm run build         # Build for production
npm run lint          # Run linting
```

## 7.3 Network Configuration

### 7.3.1 Port Allocation

```mermaid
graph TB
    subgraph "Network Ports"
        A[Frontend :5173]
        B[Backend :5000]
        C[Swagger UI :5000]
        D[Vite HMR :5173]
    end
    
    subgraph "Communication"
        E[HTTP Requests]
        F[WebSocket HMR]
        G[CORS Policy]
    end
    
    A --> E
    A --> F
    B --> E
    C --> E
    
    E --> G
    F --> D
```

### 7.3.2 CORS Configuration

| Configuration | Development | Production Notes |
|---------------|-------------|------------------|
| **Allowed Origins** | `http://localhost:5173` | Restrict to actual domains |
| **Allowed Methods** | `GET, POST, PUT, DELETE` | Limit to required methods |
| **Allowed Headers** | `*` | Specify required headers only |
| **Credentials** | `false` | Enable only if needed |

## 7.4 Environment Configuration

### 7.4.1 Frontend Environment

**File: [`vite.config.ts`](../frontend/vite.config.ts)**

```typescript
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
    host: true,
    open: true,
  },
});
```

**Key Settings:**

- Port 5173 for development server
- Host binding for network access
- Auto-open browser on start

### 7.4.2 Backend Environment

**File: [`launchSettings.json`](../backend/src/Api/Properties/launchSettings.json)**

```json
{
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

**Key Settings:**

- Port 5000 for API server
- Development environment
- Swagger UI enabled

## 7.5 Production Deployment Considerations

### 7.5.1 Frontend Production Deployment

```mermaid
graph TB
    subgraph "Build Process"
        A[Source Code] --> B[npm run build]
        B --> C[Vite Production Build]
        C --> D[Optimized Bundle]
    end
    
    subgraph "Deployment Options"
        D --> E[Static File Hosting]
        D --> F[CDN Distribution]
        D --> G[Web Server]
    end
    
    subgraph "Hosting Platforms"
        E --> H[Netlify]
        E --> I[Vercel]
        E --> J[GitHub Pages]
        F --> K[CloudFlare]
        G --> L[IIS/Apache]
    end
```

### 7.5.2 Backend Production Deployment

```mermaid
graph TB
    subgraph "Build Process"
        A[Source Code] --> B[dotnet publish]
        B --> C[Self-Contained Build]
        C --> D[Deployment Package]
    end
    
    subgraph "Hosting Options"
        D --> E[Docker Container]
        D --> F[Azure App Service]
        D --> G[IIS Server]
        D --> H[Linux Server]
    end
    
    subgraph "Configuration Changes"
        E --> I[CORS Restrictions]
        F --> J[HTTPS Enforcement]
        G --> K[Security Headers]
        H --> L[Logging Configuration]
    end
```

### 7.5.3 Production Configuration Changes

| Component | Development | Production Required Changes |
|-----------|-------------|----------------------------|
| **CORS Policy** | Permissive localhost | Restrict to actual domains |
| **HTTPS** | HTTP only | Enforce HTTPS everywhere |
| **Logging** | Console output | Structured logging to files/services |
| **Error Handling** | Detailed errors | Generic error messages |
| **Security Headers** | Basic | Full security header suite |
| **Build Optimization** | Development builds | Minified, optimized bundles |

## 7.6 Monitoring and Health Checks

### 7.6.1 Development Monitoring

```mermaid
graph TB
    A[Application Health] --> B[Frontend Monitoring]
    A --> C[Backend Monitoring]
    
    B --> D[Browser DevTools]
    B --> E[Console Logging]
    B --> F[Network Tab]
    
    C --> G[.NET Logging]
    C --> H[Swagger UI]
    C --> I[Health Endpoint]
    
    D --> J[Performance Metrics]
    E --> K[API Call Logs]
    F --> L[Network Analysis]
    
    G --> M[Server Logs]
    H --> N[API Testing]
    I --> O[Health Status]
```

### 7.6.2 Health Check Endpoints

| Endpoint | Method | Purpose | Response |
|----------|--------|---------|----------|
| `/api/health` | GET | Application health | `{"utcNow": "...", "status": "OK"}` |
| `/swagger` | GET | API documentation | Swagger UI |
| `/` | GET | Root endpoint | Swagger UI redirect |

## 7.7 Development Workflow

### 7.7.1 Daily Development Process

```mermaid
flowchart TD
    A[Start Development] --> B[Pull Latest Code]
    B --> C[Start Backend]
    C --> D[Start Frontend]
    D --> E[Make Changes]
    E --> F[Test Changes]
    F --> G{Tests Pass?}
    G -->|No| E
    G -->|Yes| H[Commit Changes]
    H --> I[Push to Feature Branch]
    I --> J[Create Pull Request]
```

### 7.7.2 Hot Reload Configuration

| Technology | Configuration | Benefit |
|------------|---------------|---------|
| **Vite HMR** | Automatic with Vite dev server | Instant frontend updates |
| **.NET Hot Reload** | `dotnet watch run` | Backend changes without restart |
| **Browser Sync** | Built into Vite | Automatic browser refresh |

---

**Navigation:** [← Runtime View](06-runtime-view.md) | [Cross-cutting Concepts →](08-cross-cutting-concepts.md)