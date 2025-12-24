import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit';
import { AuthState, fetchClientUrlPayload, LoginPayload, logOutPayload, SignupData, SignupDataAdminApprove, User } from '../../Types/authTypes';
import authApi from '../../api/authApi'; // And this as well
import { RootState } from '../store';
import { getSessionData, removeSessionData, setSessionData } from '../../utils/authUtils';

interface ErrorPayload {
  msg: string;
}

interface CustomError {
  message: string;
  response?: { data?: { message?: string } };
}

// Define the initial authentication state
const initialState: AuthState = {
  user: null,
  loading: false,
  success: false,
  error: null,
  successMessage: null,
  isAuthenticated: !!getSessionData('token'),
  token: getSessionData('token') || null,
  clientId: getSessionData('clientId') || null,
  id: getSessionData('id') || null,
  roleId: getSessionData('roleId') ? parseInt(getSessionData('roleId')!) : null,
  userTypeId: getSessionData('userTypeId') || null,
  fetchClientURL: getSessionData('fetchClientURL') || undefined,
  firstName: getSessionData('firstName') ?? '',
  lastName: getSessionData('lastName') ?? '',
  serverUrl: getSessionData('serverUrl') ?? '',
  isSelfRegistration: false,
  isSelfRegistrationAdminApproval: false,
  isPassCodeBased: false,
  isEmailDomainBased: false,
};

// Async thunk to fetch ClientIdBy Url 
export const fetchClientIdByUrlSlice = createAsyncThunk('getClientIdByUrl', async (payload: fetchClientUrlPayload, { rejectWithValue }) => {
  try {
    const response = await authApi.getClientIdByUrl(payload);
    return response.data;
  } catch (error: any) {
    const message = error?.response?.data?.msg || 'An unknown error occurred';
    return rejectWithValue(message);
  }
});

// Async thunk to fetch Client config details 
export const fetchClientConfigByClientIdSlice = createAsyncThunk('fetchClientConfigByClientIdSlice', async (clientId: string, { rejectWithValue }) => {
  try {
    const response = await authApi.getClientConfigByClientIdApi(clientId);
    return response.data;
  } catch (error: any) {
    const message = error?.response?.data?.msg || 'An unknown error occurred';
    return rejectWithValue(message);
  }
});

// Async thunk to handle user login
export const login = createAsyncThunk(
  'login', async ({ payload, forceLogin = false }: { payload: LoginPayload; forceLogin?: boolean }, { rejectWithValue }) => {
    try {
      const response = await authApi.login(payload, forceLogin);
      return response.data;
    } catch (error: any) {
      const message = error?.response?.data?.msg || 'An unknown error occurred';
      return rejectWithValue({ message, statusCode: error?.response?.status });
    }
  }
);


// Async thunk to handle user signup
export const signup = createAsyncThunk('signup', async (payload: SignupData, { rejectWithValue }) => {
  try {
    const response = await authApi.signup(payload);
    return response.data;
  } catch (error: any) {
    // return rejectWithValue(error.message || 'Signup failed');
    const errorMessage = error.response?.data?.msg || error.message || 'Signup failed';
    return rejectWithValue(errorMessage);
  }
}
);

// Async thunk to handle user signup admin approve flow
export const signupAdminApprove = createAsyncThunk('signupAdminApprove', async (payload: SignupDataAdminApprove, { rejectWithValue }) => {
  try {
    const response = await authApi.signupAdminApprove(payload);
    return response.data;
  } catch (error: any) {
    // return rejectWithValue(error.message || 'Signup failed');
    const errorMessage = error.response?.data?.msg || error.message || 'Signup failed';
    return rejectWithValue(errorMessage);
  }
}
);

// Async thunk to check username availability
export const checkavailablity = createAsyncThunk<
  any, { UserNameAlias: string; ClientId: string | null }, { rejectValue: ErrorPayload }>('checkavailablity',
    async ({ UserNameAlias, ClientId }, { rejectWithValue }) => {
      try {
        const payload = { UserNameAlias, ClientId };
        const response = await authApi.checkavailablity(payload);
        return response.data;
      } catch (error: any) {
        return rejectWithValue({ msg: error.response?.data?.msg });
      }
    }
  );

