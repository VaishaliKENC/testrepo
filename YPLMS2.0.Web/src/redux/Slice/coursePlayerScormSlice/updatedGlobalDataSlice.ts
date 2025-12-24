// src/redux/gdSlice.ts
import { createSlice, PayloadAction, createAsyncThunk } from '@reduxjs/toolkit';
import { GlobalData } from '../../../Types/coursePlayerScormType/globalDataType';
import courseScromApi from '../../../api/courseScromApi';

interface CustomError {
  message: string;
}
const initialState: GlobalData & { loading: boolean } = {
  gContentPath: '',
  gLearnerId: '',
  gStudentId: '',
  gManagerEmail: '',
  gStudentEmail: '',
  sessionId: '',
  clientId: '',
  gTrackScoreSettingFromLMS: '',
  gTotalNoOfPages: '',
  gNoOfCompletedPages: '',
  gLearnerName: '',
  gCourseName: '',
  gManifestId: '',
  gCourseSection: [] as { identifier: string; title: string; sortOrder: number, lessons: { identifier: string; title: string; sortOrder: number }[] }[], // Initialize the array variable as an empty array
  loading: false,
};


export const getGlobalData = createAsyncThunk('getGlobalData', async ({ ClientId, SessionId }: { ClientId: string; SessionId: string }, { rejectWithValue }) => {
  try {
    const payload = { ClientId, SessionId };
    const response = await courseScromApi.getGlobalData(payload);
    return response.data;
  } catch (error) {
    const customError = error as CustomError;
    return rejectWithValue(customError.message || 'An unknown error occurred');
  }
});


const updatedGlobalDataSlice = createSlice({
  name: 'updatedGlobalData', /// this name is used in store.ts as slice name
  initialState,
  reducers: {
    setUpdatedGlobalData(state, action: PayloadAction<GlobalData>) {
      console.log("updatedGlobalData payload:", action.payload);
      return { ...state, ...action.payload };
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(getGlobalData.fulfilled, (state, action) => {
        const payload = action.payload;
        state.loading = false;

        if (payload.contentModule && payload.contentModule.sections) {
          const courseSections = Object.values(payload.contentModule.sections).map((section: any) => ({
            identifier: section.identifier,
            title: section.title,
            sortOrder: section.sortOrder || 0,
            lessons: Object.values(section.lessons || {}).map((lesson: any) => ({
              identifier: lesson.identifier,
              title: lesson.title,
              masteryScore: lesson.masteryScore || 0,
              maxTimeAllowed: lesson.maxTimeAllowed || null,
              timeLimitAction: lesson.timeLimitAction || "",
              sortOrder: lesson.sortOrder || 0,
            })),
          }));

          state.gCourseSection = courseSections; // Assign transformed sections
        }

      })
      .addCase(getGlobalData.pending, (state) => {
        state.loading = true;
      })
      .addCase(getGlobalData.rejected, (state, action) => {
        state.loading = false;
      });
  },
});

export const { setUpdatedGlobalData } = updatedGlobalDataSlice.actions;
export default updatedGlobalDataSlice.reducer;