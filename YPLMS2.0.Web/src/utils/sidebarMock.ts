import { AdminPageRoutes } from "./Constants/Admin_PageRoutes";
import { LearnerPageRoutes } from "./Constants/Learner_PageRoutes";

export const adminSidebar = [
  {
    label: AdminPageRoutes.ADMIN_DASHBOARD.LABEL,
    path: AdminPageRoutes.ADMIN_DASHBOARD.FULL_PATH,
    moduleName: AdminPageRoutes.ADMIN_DASHBOARD.MODULE_NAME,
    pageName: AdminPageRoutes.ADMIN_DASHBOARD.PAGE_NAME,
    iconClass: "fa-classic fa-solid fa-house fa-fw yp-menu-icon-orange",
    subModule: [],
  },
  {
    label: AdminPageRoutes.ADMIN_LEADER_BOARD.LABEL,
    path: AdminPageRoutes.ADMIN_LEADER_BOARD.FULL_PATH,
    moduleName: AdminPageRoutes.ADMIN_LEADER_BOARD.MODULE_NAME,
    pageName: AdminPageRoutes.ADMIN_LEADER_BOARD.PAGE_NAME,
    iconClass: "fa-solid fa-ranking-star yp-menu-icon-sea-green ",
    subModule: [],
  },
  {
    label: "User Management",
    path: "",
    moduleName: "userManagement",
    pageName: "",
    iconClass:
      "yp-sidebar-icon fa-classic fa-regular fa-user fa-fw yp-menu-icon-purple",
    subModule: [
      {
        label: AdminPageRoutes.ADMIN_MANAGE_USER.LABEL,
        path: AdminPageRoutes.ADMIN_MANAGE_USER.FULL_PATH,
        subModuleName: AdminPageRoutes.ADMIN_MANAGE_USER.SUB_MODULE_NAME,
        pageName: AdminPageRoutes.ADMIN_MANAGE_USER.PAGE_NAME,
      },
      {
        label: AdminPageRoutes.ADMIN_PENDING_APPROVALS.LABEL,
        path: AdminPageRoutes.ADMIN_PENDING_APPROVALS.FULL_PATH,
        subModuleName: AdminPageRoutes.ADMIN_PENDING_APPROVALS.SUB_MODULE_NAME,
        pageName: AdminPageRoutes.ADMIN_PENDING_APPROVALS.PAGE_NAME,
      },
      {
        label: AdminPageRoutes.ADMIN_CONFIGURE_PROFILE_DEFINITION.LABEL,
        path: AdminPageRoutes.ADMIN_CONFIGURE_PROFILE_DEFINITION.FULL_PATH,
        subModuleName: AdminPageRoutes.ADMIN_CONFIGURE_PROFILE_DEFINITION.SUB_MODULE_NAME,
        pageName: AdminPageRoutes.ADMIN_CONFIGURE_PROFILE_DEFINITION.PAGE_NAME,
      },
      {
        label: AdminPageRoutes.ADMIN_MANAGE_GROUP.LABEL,
        path: AdminPageRoutes.ADMIN_MANAGE_GROUP.FULL_PATH,
        subModuleName: AdminPageRoutes.ADMIN_MANAGE_GROUP.SUB_MODULE_NAME,
        pageName: AdminPageRoutes.ADMIN_MANAGE_GROUP.PAGE_NAME,
      },
    ],
  },
  {
    label: "Content Management",
    path: "",
    moduleName: "contentManagement",
    pageName: "",
    iconClass:
      "yp-sidebar-icon fa-classic fa-regular fa-file-lines fa-fw yp-menu-icon-blue",
    subModule: [
      {
        label: AdminPageRoutes.ADMIN_CONTENT_MANAGE_COURSES.LABEL,
        path: AdminPageRoutes.ADMIN_CONTENT_MANAGE_COURSES.FULL_PATH,
        subModuleName: AdminPageRoutes.ADMIN_CONTENT_MANAGE_COURSES.SUB_MODULE_NAME,
        pageName: AdminPageRoutes.ADMIN_CONTENT_MANAGE_COURSES.PAGE_NAME,
      },
      {
        label: AdminPageRoutes.ADMIN_CONTENT_MANAGE_ASSET_LIBRARY.LABEL,
        path: AdminPageRoutes.ADMIN_CONTENT_MANAGE_ASSET_LIBRARY.FULL_PATH,
        subModuleName: AdminPageRoutes.ADMIN_CONTENT_MANAGE_ASSET_LIBRARY.SUB_MODULE_NAME,
        pageName: AdminPageRoutes.ADMIN_CONTENT_MANAGE_ASSET_LIBRARY.PAGE_NAME,
      },
      {
        label: AdminPageRoutes.ADMIN_MANAGE_LEARNING_PATH.LABEL,
        path: AdminPageRoutes.ADMIN_MANAGE_LEARNING_PATH.FULL_PATH,
        subModuleName: AdminPageRoutes.ADMIN_MANAGE_LEARNING_PATH.SUB_MODULE_NAME,
        pageName: AdminPageRoutes.ADMIN_MANAGE_LEARNING_PATH.PAGE_NAME,
      },
    ],
  },
  {
    label: "Assignment",
    path: "",
    moduleName: "assignment",
    pageName: "",
    iconClass:
      "yp-sidebar-icon fa-classic fa-regular fa-file fa-fw yp-menu-icon-red",
    subModule: [
      {
        label: AdminPageRoutes.ADMIN_ASSIGNMENT_ONE_TIME_ASSIGNMENT.LABEL,
        path: AdminPageRoutes.ADMIN_ASSIGNMENT_ONE_TIME_ASSIGNMENT.FULL_PATH,
        subModuleName: AdminPageRoutes.ADMIN_ASSIGNMENT_ONE_TIME_ASSIGNMENT.SUB_MODULE_NAME,
        pageName: AdminPageRoutes.ADMIN_ASSIGNMENT_ONE_TIME_ASSIGNMENT.PAGE_NAME,
      },
      {
        label: AdminPageRoutes.ADMIN_ASSIGNMENT_DEFAULT_ASSIGNMENT_DATES.LABEL,
        path: AdminPageRoutes.ADMIN_ASSIGNMENT_DEFAULT_ASSIGNMENT_DATES.FULL_PATH,
        subModuleName: AdminPageRoutes.ADMIN_ASSIGNMENT_DEFAULT_ASSIGNMENT_DATES.SUB_MODULE_NAME,
        pageName: AdminPageRoutes.ADMIN_ASSIGNMENT_DEFAULT_ASSIGNMENT_DATES.PAGE_NAME,
      },
    ],
  },
  {
    label: "Reports",
    path: "",
    moduleName: "reports",
    pageName: "",
    iconClass:
      "yp-sidebar-icon fa-classic fa-solid fa-chart-column fa-fw yp-menu-icon-green",
    subModule: [
      {
        label: AdminPageRoutes.ADMIN_LEARNER_PROGRESS_REPORT.LABEL,
        path: AdminPageRoutes.ADMIN_LEARNER_PROGRESS_REPORT.FULL_PATH,
        subModuleName: AdminPageRoutes.ADMIN_LEARNER_PROGRESS_REPORT.SUB_MODULE_NAME,
        pageName: AdminPageRoutes.ADMIN_LEARNER_PROGRESS_REPORT.PAGE_NAME,
      },
    ],
  }
];

