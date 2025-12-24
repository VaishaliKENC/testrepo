import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit';
import { fetchAdminDashboardCountApi, fetchGetRecentAssignmentApi, fetchGetTopAssignmentApi } from '../../../api/admin/adminDashboardApi';
import { RootState } from '../../store';


// ---------------------- TYPES ----------------------
// Define the response type
export interface AdminDashboardCountData {
  totalUsers: string;
  activeUsers: string;
  totalCourses: string;
  totalCurriculums: string;
}

export interface AssignmentItem {
  id: number;
  activityID: string;
  activityName: string;
  noOfAssignement: number;
  noOfCompletion: number;
  per: number;
}

// Define the slice state
interface AdminDashboardState {
  loading: boolean;
  error: string | null;
  data: AdminDashboardCountData;
  recentAssignment: AssignmentItem[];
  topAssignment: AssignmentItem[];
  fetched: {
    count: boolean;
    recent: boolean;
    top: boolean;
  };
}


// ---------------------- INITIAL STATE ----------------------
const initialState: AdminDashboardState = {
  loading: false,
  error: null,
  data: {
    totalUsers: '0',
    activeUsers: '0',
    totalCourses: '0',
    totalCurriculums: '0',

  },
  recentAssignment: [],
  topAssignment: [],
  fetched: { count: false, recent: false, top: false },
};

// ---------------------- ASYNC THUNKS ----------------------

export const fetchAdminDashboardCountSlice = createAsyncThunk<
  AdminDashboardCountData,
  { clientId: string; currentUserId: string },
  { rejectValue: string }
>("adminDashboard/fetchCount", async ({ clientId, currentUserId }, thunkApi) => {
  try {
    return await fetchAdminDashboardCountApi(clientId, currentUserId);
  } catch (error: any) {
    return thunkApi.rejectWithValue(
      error?.response?.data || "Error fetching dashboard count data"
    );
  }
});

export const fetchRecentAssignmentSlice = createAsyncThunk<
  AssignmentItem[],
  { clientId: string; currentUserId: string },
  { rejectValue: string }
>(
  "AdminDashboard/getRecentAssignment",
  async ({ clientId, currentUserId }, thunkApi) => {
    try {
      return await fetchGetRecentAssignmentApi(clientId, currentUserId);
    } catch (error: any) {
      return thunkApi.rejectWithValue(
        error?.response?.data || "Error fetching Recent Assignment"
      );
    }
  }
);


export const fetchTopAssignmentSlice = createAsyncThunk<
  AssignmentItem[],
  { clientId: string; currentUserId: string },
  { rejectValue: string }
>("AdminDashboard/getTopAssignment", async ({ clientId, currentUserId }, thunkApi) => {
  try {
    return await fetchGetTopAssignmentApi(clientId, currentUserId);
  } catch (error: any) {
    return thunkApi.rejectWithValue(
      error?.response?.data || "Error fetching Top Assignment"
    );
  }
});


// ---------------------- SLICE ----------------------
const adminDashboardSlice = createSlice({
  name: 'adminDashboard',
  initialState,
  reducers: {},
  extraReducers: (builder) => {

    // function to handle common states
    const setPending = (state: AdminDashboardState) => {
      state.loading = true;
      state.error = null;
    };

    const setRejected = (
      state: AdminDashboardState,
      action: PayloadAction<string | undefined>
    ) => {
      state.loading = false;
      state.error = action.payload || "Something went wrong";
    };


    builder
      // ---------------- Count API ----------------
      .addCase(fetchAdminDashboardCountSlice.pending, setPending)
      .addCase(fetchAdminDashboardCountSlice.fulfilled, (state, action) => {
        state.loading = false;
        state.data = action.payload;
        state.fetched.count = true;
      })
      .addCase(fetchAdminDashboardCountSlice.rejected, setRejected)


      // ---------------- Recent Assignment API ----------------
      .addCase(fetchRecentAssignmentSlice.pending, setPending)
      .addCase(fetchRecentAssignmentSlice.fulfilled, (state, action) => {
        state.loading = false;
        state.recentAssignment = action.payload;
        state.fetched.recent = true; // ✅ Prevent re-fetch
      })
      .addCase(fetchRecentAssignmentSlice.rejected, (state, action) => {
        setRejected(state, action);
        state.recentAssignment = [];
      })


      // ---------------- Top Assignment API ----------------
      .addCase(fetchTopAssignmentSlice.pending, setPending)
      .addCase(fetchTopAssignmentSlice.fulfilled, (state, action) => {
        state.loading = false;
        state.topAssignment = action.payload;
        state.fetched.top = true; // ✅ Mark as fetched
      })
      .addCase(fetchTopAssignmentSlice.rejected, (state, action) => {
        setRejected(state, action);
        state.topAssignment = [];
      });
  },
});

export default adminDashboardSlice.reducer;
