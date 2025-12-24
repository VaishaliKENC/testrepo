

// All routes configuration
import React, { Fragment, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Outlet, Navigate, useNavigate } from 'react-router-dom';
import LoginPage from '../pages/auth/LoginPage';
import SignupPage from '../pages/auth/SignupPage';

import MainLayout from '../layouts/MainLayout';
import 'bootstrap/dist/css/bootstrap.min.css';
//import 'bootstrap/dist/js/bootstrap.bundle.min';
import "react-datepicker/dist/react-datepicker.css";
import '../styles/common.css';
import '../styles/common-theme.scss';

import { useSelector } from 'react-redux';
import { RootState } from '../redux/store';
import { selectAuth } from '../redux/Slice/authSlice';
import { PageRoutes } from '../utils/Constants/Auth_PageRoutes';
import { AdminPageRoutes } from '../utils/Constants/Admin_PageRoutes';
import { LearnerPageRoutes } from '../utils/Constants/Learner_PageRoutes';
import ForgotPasswordPage from '../pages/auth/ForgotPasswordPage';
import DashboardPage from '../pages/admin/adm-dashboard/AdminDashboard';
import ManageUser from '../pages/admin/adm-userManagement/ManageUser';
import AddUser from '../pages/admin/adm-userManagement/AddUser';
import ConfigureProfileDefinition from '../pages/admin/adm-userManagement/ConfigureProfileDefinition';
import ManageCourses from '../pages/admin/adm-contentManagement/ManageCourses';
import AddCourse from '../pages/admin/adm-contentManagement/AddCourses';
import ManageGroup from '../pages/admin/adm-userManagement/ManageGroup';
import NotFoundPage from '../pages/NotFoundPage';
import LearnerDashboardPage from '../pages/learner/lrnr-dashboard/learnerDashboardPage';
import ManageAssetLibrary from '../pages/admin/adm-contentManagement/ManageAssetLibrary';
import AddAsset from '../pages/admin/adm-contentManagement/AddAsset';
import ManageCurriculumPlans from '../pages/admin/adm-contentManagement/ManageCurriculumPlans';
import ManageCertificates from '../pages/admin/adm-contentManagement/ManageCertificates';
import ManageCertificateMapping from '../pages/admin/adm-contentManagement/ManageCertificateMapping';
import Redirector from '../pages/Redirector';
import LaunchCourse from '../pages/admin/adm-coursePlayer/course-ScormPlayer/LaunchCourse';
import LaunchContentSCORM from '../pages/admin/adm-coursePlayer/course-ScormPlayer/LaunchContentSCORM';
import LaunchCourseWindow from '../pages/admin/adm-coursePlayer/course-ScormPlayer/LaunchCourseWindow';
import OneTimeAssignment from '../pages/admin/adm-assignment/OneTimeAssignment/OneTimeAssignment';
import DefaultAssignmentDates from '../pages/admin/adm-assignment/DefaultAssignmentDates';
import CurrentAssignmentPage from '../pages/learner/currentAssignment.page';
import CompletedAssignmentPage from '../pages/learner/completedAssignment.page';
import PreviewFile from "../components/shared/CommonComponents/PreviewFile";
import LearnerLeaderboardUsersPage from '../pages/learner/learnerLeaderboardUsers.page';
import AdminLeaderBoardUsers from '../pages/admin/adm-leaderBoardUsers.page';
import AddLearningPath from '../pages/admin/adm-learningPath/AddLearningPath';
import ManageLearningPath from '../pages/admin/adm-learningPath/ManageLearningPath';
import AddUserGroup from '../pages/admin/adm-userManagement/AddUserGroup';
import LearnerProgressReport from '../pages/admin/adm-report/LearnerProgressReport';
import PendingApprovals from '../pages/admin/adm-userManagement/PendingApprovals';

const ProtectedRoutes = () => (
  <MainLayout>
    <Outlet />
  </MainLayout>
);

const AuthChecker = () => {
  const { isAuthenticated } = useSelector((state: RootState) => selectAuth(state));
  const navigate = useNavigate();
  const pathname = window.location.pathname;

  useEffect(() => {
    // Allow public routes like login, signup, forgot password without redirecting
    const publicRoutes = [
      PageRoutes.LOGIN.FULL_PATH,
      PageRoutes.SIGNUP.FULL_PATH,
      PageRoutes.FORGOT_PASSWORD.FULL_PATH,
      AdminPageRoutes.ADMIN_ASSET_PREVIEW.FULL_PATH,
    ];

    if (!isAuthenticated && !publicRoutes.includes(pathname)) {
      navigate(PageRoutes.LOGIN.FULL_PATH);
    }
  }, [isAuthenticated, navigate]);

  return null;
};

