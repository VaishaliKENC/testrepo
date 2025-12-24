import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit';
import { fetchUserData, activateDeactivateApi, addUserApi, deleteUserApi, updateUserApi, fetchSingleUserDataApi, configureProfileUserApi, editConfigureProfileUserApi, importDefinationSendMailApi, groupListApi, deactivateApi, deleteGroupApi, addUserGetclientFieldsApi, fetchUsersPendingApprovalsApi, approveRejectUsersApi, fetchPendingForApprovalsCountApi, fetchPendingApprovalsUserDetailsApi } from '../../../api/admin/userApi';
import { IMainPayload, IActivateDeactivatePayload, IUserAddUserPayload, IDeletePayload, IApproveRejectPayload } from '../../../Types/commonTableTypes';

// User interface definition
interface User {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  status: 'Active' | 'Inactive';
}

interface UserPendingApprovals {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  userStatus: 'Pending' | 'Approved' | 'Rejected';
  country: string;
  registrationDate: string;
}

// Group interface definition
interface Group {
  id: number;
  ruleName: string;
  ruleDescription: string;
  createdById: string;
  createdOn: string;
  status: 'Active' | 'Inactive';
}

// User state interface definition
interface UserState {
  users: User[];
  groups: Group[];
  profileUser: any[];
  loading: boolean;
  error: string | null;
  usersPendingApprovals: UserPendingApprovals[];
  pendingForApprovalsCount: number;
  pendingApprovalsUserDetails: any[];
}

// Initial state
const initialState: UserState = {
  users: [],
  groups: [],
  profileUser: [],
  loading: false,
  error: null,
  fields: [],
  usersPendingApprovals: [],
  pendingForApprovalsCount: 0,
  pendingApprovalsUserDetails: []
};


//Fileds related code
interface FieldDefinition {
  fieldName: string;
  include: boolean;
}

interface UserState {
  loading: boolean;
  error: string | null;
  fields: FieldDefinition[];
}

