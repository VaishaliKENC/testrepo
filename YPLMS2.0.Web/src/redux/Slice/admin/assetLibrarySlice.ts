import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit';
import { IMainPayload, IClientIdCurrentUserIdPayload, IAssetLibraryAddEditAssetFolderPayload, IDeletePayload, IActivateDeactivatePayload, IAssetLibraryAssetListActivateDeactivatePayload } from '../../../Types/commonTableTypes';
import { fetchAssetFilesListApi, activateDeactivateAssetFilesApi, fetchAssetFoldersApi, addAssetFolderApi, editAssetFolderApi, deleteAssetFolderApi, fetchAssetTypeApi, addAssetApi, fetchSingleAssetDataApi, deleteAssetFileApi, uploadAssetThumbnailApi } from '../../../api/admin/assetLibraryApi';

// Define the Asset Library interface
interface AssetFolderData {
  
}

// User interface definition
interface AssetFilesList {
  id: string;
  assetName: string;
  modifiedByName: string;
  assetFolderId: string;
  assetFileName: string;
  assetFileType: string;
  isActive: 'Active' | 'Inactive';
}

// Define the initial state interface
interface AssetLibraryState {
  assetFolderData: AssetFolderData[];
  assetFiles: AssetFilesList[]
  loading: boolean;
  error: string | null;
  assetFolderTree: assetFolderTree[];
}

const initialState: AssetLibraryState = {
  assetFolderData: [],
  assetFiles: [],
  loading: false,
  error: null,
  assetFolderTree: [],
};

// Manage Assets Library
interface assetFolderTree {
  title: string;
  unitId: string;
  children?: assetFolderTree[]; // For folders
}

// Async thunk to fetch asset folder and sub folders
export const fetchAssetFoldersSlice = createAsyncThunk(
  'AssetLibrary/getassetfolders',
  async (params: IClientIdCurrentUserIdPayload, { rejectWithValue }) => {
    try {
      let response = await fetchAssetFoldersApi(params);
      return response;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching folder structure');
    }
  }
);
// Async thunk to add asset folder
export const addAssetFolderSlice = createAsyncThunk (
  'AssetLibrary/addassetfolders',
  async(formData: IAssetLibraryAddEditAssetFolderPayload, { rejectWithValue }) => {
    try {
      const newAssetFolder = await addAssetFolderApi(formData);
      return newAssetFolder;
    } catch(error: any) {
      return rejectWithValue(error.response?.data || 'Error adding asset folder');
    }
  }
)

// Async thunk to edit asset folder
export const editAssetFolderSlice = createAsyncThunk(
  'AssetLibrary/updateassetfolders',
  async (formData: IAssetLibraryAddEditAssetFolderPayload, { rejectWithValue }) => {
    try {
      const updatedAssetFolder = await editAssetFolderApi(formData);
      return updatedAssetFolder;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error updating asset folder');
    }
  }
);

// Async thunk to delete asset folder
export const deleteAssetFolderSlice = createAsyncThunk(
  'AssetLibrary/deleteassetfolders',
  async (param: IDeletePayload, { rejectWithValue }) => {
    try {
      const response = await deleteAssetFolderApi(param);
      return response;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error deleting asset folder');
    }
  }
);

// Async thunk to fetch asset type data
export const fetchAssetTypeSlice = createAsyncThunk(
  'Asset/getassettype',
  async (clientId: any, { rejectWithValue }) => {
    try {
      let assetType = await fetchAssetTypeApi(clientId);
      assetType = {
        ...assetType
      }
      return assetType;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching asset type');
    }
  }
);

// Async thunk to add asset data
export const addAssetSlice = createAsyncThunk(
  'Asset/addasset',
  async (param: FormData, { rejectWithValue }) => {
      try {
          const response = await addAssetApi(param);
          return response;
      } catch (error: any) {
          return rejectWithValue(error.response?.data || 'Error adding asset data');
      }
  }
);