const AppRoutes = () => {
  return (
    <Router>
      <AuthChecker />
      <Routes>
        {/* Public Routes */}
        <Route path={PageRoutes.BASE_PATH.FULL_PATH} element={<LoginPage />} />
        <Route path={PageRoutes.LOGIN.FULL_PATH} element={<LoginPage />} />
        <Route path={PageRoutes.SIGNUP.FULL_PATH} element={<SignupPage />} />
        <Route path={PageRoutes.FORGOT_PASSWORD.FULL_PATH} element={<ForgotPasswordPage />} />


        {/* CoursePlayer Routes */}
        <Route path="/LaunchCourse" element={<LaunchCourse />} />
        <Route path="/LaunchContentScorm" element={<LaunchContentSCORM />} />
        <Route path="/LaunchCourseWindow" element={<LaunchCourseWindow />} />


        <Route path="/preview" element={<PreviewFile />} />
        {/* Protected Routes */}
        <Route element={<ProtectedRoutes />}>

          {/* Admin Routes */}
          <Route path={AdminPageRoutes.ADMIN_DASHBOARD.FULL_PATH} element={<DashboardPage />} />
          <Route path={AdminPageRoutes.ADMIN_MANAGE_USER.FULL_PATH} element={<ManageUser />} />
          <Route path={AdminPageRoutes.ADMIN_PENDING_APPROVALS.FULL_PATH} element={<PendingApprovals />} />
          <Route path={AdminPageRoutes.ADMIN_ADD_USER.FULL_PATH} element={<AddUser />} />
          <Route path={AdminPageRoutes.ADMIN_EDIT_USER.FULL_PATH} element={<AddUser />} />
          <Route path={AdminPageRoutes.ADMIN_CONFIGURE_PROFILE_DEFINITION.FULL_PATH} element={<ConfigureProfileDefinition />} />
          <Route path={AdminPageRoutes.ADMIN_MANAGE_GROUP.FULL_PATH} element={<ManageGroup />} />
          <Route path={AdminPageRoutes.ADMIN_CONTENT_MANAGE_COURSES.FULL_PATH} element={<ManageCourses />} />
          <Route path={AdminPageRoutes.ADMIN_CONTENT_ADD_COURSE.FULL_PATH} element={<AddCourse />} />
          <Route path={AdminPageRoutes.ADMIN_CONTENT_EDIT_COURSE.FULL_PATH} element={<AddCourse />} />
          <Route path={AdminPageRoutes.ADMIN_CONTENT_MANAGE_ASSET_LIBRARY.FULL_PATH} element={<ManageAssetLibrary />} />
          <Route path={AdminPageRoutes.ADMIN_CONTENT_ADD_ASSET.FULL_PATH} element={<AddAsset />} />
          <Route path={AdminPageRoutes.ADMIN_CONTENT_EDIT_ASSET.FULL_PATH} element={<AddAsset />} />
          <Route path={AdminPageRoutes.ADMIN_CONTENT_MANAGE_CURRICULUM_PLANS.FULL_PATH} element={<ManageCurriculumPlans />} />
          <Route path={AdminPageRoutes.ADMIN_CONTENT_MANAGE_CERTIFICATE.FULL_PATH} element={<ManageCertificates />} />
          <Route path={AdminPageRoutes.ADMIN_CONTENT_MANAGE_CERTIFICATE_MAPPING.FULL_PATH} element={<ManageCertificateMapping />} />
          <Route path={AdminPageRoutes.ADMIN_ASSIGNMENT_ONE_TIME_ASSIGNMENT.FULL_PATH} element={<OneTimeAssignment />} />
          <Route path={AdminPageRoutes.ADMIN_ASSIGNMENT_DEFAULT_ASSIGNMENT_DATES.FULL_PATH} element={<DefaultAssignmentDates />} />
          <Route path={AdminPageRoutes.ADMIN_LEADER_BOARD.FULL_PATH} element={<AdminLeaderBoardUsers />} />
           <Route path={AdminPageRoutes.ADMIN_MANAGE_LEARNING_PATH.FULL_PATH} element={<ManageLearningPath />} />
          <Route path={AdminPageRoutes.ADMIN_CONTENT_ADD_LEARNING_PATH.FULL_PATH} element={<AddLearningPath />} />
          <Route path={AdminPageRoutes.ADMIN_ADD_USER_GROUP.FULL_PATH} element={<AddUserGroup />} />
          <Route path={AdminPageRoutes.ADMIN_LEARNER_PROGRESS_REPORT.FULL_PATH} element={<LearnerProgressReport />} />

          {/* Learner Routes */}
          <Route path={LearnerPageRoutes.LEARNER_DASHBOARD.FULL_PATH} element={<LearnerDashboardPage />} />
          <Route path={LearnerPageRoutes.CURRENT_ASSIGNMENT.FULL_PATH} element={<CurrentAssignmentPage />} />
          <Route path={LearnerPageRoutes.COMPLETED_ASSIGNMENT.FULL_PATH} element={<CompletedAssignmentPage />} />
          <Route path={LearnerPageRoutes.LEADER_BOARD.FULL_PATH} element={<LearnerLeaderboardUsersPage />} />
          
        </Route>

        <Route path="/redirect" element={<Redirector />} />

        {/* Not Found Page (without sidebar & navbar) */}
        <Route path="*" element={<NotFoundPage />} />

        {/* Fallback: Redirect any unknown route to login */}
        <Route path="*" element={<Navigate to={PageRoutes.LOGIN.FULL_PATH} />} />
      </Routes>

    </Router>
  );
};

export default AppRoutes;
