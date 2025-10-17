import React, { useState } from "react";
import { useNameValidation } from "../hooks";
import "./NameValidation.css";

export const NameValidation: React.FC = () => {
  const [nameInput, setNameInput] = useState("");
  const { validateName, result, isLoading, error, clearResult } =
    useNameValidation();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (nameInput.trim()) {
      await validateName(nameInput.trim());
    }
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setNameInput(e.target.value);
    if (result || error) {
      clearResult();
    }
  };

  const getResultClassName = () => {
    if (!result) return "";
    return result.isValid ? "result-success" : "result-error";
  };

  return (
    <div className="name-validation">
      <div className="name-validation-container">
        <h1 className="name-validation-title">Name Validation</h1>
        <p className="name-validation-description">
          Enter your name to validate it. Names can only contain letters and
          spaces.
        </p>

        <form onSubmit={handleSubmit} className="name-validation-form">
          <div className="input-group">
            <label htmlFor="nameInput" className="input-label">
              Your Name
            </label>
            <input
              id="nameInput"
              type="text"
              value={nameInput}
              onChange={handleInputChange}
              placeholder="Enter your name..."
              className="name-input"
              disabled={isLoading}
            />
          </div>

          <button
            type="submit"
            disabled={isLoading || !nameInput.trim()}
            className="submit-button"
          >
            {isLoading ? "Validating..." : "Validate Name"}
          </button>
        </form>

        {error && (
          <div className="result-container result-error">
            <div className="result-icon">❌</div>
            <div className="result-content">
              <h3>Error</h3>
              <p>Failed to validate name: {error}</p>
            </div>
          </div>
        )}

        {result && (
          <div className={`result-container ${getResultClassName()}`}>
            <div className="result-icon">{result.isValid ? "✅" : "❌"}</div>
            <div className="result-content">
              <h3>{result.isValid ? "Valid Name" : "Invalid Name"}</h3>
              <p>{result.message}</p>
            </div>
          </div>
        )}

        <div className="validation-rules">
          <h3>Validation Rules</h3>
          <ul>
            <li>Must contain only letters and spaces</li>
            <li>Must be at least 2 characters long</li>
            <li>Cannot exceed 100 characters</li>
            <li>Cannot be empty</li>
          </ul>
        </div>
      </div>
    </div>
  );
};
