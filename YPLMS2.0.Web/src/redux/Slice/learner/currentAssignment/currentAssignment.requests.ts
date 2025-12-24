import { createAsyncThunk } from "@reduxjs/toolkit";
import { fetchCurrentAssignment } from "../../../../api/learner/currentAssignment.api";

export const getCurrentAssignment = createAsyncThunk(
  "learner/currentAssignment",
  async (
    params: {
      pageIndex: number;
      pageSize: number;
      sortExpression: any;
      clientId: string;
      userId: string;
      keyWord?:string;
    },
    { rejectWithValue }
  ) => {
    try {
      let response = await fetchCurrentAssignment(params);
      return response;
    } catch (error: any) {
      return rejectWithValue(error?.response?.data ?? "Error");
    }
  }
);
