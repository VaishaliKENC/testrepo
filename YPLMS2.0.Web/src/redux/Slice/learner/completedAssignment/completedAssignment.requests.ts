



import { createAsyncThunk } from "@reduxjs/toolkit";
import { fetchCompletedAssignment } from "../../../../api/learner/completedAssignment.api";


export const getCompletedAssignment=createAsyncThunk('learner/completedAssignment',
  async(params:{pageIndex:number,pageSize:number,sortExpression:any,clientId:string,userId:string,keyWord?:string}, { rejectWithValue }) => {
  try {
      let response = await fetchCompletedAssignment(params);
      return response;
    } catch (error: any) {
      return rejectWithValue(error?.response?.data ?? 'Error');
    }
  });
