import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit';
import { fetchActivitiesApi, fetchCategoriesApi, fetchSubCategoriesApi, fetchActivityTypesApi, fetchUsersApi, fetchBusinessRuleApi, saveSelectedActivitiesApi, saveSelectedUsersApi, fetchUsersBusinessRuleApi, fetchUsersSelectedApi, deleteSelectedUserApi, fetchSelectedActivityApi, submitOnetimeAssignmentAPI } from '../../../api/admin/oneTimeAssignmentsApi';
import { AssignmentDefineProperties, IMainPayloadOneTimeAssignment, ISaveOnetimeAssignment } from '../../../Types/assignmentTypes';
import { ICategoryPayload, ISubCategoryPayload, IDeletePayload } from '../../../Types/commonTableTypes';

// Activity interface definition
interface Activity {
  id: number;
  activityId?: string;
  activityName: string;
  categoryId: string;
  subCategoryId: string;
}

interface Categories {
  id: number;
  categoryId: string;
  categoryName: string;
}

interface SubCategories {
  id: number;
  categoryId: string;
  categoryName: string;
  subCategoryId: string;
  subCategoryName: string;
}

interface ActivityTypes {
  id: number;
  activityTypeId: number;
  activityTypeName: string;
}

interface User {
  id: number;
  userNameAlias: string;
  firstName: string;
  lastName: string;
}

interface UserBusinessRule {
  id: number;
  userNameAlias: string;
  firstName: string;
  lastName: string;
}

interface UserSelected {
  id: number;
  userNameAlias: string;
  firstName: string;
  lastName: string;
}

interface BusinessRule {
  id: number;
  ruleName: string;
}

interface PriewAssignmentData {
  clientId: string;
  activities: { id: string; name: string; type: string; isSelected: boolean }[],
  learners: { id: string; isSelected: boolean; registrationDate: "" }[],
  AssignmentDateText?: string;
  DueDateText?: string;
  ExpiryDateText?: string;
  LastModifiedById?: string;
  CurrentUserID?: string;
  IsReassignmentChecked: boolean;
  BusinessRuleId: string;
  IsMailChecked: boolean;
  IsAutoMail: boolean;
  IsDirectSendMail: boolean;
  IsAssignmentBasedOnHireDate: boolean;
  IsAssignmentBasedOnCreationDate: boolean;
  AssignAfterDaysOf: number | "";
  IsNoDueDate: boolean;
  IsDueBasedOnAssignDate: boolean;
  IsDueBasedOnHireDate: boolean;
  IsDueBasedOnCreationDate: boolean;
  IsDueBasedOnStartDate: boolean;
  DueAfterDaysOf: number | "";
  IsNoExpiryDate: boolean;
  IsExpiryBasedOnAssignDate: boolean;
  IsExpiryBasedOnStartDate: boolean;
  IsExpiryBasedOnDueDate: boolean;
  ExpireAfterDaysOf: number | "";
}


interface OneTimeAssignmentsState {
  activities: Activity[];
  categories: Categories[];
  subCategories: SubCategories[];
  activityTypes: ActivityTypes[];
  users: User[];
  usersBusinessRule: UserBusinessRule[];
  usersSelected: UserSelected[];
  businessRules: BusinessRule[];
  profileUser: any[];
  loading: boolean;
  error: string | null;
  assignmentDefineProperties: AssignmentDefineProperties;
  activitySelected: Activity[];
  priewAssignmentData: PriewAssignmentData[];
}

const initialState: OneTimeAssignmentsState = {
  activities: [],
  categories: [],
  subCategories: [],
  activityTypes: [],
  users: [],
  usersBusinessRule: [],
  usersSelected: [],
  businessRules: [],
  profileUser: [],
  priewAssignmentData: [],
  loading: false,
  error: null,
  activitySelected: [],
  assignmentDefineProperties: {
    AssignmentDateText: "",
    DueDateText: "",
    ExpiryDateText: "",
    LastModifiedById: "",
    CurrentUserID: "",
    IsReassignmentChecked: false,
    BusinessRuleId: "0",
    IsMailChecked: false,
    IsAutoMail: true,
    IsDirectSendMail: true,
    IsAssignmentBasedOnHireDate: false,
    IsAssignmentBasedOnCreationDate: true,
    AssignAfterDaysOf: 0,
    IsNoDueDate: false,
    IsDueBasedOnAssignDate: false,
    IsDueBasedOnHireDate: false,
    IsDueBasedOnCreationDate: false,
    IsDueBasedOnStartDate: true,
    DueAfterDaysOf: 0,
    IsNoExpiryDate: false,
    IsExpiryBasedOnAssignDate: false,
    IsExpiryBasedOnStartDate: false,
    IsExpiryBasedOnDueDate: true,
    ExpireAfterDaysOf: 0,
  },
};

