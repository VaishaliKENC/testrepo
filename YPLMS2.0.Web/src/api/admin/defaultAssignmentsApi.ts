import  api  from "../api-config";
import { IMainDefaultassignment } from '../../Types/assignmentTypes';


// save a list of default based on assignment
export const saveDefaultAssignmentApi = async (param: IMainDefaultassignment) => {
  const response = await api.post('/ActivityAssignment/savedefaultassignment', param);
  return response.data;
};

// Fetches the list of DefaultAssignment
export const fetchDefaultAssignmentListApi = async (clientId: string) => {
  const response = await api.get(`/ActivityAssignment/getdefaultsettings?clientId=${clientId}`);
  return response.data;
};
