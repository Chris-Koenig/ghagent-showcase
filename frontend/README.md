# Frontend - Health Check Dashboard# React + TypeScript + Vite



A simple React frontend application for testing the backend API connection.This template provides a minimal setup to get React working in Vite with HMR and some ESLint rules.



## FeaturesCurrently, two official plugins are available:



- ✅ Health check page that calls the backend API- [@vitejs/plugin-react](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react) uses [Babel](https://babeljs.io/) (or [oxc](https://oxc.rs) when used in [rolldown-vite](https://vite.dev/guide/rolldown)) for Fast Refresh

- 📊 Dark mode minimal design- [@vitejs/plugin-react-swc](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react-swc) uses [SWC](https://swc.rs/) for Fast Refresh

- 🔍 Console logging for all API calls

- 📱 Responsive design## React Compiler



## Tech StackThe React Compiler is not enabled on this template because of its impact on dev & build performances. To add it, see [this documentation](https://react.dev/learn/react-compiler/installation).



- **React 19** with TypeScript## Expanding the ESLint configuration

- **Vite** for build tooling

- **Custom hooks** for state managementIf you are developing a production application, we recommend updating the configuration to enable type-aware lint rules:

- **CSS3** with dark theme styling

```js

## Getting Startedexport default defineConfig([

  globalIgnores(['dist']),

### Prerequisites  {

    files: ['**/*.{ts,tsx}'],

- Node.js (v18 or higher)    extends: [

- npm      // Other configs...



### Installation      // Remove tseslint.configs.recommended and replace with this

      tseslint.configs.recommendedTypeChecked,

1. Install dependencies:      // Alternatively, use this for stricter rules

   ```bash      tseslint.configs.strictTypeChecked,

   npm install      // Optionally, add this for stylistic rules

   ```      tseslint.configs.stylisticTypeChecked,



2. Start the development server:      // Other configs...

   ```bash    ],

   npm run dev    languageOptions: {

   ```      parserOptions: {

        project: ['./tsconfig.node.json', './tsconfig.app.json'],

3. Open your browser and navigate to `http://localhost:5173`        tsconfigRootDir: import.meta.dirname,

      },

### Usage      // other options...

    },

The application provides a health check dashboard that:  },

])

1. **Automatically checks** the backend health when the page loads```

2. **Manual refresh** button to recheck the connection

3. **Console logging** - All API calls are logged to the browser console for debuggingYou can also install [eslint-plugin-react-x](https://github.com/Rel1cx/eslint-react/tree/main/packages/plugins/eslint-plugin-react-x) and [eslint-plugin-react-dom](https://github.com/Rel1cx/eslint-react/tree/main/packages/plugins/eslint-plugin-react-dom) for React-specific lint rules:

4. **Visual status indicators** - Shows connection status with color-coded indicators

```js

### API Logging// eslint.config.js

import reactX from 'eslint-plugin-react-x'

All frontend API calls are logged to the browser console with detailed information:import reactDom from 'eslint-plugin-react-dom'



- 🚀 Request details (URL, method, headers)export default defineConfig([

- 📊 Response status codes  globalIgnores(['dist']),

- ✅ Success responses with data  {

- ❌ Error responses with details    files: ['**/*.{ts,tsx}'],

    extends: [

### Architecture      // Other configs...

      // Enable lint rules for React

```      reactX.configs['recommended-typescript'],

src/      // Enable lint rules for React DOM

├── components/     # Reusable UI components      reactDom.configs.recommended,

│   └── HealthCheck.tsx    ],

├── pages/         # Page components    languageOptions: {

│   └── HealthPage.tsx      parserOptions: {

├── hooks/         # Custom React hooks        project: ['./tsconfig.node.json', './tsconfig.app.json'],

│   └── useHealthCheck.ts        tsconfigRootDir: import.meta.dirname,

├── services/      # API service layer      },

│   └── healthService.ts      // other options...

├── types/         # TypeScript type definitions    },

│   └── health.ts  },

└── utils/         # Utility functions])

``````


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

The frontend is configured to connect to the backend API running on `http://localhost:5000`.

The health check endpoint: `GET /api/health`

Expected response:
```json
{
  "utcNow": "2024-01-01T12:00:00.000Z",
  "status": "OK"
}
```

## Development Notes

- CORS is configured in the backend to allow requests from `http://localhost:5173`
- All API calls include detailed console logging for debugging
- The app uses a modern dark theme optimized for development
- TypeScript strict mode is enabled for better type safety