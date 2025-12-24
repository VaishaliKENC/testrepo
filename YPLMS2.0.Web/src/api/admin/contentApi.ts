import api from "../api-config";
import {
  IActivateDeactivatePayload,
  IMainPayload,
  IDeletePayload,
  ICourseAddCoursePayload,
  IValidateXMLFilePayload,
  CourseListByFile,
} from "../../Types/commonTableTypes";
import { ApiRoutes } from "../../utils/Constants/apiRoutes";

// Fetches the list of courses based on given criteria
export const fetchCourseData = async (course: IMainPayload) => {
  const response = await api.post("/ContentModule/findcontentmodule", course);
  return response.data;
};

// Activates or deactivates selected courses.
export const activateDeactivateCourseApi = async (
  course: IActivateDeactivatePayload[]
) => {
  const response = await api.post(
    "/ContentModule/activatedeactivatemodules",
    course
  );
  return response.data;
};

// Deletes selected courses.
export const deleteCourseApi = async (course: IDeletePayload[]) => {
  const response = await api.delete("/ContentModule/deletecontentmodules", {
    data: course,
  });
  return response.data;
};

// Adds a new course to the system
export const addCourseApi = async (course: ICourseAddCoursePayload) => {
  const response = await api.post("/ContentModule/addcontentmodule", course);
  return response.data;
};

// Updates an existing course's details
export const updateCourseApi = async (course: ICourseAddCoursePayload) => {
  const response = await api.put("/ContentModule/editcontentmodule", course);
  return response.data;
};

// upload course thumbnail api
export const uploadCourseThumbnailApi = async (param: FormData) => {
  const response = await api.post(`/ContentModule/uploadthumbnail`, param, {
    headers: { "Content-Type": "multipart/form-data" },
  });
  return response.data;
};

// Upload course ZIP file API
export const uploadCourseZipApi = async (course: FormData) => {
  const response = await api.post(`/ContentModule/uploadcoursezip`, course, {
    headers: { "Content-Type": "multipart/form-data" },
  });
  return response.data;
};

// Validate Browsed XML file
export const validateXMLFileApi = async (course: IValidateXMLFilePayload) => {
  const response = await api.post(
    `/ContentModule/courseuploadvalidate`,
    course
  );
  return response.data;
};

// Fetches data of a single course by course ID
export const fetchSingleCourseDataApi = async (course: IMainPayload) => {
  const response = await api.post(
    "/ContentModule/getcontentmodulebyid",
    course
  );
  return response.data;
};

// Fetches the list of standard types for a given courses.
export const fetchStandardTypeData = async (clientId: any) => {
  const response = await api.get(
    `/ContentModule/getstandardtype?ClientId=${clientId}`,
    clientId
  );
  return response.data;
};

// Fetches the list of course types for a given courses.
export const fetchCourseTypeData = async (clientId: any) => {
  const response = await api.get(
    `/ContentModule/getcoursetype?ClientId=${clientId}`,
    clientId
  );
  return response.data;
};

// Fetches the list files and folders from given folder of provided client
export const fetchCourseStructureTreeDataApi = async (clientId: any) => {
  const response = await api.get(
    `/ContentModule/getcoursestructure?ClientId=${clientId}`,
    clientId
  );
  return response.data;
};

export const fetchCourseSubFoldersFiles = async (
  clientId: string,
  filePath: string
) => {
  const response = await api.get(
    `${ApiRoutes.courseSubFolders}?ClientId=${clientId}&relativePath=${filePath}`
  );
  return response.data;
};

export const fetchCourseListbyfile = async (params: CourseListByFile) => {
  const response = await api.post(`${ApiRoutes.courseFileList}`, {
    ...params,
    categoryId: "string",
    dateCreated: "2025-08-07T05:26:18.024Z",
    lastModifiedDate: "2025-08-07T05:26:18.024Z",
  });
  return response.data;
};