// Async thunk to fetch user
export const fetchActivitiesSlice = createAsyncThunk(
  'ActivityAssignment/getactivityonetimeassignmentforcurriculum',
  async (param: IMainPayloadOneTimeAssignment, { rejectWithValue }) => {
    try {
      let activities = await fetchActivitiesApi(param);
      activities = {
        ...activities,
        "totalRows": activities.totalRows,
      }
      return activities;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching activities');
    }
  }
);

// Async thunk to fetch standard type data
export const fetchCategoriesSlice = createAsyncThunk(
  'ActivityAssignment/getallcategory',
  async (param: ICategoryPayload, { rejectWithValue }) => {
    try {
      let categories = await fetchCategoriesApi(param);
      categories = {
        ...categories
      }
      return categories;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching categories');
    }
  }
);

// Async thunk to fetch standard type data
export const fetchSubCategoriesSlice = createAsyncThunk(
  'ActivityAssignment/getproductsubcategorybyid',
  async (params: ISubCategoryPayload, { rejectWithValue }) => {
    try {
      let subCategories = await fetchSubCategoriesApi(params);
      subCategories = {
        ...subCategories
      }
      return subCategories;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching sub categories');
    }
  }
);

// Async thunk to fetch standard type data
export const fetchActivityTypesSlice = createAsyncThunk(
  'ActivityAssignment/getactivitytype',
  async (clientId: string, { rejectWithValue }) => {
    try {
      let activityTypes = await fetchActivityTypesApi(clientId);
      activityTypes = {
        ...activityTypes
      }
      return activityTypes;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching activity types');
    }
  }
);

// Async thunk to fetch user
export const fetchUsersSlice = createAsyncThunk(
  'LearnerDAM/getlearnersforassignment',
  async (param: IMainPayloadOneTimeAssignment, { rejectWithValue }) => {
    try {
      let users = await fetchUsersApi(param);
      users = {
        ...users,
        "totalRows": users.totalRows,
      }
      return users;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching users');
    }
  }
);

// Async thunk to fetch business rule
export const fetchBusinessRuleSlice = createAsyncThunk(
  'ActivityAssignment/getbusinessrule',
  async (param: any, { rejectWithValue }) => {
    try {
      let businessRules = await fetchBusinessRuleApi(param);
      businessRules = {
        ...businessRules
      }
      return businessRules;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching business rule');
    }
  }
);

// Async thunk to save selected activities
export const saveSelectedActivitiesSlice = createAsyncThunk(
  'ActivityAssignment/saveselectedactivity',
  async (data: any, { rejectWithValue }) => {
    try {
      const response = await saveSelectedActivitiesApi(data);
      return response;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error saving selected activities');
    }
  }
)

// Async thunk to save selected users
export const saveSelectedUsersSlice = createAsyncThunk(
  'ActivityAssignment/saveselecteduser',
  async (data: any, { rejectWithValue }) => {
    try {
      const response = await saveSelectedUsersApi(data);
      return response;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error saving selected users');
    }
  }
)

// Async thunk to fetch user (business rule)
export const fetchUsersBusinessRuleSlice = createAsyncThunk(
  'ActivityAssignment/getbussinessruleusers',
  async (param: IMainPayloadOneTimeAssignment, { rejectWithValue }) => {
    try {
      let users = await fetchUsersBusinessRuleApi(param);
      users = {
        ...users,
        "totalRows": users.totalRows,
      }
      return users;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching users');
    }
  }
);

// Async thunk to fetch user (final selected)
export const fetchUsersSelectedSlice = createAsyncThunk(
  'ActivityAssignment/getselecteduser',
  async (param: IMainPayloadOneTimeAssignment, { rejectWithValue }) => {
    try {
      let users = await fetchUsersSelectedApi(param);
      users = {
        ...users,
        "totalRows": users.totalRows,
      }
      return users;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching users (selected)');
    }
  }
);

