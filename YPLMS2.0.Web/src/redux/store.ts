// Redux store setup

import { configureStore } from '@reduxjs/toolkit';
import authReducer from './Slice/authSlice';
import userReducer from './Slice/admin/userSlice';
import adminDashboardReducer from './Slice/admin/adminDashboardSlice';
import contentReducer from './Slice/admin/contentSlice';
import assetLibraryReducer from './Slice/admin/assetLibrarySlice';
import oneTimeAssignmentsReducer from './Slice/admin/oneTimeAssignmentsSlice';
import defaultAssignmentsReducer from './Slice/admin/defaultAssignmentsSlice';
import learnerDashboardReducer from './Slice/learner/dashboard/dashboard.slice';
import learnerCurrentAssignmentReducer from './Slice/learner/currentAssignment/currentAssignment.slice';
import learnerCompletedAssignmentReducer from './Slice/learner/completedAssignment/completedAssignment.slice';
import leaderBoardReducer from './Slice/leaderBoard/leaderBoard.slice';
import learningPathReducer from './Slice/admin/learningPathSlice';
import reporrtReducer from './Slice/admin/reportSlice';
import { sessionMiddleware } from './middleware/sessionMiddleware';

import gDataReducer from '../redux/Slice/coursePlayerScormSlice/gdSlice';
import updatedGlobalDataReducer from '../redux/Slice/coursePlayerScormSlice/updatedGlobalDataSlice';
import coursePlayerReducer from './Slice/coursePlayerScormSlice/courseLaunchSlice';

const store = configureStore({
  reducer: {
    auth: authReducer,
    adminDashboard: adminDashboardReducer,
    user: userReducer,
    course: contentReducer,
    assetLibrary: assetLibraryReducer,

    //couseplayer slice
    globalData: gDataReducer,
    updatedGlobalData : updatedGlobalDataReducer,
    coursePlayer: coursePlayerReducer,

 
    oneTimeAssignments: oneTimeAssignmentsReducer,
    defaultAssignments: defaultAssignmentsReducer,
    learnerDashboard: learnerDashboardReducer,
    currentAssignment: learnerCurrentAssignmentReducer,
    completedAssignment: learnerCompletedAssignmentReducer,
    leaderBoard:leaderBoardReducer,
    learningPath: learningPathReducer,  
    report: reporrtReducer
    // Add other reducers here
  },
  // middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(sessionMiddleware),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export default store;

