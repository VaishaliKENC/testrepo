// Auth-related utility functions
// src/utils/authUtils.ts

// export const getToken = () => localStorage.getItem('token');
// export const removeToken = () => localStorage.removeItem('token');


export const getToken = () => sessionStorage.getItem('token');
export const setToken = (token: string) => sessionStorage.setItem('token', token);
export const removeToken = () => sessionStorage.removeItem('token');


// Helper functions to manage session storage
export const getSessionData = (key: string) => sessionStorage.getItem(key);
export const setSessionData = (key: string, value: string) => sessionStorage.setItem(key, value);
export const removeSessionData = (key: string) => sessionStorage.removeItem(key);


// export const removeSessionData = (key: string) => {
//   sessionStorage.removeItem(key);
//   localStorage.removeItem(key);
// };
