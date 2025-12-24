import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit';
import {  saveDefaultAssignmentApi, fetchDefaultAssignmentListApi } from '../../../api/admin/defaultAssignmentsApi';

interface DefaultAssignment {
    clientId: string,
    currentUrl: string,
    createdById: string,

    chkAssignmentDates: boolean,

    rbAbsoluteDate: boolean,
    txtDefaultAssignmnetDays: string,

    txtAssignmentDays: string,
    ddlAssignmentDate: string,

    rbAbsoluteDueDate: boolean,
    txtDefaultDueDays: string,

    rbRelativeDueDate: boolean,
    txtDueDays: string,
    ddlDueDate: string,
    rbNoDueDate: boolean,

    rbAbsoluteExpiryDate: boolean,
    txtDefaultExpDays: string,
    rbRelativeExpiryDate: boolean,
    txtExprDays: string,
    ddlExprDate: string
    rbNoExpiryDate: boolean,

    chkIsForDynamic: boolean
};

interface DefaultAssignmentList {
    clientId: string,
}
interface DefaultAssignmentsState {
    defaultAssignmentData: DefaultAssignment[];
    defaultDataList: DefaultAssignmentList[];
    loading: boolean;
    error: string | null;
}

const initialState: DefaultAssignmentsState = {
    defaultAssignmentData: [],
    defaultDataList: [],
    loading: false,
    error: null,
}



// Async thunk to save Default Assignment 
export const saveDefaultAssignmentSlice = createAsyncThunk(
    'ActivityAssignment/savedefaultassignment',
    async (data: DefaultAssignment, { rejectWithValue }) => {
        try {
            const response = await saveDefaultAssignmentApi(data);
            return response;
        } catch (error: any) {
            return rejectWithValue(error.response?.data || 'Error saving default assignment');
        }
    }
)



// Async thunk to fetch Default Assignment List
export const fetchDefaultAssignmentListSlice = createAsyncThunk(
    'ActivityAssignment/getdefaultsettings',
    async (clientId: string, { rejectWithValue }) => {
        try {
            let defaultData = await fetchDefaultAssignmentListApi(clientId);
            return { ...defaultData };
        } catch (error: any) {
            return rejectWithValue(error.response?.data || 'Error fetching data');
        }
    }
);



// Slice definition
const defaultAssignmentsSlice = createSlice({
    name: 'defaultAssignments',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            // Handle save default assignmnet
            .addCase(saveDefaultAssignmentSlice.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(saveDefaultAssignmentSlice.fulfilled, (state, action: PayloadAction<any>) => {
                state.loading = false;
                state.defaultAssignmentData = [];
            })
            .addCase(saveDefaultAssignmentSlice.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload as string;
                state.defaultAssignmentData = [];
            })

  .addCase(fetchDefaultAssignmentListSlice.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(fetchDefaultAssignmentListSlice.fulfilled, (state, action: PayloadAction<DefaultAssignmentList[]>) => {
                state.loading = false;
                state.defaultDataList = action.payload;
            })
            .addCase(fetchDefaultAssignmentListSlice.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload as string;
                state.defaultDataList = [];
            });
    },
});

export default defaultAssignmentsSlice.reducer;

