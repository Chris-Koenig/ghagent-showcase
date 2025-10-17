# 3. System Scope and Context

## 3.1 Business Context

The ghagent-showcase application operates within a development and demonstration context, providing a reference implementation for modern full-stack web development.

```mermaid
graph TB
    subgraph "External Actors"
        DEV[Developers]
        ARCH[Architects]
        LEARN[Learners]
    end
    
    subgraph "ghagent-showcase System"
        FRONT[React Frontend]
        BACK[.NET Backend]
        HEALTH[Health Check API]
    end
    
    subgraph "External Systems"
        BROWSER[Web Browser]
        DEVTOOLS[Developer Tools]
        CONSOLE[Browser Console]
    end
    
    DEV --> FRONT
    ARCH --> FRONT
    LEARN --> FRONT
    
    FRONT --> BROWSER
    FRONT --> DEVTOOLS
    FRONT --> CONSOLE
    
    FRONT --> HEALTH
    HEALTH --> BACK
```

### Business Context Description

| Actor/System | Interface | Purpose |
|--------------|-----------|---------|
| **Developers** | Web Interface | Use as reference implementation for clean architecture |
| **Architects** | Web Interface | Evaluate architectural patterns and decisions |
| **Learners** | Web Interface | Study modern web development practices |
| **Web Browser** | HTTP/HTTPS | Render React application and handle user interactions |
| **Developer Tools** | Browser APIs | Debug and monitor application behavior |
| **Browser Console** | Console API | Display detailed API call logs and debugging information |

## 3.2 Technical Context

### System Boundaries

```mermaid
graph TB
    subgraph "Frontend Boundary"
        direction TB
        UI[User Interface]
        COMP[React Components]
        HOOKS[Custom Hooks]
        SERV[API Services]
    end
    
    subgraph "Backend Boundary"
        direction TB
        API[REST API]
        CTRL[Controllers]
        APP[Application Layer]
        DOM[Domain Layer]
    end
    
    subgraph "External Interfaces"
        CORS[CORS Policy]
        HTTP[HTTP Protocol]
        JSON[JSON Format]
    end
    
    UI --> COMP
    COMP --> HOOKS
    HOOKS --> SERV
    SERV --> CORS
    CORS --> HTTP
    HTTP --> API
    API --> CTRL
    CTRL --> APP
    APP --> DOM
    
    SERV --> JSON
    API --> JSON
```

### Technical Interfaces

| Interface | Technology | Protocol | Data Format | Purpose |
|-----------|------------|----------|-------------|---------|
| **Frontend-Backend** | HTTP REST | HTTP/1.1 | JSON | API communication |
| **User Interface** | React/DOM | Browser APIs | HTML/CSS/JS | User interaction |
| **Development** | Vite/WebSocket | WebSocket | HMR Protocol | Hot module replacement |
| **Console Logging** | Browser Console API | JavaScript | Text/Objects | Debugging and monitoring |

## 3.3 External Interfaces

### Frontend External Interfaces

```mermaid
sequenceDiagram
    participant U as User
    participant B as Browser
    participant V as Vite Dev Server
    participant R as React App
    participant C as Console
    
    U->>B: Open Application
    B->>V: HTTP Request
    V->>B: React Application
    B->>R: Initialize
    R->>C: Log Application Start
    R->>B: Render UI
    B->>U: Display Interface
```

#### Frontend Interface Details

| Interface | Description | Technology | Port |
|-----------|-------------|------------|------|
| **HTTP Server** | Serves React application | Vite Development Server | 5173 |
| **WebSocket** | Hot module replacement | Vite HMR | 5173 |
| **Browser APIs** | DOM manipulation, Console logging | Web Standards | N/A |
| **Fetch API** | HTTP requests to backend | Web Standards | N/A |

### Backend External Interfaces

```mermaid
sequenceDiagram
    participant F as Frontend
    participant C as CORS
    participant A as ASP.NET Core
    participant H as Health Controller
    participant M as Memory
    
    F->>C: HTTP Request
    C->>A: Validate Origin
    A->>H: Route Request
    H->>M: Get Health Data
    M->>H: Return Data
    H->>A: Health Response
    A->>C: HTTP Response
    C->>F: JSON Response
```

