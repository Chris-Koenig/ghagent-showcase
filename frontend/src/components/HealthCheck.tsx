import React, { useEffect } from "react";
import { useHealthCheck } from "../hooks";

const HealthCheck: React.FC = () => {
  const { health, loading, error, checkHealth } = useHealthCheck();

  useEffect(() => {
    // Perform initial health check on component mount
    checkHealth();
  }, [checkHealth]);

  const getStatusColor = (status?: string) => {
    if (!status) return "#ef4444"; // red
    return status.toLowerCase() === "ok" ? "#22c55e" : "#ef4444"; // green or red
  };

  const formatDateTime = (dateString?: string) => {
    if (!dateString) return "N/A";
    try {
      return new Date(dateString).toLocaleString();
    } catch {
      return dateString;
    }
  };

  return (
    <div className="health-check">
      <div className="health-header">
        <h2>🏥 Backend Health Check</h2>
        <button
          onClick={checkHealth}
          disabled={loading}
          className="refresh-button"
        >
          {loading ? "🔄" : "🔄"} {loading ? "Checking..." : "Refresh"}
        </button>
      </div>

      <div className="health-content">
        {loading && (
          <div className="health-status loading">
            <div className="status-indicator pulsing"></div>
            <span>Checking backend health...</span>
          </div>
        )}

        {error && (
          <div className="health-status error">
            <div className="status-indicator error"></div>
            <div>
              <strong>❌ Connection Failed</strong>
              <p>{error}</p>
            </div>
          </div>
        )}

        {health && !loading && (
          <div className="health-status success">
            <div
              className="status-indicator"
              style={{ backgroundColor: getStatusColor(health.status) }}
            ></div>
            <div className="health-details">
              <div className="health-item">
                <strong>✅ Backend Status:</strong> {health.status}
              </div>
              <div className="health-item">
                <strong>🕐 Server Time:</strong> {formatDateTime(health.utcNow)}
              </div>
            </div>
          </div>
        )}
      </div>

      <div className="health-info">
        <p>
          💡 This component tests the connection to the backend API by calling
          the health endpoint.
        </p>
        <p>🔍 Check the browser console for detailed API logs.</p>
      </div>
    </div>
  );
};

export { HealthCheck };
