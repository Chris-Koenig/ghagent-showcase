# .NET Core Backend & React Frontend Development

## Purpose / Context

This instruction defines best practices for developing full-stack applications using .NET Core for the backend API and React for the frontend. The focus is on maintaining clean architecture principles, ensuring security, and following established patterns for scalable, maintainable applications.

## Solution Description

This is a showcase application demonstrating a full-stack solution with a .NET Core backend and a React frontend. The backend provides RESTful APIs for managing user data, while the frontend offers a user interface for interacting with these APIs. The application emphasizes best practices in code organization, security, and maintainability.

## Best Practices

### Architecture & Design Principles

- **Single Responsibility Principle (SRP)**: Each class, method, and component should have one reason to change
- **DRY (Don't Repeat Yourself)**: Extract common functionality into reusable components, services, and utilities
- **Separation of Concerns**: Clearly separate business logic, data access, and presentation layers
- **Dependency Injection**: Use .NET Core's built-in DI container for loose coupling
- **Clean Architecture**: Organize code in layers (Domain, Application, Infrastructure, Presentation)

### .NET Core Backend

- **API Structure**: Use Controllers only for HTTP concerns; delegate business logic to Services
- **DTOs**: Always use Data Transfer Objects for API input/output; never expose domain entities directly
- **Validation**: Implement input validation using FluentValidation or Data Annotations
- **Error Handling**: Use global exception middleware for consistent error responses
- **Logging**: Implement structured logging with Serilog or built-in ILogger
- **Repository Pattern**: Abstract data access behind repository interfaces
- **Unit of Work**: Group related database operations for transaction management
- in the Backend store all data in memory - do not use a real database

### React Frontend

- **Component Structure**: Use functional components with hooks; keep components small and focused
- **State Management**: Use React Context for global state, useState/useReducer for local state
- **Custom Hooks**: Extract reusable logic into custom hooks
- **Error Boundaries**: Implement error boundaries for graceful error handling
- **Code Splitting**: Use React.lazy() and Suspense for route-based code splitting
- **TypeScript**: Use TypeScript for type safety and better developer experience

### Security

#### Backend Security

- **Authentication**: This is a schowcase - do not implement real authentication
- **Authorization**: This is a schowcase - do not implement real authorization
- **HTTPS**: Enforce HTTPS in production
- **CORS**: Configure CORS policies restrictively
- **Input Validation**: Validate and sanitize all inputs
- **SQL Injection**: Use parameterized queries or Entity Framework
- **Rate Limiting**: Implement API rate limiting
- **Security Headers**: Add security headers (HSTS, CSP, X-Frame-Options)

#### Frontend Security

- **XSS Prevention**: Sanitize user inputs and use React's built-in XSS protection
- **CSRF Protection**: Include CSRF tokens in forms
- **Secure Storage**: Store sensitive data in httpOnly cookies, not localStorage
- **Content Security Policy**: Implement CSP headers
- **Dependency Security**: Regularly audit and update npm packages

### Code Organization

#### Backend Structure

```text
src/
├── Api/                    # Web API layer
│   ├── Controllers/
│   ├── Middleware/
│   └── Program.cs
├── Application/            # Application services layer
│   ├── Services/
│   ├── DTOs/
│   └── Interfaces/
├── Domain/                 # Domain entities and business logic
│   ├── Entities/
│   └── Interfaces/
└── Infrastructure/         # Data access and external services
    ├── Data/
    ├── Repositories/
    └── Services/
```

#### Frontend Structure

```text
src/
├── components/             # Reusable UI components
├── pages/                  # Page components
├── hooks/                  # Custom hooks
├── services/              # API service calls
├── utils/                 # Utility functions
├── types/                 # TypeScript type definitions
└── contexts/              # React contexts
```

## Example

### Backend API Controller

```csharp
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        try
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving users");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userService.CreateUserAsync(createUserDto);
        return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
    }
}
```

### Frontend React Component

```typescript
interface User {
  id: number;
  name: string;
  email: string;
}

interface UserListProps {
  onUserSelect: (user: User) => void;
}

const UserList: React.FC<UserListProps> = ({ onUserSelect }) => {
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const response = await userService.getUsers();
        setUsers(response.data);
      } catch (err) {
        setError("Failed to load users");
      } finally {
        setLoading(false);
      }
    };

    fetchUsers();
  }, []);

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <ul>
      {users.map((user) => (
        <li key={user.id} onClick={() => onUserSelect(user)}>
          {user.name} - {user.email}
        </li>
      ))}
    </ul>
  );
};
```

## Anti-Patterns

### Backend Anti-Patterns

- **Fat Controllers**: Avoid putting business logic directly in controllers
- **Exposing Entities**: Never return domain entities directly from APIs
- **Global State**: Avoid static variables for application state
- **Hardcoded Strings**: Use configuration or constants instead
- **Ignoring Exceptions**: Always handle exceptions appropriately

### Frontend Anti-Patterns

- **Prop Drilling**: Avoid passing props through many component levels
- **Large Components**: Keep components focused and under 200 lines
- **Direct DOM Manipulation**: Use React's declarative approach
- **Inline Styles**: Use CSS modules or styled-components instead
- **Missing Keys**: Always provide keys for list items

### Security Anti-Patterns

- **Storing Secrets in Code**: Use environment variables or secure vaults
- **Client-Side Security**: Never rely solely on frontend validation
- **Weak Authentication**: Implement proper JWT handling and refresh tokens
- **Excessive Permissions**: Follow principle of least privilege

## References

- [.NET Core Security Best Practices](https://docs.microsoft.com/en-us/aspnet/core/security/)
- [React Security Best Practices](https://reactjs.org/docs/dom-elements.html#dangerouslysetinnerhtml)
- [Clean Architecture in .NET](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)
- [OWASP Top 10](https://owasp.org/www-project-top-ten/)
- [React TypeScript Best Practices](https://react-typescript-cheatsheet.netlify.app/)
