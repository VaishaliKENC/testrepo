import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit';
import { IDeleteLearningPathPayload, ILearningPathPayload } from '../../../Types/commonTableTypes';
import { ILearningPathFilterPayload, IMainPayloadLearningPath } from '../../../Types/learningPathTypes';
import { activateSelectedLearningPathApi, copyLearningPathApi, deActivateSelectedLearningPathApi, deleteSelectedLearningPathApi, fetchLearningPathApi, fetchSearchData } from '../../../api/admin/learningPathApi';

// LearningPath interface definition
interface LearningPath {
  curriculumName: number;
  createdByName: string;
  modifiedByName: string;
  isUsedT: string;
  isActive: 'Active' | 'Inactive';
}

interface CreatedBy {
  curriculumName: number;
  createdByName: string;
  isUsedT: string;
  isActive: 'Active' | 'Inactive';
}

interface LearningPathState {
  learningPaths: LearningPath[];
  createdByName: CreatedBy[];
  loading: boolean;
  error: string | null;
}

const initialState: LearningPathState = {
  learningPaths: [],
  createdByName: [],
  loading: false,
  error: null,
};

// Async thunk to fetch learningpath list
export const fetchLearningPath = createAsyncThunk(
  'Curriculum/curriculumlist',
  async (param: IMainPayloadLearningPath, { rejectWithValue }) => {
    try {
      let learningPath = await fetchLearningPathApi(param);
      learningPath = {
        ...learningPath,
        "totalRows": learningPath.totalRows,
      }
      return learningPath;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching learningPath list');
    }
  }
);


// Async thunk to delete (selected tab) Learning path
export const deleteSelectedLearningPathSlice = createAsyncThunk(
  'Curriculum/DeleteCurriculum',
  async (param: IDeleteLearningPathPayload, { rejectWithValue }) => {
    try {
      const response = await deleteSelectedLearningPathApi(param);
      return response;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error deleting learning paths (selected tab');
    }
  }
);

// Async thunk to activate Learning path
export const activateSelectedLearningPathSlice = createAsyncThunk(
  'Curriculum/activate',
  async (params: { id: string; clientId: string; userId: string }, { rejectWithValue }) => {
    try {
      const response = await activateSelectedLearningPathApi(params.id, {
        clientId: params.clientId,
        userId: params.userId,
      });
      return response.data;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || error.message);
    }
  }
);

// Async thunk to deActivate Learning path
export const deactivateSelectedLearningPathSlice = createAsyncThunk(
  "learningPath/deactivate",
  async (params: { id: string; clientId: string; userId: string }, { rejectWithValue }) => {
    try {
      const response = await deActivateSelectedLearningPathApi(params.id, {
        clientId: params.clientId,
        userId: params.userId,
      });
      return response.data;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || error.message);
    }
  }
);

// Async thunk to copy selected learningpath list
export const copyLearningPath = createAsyncThunk(
  'Curriculum/curriculumlist',
  async (param: ILearningPathPayload, { rejectWithValue }) => {
    try {
      let learningPath = await copyLearningPathApi(param);
      learningPath = {
        ...learningPath,
        "totalRows": learningPath.totalRows,
      }
      return learningPath;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error copy learningPath');
    }
  }
);


// Async thunk to fetch Admin users list
export const fetchAdminUsers = createAsyncThunk('LearnerDAM/searchadmin',
  async (params: ILearningPathFilterPayload, { rejectWithValue }) => {
    try {
      let response = await fetchSearchData(params);
      response = {
        ...response,
        "totalRows": response.totalRows,
      }
      return response;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching Admin users list');
    }
  }
);


// Slice definition
const learningPathSlice = createSlice({
  name: 'learningPaths',
  initialState,
  reducers: {

  },
  extraReducers: (builder) => {
    builder
      // Handle fetching activities
      .addCase(fetchLearningPath.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchLearningPath.fulfilled, (state, action: PayloadAction<LearningPath[]>) => {
        state.loading = false;
        state.learningPaths = action.payload;
      })
      .addCase(fetchLearningPath.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.learningPaths = []
      })


      // Handle fetching Admin users
      .addCase(fetchAdminUsers.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchAdminUsers.fulfilled, (state, action: PayloadAction<CreatedBy[]>) => {
        state.loading = false;
        state.createdByName = action.payload;
      })
      .addCase(fetchAdminUsers.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.createdByName = []
      })

  },
});

export const { } = learningPathSlice.actions;

export default learningPathSlice.reducer;

