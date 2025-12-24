import  api  from "../api-config";

// Fetches a Admin Dashboard count data based on client 
export const fetchAdminDashboardCountApi = async (clientId: string, currentUserId: string) => {
  const response = await api.get(`/AdminDashboard/getadmindashboardcountdata?ClientID=${clientId}&CurrentUserId=${currentUserId}`);
  return response.data;
};


// Fetches a Admin Dashboard Recent Assignment
export const fetchGetRecentAssignmentApi = async (clientId: string, currentUserId: string) => {
  const response = await api.get(`/AdminDashboard/getrecentassignment?ClientID=${clientId}&CurrentUserId=${currentUserId}`);
  return response.data;
};


// Fetches a Admin Dashboard Top Assignment
export const fetchGetTopAssignmentApi = async (clientId: string, currentUserId: string) => {
  const response = await api.get(`/AdminDashboard/gettopassignment?ClientID=${clientId}&CurrentUserId=${currentUserId}`);
  return response.data;
};
