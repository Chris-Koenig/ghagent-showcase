import type { HealthResponse } from "../types";

const API_BASE_URL = "http://localhost:5000"; // Backend URL

class HealthService {
  private async makeRequest<T>(
    endpoint: string,
    options?: RequestInit
  ): Promise<T> {
    const url = `${API_BASE_URL}${endpoint}`;

    console.log(`🚀 API Call: ${options?.method || "GET"} ${url}`);
    console.log("📋 Request details:", {
      url,
      method: options?.method || "GET",
      headers: options?.headers,
      body: options?.body,
    });

    try {
      const response = await fetch(url, {
        headers: {
          "Content-Type": "application/json",
          ...options?.headers,
        },
        ...options,
      });

      console.log(
        `📊 Response status: ${response.status} ${response.statusText}`
      );

      if (!response.ok) {
        const errorData = await response.text();
        console.error("❌ API Error:", {
          status: response.status,
          statusText: response.statusText,
          body: errorData,
        });
        throw new Error(`HTTP ${response.status}: ${response.statusText}`);
      }

      const data = await response.json();
      console.log("✅ API Success:", data);

      return data;
    } catch (error) {
      console.error("🔥 API Request failed:", error);
      throw error;
    }
  }

  async checkHealth(): Promise<HealthResponse> {
    return this.makeRequest<HealthResponse>("/api/health");
  }
}

export const healthService = new HealthService();
