import { ApiRoutes } from "../../utils/Constants/apiRoutes";
import api from "../api-config";

export const fetchCourseListforLeaderBoard = async (params: {
  clientId: string;
  userId?: string;
}) => {
  let url = `${ApiRoutes.leaderBoardCourseList}?ClientId=${params.clientId}`;
  if (params.userId && params.userId !== undefined) {
    url = `${ApiRoutes.leaderBoardCourseList}?ClientId=${params.clientId}&SystemUserGUID=${params?.userId}`;
  }

  const response = await api.get(url);
  return response.data;
};

export const fetchAdminLeaderBoardData = async (params: {
  clientId: string;
  activityId: string;
  pageIndex?: number;
  pageSize?: number;
}) => {
  const response = await api.get(
    `${ApiRoutes.leaderBoardRank}?ClientId=${params.clientId}&ActivityId=${params.activityId}&PageIndex=${params.pageIndex}&PageSize=${params.pageSize}`
  );
  return response.data;
};

export const fetchLearnerLeaderboardData = async (params: {
  clientId: string;
  systemId: string;
  activityId: string;
  rows?: number;
}) => {
  const response = await api.get(
    `${ApiRoutes.leaderBoardMyRank}?ClientId=${params.clientId}&SystemUserGUID=${params.systemId}&ActivityId=${params.activityId}&Total_Row=${params?.rows}`
  );
  return response.data;
};
