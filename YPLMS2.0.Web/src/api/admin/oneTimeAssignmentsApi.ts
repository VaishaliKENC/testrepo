import  api  from "../api-config";
import { IMainPayloadOneTimeAssignment, ISaveOnetimeAssignment } from '../../Types/assignmentTypes';
import { ICategoryPayload, ISubCategoryPayload, IDeletePayload } from '../../Types/commonTableTypes';


// Fetches a list of activities based on filter criteria
export const fetchActivitiesApi = async (param: IMainPayloadOneTimeAssignment) => {
  const response = await api.post('/ActivityAssignment/getactivityonetimeassignmentforcurriculum', param);
  return response.data;
};

// Fetches the list of categories
export const fetchCategoriesApi = async (param: ICategoryPayload) => {
  const response = await api.post(`/ActivityAssignment/getallcategory`, param);
  return response.data;
};

// Fetches the list of sub categories
export const fetchSubCategoriesApi = async (param: ISubCategoryPayload) => {
  const response = await api.post(`/ActivityAssignment/getproductsubcategorybyid`, param);
  return response.data;
};

// Fetches the list of activity types
export const fetchActivityTypesApi = async (clientId: string) => {
  const response = await api.get(`/ActivityAssignment/getactivitytype?ClientId=${clientId}`);
  return response.data;
};

// Fetches a list of users based on filter criteria
export const fetchUsersApi = async (param: IMainPayloadOneTimeAssignment) => {
  const response = await api.post('/LearnerDAM/getlearnersforassignment', param);
  return response.data;
};

// Fetches a list of business rule based on client id
export const fetchBusinessRuleApi = async (param: any) => {
  const response = await api.post(`/ActivityAssignment/getbusinessrule?clientId=${param.clientId}`, param);
  return response.data;
};

// Save selected activities
export const saveSelectedActivitiesApi = async (param: any) => {
  const response = await api.post('/ActivityAssignment/saveselectedactivity', param);
  return response.data;
};

// Save selected users
export const saveSelectedUsersApi = async (param: any) => {
  const response = await api.post('/ActivityAssignment/saveselecteduser', param);
  return response.data;
};

// Fetches a list of users (business rule) based on filter criteria
export const fetchUsersBusinessRuleApi = async (param: IMainPayloadOneTimeAssignment) => {
  const response = await api.post('/ActivityAssignment/getbussinessruleusers', param);
  return response.data;
};

// Fetches a list of users (selected) based on filter criteria // Fetches a list of selected Users
export const fetchUsersSelectedApi = async (param: IMainPayloadOneTimeAssignment) => {
  const response = await api.post('/ActivityAssignment/getselecteduser', param);
  return response.data;
};

// Deletes selected user
export const deleteSelectedUserApi = async (param: IDeletePayload[]) => {
  const response = await api.delete('/ActivityAssignment/deleteselecteduser', { data: param, });
  return response.data;
};

// Fetches a list of selected activity
export const fetchSelectedActivityApi = async (param: IMainPayloadOneTimeAssignment) => {
  const response = await api.post('/ActivityAssignment/getselectedactivity', param);
  return response.data;
}

// Fetches a list of selected activity
export const submitOnetimeAssignmentAPI = async (param: ISaveOnetimeAssignment) => {
  const response = await api.post('/ActivityAssignment/submitonetimeassignment', param);
  return response.data;
}


