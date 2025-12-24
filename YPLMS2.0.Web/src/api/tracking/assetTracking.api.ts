import { ApiRoutes } from "../../utils/Constants/apiRoutes";
import  api  from "../api-config";

export const enableAssetTracking = async (params: {
  clientId: string;
  currentUserID: string;
  activityId: string;
  activityType: string;
  languageId?: string;
  isForAdminPreview: boolean;
  progress: number;
}) => {
  const response = await api.post(`${ApiRoutes.setAssetTracking}`, {
    ClientId: params.clientId,
    CurrentUserID: params.currentUserID,
    ActivityId: params.activityId,
    ActivityType: params.activityType,
    LanguageId: params.languageId??'en',
    IsForAdminPreview: params.isForAdminPreview,
    Progress: params.progress,
  });
  return response.data;
};


export const enableVideoTracking = async (params: {
  activityId: string;
  activityType: string;
  clientId: string;
  learnerId: string;
  totalDuration: number | string;
  elaspedTime: number | string;
  videoEvent: string;
  activityName: string;
}) => {
  const response = await api.post(`${ApiRoutes.updateVideoTracking}`, {
    ActivityId: params.activityId,
    ActivityType: params.activityType,
    ClientId: params.clientId,
    LearnerId: params.learnerId,
    TotalDuration: params.totalDuration,
    ElaspedTime: params.elaspedTime,
    VideoEvent: params.videoEvent,
    ActivityName: params.activityName,
  });

  return response.data;
};

