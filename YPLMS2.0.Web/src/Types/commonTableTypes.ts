import {
  COMMON_TABLE_TYPE,
  COMMON_FOLDER_TREEVIEW_TYPE,
} from "../utils/Constants/Enums";

//Common for User Management - Content Management
export interface IMainPayload {
  id?: string;
  clientId: string;
  listRange?: {
    pageIndex: number;
    pageSize: number;
    sortExpression: string;
    requestedById?: string;
  };
  keyWord?: string;
  userCriteria?: {
    FirstName?: string;
    LastName?: string;
    LoginId?: string;
    Email?: string;
    Active?: boolean;

    /* Pending Approvals Search Filters */
    firstName?: string;
    lastName?: string;
    loginId?: string;
    email?: string;
    registrationDateFrom?: null;
    registrationDateTo?:null;
    userStatus?: string;
    country?: string;
    organization?: string;
    department?: string;
  };
}

export interface LookupTextValue {
  lookupText: string;
  lookupValue: string;
}

export interface ICategoryPayload {
  id?: string;
  clientId: string;
  listRange?: {
    pageIndex: number;
    pageSize: number;
    totalRows?: number;
    sortExpression: string;
    requestedById?: string;
  };
  keyWord?: string;
  IsActiveActivity?: boolean;
}
export interface ISubCategoryPayload {
  Id?: string;
  clientId: string;
  IsActiveActivity?: boolean;
}

export interface IClientIdCurrentUserIdPayload {
  clientId: string;
  currentUserId: string;
}

export interface IActivateDeactivatePayload {
  id: string;
  clientId: string;
  lastModifiedById: string;
  isActive: boolean;
}

export interface IApproveRejectPayload {
  id?: string; //id = signupId
  signUpId?: string;
  clientId: string;
  lastModifiedBy: string;
  userStatus: string;
}

export interface IDeletePayload {
  id: string;
  clientId: string;
  lastModifiedById?: string;
  createdById?: string;
}

export interface IValidateXMLFilePayload {
  clientId: string;
  contentModuleURL: string;
  contentModuleTypeId: string;
}

interface TableColumn {
  key: string;
  label: string;
}
export interface SortColumn {
  key: string;
  sort: string;
}
export interface TableConfig {
  showActionColumnForTheseTables: boolean;
  showFilterSectionForTheseTables: boolean;
  showSearchSectionForTheseTables: boolean;
  showRadioForTheseTables: boolean;
  showCheckboxForTheseTables: boolean;
  showEditIconForTheseTables: boolean;
  ISColumnsFiledsActivityName: boolean;
  ISColumnsFiledsUserName: boolean;
  searchPlaceholder: string;
  columns: TableColumn[];
  sortConfig?:string[];
  // sortConfig: SortColumn[] |[];
}
//User Management ---> Users
export interface CommonTableProps<T extends { id: string }> {
  type: COMMON_TABLE_TYPE;
  tableConfig: TableConfig;
  filterComponent?: any;
  onFilterSubmit?: (filterData: any,sortExpression?:string) => void;
  onClearFilter?:(sortExpression?:string)=>void;
  data: T[];
  onSearch?: (keyword: string,sortExpression?:string) => void;
  onPageChange: (page: number, pageSize: number,sortExpression?:string) => void;
  currentPage: number;
  totalRecords: number;
  pageSize: number;
  selectedUsers?: string[];
  actionButtonList?: any[];
  actionButtonClick?: (actionType: string) => void;
  handleCheckboxChange?: (userId: string) => void;
  handleSelectAll?: any;
  onCourseLaunch?: any;
  searchMode?: "keyword" | "filter" | null;
  setSearchMode?: React.Dispatch<
    React.SetStateAction<"keyword" | "filter" | null>
  >;
  isFilterVisible?: any;
  setFilterVisible?: any;
  actionDeleteClick?: (id: any) => void;
  actionCopyClick?: (id: any) => void;
  isHidePagination?: boolean;
  isHideHeader?: boolean;
  searchValue?: string;
  tableTitle?: string;
  handleSelectChange?: (selectedOption: Record<string, any>) => void;
  defaultAutcompleteValue?: Record<string, any>;
  autocompleteData?: any[];
  getSortedList?:(sortExpression:string)=>void;
  handleOpenDetailsPopup?: any;
}

export interface User {
  id: string;
  firstName: string;
  lastName: string;
  userNameAlias: string;
  emailID: string;
  isActive: "Active" | "Inactive";
  managerName: string;
  userDefaultOrg: string;
  userStatus?: string;
  signUpId?: string;
}

