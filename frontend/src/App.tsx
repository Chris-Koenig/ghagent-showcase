import ErrorBoundary from './components/ErrorBoundary';
import Users from './pages/Users';
import './App.css';

function App() {
  return (
    <ErrorBoundary>
      <div className="app">
        <Users />
      </div>
    </ErrorBoundary>
  );
}

export default App;
