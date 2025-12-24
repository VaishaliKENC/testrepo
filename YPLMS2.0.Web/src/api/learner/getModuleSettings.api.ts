import { ApiRoutes } from "../../utils/Constants/apiRoutes";
import  api  from "../api-config";

export const fetchModuleSettings = async (params: {
  moduleId: string;
  clientId: string;
}) => {
  const response = await api.post(`${ApiRoutes.getByModuleId}`, {
    id: params.moduleId,
    clientId: params.clientId,
    langugae: "en-US",
  });
  return response;
};
