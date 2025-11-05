# Frontend - User Management Application

A modern, secure React frontend application built with Vite and TypeScript for managing users.

## Features

- 🎨 Clean, modern UI design
- ✅ Form validation (client-side)
- 🔒 Security best practices (XSS prevention, input sanitization)
- 🎯 TypeScript for type safety
- 🚀 Fast development with Vite
- 📱 Responsive design
- ⚡ Error boundaries for graceful error handling
- 🧩 Component-based architecture

## Tech Stack

- **React 19** - UI library
- **TypeScript** - Type safety
- **Vite** - Build tool and dev server
- **CSS3** - Styling

## Project Structure

```
src/
├── components/         # Reusable UI components
│   ├── ErrorBoundary.tsx
│   ├── UserList.tsx
│   └── UserForm.tsx
├── pages/             # Page components
│   └── Users.tsx
├── services/          # API service calls
│   └── api.ts
├── types/             # TypeScript type definitions
│   └── user.ts
├── utils/             # Utility functions
│   └── sanitize.ts
├── App.tsx            # Main application component
└── main.tsx           # Application entry point
```

## Getting Started

### Prerequisites

- Node.js 18 or higher
- npm or yarn

### Installation

```bash
# Install dependencies
npm install

# Copy environment variables
cp .env.example .env

# Update the API URL in .env if needed
VITE_API_URL=http://localhost:5000/api
```

### Development

```bash
# Start the development server
npm run dev

# The app will be available at http://localhost:5173
```

### Build

```bash
# Build for production
npm run build

# Preview the production build
npm run preview
```

### Linting

```bash
# Run ESLint
npm run lint
```

## Security Features

- **XSS Prevention**: All user inputs are sanitized using React's built-in protection
- **Input Validation**: Client-side validation for email format and required fields
- **Error Boundaries**: Graceful error handling to prevent application crashes
- **Type Safety**: TypeScript ensures type correctness throughout the application

## API Integration

The frontend communicates with a .NET Core backend API. Configure the API URL in the `.env` file:

```
VITE_API_URL=http://localhost:5000/api
```

## Available Scripts

- `npm run dev` - Start development server
- `npm run build` - Build for production
- `npm run preview` - Preview production build
- `npm run lint` - Run ESLint

## Browser Support

- Chrome (latest)
- Firefox (latest)
- Safari (latest)
- Edge (latest)