// Async thunk to edit asset data
export const editAssetSlice = createAsyncThunk(
  'Asset/editasset',
  async (param: FormData, { rejectWithValue }) => {
      try {
          const response = await addAssetApi(param);
          return response;
      } catch (error: any) {
          return rejectWithValue(error.response?.data || 'Error updating asset data');
      }
  }
);

// Async thunk to upload asset thumbnail
export const uploadAssetThumbnailSlice = createAsyncThunk(
  'Asset/uploadthumbnail',
  async (param: FormData, { rejectWithValue }) => {
      try {
          const response = await uploadAssetThumbnailApi(param);
          return response;
      } catch (error: any) {
          return rejectWithValue(error.response?.data || 'Error uploading asset thumbnail');
      }
  }
);

// Async thunk to Single asset fetch parameters type
export const fetchSingleAssetDataSlice = createAsyncThunk(
  'Asset/getassetbyid',
  async (param: IMainPayload, { rejectWithValue }) => {
    try {
      let data = await fetchSingleAssetDataApi(param);
      return data;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching asset data');
    }
  }
);

// Fetch asset files list with pagination and filters
export const fetchAssetFilesListSlice = createAsyncThunk(
  'AssetLibrary/getassetlist',
  async (param: IMainPayload, { rejectWithValue }) => {
    try {
      let assetFilesList = await fetchAssetFilesListApi(param); // Assuming fetchUserData fetches the user data
      assetFilesList = {
        ...assetFilesList,
        "totalRows": assetFilesList.totalRows,
      }
      return assetFilesList;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error fetching asset files List');
    }
  }
);

// Async thunk to activate/deactivate asset files
export const activateDeactivateAssetFilesSlice = createAsyncThunk(
  'AssetLibrary/deactivateasset',
  async (params: IAssetLibraryAssetListActivateDeactivatePayload, { rejectWithValue }) => {
    try {
      const data = await activateDeactivateAssetFilesApi(params);
      return data;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error updating asset files status');
    }
  }
);

// Async thunk to delete asset file
export const deleteAssetFileSlice = createAsyncThunk(
  'AssetLibrary/deleteasset',
  async (params: IDeletePayload, { rejectWithValue }) => {
    try {
      const response = await deleteAssetFileApi(params);
      return response;
    } catch (error: any) {
      return rejectWithValue(error.response?.data || 'Error deleting asset file');
    }
  }
);

