# 5. Building Block View

## 5.1 Whitebox Overall System

The ghagent-showcase system consists of two main subsystems: a React frontend and a .NET Core backend, communicating via HTTP REST API.

```mermaid
graph TB
    subgraph "ghagent-showcase System"
        subgraph "Frontend Subsystem"
            FE[React Application]
            COMP[Components Layer]
            SERV[Services Layer]
            TYPES[Types Layer]
        end
        
        subgraph "Backend Subsystem"
            BE[.NET Core API]
            API[API Layer]
            APP[Application Layer]
            DOM[Domain Layer]
            INF[Infrastructure Layer]
        end
        
        subgraph "External Systems"
            BROWSER[Web Browser]
            DEVTOOLS[Developer Tools]
        end
    end
    
    FE --> COMP
    COMP --> SERV
    SERV --> TYPES
    
    BE --> API
    API --> APP
    APP --> DOM
    API --> INF
    
    FE --> BE
    FE --> BROWSER
    FE --> DEVTOOLS
```

### System Components Overview

| Component | Responsibility | Technology | Key Files |
|-----------|----------------|------------|-----------|
| **React Frontend** | User interface and client-side logic | React 19 + TypeScript | [`/frontend/src/`](../frontend/src/) |
| **.NET Backend** | Business logic and data management | .NET 9.0 + ASP.NET Core | [`/backend/src/`](../backend/src/) |
| **HTTP API** | Communication interface | REST over HTTP/JSON | Controllers and Services |
| **Development Tools** | Build and development support | Vite, .NET CLI | Configuration files |

## 5.2 Frontend Building Blocks (Level 2)

### Frontend Architecture Whitebox

```mermaid
graph TB
    subgraph "React Frontend Application"
        subgraph "Presentation Layer"
            APP[App.tsx]
            PAGES[Pages]
            COMP[Components]
        end
        
        subgraph "Logic Layer"
            HOOKS[Custom Hooks]
            CONTEXT[React Context]
        end
        
        subgraph "Data Layer"
            SERVICES[API Services]
            TYPES[TypeScript Types]
        end
        
        subgraph "Infrastructure"
            UTILS[Utilities]
            CONFIG[Configuration]
        end
    end
    
    APP --> PAGES
    PAGES --> COMP
    COMP --> HOOKS
    HOOKS --> CONTEXT
    HOOKS --> SERVICES
    SERVICES --> TYPES
    UTILS --> SERVICES
    CONFIG --> APP
```

### Frontend Components Detail

#### 5.2.1 Presentation Layer

| Component | Purpose | Responsibility | File Path |
|-----------|---------|----------------|-----------|
| **App.tsx** | Root application component | Application structure and routing | [`/frontend/src/App.tsx`](../frontend/src/App.tsx) |
| **HealthPage** | Main page component | Page layout and health dashboard | [`/frontend/src/pages/HealthPage.tsx`](../frontend/src/pages/HealthPage.tsx) |
| **HealthCheck** | Health monitoring component | Display health status and controls | [`/frontend/src/components/HealthCheck.tsx`](../frontend/src/components/HealthCheck.tsx) |

**Component Interaction:**

```mermaid
graph LR
    A[App.tsx] --> B[HealthPage.tsx]
    B --> C[HealthCheck.tsx]
    C --> D[useHealthCheck]
    D --> E[healthService]
```

#### 5.2.2 Logic Layer

| Component | Purpose | Responsibility | File Path |
|-----------|---------|----------------|-----------|
| **useHealthCheck** | Custom hook for health logic | State management and API calls | [`/frontend/src/hooks/useHealthCheck.ts`](../frontend/src/hooks/useHealthCheck.ts) |

**Hook Architecture:**

```mermaid
graph TB
    A[useHealthCheck Hook] --> B[useState: health]
    A --> C[useState: loading]
    A --> D[useState: error]
    A --> E[useCallback: checkHealth]
    E --> F[healthService.checkHealth]
    F --> G[Update State]
```

#### 5.2.3 Data Layer

