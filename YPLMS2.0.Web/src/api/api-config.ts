import axios from "axios";
import store from "../redux/store";
import { logout } from "../redux/Slice/authSlice";

// Set default headers
const defaultHeaders = {
  "Content-Type": "application/json",
};

// Create Axios instance
const apiInstanceClient = axios.create({
  baseURL: process.env.REACT_APP_API_BASE_URL,
  headers: defaultHeaders
});

// Request interceptor – inject token from Redux store
apiInstanceClient.interceptors.request.use(
  (config) => {
    // const { token } = store.getState().auth;
    const token = sessionStorage.getItem("token");

    config.headers = config.headers || {};

    if (token) {
      config.headers["Authorization"] = `Bearer ${token}`;
    }

    return config;
  },
  (error) => {
    console.error("Request Error:", error);
    return Promise.reject(
      error instanceof Error ? error : new Error(String(error))
    );
  }
);

// Response interceptor – handle 401/403 unauthorized
apiInstanceClient.interceptors.response.use(
  (response) => response,
  (error) => {
    const status = error?.response?.status;

    if ((status === 403 || status === 401) && typeof window !== "undefined") {
      console.warn("Unauthorized or token expired. Logging out.");
      sessionStorage.removeItem("token");
      localStorage.removeItem("persist:user");
      localStorage.removeItem("persist:osc-checkout");

      window.location.href = "/login";
    }

    return Promise.reject(
      error instanceof Error ? error : new Error(String(error))
    );
  }
);

export default apiInstanceClient;

// Optional helper
export const apiInstanceClientMethods = {
  removeToken: () => {
    store.dispatch(logout());
  },
};


// import axios from "axios";
// import { logout } from "../redux/Slice/authSlice";
// import store from "../redux/store";

// export const api = axios.create({
//   baseURL: process.env.REACT_APP_API_BASE_URL,
//   headers: {
//     'Content-Type': 'application/json',
//   },
// });


// // Add an interceptor to handle unauthorized (401) responses
// api.interceptors.response.use(
//   (response) => response,
//   (error) => {
//     if (error.response && error.response.status === 401) {
//       // If the server responds with a 401 (Unauthorized), log the user out
//       store.dispatch(logout());
//     }
//     return Promise.reject(
//       error instanceof Error ? error : new Error(String(error))
//     );
//   }
// );