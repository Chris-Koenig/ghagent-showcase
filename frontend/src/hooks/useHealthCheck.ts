import { useState, useCallback } from "react";
import type { HealthResponse } from "../types";
import { healthService } from "../services";

interface UseHealthCheck {
  health: HealthResponse | null;
  loading: boolean;
  error: string | null;
  checkHealth: () => Promise<void>;
}

export const useHealthCheck = (): UseHealthCheck => {
  const [health, setHealth] = useState<HealthResponse | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const checkHealth = useCallback(async () => {
    setLoading(true);
    setError(null);

    try {
      const healthData = await healthService.checkHealth();
      setHealth(healthData);
    } catch (err) {
      const errorMessage =
        err instanceof Error ? err.message : "Unknown error occurred";
      setError(errorMessage);
      setHealth(null);
    } finally {
      setLoading(false);
    }
  }, []);

  return {
    health,
    loading,
    error,
    checkHealth,
  };
};
