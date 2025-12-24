import { ApiRoutes } from "../../utils/Constants/apiRoutes";
import  api  from "../api-config";

export const fetchDashboardData = async (params: {
  clientId: string;
  userId: string;
}) => {
  const response = await api.post(
    `${ApiRoutes.learnerDashboard}?clientId=${params.clientId}&userID=${params.userId}`
  );
  return response.data;
};


export const fetchDashboardCurrentAssignment =async(params:{
  clientId: string;
  userId: string;
}) => {
  const response = await api.post(
    `${ApiRoutes.learnerCurrentAssignment}?ClientId=${params.clientId}&UserId=${params.userId}`
  );
  return response.data;
};

export const fetchDashboardCompletedAssignment=async(params:{ clientId: string;
  userId: string;}) => {
  const response = await api.post(
    `${ApiRoutes.learnerCompletedAssignment}?ClientId=${params.clientId}&UserId=${params.userId}`
  );
  return response.data;
};