// Async thunk to delete (selected tab) user
export const deleteSelectedUserSlice = createAsyncThunk(
  'ActivityAssignment/deleteselecteduser',
  async (param: IDeletePayload[], { rejectWithValue }) => {
    try {
      const response = await deleteSelectedUserApi(param);
      return response;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error deleting user (selected tab');
    }
  }
);

// Async thunk to fetch selected Activity (final selected)
export const fetchSelectedActivitySlice = createAsyncThunk(
  'ActivityAssignment/getselectedactivity',
  async (param: IMainPayloadOneTimeAssignment, { rejectWithValue }) => {
    try {
      let activity = await fetchSelectedActivityApi(param);
      activity = {
        ...activity,
        "totalRows": activity.totalRows,
      }
      return activity;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching Activity (selected)');
    }
  }
);

export const SaveOnetimeAssignment = createAsyncThunk(
  'ActivityAssignment/submitonetimeassignment',
  async (payload: ISaveOnetimeAssignment, { rejectWithValue }) => {
    try {
      const Assignment = await submitOnetimeAssignmentAPI(payload);
      return Assignment;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error adding user');
    }
  }
);


// Slice definition
const oneTimeAssignmentsSlice = createSlice({
  name: 'oneTimeAssignments',
  initialState,
  reducers: {
    setAssignmentProperties(state, action: PayloadAction<Partial<AssignmentDefineProperties>>) {
      state.assignmentDefineProperties = {
        ...state.assignmentDefineProperties,
        ...action.payload,
      };
    },
    resetAssignmentProperties(state) {
      state.assignmentDefineProperties = initialState.assignmentDefineProperties;
    },
    resetOneTimeAssignment: (state) => {
      state.activities = [];
      state.activitySelected = [];
      state.users = [];
      state.usersSelected = [];
      state.usersBusinessRule = [];
      state.businessRules = [];
      state.assignmentDefineProperties = { ...initialState.assignmentDefineProperties };
      state.priewAssignmentData = [];
      state.loading = false;
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Handle fetching activities
      .addCase(fetchActivitiesSlice.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchActivitiesSlice.fulfilled, (state, action: PayloadAction<Activity[]>) => {
        state.loading = false;
        state.activities = action.payload;
      })
      .addCase(fetchActivitiesSlice.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.activities = []
      })

      // Handle fetching categories
      .addCase(fetchCategoriesSlice.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchCategoriesSlice.fulfilled, (state, action: PayloadAction<Categories[]>) => {
        state.loading = false;
        state.categories = action.payload;
      })
      .addCase(fetchCategoriesSlice.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.categories = []
      })

      // Handle fetching sub categories
      .addCase(fetchSubCategoriesSlice.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchSubCategoriesSlice.fulfilled, (state, action: PayloadAction<SubCategories[]>) => {
        state.loading = false;
        state.subCategories = action.payload;
      })
      .addCase(fetchSubCategoriesSlice.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.subCategories = []
      })

      // Handle fetching activity types
      .addCase(fetchActivityTypesSlice.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchActivityTypesSlice.fulfilled, (state, action: PayloadAction<ActivityTypes[]>) => {
        state.loading = false;
        state.activityTypes = action.payload;
      })
      .addCase(fetchActivityTypesSlice.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.activityTypes = []
      })

      // Handle fetching users
      .addCase(fetchUsersSlice.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchUsersSlice.fulfilled, (state, action: PayloadAction<User[]>) => {
        state.loading = false;
        state.users = action.payload;
      })
      .addCase(fetchUsersSlice.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.users = []
      })

      // Handle fetching business rules
      .addCase(fetchBusinessRuleSlice.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchBusinessRuleSlice.fulfilled, (state, action: PayloadAction<BusinessRule[]>) => {
        state.loading = false;
        state.businessRules = action.payload;
      })
      .addCase(fetchBusinessRuleSlice.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.businessRules = []
      })

      .addCase(saveSelectedActivitiesSlice.fulfilled, (state, action: PayloadAction<Activity[]>) => {
        state.loading = false;
      })
      .addCase(saveSelectedActivitiesSlice.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      .addCase(saveSelectedActivitiesSlice.pending, (state, action) => {
        state.loading = true;
        state.error = null;
      })

      .addCase(saveSelectedUsersSlice.fulfilled, (state, action: PayloadAction<User[]>) => {
        state.loading = false;
      })
      .addCase(saveSelectedUsersSlice.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      .addCase(saveSelectedUsersSlice.pending, (state, action) => {
        state.loading = true;
        state.error = null;
      })

      // Handle fetching users (business rule)
      .addCase(fetchUsersBusinessRuleSlice.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchUsersBusinessRuleSlice.fulfilled, (state, action: PayloadAction<UserBusinessRule[]>) => {
        state.loading = false;
        state.usersBusinessRule = action.payload;
      })
      .addCase(fetchUsersBusinessRuleSlice.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.usersBusinessRule = []
      })

      // Handle fetching users (selected)
      .addCase(fetchUsersSelectedSlice.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchUsersSelectedSlice.fulfilled, (state, action: PayloadAction<UserSelected[]>) => {
        state.loading = false;
        state.usersSelected = action.payload;
      })
      .addCase(fetchUsersSelectedSlice.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.usersSelected = []
      })

      // Handle fetching Selected activity (final)
      .addCase(fetchSelectedActivitySlice.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchSelectedActivitySlice.fulfilled, (state, action: PayloadAction<Activity[]>) => {
        state.loading = false;
        state.activitySelected = action.payload;
      })
      .addCase(fetchSelectedActivitySlice.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.activitySelected = []
      })

      //save oneTimeAssignment
      .addCase(SaveOnetimeAssignment.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(SaveOnetimeAssignment.fulfilled, (state, action: PayloadAction<PriewAssignmentData[]>) => {
        state.loading = false;
        state.priewAssignmentData = action.payload;
      })
      .addCase(SaveOnetimeAssignment.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.priewAssignmentData = []
      })

  },
});

export const {
  setAssignmentProperties,
  resetAssignmentProperties, resetOneTimeAssignment
} = oneTimeAssignmentsSlice.actions;

export default oneTimeAssignmentsSlice.reducer;

