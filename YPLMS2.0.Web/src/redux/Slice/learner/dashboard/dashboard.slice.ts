import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { getDashboardCompletedAssignment, getDashboardCurrentAssignment, getDashboardData } from "./dashboard.requests";

export interface LearnerDasboardProps {
  completedCourses: number;
  assignedCourses: number;
  assignedCirriculumns: number;
  currentAssignmentCards: [];
  completedAssignmentCards:[];
  totalCourseAssigned:number;
  totalCourseCompleted:number;
  loading: boolean;
  error: string | null;
}
const initialState: LearnerDasboardProps = {
  completedCourses: 0,
  assignedCourses: 0,
  assignedCirriculumns: 0,
  currentAssignmentCards: [],
  completedAssignmentCards:[],
  totalCourseAssigned:0,
  totalCourseCompleted:0,
  loading: false,
  error: null,
};
const learnerDashboard = createSlice({
  name: "learnerDashboard",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(getDashboardData.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(
        getDashboardData.fulfilled,
        (state, action: PayloadAction<any>) => {
          state.loading = false;
          state.completedCourses =
            action.payload?.activityAssignment?.totalRowsCompleted ?? 0;
          state.assignedCourses =
            action.payload?.activityAssignment?.totalRowsAssigned ?? 0;
          state.assignedCirriculumns =
            action.payload?.activityAssignment
              ?.curriculumCompletionPercentage ?? 0;
              state.totalCourseAssigned=action.payload?.activityAssignment?.totalCourseAssigned??0;
              state.totalCourseCompleted=action.payload?.activityAssignment?.totalCourseCompleted??0;
        }
      )
      .addCase(getDashboardData.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      /**FETCH DASHBOARD CURRENT ASSIGNMENT */
      .addCase(getDashboardCurrentAssignment.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(
        getDashboardCurrentAssignment.fulfilled,
        (state, action: PayloadAction<any>) => {
          state.loading = false;
          state.currentAssignmentCards =action.payload.activityAssignment;
            
        }
      )
      .addCase(getDashboardCurrentAssignment.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      /**FETCH DASHBOARD COMPLETED ASSIGNMENT */
      .addCase(getDashboardCompletedAssignment.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(
        getDashboardCompletedAssignment.fulfilled,
        (state, action: PayloadAction<any>) => {
          state.loading = false;
          state.completedAssignmentCards =action.payload.activityAssignment;
            
        }
      )
      .addCase(getDashboardCompletedAssignment.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      });
  },
});

export default learnerDashboard.reducer;