// Async thunk to fetch user
export const fetchUsers = createAsyncThunk('user/fetchUsers',
  async (user: IMainPayload, { rejectWithValue }) => {
    try {
      // const users = await fetchUserData(user); 
      let users = await fetchUserData(user); // Assuming fetchUserData fetches the user data
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

// Async thunk to activate/deactivate users
export const activateDeactivateUser = createAsyncThunk(
  'user/activateDeactivateUser',
  async (user: IActivateDeactivatePayload[], { rejectWithValue }) => {
    try {
      const updatedUsers = await activateDeactivateApi(user);
      return updatedUsers; // Return the updated users after activation/deactivation
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error updating user status');
    }
  }
);

// Async thunk to add users
export const addUsers = createAsyncThunk(
  'user/addUser',
  async (user: IUserAddUserPayload, { rejectWithValue }) => {
    try {
      const newUser = await addUserApi(user);
      return newUser;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error adding user');
    }
  }
);

// Async thunk to delete users
export const deleteUserThunk = createAsyncThunk('user/deleteUser', async (userId: number, { rejectWithValue }) => {
  try {
    await deleteUserApi(userId);
    return userId; // Return the ID of the deleted user
  } catch (error: any) {
    return rejectWithValue(error.response?.data || 'Error deleting user');
  }
});

// Async thunk to edit users
export const updateUser = createAsyncThunk(
  'user/updateUser',
  async (user: IUserAddUserPayload, { rejectWithValue }) => {
    try {
      const updatedUser = await updateUserApi(user);
      return updatedUser;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error updating user');
    }
  }
);

// Async thunk to Single User fetch parameters type
export const fetchSingleUserData = createAsyncThunk('user/fetchSingleUserData',
  async (user: IMainPayload, { rejectWithValue }) => {
    try {
      let profileUsers = await fetchSingleUserDataApi(user); // Assuming fetchSingleUserDataApi fetches the single user data
      profileUsers = {
        ...profileUsers
      }
      return profileUsers;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching users');
    }
  }
);

// Async thunk to Add User Get client Fields
// export const addUserGetclientFields = createAsyncThunk(
//   'user/addUserGetclientFields',
//   async (user: IMainPayload, { rejectWithValue }) => {
//     try {
//       const newUser = await addUserGetclientFieldsAPI(user);
//       return newUser;
//     } catch (error: any) {
//       return rejectWithValue(error.response?.data || 'Error adding user');
//     }
//   }
// );

// Async thunk to fetch user fields
export const addUserGetclientFields = createAsyncThunk(
  'user/addUserGetclientFields',
  async (user: IMainPayload, { rejectWithValue }) => {
    try {
      const response = await addUserGetclientFieldsApi(user);
      return response.importDefination;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching user fields');
    }
  }
);

// Async thunk to configure Profile get users fields
export const configureProfileData = createAsyncThunk('user/configureProfileData',
  async (profileUser: IMainPayload, { rejectWithValue }) => {
    try {
      let users = await configureProfileUserApi(profileUser);
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

//Async thunk to configure Profile edit users fields
export const editConfigureProfileData = createAsyncThunk('user/editConfigureProfileData',
  async (profileUser: IMainPayload, { rejectWithValue }) => {
    try {
      let users = await editConfigureProfileUserApi(profileUser);
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

//Async thunk to Import Defination Send Mail
export const importDefinationSendMail = createAsyncThunk(
  'user/importDefinationSendMailData',
  async ({ clientId, currentUserId, emailId }: { clientId: string; currentUserId: string; emailId: string }, { rejectWithValue }) => {
    try {
      let response = await importDefinationSendMailApi(clientId, currentUserId, emailId);
      return response;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching Import Defination Send Mail result');
    }
  }
);

// Async thunk to group list data
export const fetchGroups = createAsyncThunk('group/fetchGroups',
  async (group: IMainPayload, { rejectWithValue }) => {
    try {
      let groups = await groupListApi(group); // Assuming fetchUserData fetches the user data
      groups = {
        ...groups,
        "totalRows": groups.totalRows,
      }
      return groups;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching groups');
    }
  }
);

// Async thunk to deactivate groups
export const deactivateGroupRule = createAsyncThunk(
  'group/deactivateGroupRule',
  async (group: IActivateDeactivatePayload[], { rejectWithValue }) => {
    try {
      const updatedgroups = await deactivateApi(group);
      return updatedgroups;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error updating user status');
    }
  }
);

// Async thunk to delete courses
export const deleteGroupRule = createAsyncThunk(
  'group/deleteGroupRule',
  async (group: IDeletePayload[], { rejectWithValue }) => {
    try {
      const response = await deleteGroupApi(group);
      return response;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error updating user status');
    }
  }
);

// Async thunk to fetch Pending Approvals list
export const fetchUsersPendingApprovalsSlice = createAsyncThunk('user/fetchUsersPendingApprovalsSlice',
  async (user: IMainPayload, { rejectWithValue }) => {
    try {
      let usersPendingApprovals = await fetchUsersPendingApprovalsApi(user); // Assuming fetchUsersPendingApprovalsApi fetches the data
      usersPendingApprovals = {
        ...usersPendingApprovals,
        "totalRows": usersPendingApprovals.totalRows,
      }
      return usersPendingApprovals;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching pending approvals users list');
    }
  }
);

// Async thunk to activate/deactivate users
export const approveRejectUserSlice = createAsyncThunk(
  'user/approveRejectUserSlice',
  async (user: IApproveRejectPayload, { rejectWithValue }) => {
    try {
      const updatedUsers = await approveRejectUsersApi(user);
      return updatedUsers; // Return the updated users after approve/reject
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error updating user status');
    }
  }
);

// Async thunk to activate/deactivate users
export const fetchPendingForApprovalsCountSlice = createAsyncThunk(
  'user/fetchPendingForApprovalsCountSlice',
  async (clientId: IMainPayload, { rejectWithValue }) => {
    try {
      const pendingForApprovalsCount = await fetchPendingForApprovalsCountApi(clientId);
      return pendingForApprovalsCount; 
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching pending for approval users count');
    }
  }
);

// Async thunk to activate/deactivate users
export const fetchPendingApprovalsUserDetailsSlice = createAsyncThunk(
  'user/fetchPendingApprovalsUserDetailsSlice',
  async ({ clientId, signUpId }: { clientId: string; signUpId: string; }, { rejectWithValue }) => {
    try {
      const pendingApprovalsUserDetails = await fetchPendingApprovalsUserDetailsApi(clientId, signUpId);
      return pendingApprovalsUserDetails; 
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching pending for approval users count');
    }
  }
);

// Slice definition
const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      // Handle fetching users
      .addCase(fetchUsers.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchUsers.fulfilled, (state, action: PayloadAction<User[]>) => {
        state.loading = false;
        state.users = action.payload;
      })
      .addCase(fetchUsers.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.users = []
      })

      // Handle activating/deactivating users
      .addCase(activateDeactivateUser.fulfilled, (state, action: PayloadAction<User[]>) => {
        state.users = action.payload;
      })
      .addCase(activateDeactivateUser.rejected, (state, action) => {
        state.error = action.payload as string;
      })

      // Handle Add user
      .addCase(addUsers.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(addUsers.fulfilled, (state, action: PayloadAction<User>) => {
        //state.users.push(action.payload);
        state.loading = false;
        if (!Array.isArray(state.users)) {
          state.users = [];  // ✅ Reset to an empty array if it's not an array
      }
        state.users = [...state.users, action.payload]; // Create a new array
      })
      .addCase(addUsers.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Handle Delete user
      .addCase(deleteUserThunk.fulfilled, (state, action: PayloadAction<number>) => {
        state.users = state.users.filter((user) => user.id !== action.payload);
      })
      .addCase(deleteUserThunk.rejected, (state, action) => {
        state.error = action.payload as string;
      })

      //Handle Update user
      .addCase(updateUser.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(updateUser.fulfilled, (state, action: PayloadAction<User>) => {
        state.loading = false;
        if (!Array.isArray(state.users)) {
          console.error('state.users is not an array:', state.users);
          return;
        }
        const index = state.users.findIndex((user) => user.id === action.payload.id);
        if (index !== -1) {
          state.users[index] = action.payload;
        }
      })
      .addCase(updateUser.rejected, (state, action) => {
        console.error('Update user error:', action.payload);
        state.loading = false;
        state.error = action.payload as string;
      })
      
       // Handle User Get client Fields validation
       .addCase(addUserGetclientFields.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(addUserGetclientFields.fulfilled, (state, action: PayloadAction<FieldDefinition[]>) => {
        state.loading = false;
        state.fields = action.payload;
      })
      .addCase(addUserGetclientFields.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      //Handle configure profile  users
      .addCase(configureProfileData.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(configureProfileData.fulfilled, (state, action: PayloadAction<any>) => {
        state.loading = false;
        state.profileUser = action.payload;
      })
      .addCase(configureProfileData.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.profileUser = []
      })

      // Handle Import Defination Send Mail
      .addCase(importDefinationSendMail.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(importDefinationSendMail.fulfilled, (state, action: PayloadAction<any>) => {
        state.loading = false;
        state.profileUser = action.payload;
      })
      .addCase(importDefinationSendMail.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.profileUser = []
      })

      //Handle Fetching group list
      .addCase(fetchGroups.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchGroups.fulfilled, (state, action: PayloadAction<Group[]>) => {
        state.loading = false;
        state.groups = action.payload;
      })
      .addCase(fetchGroups.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.groups = []
      })

      // Handle deactivating groups
      .addCase(deactivateGroupRule.fulfilled, (state, action: PayloadAction<Group[]>) => {
        state.groups = action.payload;
      })
      .addCase(deactivateGroupRule.rejected, (state, action) => {
        state.error = action.payload as string;
      })

      // Handle deleting courses
      .addCase(deleteGroupRule.fulfilled, (state, action: PayloadAction<Group[]>) => {
        state.groups = action.payload;
      })
      .addCase(deleteGroupRule.rejected, (state, action) => {
        state.error = action.payload as string;
      })


      // Handle fetching pending approvals users
      .addCase(fetchUsersPendingApprovalsSlice.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchUsersPendingApprovalsSlice.fulfilled, (state, action: PayloadAction<UserPendingApprovals[]>) => {
        state.loading = false;
        state.usersPendingApprovals = action.payload;
      })
      .addCase(fetchUsersPendingApprovalsSlice.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.usersPendingApprovals = []
      })

      // Handle approve/reject users
      .addCase(approveRejectUserSlice.fulfilled, (state, action: PayloadAction<UserPendingApprovals[]>) => {
        state.usersPendingApprovals = action.payload;
      })
      .addCase(approveRejectUserSlice.rejected, (state, action) => {
        state.error = action.payload as string;
      })

      // Handle fetching pending approvals users count
      .addCase(fetchPendingForApprovalsCountSlice.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchPendingForApprovalsCountSlice.fulfilled, (state, action: PayloadAction<number>) => {
        state.loading = false;
        state.pendingForApprovalsCount = action.payload;
      })
      .addCase(fetchPendingForApprovalsCountSlice.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.pendingForApprovalsCount = 0;
      })

      // Handle fetching pending approvals users count
      .addCase(fetchPendingApprovalsUserDetailsSlice.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchPendingApprovalsUserDetailsSlice.fulfilled, (state, action: PayloadAction<any[]>) => {
        state.loading = false;
        state.pendingApprovalsUserDetails = action.payload;
      })
      .addCase(fetchPendingApprovalsUserDetailsSlice.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.pendingApprovalsUserDetails = [];
      })
  },
});

export default userSlice.reducer;