export const learnerSidebar = [
  {
    label: LearnerPageRoutes.LEARNER_DASHBOARD.LABEL,
    path: LearnerPageRoutes.LEARNER_DASHBOARD.FULL_PATH,
    moduleName: LearnerPageRoutes.LEARNER_DASHBOARD.MODULE_NAME,
    pageName: LearnerPageRoutes.LEARNER_DASHBOARD.PAGE_NAME,
    iconClass: "fa-classic fa-solid fa-house fa-fw yp-menu-icon-orange",
    subModule: [],
  },
  {
    label: "Training",
    path: "",
    moduleName: "training",
    pageName: "",
    iconClass:
      "yp-sidebar-icon fa-solid fa-regular fa-tv fa-fw yp-menu-icon-blue",
    subModule: [
      {
        label: LearnerPageRoutes.CURRENT_ASSIGNMENT.LABEL,
        path: LearnerPageRoutes.CURRENT_ASSIGNMENT.FULL_PATH,
        subModuleName: LearnerPageRoutes.CURRENT_ASSIGNMENT.SUB_MODULE_NAME,
        pageName: LearnerPageRoutes.CURRENT_ASSIGNMENT.PAGE_NAME,
      },
      {
        label: LearnerPageRoutes.COMPLETED_ASSIGNMENT.LABEL,
        path: LearnerPageRoutes.COMPLETED_ASSIGNMENT.FULL_PATH,
        subModuleName: LearnerPageRoutes.COMPLETED_ASSIGNMENT.SUB_MODULE_NAME,
        pageName: LearnerPageRoutes.COMPLETED_ASSIGNMENT.PAGE_NAME,
      },
    ],
  },
];