//User Management ---> Add/Edit User
export interface IUserAddUserPayload {
  id?: string;
  clientId?: string;
  lastModifiedById?: string;

  firstName: string;
  lastName: string;
  emailID: string;
  userNameAlias: string;
  userNameAliasHidden?: string;
  defaultLanguageId: string;
  userPassword: string;
  confirmPassword: string;
  isActive: boolean | undefined;
  managerEmailId: string;
  isSendEmail: boolean;
  isAutoEmail: boolean;
  isDirectSendMail: boolean;
  FlagAddUserPage?: boolean;
}

//User Management ---> Configure Profile Definition
export interface ManageRowEditableUserProps {
  usersProfile: any;
  onSaveClick: (data: any) => void;
  // onSearch: (keyword: string) => void;
  // onPageChange: (page: number, pageSize: number) => void;
  // currentPage: number;
  // totalRecords: number;
  // pageSize: number;
}

//User Management ---> Groups
export interface Group {
  id: string;
  ruleName: string;
  ruleDescription: string;
  createdById: string;
  dateCreated: string;
  isActive: "Active" | "Inactive";
}

//Content Management ---> Courses
export interface Courses {
  id: string;
  contentModuleEnglishName: string;
  contentModuleTypeId: string;
  isActive: "Active" | "Inactive";
}
export interface ICourseAddCoursePayload {
  id?: string;
  clientId?: string;
  createdById?: string | null;
  lastModifiedById?: string | null;
  contentModuleName: string;
  contentModuleEnglishName: string;
  contentModuleTypeId: string;
  contentModuleSubTypeId: string;
  contentModuleDescription: string;
  contentModuleKeyWords: string;
  contentModuleURL: string;
  masteryScore: number;
  courseLaunchSameWindow: boolean;
  courseLaunchNewWindow: boolean;
  isAssessment: boolean;
  isMiddlePage: boolean;
  isPrintCertificate: boolean;
  protocol: string;
  allowScroll: boolean;
  allowResize: boolean;
  courseWindowWidth: number;
  courseWindowHeight: number;
  sendEmailTo: string;
  isActive: boolean;
  ThumbnailImgRelativePath?: string;
}

export interface IAssetLibraryAddEditAssetFolderPayload {
  id?: string;
  clientId?: string;
  currentUserId?: string;
  assetFolderName: string;
  parentFolderId: string | null;
  assetFolderDescription?: string;
}

export interface IAssetLibraryAssetListPayload {
  id: string;
  assetName: string;
  modifiedByName: string;
  assetFolderId: string;
  assetFileName: string;
  assetFileType: string;
  assetDescription: string;
  isActive: "Active" | "Inactive";
}

export interface IAssetLibraryAssetListActivateDeactivatePayload {
  id: string;
  clientId: string;
  currentUserId: string;
  assetName: string;
  assetFolderId: string;
  assetFileType: string;
  assetDescription: string;
  assetFileName: string;
  isActive: boolean;
}

// Course Folder Tree
export interface CommonFolderTreeViewProps<T extends { id: string }> {
  id?: string;
  type?: COMMON_FOLDER_TREEVIEW_TYPE;
  data: T[];
  onPageChange?: (page: number, pageSize: number) => void;
  currentPage?: number;
  totalRecords?: number;
  pageSize?: number;
  onSelect?: string;
  onFileSelect?: string | any;
  //onSearch: (keyword: string) => void;
  //actionButtonList?: any[],
  //actionButtonClick: (actionType: string) => void;
}

export interface CourseListByFile {
  id: string;
  clientId: string;
  ruleId?: string;
  createdById: string;
  categoryId?: string;
  dateCreated?: string;
  lastModifiedById?: string;
  contactPersonDetails?: string;
  lastModifiedDate?: string;
  createdByName?: string;
  lastModifiedByName?: string;
  currentUserId: string;
  listRange: {
    pageIndex: number;
    pageSize: number;
    totalRows: number;
    sortExpression: string;
    requestedById: string;
    keyWord: string;
  };
  fileURL: string;
  keyword: string;
}

//Learning Path
export interface LearningPath {
  id: string;
  curriculumName: string;
  lastModifiedByName: string;
  isUsed: string;
  isActive: "Active" | "Inactive";
}
export interface IDeleteLearningPathPayload {
  id: string;
  clientId: string;
  inUse?: string;
  userId?: string;
}

export interface IActivateDeActivateLearningPathPayload {
  clientId: string;
  userId: string;
}

export interface ILearningPathPayload {
  id: string;
  clientId: string;
  userId: string;
}


export interface GenerateReportPayload {
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