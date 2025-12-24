import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit';
import { GenerateReportPayload, IDeleteLearningPathPayload, ILearningPathPayload } from '../../../Types/commonTableTypes';
import { ILearningPathFilterPayload, IMainPayloadLearningPath } from '../../../Types/learningPathTypes';
import { activateSelectedLearningPathApi, copyLearningPathApi, deActivateSelectedLearningPathApi, deleteSelectedLearningPathApi, fetchLearningPathApi, fetchSearchData } from '../../../api/admin/learningPathApi';
import { fetchStandardProfileFieldApi, generateReportApi } from '../../../api/admin/reportApi';

// LearningPath interface definition
interface ReportUsers {
    ClientId: number;
    PageIndex: number;
    PageSize: number;
    SortExpression: string;
    SortDirection: string;
    ActivityName: string;
    ActivityType: string | null;
    CompletionStatus: string | null;
    UserName: string | null;
    AssignmentDateFrom: string | null;
    AssignmentDateTo: string | null;
    DueDateFrom: string | null;
    DueDateTo: string | null;
    ExpiryDateFrom: string | null;
    ExpiryDateTo: string | null;
    CompletionDateFrom: string | null;
    CompletionDateTo: string | null;
    IncludeInactiveUsers: boolean;
    Attempt: string;
    BusinessRuleId: number | null;
    StandardFields: string | null;
    CustomFields: string | null;
    UserId: string;
}

export interface StandardProfileField {
    id: number;
    fieldName: string;
    fieldTypes: string;
}

interface ReportState {
    reportUserList: ReportUsers[];
    standardProfileField: StandardProfileField[];
    loading: boolean;
    error: string | null;
}

const initialState: ReportState = {
    reportUserList: [],
    standardProfileField: [],
    loading: false,
    error: null,
};


interface FetchStandardProfileFieldArgs {
    clientId: string;
    pageIndex: number;
    pageSize: number;
}

export const fetchStandardProfileFieldSlice = createAsyncThunk(
    "Report/standardprofilefield",
    async (
        { clientId, pageIndex, pageSize }: FetchStandardProfileFieldArgs,
        { rejectWithValue }
    ) => {
        try {
            let profileField = await fetchStandardProfileFieldApi(clientId, pageIndex, pageSize);
            return profileField;
        } catch (error: any) {
            return rejectWithValue(
                error.response?.data || "Error fetching profileField List"
            );
        }
    }
);

// Async thunk to generate report
export const generateReportSlice = createAsyncThunk(
    'report/generate',
    async (reportList: GenerateReportPayload, { rejectWithValue }) => {
        try {
            let response: any = await generateReportApi(reportList); // assume { data: Report[], totalRows }
            // response = {
            //     ...response,
            //     "totalPages": response.totalPages,
            //     "totalRecords": response.totalRecords,
            // }
            return response;
        } catch (error: any) {
            return rejectWithValue(error.response?.data || 'Error fetching report');
        }
    }
);



// Slice definition
const reportSlice = createSlice({
    name: 'reports',
    initialState,
    reducers: {

    },
    extraReducers: (builder) => {
        builder
            // Handle fetching activities
            .addCase(fetchStandardProfileFieldSlice.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(fetchStandardProfileFieldSlice.fulfilled, (state, action: PayloadAction<StandardProfileField[]>) => {
                state.loading = false;
                state.standardProfileField = action.payload;
            })
            .addCase(fetchStandardProfileFieldSlice.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload as string;
                state.standardProfileField = []
            })

            // Handle fetching reports
            .addCase(generateReportSlice.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(generateReportSlice.fulfilled, (state, action: PayloadAction<ReportUsers[]>) => {
                state.loading = false;
                state.reportUserList = action.payload;
            })
            .addCase(generateReportSlice.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload as string;
                state.reportUserList = []
            })




    },
});

export const { } = reportSlice.actions;

export default reportSlice.reducer;

