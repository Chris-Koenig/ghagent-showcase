# 12. Glossary

## 12.1 Technical Terms

### A

**API (Application Programming Interface)**
A set of protocols, routines, and tools for building software applications. In this project, refers to the REST API provided by the .NET backend.

**Arc42**
A template for architecture communication and documentation. Provides a standardized structure for documenting software architectures.

**ASP.NET Core**
A cross-platform, high-performance framework for building modern, cloud-based, Internet-connected applications. Used for the backend API in this project.

### B

**Backend**
The server-side component of the application responsible for business logic, data management, and API endpoints. Built with .NET 9.0 and ASP.NET Core.

**Build Tool**
Software used to automate the process of compiling source code. Vite is used for the frontend, dotnet CLI for the backend.

### C

**CI/CD (Continuous Integration/Continuous Deployment)**
A method to frequently deliver apps by introducing automation into the stages of app development, testing, and deployment.

**Clean Architecture**
An architectural pattern that separates concerns by organizing code into layers with dependency direction pointing inward toward the business logic.

**Component**
In React context, a reusable piece of UI that can accept inputs (props) and return elements describing what should appear on the screen.

**CORS (Cross-Origin Resource Sharing)**
A mechanism that allows restricted resources on a web page to be requested from another domain outside the domain from which the first resource was served.

**CSS (Cascading Style Sheets)**
A style sheet language used for describing the presentation of a document written in HTML or XML.

### D

**Dependency Injection (DI)**
A design pattern where dependencies are provided to an object rather than the object creating them itself. Built into .NET Core.

**Development Server**
A local server used during development to serve the application. Vite dev server runs on port 5173, .NET development server on port 5000.

**DTO (Data Transfer Object)**
An object that carries data between processes. Used to transfer data between the frontend and backend API.

### E

**ESLint**
A static code analysis tool for identifying problematic patterns in JavaScript/TypeScript code.

**Entity**
A domain object that has a distinct identity and lifecycle. Part of Domain-Driven Design terminology.

### F

**Frontend**
The client-side component of the application that users interact with directly. Built with React 19, TypeScript, and Vite.

**Functional Component**
A React component defined as a JavaScript function that takes props as arguments and returns JSX.

### G

**Git**
A distributed version control system used for tracking changes in source code during software development.

**GitHub**
A web-based hosting service for Git repositories, providing collaboration features and issue tracking.

### H

**Health Check**
An endpoint or mechanism used to verify that an application or service is running properly and can respond to requests.

**Hook**
In React, functions that let you use state and other React features in functional components. Examples: useState, useEffect, custom hooks.

**Hot Module Replacement (HMR)**
A development feature that allows modules to be updated in real-time without refreshing the entire page.

### I

**Infrastructure Layer**
In Clean Architecture, the outermost layer responsible for data access, external services, and technical concerns.

**Interface**
In programming, a contract that defines the methods and properties that a class must implement. Used extensively in .NET for dependency abstraction.

### J

**JSX (JavaScript XML)**
A syntax extension for JavaScript that allows writing HTML-like code in JavaScript files. Used in React components.

**JSON (JavaScript Object Notation)**
A lightweight data interchange format that is easy for humans to read and write, commonly used for API communication.

### L

**Localhost**
The standard hostname that refers to the current computer. Used for local development (localhost:5173 for frontend, localhost:5000 for backend).

### M

**Middleware**
Software that acts as a bridge between different applications or components. In ASP.NET Core, used for request processing pipeline.

**Mermaid**
A JavaScript-based diagramming and charting tool that uses text definitions to create diagrams. Used for all architectural diagrams in this documentation.

### N

**Node.js**
A JavaScript runtime built on Chrome's V8 JavaScript engine, used for running the frontend development tools and build processes.

**npm (Node Package Manager)**
The default package manager for Node.js, used to install and manage frontend dependencies.

**NuGet**
The package manager for .NET, used to install and manage backend dependencies.

### P

**Package.json**
A file that contains metadata about a Node.js project, including dependencies, scripts, and project information.

**Props**
Short for "properties," the inputs that React components accept to customize their behavior and appearance.

### R

**React**
A JavaScript library for building user interfaces, particularly single-page applications. Version 19 is used in this project.

**Repository Pattern**
A design pattern that encapsulates data access logic and provides a more object-oriented view of the persistence layer.

**REST (Representational State Transfer)**
An architectural style for designing networked applications, commonly used for web APIs.

### S

**SPA (Single Page Application)**
A web application that loads a single HTML page and dynamically updates content as the user interacts with the app.

**State**
In React, data that can change over time and affects what is rendered on the screen. Managed using hooks like useState.

### T

**TypeScript**
A programming language that adds static type definitions to JavaScript, providing better tooling and error detection.

**Technical Debt**
The cost of additional rework caused by choosing an easy solution now instead of using a better approach that would take longer.

### U

**UI (User Interface)**
The visual elements and controls that users interact with in an application.

**Unit of Work**
A design pattern that maintains a list of objects affected by a business transaction and coordinates writing out changes.

### V

