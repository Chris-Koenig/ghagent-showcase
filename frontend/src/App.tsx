import React, { useState } from "react";
import { HealthPage, NameValidationPage } from "./pages";
import { Navigation } from "./components";
import "./App.css";

type PageType = "health" | "nameValidation";

const App: React.FC = () => {
  const [currentPage, setCurrentPage] = useState<PageType>("health");

  const renderPage = () => {
    switch (currentPage) {
      case "health":
        return <HealthPage />;
      case "nameValidation":
        return <NameValidationPage />;
      default:
        return <HealthPage />;
    }
  };

  return (
    <div className="app">
      <Navigation currentPage={currentPage} onPageChange={setCurrentPage} />
      <main className="app-content">{renderPage()}</main>
    </div>
  );
};

export default App;
