export interface HealthResponse {
  utcNow: string;
  status: string;
}

export interface ApiError {
  message: string;
  status?: number;
}
