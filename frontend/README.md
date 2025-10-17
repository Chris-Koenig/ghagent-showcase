# Frontend - Health Check Dashboard

A simple React frontend application for testing the backend API connection.

## Features

- ✅ Health check page that calls the backend API
- 📊 Dark mode minimal design
- 🔍 Console logging for all API calls
- 📱 Responsive design

## Tech Stack

- **React 19** with TypeScript
- **Vite** for build tooling
- **Custom hooks** for state management

- **CSS3** with dark theme styling

## Getting Started

### Prerequisites

- Node.js (v18 or higher)
- npm

### Installation

1. Install dependencies:

   ```bash
   npm install
   ```

2. Start the development server:

   ```bash
   npm run dev
   ```

3. Open your browser and navigate to `http://localhost:5173`

### Usage

The application provides a health check dashboard that:

1. **Automatically checks** the backend health when the page loads

2. **Manual refresh** button to recheck the connection
3. **Console logging** - All API calls are logged to the browser console
   for debugging
4. **Visual status indicators** - Shows connection status with color-coded
   indicators

### API Logging

All frontend API calls are logged to the browser console with detailed
information:

- 🚀 Request details (URL, method, headers)
- 📊 Response status codes
- ✅ Success responses with data
- ❌ Error responses with details

### Architecture

```text
src/
├── components/     # Reusable UI components
│   └── HealthCheck.tsx
├── pages/         # Page components
│   └── HealthPage.tsx
├── hooks/         # Custom React hooks
│   └── useHealthCheck.ts
├── services/      # API service layer
│   └── healthService.ts
├── types/         # TypeScript type definitions
│   └── health.ts
└── utils/         # Utility functions
```

### Build for Production

```bash
npm run build
```

The build artifacts will be stored in the `dist/` directory.

### Linting

```bash
npm run lint
```

## Backend Integration

The frontend is configured to connect to the backend API running on
`http://localhost:5000`.

The health check endpoint: `GET /api/health`

Expected response:

```json
{
  "utcNow": "2024-01-01T12:00:00.000Z",
  "status": "OK"
}
```

## Development Notes

- CORS is configured in the backend to allow requests from
  `http://localhost:5173`
- All API calls include detailed console logging for debugging
- The app uses a modern dark theme optimized for development
- TypeScript strict mode is enabled for better type safety
