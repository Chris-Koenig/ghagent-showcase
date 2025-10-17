import React from "react";
import { HealthCheck } from "../components";

const HealthPage: React.FC = () => {
  return (
    <div className="page">
      <div className="page-header">
        <h1>🚀 Frontend Health Dashboard</h1>
        <p>Simple health monitoring for the backend connection</p>
      </div>

      <div className="page-content">
        <HealthCheck />
      </div>
    </div>
  );
};

export { HealthPage };
