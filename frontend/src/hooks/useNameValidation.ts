import { useState } from "react";
import { nameValidationService } from "../services/nameValidationService";
import type { NameValidationResponse } from "../types";

interface UseNameValidationReturn {
  validateName: (name: string) => Promise<void>;
  result: NameValidationResponse | null;
  isLoading: boolean;
  error: string | null;
  clearResult: () => void;
}

export const useNameValidation = (): UseNameValidationReturn => {
  const [result, setResult] = useState<NameValidationResponse | null>(null);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const validateName = async (name: string): Promise<void> => {
    setIsLoading(true);
    setError(null);
    setResult(null);

    try {
      const validationResult = await nameValidationService.validateName(name);
      setResult(validationResult);
    } catch (err) {
      setError(
        err instanceof Error ? err.message : "An unknown error occurred"
      );
    } finally {
      setIsLoading(false);
    }
  };

  const clearResult = (): void => {
    setResult(null);
    setError(null);
  };

  return {
    validateName,
    result,
    isLoading,
    error,
    clearResult,
  };
};
