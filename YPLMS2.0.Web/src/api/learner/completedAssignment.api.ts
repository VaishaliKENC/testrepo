import { ApiRoutes } from "../../utils/Constants/apiRoutes";
import  api  from "../api-config";

export const fetchCompletedAssignment = async (params: {
  pageIndex: number;
  pageSize: number;
  sortExpression: any;
  clientId: string;
  userId: string;
  keyWord?: string;
}) => {
  const response = await api.post(`${ApiRoutes.completedAssignment}`, {
    listRange: {
      pageIndex: params.pageIndex,
      pageSize: params.pageSize,
      sortExpression: params.sortExpression,
      ...(params.keyWord && { keyWord: params.keyWord }),
    },
    userID: params.userId,
    clientId: params.clientId,
  });
  return response.data;
};
