import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { getCompletedAssignment } from "./completedAssignment.requests";

export interface CompletedProps {
  completedAssignment:[]
  loading: boolean;
  error: string | null;
}
const initialState: CompletedProps = {
 completedAssignment: [],
  loading: false,
  error: null,
};
const learnerCompletedAssignment = createSlice({
  name: "learnerCompletedAssignment",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(getCompletedAssignment.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(
        getCompletedAssignment.fulfilled,
        (state, action: PayloadAction<any>) => {
          state.loading = false;
          state.completedAssignment =
          action.payload?.activityAssignment?? [];      
        }
      )
      .addCase(getCompletedAssignment.rejected, (state, action) => {
        state.completedAssignment=[];
        state.loading = false;
        state.error = action.payload as string;
      })

      
      
  },
});

export default learnerCompletedAssignment.reducer;
