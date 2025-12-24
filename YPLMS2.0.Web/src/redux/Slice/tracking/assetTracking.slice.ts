import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { setAssetTracking } from "./assetTracking.requests";

export interface AssetTrackingProps {
  tracking: {}
  loading: boolean;
  error: string | null;
}
const initialState: AssetTrackingProps = {
  tracking: {},
  loading: false,
  error: null,
};
const assetTracking = createSlice({
  name: "assetTracking",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(setAssetTracking.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(
        setAssetTracking.fulfilled,
        (state, action: PayloadAction<any>) => {
          state.loading = false;
          state.tracking = {};
        }
      )
      .addCase(setAssetTracking.rejected, (state, action) => {
        state.tracking = [];
        state.loading = false;
        state.error = action.payload as string;
      })

  },
});

export default assetTracking.reducer;