| Component | Purpose | Responsibility | File Path |
|-----------|---------|----------------|-----------|
| **healthService** | API communication service | HTTP requests and response handling | [`/frontend/src/services/healthService.ts`](../frontend/src/services/healthService.ts) |
| **HealthResponse** | Type definition for API response | Type safety for health data | [`/frontend/src/types/health.ts`](../frontend/src/types/health.ts) |

**Service Architecture:**

```mermaid
graph TB
    A[healthService] --> B[makeRequest Method]
    B --> C[Console Logging]
    B --> D[Fetch API]
    B --> E[Error Handling]
    D --> F[JSON Response]
    E --> G[Error State]
    F --> H[Success State]
```

## 5.3 Backend Building Blocks (Level 2)

### Backend Architecture Whitebox

```mermaid
graph TB
    subgraph ".NET Core Backend"
        subgraph "API Layer"
            PROG[Program.cs]
            CTRL[Controllers]
            MIDDLEWARE[Middleware]
        end
        
        subgraph "Application Layer"
            SERVICES[Application Services]
            DTO[Data Transfer Objects]
            INTERFACES[Service Interfaces]
        end
        
        subgraph "Domain Layer"
            ENTITIES[Domain Entities]
            LOGIC[Business Logic]
            CONTRACTS[Domain Contracts]
        end
        
        subgraph "Infrastructure Layer"
            REPOS[Repositories]
            DATA[Data Access]
            EXTERNAL[External Services]
        end
    end
    
    PROG --> CTRL
    CTRL --> MIDDLEWARE
    CTRL --> SERVICES
    SERVICES --> DTO
    SERVICES --> INTERFACES
    SERVICES --> ENTITIES
    ENTITIES --> LOGIC
    LOGIC --> CONTRACTS
    SERVICES --> REPOS
    REPOS --> DATA
    DATA --> EXTERNAL
```

### Backend Components Detail

#### 5.3.1 API Layer

| Component | Purpose | Responsibility | File Path |
|-----------|---------|----------------|-----------|
| **Program.cs** | Application entry point | Service configuration, middleware setup | [`/backend/src/Api/Program.cs`](../backend/src/Api/Program.cs) |
| **HealthController** | Health endpoint controller | HTTP request handling for health checks | [`/backend/src/Api/Controllers/HealthController.cs`](../backend/src/Api/Controllers/HealthController.cs) |

**API Layer Architecture:**

```mermaid
graph LR
    A[HTTP Request] --> B[CORS Middleware]
    B --> C[Routing]
    C --> D[HealthController]
    D --> E[Action Method]
    E --> F[DTO Response]
    F --> G[JSON Serialization]
    G --> H[HTTP Response]
```

#### 5.3.2 Application Layer

| Component | Purpose | Responsibility | File Path |
|-----------|---------|----------------|-----------|
| **HealthTimeDto** | Data transfer object | API response contract | [`/backend/src/Application/DTOs/HealthTimeDto.cs`](../backend/src/Application/DTOs/HealthTimeDto.cs) |

**Application Layer Pattern:**

```mermaid
graph TB
    A[Controller] --> B[Application Service]
    B --> C[Domain Service]
    C --> D[Domain Entity]
    D --> E[DTO Mapping]
    E --> F[Response]
```

#### 5.3.3 Domain Layer

| Component | Purpose | Responsibility | File Path |
|-----------|---------|----------------|-----------|
| **Domain Entities** | Core business objects | Business logic and rules | [`/backend/src/Domain/Entities/`](../backend/src/Domain/Entities/) |

#### 5.3.4 Infrastructure Layer

| Component | Purpose | Responsibility | File Path |
|-----------|---------|----------------|-----------|
| **Repositories** | Data access abstraction | Data persistence operations | [`/backend/src/Infrastructure/Repositories/`](../backend/src/Infrastructure/Repositories/) |

## 5.4 Component Interaction (Level 3)

### Health Check Flow Detail

