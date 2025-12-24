import { createSlice, createAsyncThunk, PayloadAction } from "@reduxjs/toolkit";
import {
  IActivateDeactivatePayload,
  IMainPayload,
  IDeletePayload,
  ICourseAddCoursePayload,
  IValidateXMLFilePayload,
  CourseListByFile,
} from "../../../Types/commonTableTypes";
import {
  fetchCourseData,
  fetchStandardTypeData,
  fetchCourseTypeData,
  activateDeactivateCourseApi,
  addCourseApi,
  updateCourseApi,
  fetchSingleCourseDataApi,
  deleteCourseApi,
  fetchCourseStructureTreeDataApi,
  uploadCourseZipApi,
  validateXMLFileApi,
  uploadCourseThumbnailApi,
  fetchCourseSubFoldersFiles,
  fetchCourseListbyfile,
} from "../../../api/admin/contentApi";

// Define the Course interface
interface Courses {
  id: number;
  courseId: string;
  courseName: string;
  standardType: string;
  status: "Active" | "Inactive";
}

// Define the initial state interface
interface CourseState {
  courses: Courses[];
  profileUser: any[];
  loading: boolean;
  error: string | null;
  courseStructureTree: courseStructureTree[];
  courseTreeFileList: [];
}

const initialState: CourseState = {
  courses: [],
  profileUser: [],
  loading: false,
  error: null,
  courseStructureTree: [],
  courseTreeFileList: [],
};

interface courseStructureTree {
  name: string;
  path: string;
  children?: courseStructureTree[]; // For folders
  size?: number; // For files
  type?: string; // For files
  lastModified?: string; // For files
  isFile?: boolean; // Indicate whether it's a file or folder.
}

// Async thunk to fetch courses
export const fetchCourses = createAsyncThunk(
  "course/fetchCourses",
  async (course: IMainPayload, { rejectWithValue }) => {
    try {
      let courses = await fetchCourseData(course); // Assuming fetchUserData fetches the course data
      courses = {
        ...courses,
        totalRows: courses.totalRows,
      };
      return courses;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || "Error fetching users");
    }
  }
);

// Async thunk to activate/deactivate courses
export const activateDeactivateCourse = createAsyncThunk(
  "course/activateDeactivateCourse",
  async (course: IActivateDeactivatePayload[], { rejectWithValue }) => {
    try {
      const updatedCourses = await activateDeactivateCourseApi(course);
      return updatedCourses; // Return the updated courses after activation/deactivation
    } catch (error: any) {
      return rejectWithValue(
        error.response?.data || "Error updating user status"
      );
    }
  }
);

// Async thunk to add course
export const addCourse = createAsyncThunk(
  "course/addCourse",
  async (course: ICourseAddCoursePayload, { rejectWithValue }) => {
    try {
      const newCourse = await addCourseApi(course);
      return newCourse;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || "Error adding course");
    }
  }
);

// Async thunk to edit course
export const updateCourse = createAsyncThunk(
  "course/updateCourse",
  async (course: ICourseAddCoursePayload, { rejectWithValue }) => {
    try {
      const updatedCourse = await updateCourseApi(course);
      return updatedCourse;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || "Error updating course");
    }
  }
);

// Async thunk to upload course thumbnail
export const uploadCourseThumbnailSlice = createAsyncThunk(
  "course/uploadthumbnail",
  async (param: FormData, { rejectWithValue }) => {
    try {
      const response = await uploadCourseThumbnailApi(param);
      return response;
    } catch (error: any) {
      return rejectWithValue(
        error.response?.data || "Error uploading course thumbnail"
      );
    }
  }
);

//Async thunk to upload ZIP file
export const uploadCourseZip = createAsyncThunk(
  "course/uploadCourseZip",
  async (course: FormData, { rejectWithValue }) => {
    try {
      const response = await uploadCourseZipApi(course);
      return response;
    } catch (error: any) {
      return rejectWithValue(
        error.response?.data || "Error uploading ZIP file"
      );
    }
  }
);

//Async thunk to validate XML file
export const validateXMLFile = createAsyncThunk(
  "course/validateXMLFile",
  async (course: IValidateXMLFilePayload, { rejectWithValue }) => {
    try {
      const response = await validateXMLFileApi(course);
      return response;
    } catch (error: any) {
      return rejectWithValue(
        error.response?.data || "Error validating XML file"
      );
    }
  }
);

// Async thunk to Single Course fetch parameters type
export const fetchSingleCourseData = createAsyncThunk(
  "user/fetchSingleCourseData",
  async (course: IMainPayload, { rejectWithValue }) => {
    try {
      let courseData = await fetchSingleCourseDataApi(course);
      courseData = {
        ...courseData,
      };
      return courseData;
    } catch (error: any) {
      return rejectWithValue(
        error.response?.data || "Error fetching course data"
      );
    }
  }
);

// Async thunk to delete courses
export const deleteCourses = createAsyncThunk(
  "course/deleteCourses",
  async (course: IDeletePayload[], { rejectWithValue }) => {
    try {
      const response = await deleteCourseApi(course);
      return response;
    } catch (error: any) {
      return rejectWithValue(
        error.response?.data || "Error updating user status"
      );
    }
  }
);

// Async thunk to fetch standard type data
export const fetchStandardType = createAsyncThunk(
  "course/fetchStandardType",
  async (clientId: any, { rejectWithValue }) => {
    try {
      let standardTypes = await fetchStandardTypeData(clientId);
      standardTypes = {
        ...standardTypes,
      };
      return standardTypes;
    } catch (error: any) {
      return rejectWithValue(
        error.response?.data || "Error fetching standard type"
      );
    }
  }
);