#### Backend Interface Details

| Interface | Description | Technology | Port |
|-----------|-------------|------------|------|
| **REST API** | HTTP endpoints for frontend | ASP.NET Core | 5000 |
| **CORS** | Cross-origin resource sharing | ASP.NET Core CORS | 5000 |
| **Swagger UI** | API documentation | Swagger/OpenAPI | 5000 |
| **Health Endpoint** | System health monitoring | ASP.NET Core | 5000 |

## 3.4 Communication Protocols

### HTTP REST API Protocol

```mermaid
graph LR
    subgraph "Request Flow"
        A[Frontend Request] --> B[CORS Validation]
        B --> C[Route Matching]
        C --> D[Controller Action]
        D --> E[Response Generation]
    end
    
    subgraph "Response Flow"
        F[JSON Serialization] --> G[HTTP Response]
        G --> H[CORS Headers]
        H --> I[Frontend Processing]
    end
    
    E --> F
```

#### API Specification

| Endpoint | Method | Request | Response | Purpose |
|----------|--------|---------|----------|---------|
| `/api/health` | GET | None | `HealthTimeDto` | System health check |
| `/swagger` | GET | None | HTML | API documentation |
| `/` | GET | None | Swagger UI | Interactive API explorer |

#### Health Check API Contract

```json
{
  "endpoint": "/api/health",
  "method": "GET",
  "response": {
    "utcNow": "2024-10-17T09:30:00.000Z",
    "status": "OK"
  },
  "statusCodes": {
    "200": "Success",
    "500": "Internal Server Error"
  }
}
```

## 3.5 Data Flow Context

### Request-Response Flow

```mermaid
sequenceDiagram
    participant User
    participant ReactApp
    participant HealthService
    participant Backend
    participant Console
    
    User->>ReactApp: Load Page
    ReactApp->>HealthService: checkHealth()
    HealthService->>Console: Log Request Details
    HealthService->>Backend: GET /api/health
    Backend->>HealthService: HealthTimeDto
    HealthService->>Console: Log Response
    HealthService->>ReactApp: Success/Error
    ReactApp->>User: Display Status
```

### Error Handling Flow

```mermaid
graph TB
    A[API Request] --> B{Request Success?}
    B -->|Yes| C[Log Success]
    B -->|No| D[Log Error]
    
    C --> E[Update Success State]
    D --> F[Update Error State]
    
    E --> G[Display Status]
    F --> H[Display Error]
    
    G --> I[Console Logging]
    H --> I
```

## 3.6 Development Context

### Development Environment Setup

```mermaid
graph TB
    subgraph "Developer Machine"
        NODE[Node.js 18+]
        DOTNET[.NET 9.0 SDK]
        GIT[Git Repository]
        VS[VS Code]
    end
    
    subgraph "Frontend Development"
        VITE[Vite Dev Server :5173]
        NPM[npm Dependencies]
        TS[TypeScript Compiler]
    end
    
    subgraph "Backend Development"
        KESTREL[Kestrel Server :5000]
        NUGET[NuGet Packages]
        COMPILER[C# Compiler]
    end
    
    NODE --> VITE
    NODE --> NPM
    NODE --> TS
    
    DOTNET --> KESTREL
    DOTNET --> NUGET
    DOTNET --> COMPILER
    
    GIT --> VS
    VS --> VITE
    VS --> KESTREL
```

### Development Workflow Context

| Stage | Technology | Purpose | Port/Protocol |
|-------|------------|---------|---------------|
| **Code Editing** | VS Code | Development environment | File System |
| **Frontend Build** | Vite | Fast development builds | HTTP :5173 |
| **Backend Build** | .NET CLI | Compilation and execution | HTTP :5000 |
| **Version Control** | Git | Source code management | Git Protocol |
| **Hot Reloading** | Vite HMR | Live code updates | WebSocket |

---

**Navigation:** [← Architecture Constraints](02-architecture-constraints.md) | [Solution Strategy →](04-solution-strategy.md)