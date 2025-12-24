import { createAsyncThunk } from "@reduxjs/toolkit";
import { enableAssetTracking, enableVideoTracking } from "../../../api/tracking/assetTracking.api";

export const setAssetTracking = createAsyncThunk(
  "tracking/asset",
  async (
    params: {
      clientId: string;
      currentUserID: string;
      activityId: string;
      activityType: string;
      languageId?: string;
      isForAdminPreview: boolean;
      progress: number;
    },
    { rejectWithValue }
  ) => {
    try {
      let response = await enableAssetTracking(params);
      return response;
    } catch (error: any) {
      return rejectWithValue(error?.response?.data ?? "Error");
    }
  }
);

export const updateVideoTracking = createAsyncThunk(
  "Tracking/updatevideotracking",
  async (
    params: {
      activityId: string;
      activityType: string;
      clientId: string;
      learnerId: string;
      totalDuration: number | string;
      elaspedTime: number | string;
      videoEvent: string;
      activityName: string;
    },
    { rejectWithValue }
  ) => {
   
    try {
      let response = await enableVideoTracking(params);
      return response;
    } catch (error: any) {
      return rejectWithValue(error?.response?.data ?? "Error");
    }
  }
);