// Slice definition
const assetLibrarySlice = createSlice({
  name: 'assetLibrary',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
    // Handle fetching asset folders and sub folders tree data
    .addCase(fetchAssetFoldersSlice.pending, (state) => {
        state.loading = true;
        state.error = null;
    })
    .addCase(fetchAssetFoldersSlice.fulfilled, (state, action: PayloadAction<assetFolderTree[]>) => {
      state.loading = false;
      state.assetFolderTree = action.payload;
    })
    .addCase(fetchAssetFoldersSlice.rejected, (state, action) => {
      state.loading = false;
      state.error = action.payload as string;
      state.assetFolderTree = []
    })

    // Handle fetching users
    .addCase(fetchAssetFilesListSlice.pending, (state) => {
      state.loading = true;
      state.error = null;
    })
    .addCase(fetchAssetFilesListSlice.fulfilled, (state, action: PayloadAction<AssetFilesList[]>) => {
      state.loading = false;
      state.assetFiles = action.payload;
    })
    .addCase(fetchAssetFilesListSlice.rejected, (state, action) => {
      state.loading = false;
      state.error = action.payload as string;
      state.assetFiles = []
    })

    // Handle activating/deactivating asset files
    .addCase(activateDeactivateAssetFilesSlice.pending, (state) => {
      state.loading = true;
      state.error = null;
    })
    .addCase(activateDeactivateAssetFilesSlice.fulfilled, (state, action: PayloadAction<AssetFilesList[]>) => {
      state.loading = false;
      state.assetFiles = action.payload;
    })
    .addCase(activateDeactivateAssetFilesSlice.rejected, (state, action) => {
      state.loading = false;
      state.error = action.payload as string;
    })

    // Handle deleting asset file
    .addCase(deleteAssetFileSlice.pending, (state, action) => {
      state.loading = true;
      state.error = null;
    })
    .addCase(deleteAssetFileSlice.fulfilled, (state, action: PayloadAction<AssetFilesList[]>) => {
      state.assetFiles = action.payload;
      state.loading = false;
    })
    .addCase(deleteAssetFileSlice.rejected, (state, action) => {
      state.error = action.payload as string;
      state.loading = false;
    })

    // Handle add asset folder
    .addCase(addAssetFolderSlice.pending, (state) => {
        state.loading = true;
        state.error = null;
    })
    .addCase(addAssetFolderSlice.fulfilled, (state, action: PayloadAction<AssetFolderData[]>) => {
      state.loading = false;
      state.assetFolderData = action.payload;
    })
    .addCase(addAssetFolderSlice.rejected, (state, action) => {
      state.loading = false;
      state.error = action.payload as string;
      state.assetFolderData = []
    })

    // Handle edit asset folder
    .addCase(editAssetFolderSlice.pending, (state) => {
        state.loading = true;
        state.error = null;
    })
    .addCase(editAssetFolderSlice.fulfilled, (state, action: PayloadAction<AssetFolderData[]>) => {
      state.loading = false;
      state.assetFolderData = action.payload;
    })
    .addCase(editAssetFolderSlice.rejected, (state, action) => {
      state.loading = false;
      state.error = action.payload as string;
      state.assetFolderData = []
    })

    // Handle deleting asset folder
    .addCase(deleteAssetFolderSlice.fulfilled, (state, action: PayloadAction<AssetFolderData[]>) => {
      state.assetFolderData = action.payload;
    })
    .addCase(deleteAssetFolderSlice.rejected, (state, action) => {
      state.error = action.payload as string;
    })

    // Handle fetching asset type
    .addCase(fetchAssetTypeSlice.pending, (state) => {
      state.loading = true;
      state.error = null;
    })
    .addCase(fetchAssetTypeSlice.fulfilled, (state, action: PayloadAction<AssetFolderData[]>) => {
      state.loading = false;
      state.assetFolderData = action.payload;
    })
    .addCase(fetchAssetTypeSlice.rejected, (state, action) => {
      state.loading = false;
      state.error = action.payload as string;
      state.assetFolderData = []
    })

    // Handle add asset in slice
    .addCase(addAssetSlice.pending, (state) => { 
        state.loading = true; 
        state.error = null;           
    })
    .addCase(addAssetSlice.fulfilled, (state, action: PayloadAction<AssetFolderData[]>) => {
        state.loading = false; 
        state.error = null;
        state.assetFolderData = Array.isArray(state.assetFolderData) ? [...state.assetFolderData, action.payload] : [action.payload];
    })
    .addCase(addAssetSlice.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
    })

    // Handle edit asset in slice
    .addCase(editAssetSlice.pending, (state) => { 
        state.loading = true; 
        state.error = null;           
    })
    .addCase(editAssetSlice.fulfilled, (state, action: PayloadAction<AssetFolderData[]>) => {
        state.loading = false; 
        state.error = null;
        state.assetFolderData = Array.isArray(state.assetFolderData) ? [...state.assetFolderData, action.payload] : [action.payload];
    })
    .addCase(editAssetSlice.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
    })

    // Handle fetching single asset data
    .addCase(fetchSingleAssetDataSlice.pending, (state) => {
      state.loading = true;
      state.error = null;
    })
    .addCase(fetchSingleAssetDataSlice.fulfilled, (state, action: PayloadAction<AssetFolderData[]>) => {
      state.loading = false;
      state.assetFolderData = action.payload;
    })
    .addCase(fetchSingleAssetDataSlice.rejected, (state, action) => {
      state.loading = false;
      state.error = action.payload as string;
      state.assetFolderData = []
    })
  },
});

export default assetLibrarySlice.reducer;