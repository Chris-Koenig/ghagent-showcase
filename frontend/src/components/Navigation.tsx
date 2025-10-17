import React from "react";
import "./Navigation.css";

interface NavigationProps {
  currentPage: "health" | "nameValidation";
  onPageChange: (page: "health" | "nameValidation") => void;
}

export const Navigation: React.FC<NavigationProps> = ({
  currentPage,
  onPageChange,
}) => {
  return (
    <nav className="navigation">
      <div className="navigation-container">
        <div className="navigation-brand">
          <h2>GH Agent Showcase</h2>
        </div>

        <div className="navigation-links">
          <button
            className={`nav-button ${currentPage === "health" ? "active" : ""}`}
            onClick={() => onPageChange("health")}
          >
            <span className="nav-icon">❤️</span>
            Health Check
          </button>

          <button
            className={`nav-button ${
              currentPage === "nameValidation" ? "active" : ""
            }`}
            onClick={() => onPageChange("nameValidation")}
          >
            <span className="nav-icon">✅</span>
            Name Validation
          </button>
        </div>
      </div>
    </nav>
  );
};
