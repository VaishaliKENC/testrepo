import { Middleware } from "@reduxjs/toolkit";
import { logoutUser } from "../../../src/redux/Slice/authSlice"; // Adjust path as needed

const SESSION_TIMEOUT = 6 * 60 * 1000; // 6 minute
let timeoutId: NodeJS.Timeout | null = null;

export const sessionMiddleware: Middleware = (store) => (next) => (action) => {
  if (timeoutId) clearTimeout(timeoutId); // Reset timeout on any Redux action

  timeoutId = setTimeout(() => {
    // store.dispatch(logoutUser()); // Dispatch logout action
    window.location.href = "/login"; //once logout API implement- remove this 
  }, SESSION_TIMEOUT);

  return next(action);
};
