import  api  from "../api-config";
import { IMainPayload, IActivateDeactivatePayload, IUserAddUserPayload, IDeletePayload, IApproveRejectPayload } from '../../Types/commonTableTypes';

// Fetches a list of users based on filter criteria
export const fetchUserData = async (user: IMainPayload) => {
  const response = await api.post('/LearnerDAM/searchlearners', user);
  return response.data;
};

// Activates or deactivates multiple users in bulk
export const activateDeactivateApi = async (user: IActivateDeactivatePayload[]) => {
  const response = await api.post('/LearnerDAM/bulkactivatedeactivate', user);
  return response.data;
};

// Adds a new user to the system
export const addUserApi = async (user: IUserAddUserPayload) => {
  const response = await api.post('/LearnerDAM/adduser', user);
  return response.data;
};

// Deletes a user by user ID
export const deleteUserApi = async (userId: number) => {
  const response = await api.delete(`/users/${userId}`);
  return response.data;
};

// Updates an existing user's details
export const updateUserApi = async (user: IUserAddUserPayload) => {
  const response = await api.post(`/LearnerDAM/updateuser`, user);
  return response.data;
};

// Fetches data of a single user by user ID
export const fetchSingleUserDataApi = async (user: IMainPayload) => {
  const response = await api.post('/LearnerDAM/getuserbyid', user);
  return response.data;
};

// Adds a new user to the system
export const addUserGetclientFieldsApi = async (user: IMainPayload) => {
  const response = await api.post('/LearnerDAM/addusergetclientfields', user);
  return response.data;
};

// Fetches import definition list for configuring a profile user
export const configureProfileUserApi = async (profileUser: any) => {
  const response = await api.post('/ImportDefination/getimportdefinationlist', profileUser);
  return response.data;
};

// Edits an existing import definition for a profile user
export const editConfigureProfileUserApi = async (profileUser: any) => {
  const response = await api.post('/ImportDefination/editimportdefination', profileUser);
  return response.data;
};

// Send Email Profile Definition
export const importDefinationSendMailApi = async (clientId: string, currentUserId: string, emailId: string) => {
  const response = await api.get(`/ImportDefination/importdefinationsendmail?ClientID=${clientId}&CurrentUserId=${currentUserId}&EmailID=${emailId}`);
  return response.data;
};

// Fetches a list of group rules for users
export const groupListApi = async (group: IMainPayload) => {
  const response = await api.post('/GroupRule/getgrouprulelist', group);
  return response.data;
};

// deactivates multiple groups in bulk
export const deactivateApi = async (user: IActivateDeactivatePayload[]) => {
  const response = await api.post('/GroupRule/deactivategrouprule', user);
  return response.data;
};

// Deletes a group by user ID
export const deleteGroupApi = async (group: IDeletePayload[]) => {
  const response = await api.delete('/GroupRule/deletegrouprule', { data: group, });
  return response.data;
};

// Fetches a list of pending approval users
export const fetchUsersPendingApprovalsApi = async (user: IMainPayload) => {
  const response = await api.post('/LearnerDAM/pendingapprovals', user);
  return response.data;
};

// Approve and reject pending approvals user list
export const approveRejectUsersApi = async (user: IApproveRejectPayload) => {
  const response = await api.post('/LearnerDAM/updateuserregistrationstatus', user);
  return response.data;
};

// Fetches count of users pending for approval
export const fetchPendingForApprovalsCountApi = async (clientId: IMainPayload) => {
  const response = await api.post('/LearnerDAM/pendingcount', clientId);
  return response.data;
};

// Pending approvals modal popup: Get user details 
export const fetchPendingApprovalsUserDetailsApi = async (clientId: string, signUpId: string) => {
  const response = await api.get(`/LearnerDAM/viewpendingapprovals?ClientID=${clientId}&SignUpId=${signUpId}`);
  return response.data;
};
