import { createAsyncThunk } from "@reduxjs/toolkit";
import { fetchDashboardCompletedAssignment, fetchDashboardCurrentAssignment, fetchDashboardData } from "../../../../api/learner/dashboard.api";

export const getDashboardData = createAsyncThunk(
  'learner/dashboard',
  async (params: {clientId:string,userId:string}, { rejectWithValue }) => {
    try {
      let response = await fetchDashboardData(params);
      return response;
    } catch (error: any) {
      return rejectWithValue(error?.response?.data ?? 'Error');
    }
  }
);

export const getDashboardCurrentAssignment=createAsyncThunk('learner/currentAssignment',
  async(params:{clientId:string,userId:string}, { rejectWithValue }) => {
  try {
      let response = await fetchDashboardCurrentAssignment(params);
      return response;
    } catch (error: any) {
      return rejectWithValue(error?.response?.data ?? 'Error');
    }
  });

  export const getDashboardCompletedAssignment=createAsyncThunk('learner/completedAssignment',
    async(params:{clientId:string,userId:string}, { rejectWithValue })=>{
      try {
      let response = await fetchDashboardCompletedAssignment(params);
      return response;
    } catch (error: any) {
      return rejectWithValue(error?.response?.data ?? 'Error');
    }
    }
  )