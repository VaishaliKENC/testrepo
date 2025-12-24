import { createAsyncThunk } from "@reduxjs/toolkit";
import {
  fetchCourseListforLeaderBoard,
  fetchAdminLeaderBoardData,
  fetchLearnerLeaderboardData,
 
} from "../../../api/leaderBoard/leaderBoard.api";

export const getLeaderBoardCourseList = createAsyncThunk(
  "learner/leaderBoardCourseList",
  async (params: { clientId: string; userId?: string }, { rejectWithValue }) => {
    try {
      let response = await fetchCourseListforLeaderBoard(params);
      return response;
    } catch (error: any) {
      return rejectWithValue(error?.response?.data ?? "Error");
    }
  }
);

export const getAdminLeaderBoardRankList = createAsyncThunk(
  "learner/adminLeaderboard",
  async (
    params: { clientId: string; activityId: string ,pageIndex?:number,pageSize?:number},
    { rejectWithValue }
  ) => {
    
    try {
      let response = await fetchAdminLeaderBoardData(params);
      return response;
    } catch (error: any) {
      return rejectWithValue(error?.response?.data ?? "Error");
    }
  }
);

export const getLearnerLeaderBoardRankList = createAsyncThunk(
  "learner/learnerLeaderboard",
  async (
    params: {
      clientId: string;
      systemId: string;
      activityId: string;
      rows?: number;
    },
    { rejectWithValue }
  ) => {
    try {
      let response = await fetchLearnerLeaderboardData(params);
      return response;
    } catch (error: any) {
      return rejectWithValue(error?.response?.data ?? "Error");
    }
  }
);