```mermaid
sequenceDiagram
    participant U as User
    participant HC as HealthCheck Component
    participant UH as useHealthCheck Hook
    participant HS as healthService
    participant HC as HealthController
    participant DTO as HealthTimeDto
    
    U->>HC: Page Load
    HC->>UH: useEffect()
    UH->>HS: checkHealth()
    HS->>HS: Log Request
    HS->>HC: GET /api/health
    HC->>DTO: Create Response
    DTO->>HC: Return Data
    HC->>HS: HTTP 200 + JSON
    HS->>HS: Log Response
    HS->>UH: Success State
    UH->>HC: Update UI
    HC->>U: Display Status
```

### Error Handling Flow Detail

```mermaid
graph TB
    A[API Request] --> B{Network Available?}
    B -->|No| C[Network Error]
    B -->|Yes| D{Server Running?}
    D -->|No| E[Connection Error]
    D -->|Yes| F{Valid Response?}
    F -->|No| G[HTTP Error]
    F -->|Yes| H[Success]
    
    C --> I[Log Error]
    E --> I
    G --> I
    H --> J[Log Success]
    
    I --> K[Update Error State]
    J --> L[Update Success State]
    
    K --> M[Display Error UI]
    L --> N[Display Success UI]
```

## 5.5 File Organization Structure

### Frontend File Structure

```
frontend/src/
├── components/              # Reusable UI components
│   ├── HealthCheck.tsx      # Main health monitoring component
│   └── index.ts             # Component exports
├── pages/                   # Page-level components
│   ├── HealthPage.tsx       # Health dashboard page
│   └── index.ts             # Page exports
├── hooks/                   # Custom React hooks
│   ├── useHealthCheck.ts    # Health check business logic
│   └── index.ts             # Hook exports
├── services/                # API communication layer
│   ├── healthService.ts     # Backend API integration
│   └── index.ts             # Service exports
├── types/                   # TypeScript type definitions
│   ├── health.ts            # Health API types
│   └── index.ts             # Type exports
├── utils/                   # Utility functions
├── App.css                  # Application styles
├── App.tsx                  # Root component
├── index.css                # Global styles
└── main.tsx                 # Application entry point
```

### Backend File Structure

```
backend/src/
├── Api/                     # Web API layer
│   ├── Controllers/         # HTTP controllers
│   │   └── HealthController.cs
│   ├── Properties/          # Launch settings
│   │   └── launchSettings.json
│   ├── Api.csproj          # Project file
│   ├── GlobalUsings.cs     # Global using statements
│   └── Program.cs          # Application entry point
├── Application/             # Application services layer
│   ├── DTOs/               # Data transfer objects
│   │   └── HealthTimeDto.cs
│   ├── Application.csproj  # Project file
│   └── GlobalUsings.cs     # Global using statements
├── Domain/                  # Domain entities and logic
│   ├── Entities/           # Domain entities
│   ├── Domain.csproj       # Project file
│   └── GlobalUsings.cs     # Global using statements
└── Infrastructure/          # Data access and external services
    ├── Repositories/        # Data repositories
    ├── Infrastructure.csproj # Project file
    └── GlobalUsings.cs      # Global using statements
```

## 5.6 Interface Definitions

### Frontend Service Interface

```typescript
interface HealthService {
  checkHealth(): Promise<HealthResponse>;
}

interface HealthResponse {
  utcNow: string;
  status: string;
}
```

### Backend API Interface

```csharp
[ApiController]
[Route("api/[controller]")]
public interface IHealthController
{
    ActionResult<HealthTimeDto> Get();
}

public record HealthTimeDto(DateTime UtcNow, string Status);
```

### Component Props Interface

```typescript
interface HealthCheckProps {
  // Component is self-contained, no props required
}

interface UseHealthCheckReturn {
  health: HealthResponse | null;
  loading: boolean;
  error: string | null;
  checkHealth: () => Promise<void>;
}
```

---

**Navigation:** [← Solution Strategy](04-solution-strategy.md) | [Runtime View →](06-runtime-view.md)