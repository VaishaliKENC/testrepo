import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit';
import { ICourseLaunchSessionPayload, ICoursePlayerPayload, ISendDataLMSPayload } from '../../../Types/coursePlayerScormType/coursePlayerType';
import courseScromApi from '../../../api/courseScromApi';


interface CourseLaunchData {
  contentModuleSession?: {
    sessionId?: string;
    // Add more properties here if needed
  };
  // Add more properties here if your API returns more
} 

// Define the initial state interface
interface CourseState {
  // courseLaunch: any[];
  // coursePlayerLoading: boolean;
  courseLaunch: CourseLaunchData;
  coursePlayerLoading: boolean;
  error: string | null;
}

// const getPersistedCourseLaunch = (): any[] => {
//   try {
//     const stored = localStorage.getItem('courseLaunch');
//     return stored ? JSON.parse(stored) : [];
//   } catch {
//     return [];
//   }
// };

const getPersistedCourseLaunch = (): CourseLaunchData => {
  try {
    const stored = localStorage.getItem('courseLaunch');
    return stored ? JSON.parse(stored) : {};
  } catch {
    return {};
  }
};


const initialState: CourseState = {
  courseLaunch: getPersistedCourseLaunch(),
  coursePlayerLoading: false,
  error: null,
};



// Async thunk to save content module session
// export const contentModuleSession = createAsyncThunk<any, ICourseLaunchSessionPayload>(
//   'contentModuleSession',
//   async (sessionDataPayload, { rejectWithValue }) => {
//     try {
//       const response = await courseScromApi.contentModuleSessionSave(sessionDataPayload);
//       return { data: response.data, sessionDataPayload };
//     } catch (error: any) {
//       return rejectWithValue(error.response?.data || 'Error saving session');
//     }
//   }
// );
export const contentModuleSession = createAsyncThunk<
  any,
  { sessionDataPayload: ICourseLaunchSessionPayload; courseWindowWidth: number, courseWindowHeight: number, courseLaunchSameWindow: boolean }
>(
  'contentModuleSession',
  async ({ sessionDataPayload }, { rejectWithValue }) => {
    try {
      const response = await courseScromApi.contentModuleSessionSave(sessionDataPayload);
      return { data: response.data, sessionDataPayload };
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error saving session');
    }
  }
);

// Async thunk to save content module session
// export const getAssignmentForLaunch = createAsyncThunk<any, ICoursePlayerPayload>(
//   'getAssignmentForLaunch',
//   async (coursePlayerPayload, { rejectWithValue }) => {
//     try {
//       const response = await courseScromApi.getAssignmentForLaunch(coursePlayerPayload);
//       console.log("API response (getAssignmentForLaunch):", response.data);
//       return { data: response.data, coursePlayerPayload };
//     } catch (error: any) {
//       return rejectWithValue(error.response?.data || 'Error saving session');
//     }
//   }
// );

export const getAssignmentForLaunch = createAsyncThunk<any, ICoursePlayerPayload>(
  'getAssignmentForLaunch',
  async (coursePlayerPayload, { rejectWithValue }) => {
    try {
      const response = await courseScromApi.getAssignmentForLaunch(coursePlayerPayload);
      console.log("API response (getAssignmentForLaunch):", response.data);
      return { data: response.data, coursePlayerPayload };
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error saving session');
    }
  }
);


// Async thunk to send data to LMS
export const sendDataToLMS = createAsyncThunk<any, ISendDataLMSPayload>(
  'sendDataToLMS',
  async (sendData, { rejectWithValue }) => {
    try {
      const response = await courseScromApi.sendDataToLMS(sendData);
      console.log("API response (sendDataToLMS):", response.data);
      return response.data;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error sending data to LMS');
    }
  }
);


const courseLaunchSlice = createSlice({
  name: 'course',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      // Handle course launch session
      .addCase(contentModuleSession.pending, (state) => {
        state.coursePlayerLoading = true;
        state.error = null;
      })

      .addCase(contentModuleSession.fulfilled, (state, action: PayloadAction<
        any,
        string,
        { arg: { sessionDataPayload: ICourseLaunchSessionPayload; courseWindowWidth: number, courseWindowHeight: number, courseLaunchSameWindow: boolean } }
      >) => {
        state.coursePlayerLoading = false;
        state.courseLaunch = action.payload.data;
        
                // Extract dynamic dimensions from meta
        const { courseWindowWidth, courseWindowHeight , courseLaunchSameWindow } = action.meta.arg;

        // console.log("action.meta.arg", action.meta.arg)
        // Save courseLaunch to localStorage
        try {
          localStorage.setItem('courseLaunch', JSON.stringify(action.payload.data));
        } catch (e) {
          console.error("Could not persist courseLaunch to localStorage", e);
        }

        // const launchUrl = '/LaunchCourse';
        // // const launchFeatures = "width=1200,height=800,top=100,resizable=yes,scrollbars=yes";
        // const launchFeatures = `width=${courseWindowWidth},height=${courseWindowHeight}}`;
        // // console.log("launchFeatures", launchFeatures)

        // if (courseLaunchSameWindow) {
        //   window.location.href = launchUrl;// Open in the same tab
        // } else {
        //   window.open(launchUrl, '_blank', launchFeatures);  // Open in a new window
        // }
      })

      .addCase(contentModuleSession.rejected, (state, action) => {
        state.coursePlayerLoading = false;
        state.error = action.payload as string;
        // state.courseLaunch = [];
        state.courseLaunch = {};
      })


      .addCase(getAssignmentForLaunch.pending, (state) => {
        state.coursePlayerLoading = true;
        state.error = null;
      })
      .addCase(getAssignmentForLaunch.fulfilled, (state, action: PayloadAction<any>) => {
        state.coursePlayerLoading = false;
        state.courseLaunch = action.payload.data;

        // Save courseLaunch to localStorage
        // try {
        //   localStorage.setItem('courseLaunch', JSON.stringify(action.payload.data));
        // } catch (e) {
        //   console.error("Could not persist courseLaunch to localStorage", e);
        // }

        // const launchUrl = '/LaunchCourse';
        // const launchFeatures = "width=1200,height=800,top=100,resizable=yes,scrollbars=yes";

        // if (action.payload.sessionDataPayload.sameWindow) {
        // window.open(launchUrl, '_blank', launchFeatures);
        // } else {
        //   window.open(launchUrl, '_blank', launchFeatures);
        // }
      })
      .addCase(getAssignmentForLaunch.rejected, (state, action) => {
        state.coursePlayerLoading = false;
        state.error = action.payload as string;
        // state.courseLaunch = [];
        state.courseLaunch = {};
      });


    // Optional: Uncomment below if you want to manage sendDataToLMS state
    // .addCase(sendDataToLMS.pending, (state) => {
    //   state.loading = true;
    //   state.error = null;
    // })
    // .addCase(sendDataToLMS.fulfilled, (state, action: PayloadAction<any>) => {
    //   state.loading = false;
    //   // Handle LMS response if needed
    // })
    // .addCase(sendDataToLMS.rejected, (state, action) => {
    //   state.loading = false;
    //   state.error = action.payload as string;
    // });
  },
});


export default courseLaunchSlice.reducer;