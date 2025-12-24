import  api  from "./api-config";
import { ICourseLaunchSessionPayload, ICoursePlayerPayload, ISendDataLMSPayload } from '../Types/coursePlayerScormType/coursePlayerType';

const courseScromApi = {
  getGlobalData: (payload: { ClientId: string; SessionId: string }) =>
    api.get("/ContentModuleSessionDAM/getbyidforcourselaunch", {
      params: {
        ClientId: payload.ClientId,
        SessionId: payload.SessionId,
      },
    }),

  sendDataToLMS: (sendData: ISendDataLMSPayload) =>
    api.post("/ContentModule/savecontrolframe", sendData),

  contentModuleSessionSave: (session: ICourseLaunchSessionPayload) =>
    api.post("/ContentModuleSessionDAM/contentmodulesessionsave", session),

  getAssignmentForLaunch: (coursePlayer: ICoursePlayerPayload) =>
    api.get("/CoursePlayer/getassignmentforlaunch", {
      params: coursePlayer,
    }),
};

// const courseScromApi = {
//   getGlobalData: (payload: { ClientId: string, SessionId: string }) => axios.get(`${API_URL}/ContentModuleSessionDAM/getbyidforcourselaunch`, {
//     params: {
//       ClientId: payload.ClientId,
//       SessionId: payload.SessionId
//     }
//   }),
//   sendDataToLMS: async (sendData: ISendDataLMSPayload) => {
//     return await api.post(`${API_URL}/ContentModule/savecontrolframe`, sendData);
//   },
//   contentModuleSessionSave: async (session: ICourseLaunchSessionPayload) => {
//     return await api.post(`${API_URL}/ContentModuleSessionDAM/contentmodulesessionsave`, session);
//   },
//   getAssignmentForLaunch: async (coursePlayer: ICoursePlayerPayload) => {
//     return await api.get(`${API_URL}/CoursePlayer/getassignmentforlaunch`, {
//       params: coursePlayer, // <- pass payload as query parameters
//     });
//   }



export default courseScromApi;