// Async thunk to fetch course type data
export const fetchCourseType = createAsyncThunk(
  "course/fetchCourseType",
  async (clientId: any, { rejectWithValue }) => {
    try {
      let courseTypes = await fetchCourseTypeData(clientId);
      courseTypes = {
        ...courseTypes,
      };
      return courseTypes;
    } catch (error: any) {
      return rejectWithValue(
        error.response?.data || "Error fetching course type"
      );
    }
  }
);

// Async thunk to fetch course
export const fetchCourseStructureTree = createAsyncThunk(
  "ContentModule/getcoursestructure",
  async (clientId: any, { rejectWithValue }) => {
    try {
      let response = await fetchCourseStructureTreeDataApi(clientId);
      return response;
    } catch (error: any) {
      return rejectWithValue(
        error.response?.data || "Error fetching folder structure"
      );
    }
  }
);

export const fetchCourseSubFolders = createAsyncThunk(
  "ContentModule/getCourseSubFolders",
  async (
    params: { clientId: string; filePath: string },
    { rejectWithValue }
  ) => {
    try {
      let response = await fetchCourseSubFoldersFiles(
        params.clientId,
        params.filePath
      );
      return response;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || "Error fetching folders");
    }
  }
);

export const FetchCourseListByFile = createAsyncThunk(
  "ContentModule/getcourselist",
  async (params: CourseListByFile, { rejectWithValue }) => {
    try {
      let response = await fetchCourseListbyfile(params);
      return response;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || "Error fetching files");
    }
  }
);
// Slice definition
const contentSlice = createSlice({
  name: "course",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      // Handle fetching courses
      .addCase(fetchCourses.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(
        fetchCourses.fulfilled,
        (state, action: PayloadAction<Courses[]>) => {
          state.loading = false;
          state.courses = action.payload;
        }
      )
      .addCase(fetchCourses.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.courses = [];
      })

      // Handle activating/deactivating courses
      .addCase(
        activateDeactivateCourse.fulfilled,
        (state, action: PayloadAction<Courses[]>) => {
          state.courses = action.payload;
        }
      )
      .addCase(activateDeactivateCourse.rejected, (state, action) => {
        state.error = action.payload as string;
      })

      // Handle deleting courses
      .addCase(
        deleteCourses.fulfilled,
        (state, action: PayloadAction<Courses[]>) => {
          state.courses = action.payload;
        }
      )
      .addCase(deleteCourses.rejected, (state, action) => {
        state.error = action.payload as string;
      })

      // Handle fetching standard type data
      .addCase(fetchStandardType.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(
        fetchStandardType.fulfilled,
        (state, action: PayloadAction<Courses[]>) => {
          state.loading = false;
          state.courses = action.payload;
        }
      )
      .addCase(fetchStandardType.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.courses = [];
      })

      // Handle fetching course type data
      .addCase(fetchCourseType.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(
        fetchCourseType.fulfilled,
        (state, action: PayloadAction<Courses[]>) => {
          state.loading = false;
          state.courses = action.payload;
        }
      )
      .addCase(fetchCourseType.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.courses = [];
      })

      // Handle Add Course
      .addCase(addCourse.fulfilled, (state, action: PayloadAction<Courses>) => {
        state.loading = false;
        state.courses = Array.isArray(state.courses)
          ? [...state.courses, action.payload]
          : [action.payload];
      })
      .addCase(addCourse.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      .addCase(addCourse.pending, (state, action) => {
        state.loading = true;
        state.error = null;
      })

      //Handle Update course
      .addCase(
        updateCourse.fulfilled,
        (state, action: PayloadAction<Courses>) => {
          state.loading = false;
          if (!Array.isArray(state.courses)) {
            console.error("state.courses is not an array:", state.courses);
            return;
          }
          const index = state.courses.findIndex(
            (course) => course.id === action.payload.id
          );
          if (index !== -1) {
            state.courses[index] = action.payload;
          }
        }
      )
      .addCase(updateCourse.rejected, (state, action) => {
        console.error("Update course error:", action.payload);
        state.loading = false;
        state.error = action.payload as string;
      })
      .addCase(updateCourse.pending, (state, action) => {
        state.loading = true;
        state.error = null;
      })

      // Handle upload ZIP case in slice
      .addCase(uploadCourseZip.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(uploadCourseZip.fulfilled, (state) => {
        state.loading = false;
        state.error = null;
      })
      .addCase(uploadCourseZip.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Handle validate XML file case in slice
      .addCase(validateXMLFile.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(validateXMLFile.fulfilled, (state) => {
        state.loading = false;
        state.error = null;
      })
      .addCase(validateXMLFile.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Handle fetching folder structure data
      .addCase(fetchCourseStructureTree.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(
        fetchCourseStructureTree.fulfilled,
        (state, action: PayloadAction<courseStructureTree[]>) => {
          state.loading = false;
          state.courseStructureTree = action.payload;
          state.courseTreeFileList = [];
        }
      )
      .addCase(fetchCourseStructureTree.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
       
      })
      //Fetch subfolders

      .addCase(fetchCourseSubFolders.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchCourseSubFolders.fulfilled, (state, action) => {
        state.loading = false;

     
      })
      .addCase(fetchCourseSubFolders.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        
      })
      // Fetch table list by file name
      .addCase(FetchCourseListByFile.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(FetchCourseListByFile.fulfilled, (state, action) => {
        state.loading = false;

        state.courseTreeFileList = action.payload;
      })
      .addCase(FetchCourseListByFile.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.courseTreeFileList = [];
      });
  },
});

export default contentSlice.reducer;
