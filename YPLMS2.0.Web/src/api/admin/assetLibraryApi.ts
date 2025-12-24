import  api  from "../api-config";
import { IMainPayload, IAssetLibraryAssetListActivateDeactivatePayload, IDeletePayload, IClientIdCurrentUserIdPayload, IAssetLibraryAddEditAssetFolderPayload } from '../../Types/commonTableTypes';

// Fetches the folders and sub folders for given clientId
export const fetchAssetFoldersApi = async (params: IClientIdCurrentUserIdPayload) => {
  const response = await api.post(`/AssetLibrary/getassetfolders`, params);
  return response.data;
};

// Fetches a list of asset files based on filter criteria
export const fetchAssetFilesListApi = async (params: IMainPayload) => {
  const response = await api.post('/AssetLibrary/getassetlist', params);
  return response.data;
};

// Deletes selected asset file
export const deleteAssetFileApi = async (params: IDeletePayload) => {
  const response = await api.post(`/AssetLibrary/deleteasset?clientId=${params.clientId}&id=${params.id}`, { params });
  return response.data;
};

// Activates or deactivates multiple asset files in bulk
export const activateDeactivateAssetFilesApi = async (params: IAssetLibraryAssetListActivateDeactivatePayload) => {
  const response = await api.post('/AssetLibrary/deactivateasset', params);
  return response.data;
};

// Add asset folder
export const addAssetFolderApi = async (asset: IAssetLibraryAddEditAssetFolderPayload) => {
  const response = await api.post('/AssetLibrary/addassetfolders', asset);
  return response.data;
};

// Update asset folder
export const editAssetFolderApi = async (asset: IAssetLibraryAddEditAssetFolderPayload) => {
  const response = await api.put('/AssetLibrary/updateassetfolders', asset);
  return response.data;
};

// Delete selected asset folder
export const deleteAssetFolderApi = async (param: IDeletePayload) => {
  const response = await api.delete('/AssetLibrary/deleteassetfolders', { data: param, });
  return response.data;
};

// Fetches the list of asset types for a given client.
export const fetchAssetTypeApi = async (clientId: any) => {
  const response = await api.get(`/Asset/getassettype?clientId=${clientId}`, clientId);
  return response.data;
};

// add asset data api
export const addAssetApi = async (param: FormData) => {
  const response = await api.post(`/Asset/addasset`, param, {
      headers: { 'Content-Type': 'multipart/form-data' },
  });
  return response.data;
};

// upload asset thumbnail api
export const uploadAssetThumbnailApi = async (param: FormData) => {
  const response = await api.post(`/Asset/uploadthumbnail`, param, {
      headers: { 'Content-Type': 'multipart/form-data' },
  });
  return response.data;
};

// Fetches data of a single asset by asset ID
export const fetchSingleAssetDataApi = async (param: IMainPayload) => {
  const response = await api.post(`/Asset/getassetbyid?ClientId=${param.clientId}&AssetId=${param.id}`, param);
  return response.data;
};