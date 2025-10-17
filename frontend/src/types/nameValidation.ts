export interface NameValidationRequest {
  name: string;
}

export interface NameValidationResponse {
  isValid: boolean;
  message: string;
}
