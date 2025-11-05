/**
 * Basic text escaping for display purposes
 * NOTE: This is a minimal implementation. React's JSX already provides XSS protection
 * by escaping content by default. For cases where you need to render HTML content,
 * use a proper sanitization library like DOMPurify in production.
 * 
 * This function is provided as a utility but may not be necessary for most React use cases.
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