// Async thunk to handle forgot password request
// export const forgotPassword = createAsyncThunk('forgotPassword', async ({ UserNameAlias, ClientId }: { UserNameAlias: string; ClientId: string | null }, { rejectWithValue }) => {
//   try {
//     const payload = { UserNameAlias, ClientId }
//     const response = await authApi.forgotPassword(payload);
//     return response.data;
//   } catch (error: any) {
//     return rejectWithValue(error.response?.data?.message || 'An error occurred');
//   }
// }
// );
export const forgotPassword = createAsyncThunk(
  'forgotPassword',
  async (
    {
      UserNameAlias,
      EmailID,
      ClientId,
    }: { UserNameAlias?: string; EmailID?: string; ClientId: string | null },
    { rejectWithValue }
  ) => {
    try {
      // Build dynamic payload
      const payload: any = { ClientId };

      if (EmailID) {
        payload.EmailID = EmailID;
      } else if (UserNameAlias) {
        payload.UserNameAlias = UserNameAlias;
      }


      // Send full payload to API
      const response = await authApi.forgotPassword(payload);
      return response.data;
    } catch (error: any) {
      return rejectWithValue(
        error.response?.data?.message || 'An error occurred'
      );
    }
  }
);




// Accept an object payload with token property
export const logoutUser = createAsyncThunk('logoutUser', async (payload: logOutPayload, { rejectWithValue }) => {
  try {
    const response = await authApi.logout(payload);
    return response.data;
  } catch (error: any) {
    const message = error?.response?.data?.msg || 'An unknown error occurred';
    return rejectWithValue(message);
  }
});