**Vite**
A modern build tool for frontend development that provides fast development server and optimized production builds.

**Virtual DOM**
A JavaScript representation of the actual DOM that React uses to optimize rendering performance.

### W

**Webpack**
A module bundler for JavaScript applications (not used in this project, replaced by Vite for better performance).

**Web API**
An API that can be accessed over the web using HTTP protocol, typically returning JSON data.

## 12.2 Project-Specific Terms

### Application Layers

**API Layer**
The presentation layer containing controllers and HTTP-related concerns. Located in `backend/src/Api/`.

**Application Layer**
Contains application services, DTOs, and use case logic. Located in `backend/src/Application/`.

**Domain Layer**
Contains business entities and domain logic. Located in `backend/src/Domain/`.

**Infrastructure Layer**
Contains data access and external service implementations. Located in `backend/src/Infrastructure/`.

### File and Directory Structure

**backend/src/**
Root directory containing all backend source code organized by architectural layers.

**frontend/src/**
Root directory containing all frontend source code organized by feature and responsibility.

**documentation/**
Directory containing Arc42 architecture documentation files.

### Development Concepts

**Health Check Endpoint**
A specific API endpoint (`/api/health`) that returns the current status of the backend application.

**Console Logging**
Debug output displayed in the browser's developer console, used for tracking API calls and application state.

**Dark Mode Design**
A user interface design using dark colors for backgrounds and light colors for text, reducing eye strain in low-light environments.

### Configuration and Environment

**Development Environment**
Local development setup using Vite dev server (port 5173) and .NET development server (port 5000).

**CORS Configuration**
Cross-origin resource sharing settings that allow the frontend (localhost:5173) to make requests to the backend (localhost:5000).

**In-Memory Storage**
Temporary data storage that exists only while the application is running, used for demonstration purposes.

## 12.3 Abbreviations and Acronyms

| Abbreviation | Full Form | Context |
|--------------|-----------|---------|
| **API** | Application Programming Interface | Web services communication |
| **SPA** | Single Page Application | Frontend architecture |
| **DI** | Dependency Injection | .NET Core pattern |
| **DTO** | Data Transfer Object | Data layer pattern |
| **CORS** | Cross-Origin Resource Sharing | Web security mechanism |
| **HMR** | Hot Module Replacement | Development feature |
| **JSX** | JavaScript XML | React syntax |
| **JSON** | JavaScript Object Notation | Data format |
| **REST** | Representational State Transfer | API architecture |
| **UI** | User Interface | Frontend layer |
| **UX** | User Experience | Design consideration |
| **CI/CD** | Continuous Integration/Continuous Deployment | DevOps practice |
| **IDE** | Integrated Development Environment | Development tool |
| **CLI** | Command Line Interface | Terminal tools |
| **HTTP** | Hypertext Transfer Protocol | Web communication |
| **HTTPS** | HTTP Secure | Secure web communication |
| **CSS** | Cascading Style Sheets | Styling language |
| **HTML** | Hypertext Markup Language | Markup language |
| **DOM** | Document Object Model | Browser API |
| **npm** | Node Package Manager | JavaScript package manager |
| **URL** | Uniform Resource Locator | Web address |
| **GUID** | Globally Unique Identifier | Unique ID format |

## 12.4 Technology Stack Glossary

### Frontend Technologies

**React 19**
The latest version of React library used for building the user interface with improved performance and new features.

**TypeScript 5.x**
The version of TypeScript used providing enhanced type checking and modern JavaScript features.

**Vite**
Modern build tool providing fast development server, hot module replacement, and optimized production builds.

**ESLint**
Linting tool configured to enforce code quality and consistency across the TypeScript/React codebase.

### Backend Technologies

**.NET 9.0**
The latest version of Microsoft's development platform used for building the backend API with improved performance and features.

**ASP.NET Core**
Web framework built on .NET for creating web APIs and web applications with cross-platform support.

**Minimal APIs**
A simplified approach to creating APIs in ASP.NET Core with reduced boilerplate code.

### Development Tools

**Visual Studio Code**
The primary development environment recommended for this project with excellent TypeScript and C# support.

**Git**
Version control system used for tracking code changes and collaboration.

**dotnet CLI**
Command-line interface for .NET development, building, and running applications.

**Node.js**
JavaScript runtime required for frontend development tools and build processes.

## 12.5 Business Domain Terms

### Health Monitoring

**Health Status**
The current operational state of a system or service, typically reported as healthy, unhealthy, or degraded.

**Health Check**
A diagnostic test that verifies whether a system component is functioning correctly.

**Service Availability**
The degree to which a service is operational and accessible when required for use.

### Development Process

**Showcase Application**
A demonstration project designed to illustrate best practices, technologies, and architectural patterns.

**Proof of Concept**
A demonstration of feasibility for an idea or approach, typically with limited functionality.

**Technical Demonstration**
A presentation of technical capabilities and implementation approaches for educational or evaluation purposes.

---

**Navigation:** [← Risks and Technical Debts](11-risks-and-technical-debts.md) | [↑ Documentation Home](README.md)