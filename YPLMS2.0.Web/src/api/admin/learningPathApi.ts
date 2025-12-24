import api from "../api-config";
import { ILearningPathFilterPayload, IMainPayloadLearningPath } from "../../Types/learningPathTypes";
import { ILearningPathPayload, IDeleteLearningPathPayload, IActivateDeActivateLearningPathPayload } from "../../Types/commonTableTypes";

// Fetches a list of Learning path list based on the provided
export const fetchLearningPathApi = async (param: IMainPayloadLearningPath) => {
  const response = await api.post('/Curriculum/curriculumlist', param);
  return response.data;
};

// Deletes selected Learning path list
export const deleteSelectedLearningPathApi = async (param: IDeleteLearningPathPayload) => {
  const response = await api.delete('/Curriculum/DeleteCurriculum', { data: param, });
  return response.data;
};

//  activates selected Learning path list
export const activateSelectedLearningPathApi = async (id:string | undefined, params: IActivateDeActivateLearningPathPayload) => {
  const response = await api.patch(`/Curriculum/activate/${id}`, params);
  return response.data;
};

//  deActivates selected Learning path list
export const deActivateSelectedLearningPathApi = async (id:string | undefined, params: IActivateDeActivateLearningPathPayload) => {
  const response = await api.patch(`/Curriculum/deactivate/${id}`, params);
  return response.data;
};

// Copy selected Learning path list
export const copyLearningPathApi = async (param: ILearningPathPayload) => {
  const response = await api.post('/Curriculum/copy', param);
  return response.data;
};


// Fetches a list of admin based on filter criteria
export const fetchSearchData = async (param: ILearningPathFilterPayload) => {
  const response = await api.post('/LearnerDAM/searchadmin', param);
  return response.data;
};