// Create the authentication slice
const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    // Handles user logout and clears session data
    logout: (state) => {
      state.user = null;
      state.isAuthenticated = false;
      state.token = null;
      state.clientId = null;
      state.userTypeId = null;
      state.id = null;
      state.roleId = null;
      removeSessionData('token');
      removeSessionData('clientId');
      removeSessionData('id');
      removeSessionData('roleId');
      removeSessionData('userTypeId');

      state.isSelfRegistration = false;
      state.isSelfRegistrationAdminApproval = false;
      state.isPassCodeBased = false;
      state.isEmailDomainBased = false;
      removeSessionData('isSelfRegistration');
      removeSessionData('isSelfRegistrationAdminApproval');
      removeSessionData('isPassCodeBased');
      removeSessionData('isEmailDomainBased');

      sessionStorage.clear();
      localStorage.clear();
    },
    //  reducer to switch role dynamically
    switchUserType: (state, action: PayloadAction<string>) => {
      state.userTypeId = action.payload;
      setSessionData("userTypeId", action.payload); //  Persist in session storage
    },

    // Clears error message from state
    clearError(state) {
      state.error = null;
    },
    // Clears success message from state
    clearSuccess: (state) => {
      state.successMessage = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Handle login states
      .addCase(login.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(login.fulfilled, (state, action: PayloadAction<any>) => {
        const learner = action.payload?.learner;
        state.loading = false;
        state.isAuthenticated = true;
        state.user = action.payload;
        state.token = action.payload?.token || null;
        state.clientId = learner?.clientId || null;
        state.id = learner?.id || null;
        state.userTypeId = learner?.userTypeId || null;

        state.roleId = learner?.userAdminRole?.map((role: any) => role.roleId) || [];
        state.firstName = learner?.firstName;
        state.lastName = learner?.lastName;
        setSessionData('token', state.token || '');
        setSessionData('clientId', state.clientId || '');
        setSessionData('id', state.id || '');
        setSessionData('roleId', JSON.stringify(state.roleId) || '[]');
        setSessionData('userTypeId', state.userTypeId || '');
        setSessionData('firstName', state.firstName || '');
        setSessionData('lastName', state.lastName || '');

      })
      .addCase(login.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      // Handle client ID fetch states
      .addCase(fetchClientIdByUrlSlice.pending, (state) => {
        state.loading = true;
        state.error = null;
        state.success = false;
      })
      .addCase(fetchClientIdByUrlSlice.fulfilled, (state, action) => {
        state.loading = false;
        state.success = true;

        const fetchedClientId = action.payload?.client?.id; // <-- your actual clientId
        // console.log("fetchedClientId", fetchedClientId)
        if (fetchedClientId) {
          state.clientId = fetchedClientId;
          state.fetchClientURL = fetchedClientId;
          state.serverUrl = action?.payload?.client?.contentServerURL;
          setSessionData('clientId', fetchedClientId);
          setSessionData('fetchClientURL', fetchedClientId);
          setSessionData('serverUrl', action?.payload?.client?.contentServerURL);

          /**/
          state.isSelfRegistration = action?.payload?.client?.isSelfRegistration;
          state.isSelfRegistrationAdminApproval = action?.payload?.client?.isSelfRegistrationAdminApproval;
          state.isPassCodeBased = action?.payload?.client?.isPassCodeBased;
          state.isEmailDomainBased = action?.payload?.client?.isEmailDomainBased;
          setSessionData('isSelfRegistration', action?.payload?.client?.isSelfRegistration);
          setSessionData('isSelfRegistrationAdminApproval', action?.payload?.client?.isSelfRegistrationAdminApproval);
          setSessionData('isPassCodeBased', action?.payload?.client?.isPassCodeBased);
          setSessionData('isEmailDomainBased', action?.payload?.client?.isEmailDomainBased);
        }
      })
      .addCase(fetchClientIdByUrlSlice.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Handle client config details fetch states
      .addCase(fetchClientConfigByClientIdSlice.pending, (state) => {
        state.loading = true;
        state.error = null;
        state.success = false;
      })
      .addCase(fetchClientConfigByClientIdSlice.fulfilled, (state, action) => {
        state.loading = false;
        state.success = true;        

        state.isSelfRegistration = action?.payload?.client?.isSelfRegistration;
        state.isSelfRegistrationAdminApproval = action?.payload?.client?.isSelfRegistrationAdminApproval;
        state.isPassCodeBased = action?.payload?.client?.isPassCodeBased;
        state.isEmailDomainBased = action?.payload?.client?.isEmailDomainBased;
        setSessionData('isSelfRegistration', action?.payload?.client?.isSelfRegistration);
        setSessionData('isSelfRegistrationAdminApproval', action?.payload?.client?.isSelfRegistrationAdminApproval);
        setSessionData('isPassCodeBased', action?.payload?.client?.isPassCodeBased);
        setSessionData('isEmailDomainBased', action?.payload?.client?.isEmailDomainBased);
      })
      .addCase(fetchClientConfigByClientIdSlice.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Handle signup states
      .addCase(signup.pending, (state) => {
        state.loading = true;
        state.error = null;
        state.success = false;
      })
      .addCase(signup.fulfilled, (state) => {
        state.loading = false;
        state.success = true;
        state.successMessage = "User created successfully";
      })
      .addCase(signup.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Handle signup admin approve states
      .addCase(signupAdminApprove.pending, (state) => {
        state.loading = true;
        state.error = null;
        state.success = false;
      })
      .addCase(signupAdminApprove.fulfilled, (state) => {
        state.loading = false;
        state.success = true;
        state.successMessage = "User created successfully";
      })
      .addCase(signupAdminApprove.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Handle forgot password states
      .addCase(forgotPassword.pending, (state) => {
        state.loading = true;
        state.error = null;
        state.successMessage = null;
      })
      .addCase(forgotPassword.fulfilled, (state) => {
        state.loading = false
        state.success = true;
        state.successMessage = null;
      })
      .addCase(forgotPassword.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      // Handle username availability states
      .addCase(checkavailablity.pending, (state) => {
        state.loading = true;
        state.error = null;
        state.success = false;
      })
      .addCase(checkavailablity.fulfilled, (state) => {
        state.loading = false;
        state.success = true;
        state.successMessage = "Login ID available auth slice";
      })
      .addCase(checkavailablity.rejected, (state, action) => {
        state.loading = false;
        state.success = false;
        state.error = action.payload?.msg || action.error.message || 'An error occurred. Please try again.';
      });
  },
});

// Selectors and exports
export const selectAuth = (state: RootState) => state.auth;
export const { clearError, clearSuccess, logout, switchUserType } = authSlice.actions;
export default authSlice.reducer;
