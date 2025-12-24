export {};

/* Context-based- medium apps  */

// import React, { createContext, useEffect, useState, useCallback } from "react";
// import { useNavigate } from "react-router-dom";
// import { useDispatch } from "react-redux";
// import { logoutUser } from "../redux/actions/authActions"; // Adjust path as needed

// const SESSION_TIMEOUT = 60 * 1000; // 1 minute

// interface SessionContextType {
//   resetSession: () => void;
// }

// export const SessionContext = createContext<SessionContextType | null>(null);

// export const SessionProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
//   const [timeoutId, setTimeoutId] = useState<NodeJS.Timeout | null>(null);
//   const navigate = useNavigate();
//   const dispatch = useDispatch();

//   const logout = useCallback(() => {
//     dispatch(logoutUser());
//     navigate("/login");
//   }, [dispatch, navigate]);

//   const resetSession = useCallback(() => {
//     if (timeoutId) clearTimeout(timeoutId);
//     const id = setTimeout(logout, SESSION_TIMEOUT);
//     setTimeoutId(id);
//   }, [timeoutId, logout]);

//   useEffect(() => {
//     resetSession();

//     window.addEventListener("mousemove", resetSession);
//     window.addEventListener("keydown", resetSession);
//     window.addEventListener("click", resetSession);

//     return () => {
//       if (timeoutId) clearTimeout(timeoutId);
//       window.removeEventListener("mousemove", resetSession);
//       window.removeEventListener("keydown", resetSession);
//       window.removeEventListener("click", resetSession);
//     };
//   }, [resetSession, timeoutId]);

//   return (
//     <SessionContext.Provider value={{ resetSession }}>
//       {children}
//     </SessionContext.Provider>
//   );
// };


/* Requiring persistence (multi-tab)-- using Local Storage */

// import { useEffect } from "react";
// import { useNavigate } from "react-router-dom";
// import { useDispatch } from "react-redux";
// import { logoutUser } from "../redux/actions/authActions"; // Adjust path as needed

// const SESSION_TIMEOUT = 60 * 1000; // 1 minute

// export const useSessionTimeout = () => {
//   const navigate = useNavigate();
//   const dispatch = useDispatch();

//   useEffect(() => {
//     const checkTimeout = () => {
//       const lastActivity = localStorage.getItem("lastActivity");
//       if (lastActivity && Date.now() - parseInt(lastActivity) > SESSION_TIMEOUT) {
//         dispatch(logoutUser());
//         navigate("/login");
//       }
//     };

//     const updateActivity = () => {
//       localStorage.setItem("lastActivity", Date.now().toString());
//     };

//     updateActivity();
//     const interval = setInterval(checkTimeout, 5000);

//     window.addEventListener("mousemove", updateActivity);
//     window.addEventListener("keydown", updateActivity);
//     window.addEventListener("click", updateActivity);

//     return () => {
//       clearInterval(interval);
//       window.removeEventListener("mousemove", updateActivity);
//       window.removeEventListener("keydown", updateActivity);
//       window.removeEventListener("click", updateActivity);
//     };
//   }, [dispatch, navigate]);
// };
