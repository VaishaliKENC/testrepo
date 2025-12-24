import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { getCurrentAssignment } from "./currentAssignment.requests";

export interface CurrentProps {
  currentAssignment: [];
  loading: boolean;
  error: string | null;
}
const initialState: CurrentProps = {
  currentAssignment: [],
  loading: false,
  error: null,
};
const learnerCurrentAssignment = createSlice({
  name: "learnerCurrentAssignment",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(getCurrentAssignment.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(
        getCurrentAssignment.fulfilled,
        (state, action: PayloadAction<any>) => {
          state.loading = false;
          state.currentAssignment = action.payload?.activityAssignment ?? [];
        }
      )
      .addCase(getCurrentAssignment.rejected, (state, action) => {
        state.currentAssignment = [];
        state.loading = false;
        state.error = action.payload as string;
      });
  },
});

export default learnerCurrentAssignment.reducer;
