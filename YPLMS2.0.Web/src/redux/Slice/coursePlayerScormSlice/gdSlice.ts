// src/redux/gdSlice.ts
import { createSlice, PayloadAction, createAsyncThunk } from '@reduxjs/toolkit';
import { GlobalData } from '../../../Types/coursePlayerScormType/globalDataType';
import courseScromApi from '../../../api/courseScromApi';
import { getSessionData, setSessionData } from '../../../utils/authUtils';

interface GlobalDataState {
  gDataObjs: GlobalData[];
  loading: false,
}

interface CustomError {
  message: string;
}
// const initialState: GlobalDataState = {
//   gDataObjs: [], // Initial empty array
// };

const initialState: any = {
  courseWindowWidth: getSessionData('courseWindowWidth') || null,
  courseWindowHeight: getSessionData('courseWindowHeight') || null,
};
//GlobalData = {
//   gContentPath: '',
//   gLearnerId: '',
//   gStudentId: '',
//   gManagerEmail: '',
//   gStudentEmail: '',
//   sessionId: '',
//   clientId: '',
//   gTrackScoreSettingFromLMS: '',
//   gTotalNoOfPages: '',
//   gNoOfCompletedPages: '',
//   gLearnerName: '',
//   gCourseName: '',
//   gManifestId: '',
//   gCourseSection: [] as { identifier: string; title: string; sortOrder: number,lessons: { identifier: string; title: string; sortOrder: number }[] }[], // Initialize the array variable as an empty array
//   gLessonsArray: [], // Initialize the array variable as an empty array
// };


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


const gDataSlice = createSlice({
  name: 'globalData', /// this name is used in store.ts as slice name
  initialState,
  reducers: {
    setCoursesGlobalData(state, action: PayloadAction<GlobalData>) {
      console.log("payload:", action.payload);
      return { ...state, ...action.payload };
      //state.gDataObjs = action.payload;
    },
    addToArray(state, action: PayloadAction<{ identifier: string; title: string; sortOrder: number, lessons: { identifier: string; title: string; sortOrder: number }[] }>) {
      // Add an item to the array
      state.gCourseSection?.push(action.payload);
    },
    updateArrayItem(state, action: PayloadAction<{
      index: number; value: {
        identifier: string; title: string; sortOrder: number,
        lessons: { identifier: string; title: string; sortOrder: number }[]
      }
    }>) {
      // Update an item in the array by index
      if (state.gCourseSection && state.gCourseSection[action.payload.index]) {
        state.gCourseSection[action.payload.index] = action.payload.value;
      }
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(getGlobalData.fulfilled, (state, action: PayloadAction<any>) => {
        const payload = action.payload;
        const contentModule = payload.session?.contentModule;
        state.loading = false;

        const courseWindowWidth = contentModule?.courseWindowWidth || null;
        const courseWindowHeight = contentModule?.courseWindowHeight || null;
        const courseLaunchSameWindow = contentModule?.courseLaunchSameWindow || null;

        state.courseWindowWidth = courseWindowWidth;
        state.courseWindowHeight = courseWindowHeight;
        state.courseLaunchSameWindow = courseLaunchSameWindow;

        setSessionData('courseWindowWidth', courseWindowWidth || '');
        setSessionData('courseWindowHeight', courseWindowHeight || '');
        setSessionData('courseLaunchSameWindow', courseLaunchSameWindow || '');

        // console.log(" state.courseWindowWidth", courseWindowWidth);
        // console.log(" state.courseWindowHeight", courseWindowHeight);

        if (payload.contentModule && payload.contentModule.sections) {
          const courseSections = Object.values(payload.contentModule.sections).map((section: any) => ({
            identifier: section.identifier,
            title: section.title,
            sortOrder: section.sortOrder || 0,
            lessons: Object.values(section.lessons).map((lesson: any) => ({
              identifier: lesson.identifier,
              title: lesson.title,
              sortOrder: lesson.sortOrder || 0,
            })),
          }));

          state.gCourseSection = courseSections; // Assign transformed sections
        }

        // Update other fields in the global state if needed
        // state.gContentPath = payload.contentModule?.absoluteFolderUrl || '';
        // state.gLearnerId = payload.learner?.userNameAlias || '';
        // state.gStudentId = payload.learner?.userNameAlias || '';
        // state.gManagerEmail = payload.learner?.managerEmailId || '';
        // state.gStudentEmail = payload.learner?.emailID || '';
        // state.sessionId = payload.learner?.sessionId || '';
        // state.clientId = payload.learner?.clientId || '';
        // state.gTrackScoreSettingFromLMS = payload.contentModule?.ScoreTracking || '';
        // state.gTotalNoOfPages = payload.contentModule?.totalLessons || '';
        // state.gLearnerName = `${payload.learner?.firstName || ''} ${payload.learner?.lastName || ''}`;
        // state.gCourseName = payload.assignment?.activityName || '';
        // state.gManifestId = payload.contentModule?.id || '';
      })

      .addCase(getGlobalData.pending, (state) => {
        state.loading = true;
      })
      .addCase(getGlobalData.rejected, (state, action) => {
        state.loading = false;
      });
  },
});


export const { setCoursesGlobalData, addToArray, updateArrayItem } = gDataSlice.actions;
export default gDataSlice.reducer;