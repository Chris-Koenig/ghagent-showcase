/**
 * Sanitize user input to prevent XSS attacks
 * This is a basic implementation - in production, consider using DOMPurify
 */
export function sanitizeInput(input: string): string {
  const element = document.createElement('div');
  element.textContent = input;
  return element.innerHTML;
}

/**
 * Validate email format
 */
export function isValidEmail(email: string): boolean {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return emailRegex.test(email);
}

/**
 * Validate that a string is not empty after trimming
 */
export function isNotEmpty(value: string): boolean {
  return value.trim().length > 0;
}
