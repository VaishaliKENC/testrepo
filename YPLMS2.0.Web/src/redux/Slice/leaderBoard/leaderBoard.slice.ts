import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import {
  getLeaderBoardCourseList,
  getLearnerLeaderBoardRankList,
  getAdminLeaderBoardRankList,
} from "./leaderBoard.requests";

export interface LeaderBoardProps {
  courseList: [];
  learnerCourseLeaders: any[];
  adminCourseLeaders:{rankList:any[],totalRows:number};
  loading: boolean;
  error: string | null;
}
const initialState: LeaderBoardProps = {
  courseList: [],
  learnerCourseLeaders: [],
  adminCourseLeaders: {rankList:[],totalRows:0},
  loading: false,
  error: null,
};
const leaderBoard = createSlice({
  name: "leaderBoard",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
    //LEADER BOARD COURSE LIST
      .addCase(getLeaderBoardCourseList.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(
        getLeaderBoardCourseList.fulfilled,
        (state, action: PayloadAction<any>) => {
          state.loading = false;
          state.courseList = action.payload?.assetTypeList ?? [];
        }
      )
      .addCase(getLeaderBoardCourseList.rejected, (state, action) => {
        state.courseList = [];
        state.loading = false;
        state.error = action.payload as string;
      })
      //LEADER BOARD RANK LIST
      .addCase(getAdminLeaderBoardRankList.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(
        getAdminLeaderBoardRankList.fulfilled,
        (state, action: PayloadAction<any>) => {
          state.loading = false;
          state.adminCourseLeaders.rankList=action?.payload?.assetTypeList||[];
           state.adminCourseLeaders.totalRows=action?.payload?.totalRows||0;
        }
      )
      .addCase(getAdminLeaderBoardRankList.rejected, (state, action) => {
        state.adminCourseLeaders = {rankList:[],totalRows:0};
        state.loading = false;
        state.error = action.payload as string;
      })

      //LEADER BOARD MY RANK
      .addCase(getLearnerLeaderBoardRankList.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(
        getLearnerLeaderBoardRankList.fulfilled,
        (state, action: PayloadAction<any>) => {
          state.loading = false;
          state.learnerCourseLeaders = action.payload?.assetTypeList ?? [];
        }
      )
      .addCase(getLearnerLeaderBoardRankList.rejected, (state, action) => {
        state.learnerCourseLeaders = [];
        state.loading = false;
        state.error = action.payload as string;
      });
  },
});

export default leaderBoard.reducer;
