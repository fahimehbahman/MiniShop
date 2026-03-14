import axios from "axios"

/**
 * Axios API Client
 *
 * This instance is used to communicate with the backend API. 
 * It automatically attaches the JWT token (if available) from localStorage
 * to the Authorization header for every request.
 *
 * Base URL:
 * https://localhost:7256/api
 *
 * Example:
 * apiClient.get("/users")
 * apiClient.post("/login", data)
 */
const apiClient = axios.create({
    baseURL: "https://localhost/api/",
    withCredentials: true
  });
  
  export default apiClient;