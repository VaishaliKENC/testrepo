export const MANAGE_USER_TABLE_CONFIG = {
  showActionColumnForTheseTables: true,
  showFilterSectionForTheseTables: true,
  showSearchSectionForTheseTables: true,
  showRadioForTheseTables: false,
  showCheckboxForTheseTables: true,
  showEditIconForTheseTables: true,
  ISColumnsFiledsActivityName: false,
  ISColumnsFiledsUserName: false,
  searchPlaceholder: "Name/Login/Email",
  columns: [
    { key: "firstName", label: "First Name" },
    { key: "lastName", label: "Last Name" },
    { key: "userNameAlias", label: "Login ID" },
    { key: "emailID", label: "Email ID" },
    { key: "isActive", label: "Status", isBadge: true },
  ],
  sortConfig:["firstName","lastName","userNameAlias","emailID","isActive"],
  //for isActive in asc false value will come first and for desc true values will come up
};

export const PENDING_APPROVALS_TABLE_CONFIG = {
  showActionColumnForTheseTables: true,
  showFilterSectionForTheseTables: true,
  showSearchSectionForTheseTables: true,
  showRadioForTheseTables: true,
  showCheckboxForTheseTables: false,
  showEditIconForTheseTables: false,
  ISColumnsFiledsActivityName: false,
  ISColumnsFiledsUserName: false,
  searchPlaceholder: "Name/Login/Email",
  columns: [
    { key: "firstName", label: "First Name" },
    { key: "lastName", label: "Last Name" },
    { key: "emailId", label: "Email Address" },
    { key: "userStatus", label: "Status", isApprovedPendingReject: true },
    { key: "country", label: "Country" },
    { key: "registrationDate", label: "Registration Date" },
  ],
  sortConfig:["firstName","lastName","emailId","userStatus", "country", "registrationDate"],
  //for isActive in asc false value will come first and for desc true values will come up
};

export const MANAGE_GROUP_TABLE_CONFIG = {
  showActionColumnForTheseTables: true,
  showFilterSectionForTheseTables: false,
  showSearchSectionForTheseTables: true,
  showRadioForTheseTables: false,
  showCheckboxForTheseTables: true,
  showEditIconForTheseTables: true,
  ISColumnsFiledsActivityName: false,
  ISColumnsFiledsUserName: false,
  searchPlaceholder: "Group Name",
  columns: [
    { key: "ruleName", label: "Group Name" },
    { key: "ruleDescription", label: "Description" },
    { key: "createdById", label: "Created by" },
    { key: "dateCreated", label: "Created On" },
    { key: "isActive", label: "Status", isBadge: true },
  ],
   sortConfig: [
   
  ],
};

export const MANAGE_COURSES_TABLE_CONFIG = {
  showActionColumnForTheseTables: true,
  showFilterSectionForTheseTables: false,
  showSearchSectionForTheseTables: true,
  showRadioForTheseTables: false,
  showCheckboxForTheseTables: true,
  showEditIconForTheseTables: true,
  ISColumnsFiledsActivityName: false,
  ISColumnsFiledsUserName: false,
  searchPlaceholder: "Course ID/Course Name/Standard Type",
  columns: [
    { key: "id", label: "Course ID" },
    { key: "contentModuleEnglishName", label: "Course Name" },
    { key: "contentModuleTypeId", label: "Standard Type" },
    { key: "isActive", label: "Status", isBadge: true },
  ],
   sortConfig: [
   
  ],
};

export const MANAGE_ASSET_LIBRARY_TABLE_CONFIG = {
  showActionColumnForTheseTables: true,
  showFilterSectionForTheseTables: false,
  showSearchSectionForTheseTables: true,
  showRadioForTheseTables: true,
  showCheckboxForTheseTables: false,
  showEditIconForTheseTables: true,
  ISColumnsFiledsActivityName: false,
  ISColumnsFiledsUserName: false,
  searchPlaceholder: "Asset Name/Type",
  columns: [
    { key: "assetName", label: "Asset Name" },
    { key: "assetFileType", label: "Type" },
    { key: "modifiedByName", label: "Modified By" },
    { key: "isActive", label: "Status", isBadge: true },
  ],
   sortConfig: [
   
  ],
};

export const MANAGE_ONE_TIME_ASSIGNMENT_ACTIVITY_TABLE_CONFIG = {
  showActionColumnForTheseTables: false,
  showFilterSectionForTheseTables: true,
  showSearchSectionForTheseTables: true,
  showRadioForTheseTables: false,
  showCheckboxForTheseTables: true,
  showEditIconForTheseTables: false,
  ISColumnsFiledsActivityName: true,
  ISColumnsFiledsUserName: false,
  searchPlaceholder: "Activity Name",
  columns: [
    { key: "activityName", label: "Activity Name" },
    { key: "activityType", label: "Activity Type" },
    { key: "createdByName", label: "Created By" },
  ],
   sortConfig: [
   
  ],
};

export const MANAGE_ONE_TIME_ASSIGNMENT_USER_TABLE_CONFIG = {
  showActionColumnForTheseTables: false,
  showFilterSectionForTheseTables: false,
  showSearchSectionForTheseTables: true,
  showRadioForTheseTables: false,
  showCheckboxForTheseTables: true,
  showEditIconForTheseTables: false,
  ISColumnsFiledsActivityName: false,
  ISColumnsFiledsUserName: true,
  searchPlaceholder: "User Name/Login ID",
  columns: [
    { key: "firstName", label: "User Name" },
    { key: "userNameAlias", label: "Login ID" },
  ],
   sortConfig: [
   
  ],
};

export const MANAGE_ONE_TIME_ASSIGNMENT_USER_BUSINESS_RULE_TABLE_CONFIG = {
  showActionColumnForTheseTables: false,
  showFilterSectionForTheseTables: false,
  showSearchSectionForTheseTables: false,
  showRadioForTheseTables: false,
  showCheckboxForTheseTables: false,
  showEditIconForTheseTables: false,
  ISColumnsFiledsActivityName: false,
  ISColumnsFiledsUserName: false,
  searchPlaceholder: "Group Name", // default fallback
  columns: [
    { key: "firstName", label: "First Name" },
    { key: "lastName", label: "Last Name" },
    { key: "userNameAlias", label: "Login ID" },
    { key: "emailID", label: "Email ID" },
    { key: "userScope", label: "Organisation Hierarchy" },
  ],
   sortConfig: [
   
  ],
};
export const MANAGE_ONE_TIME_ASSIGNMENT_USER_SELECTED_TABLE_CONFIG = {
  showActionColumnForTheseTables: false,
  showFilterSectionForTheseTables: false,
  showSearchSectionForTheseTables: false,
  showRadioForTheseTables: false,
  showCheckboxForTheseTables: true,
  showEditIconForTheseTables: false,
  ISColumnsFiledsActivityName: false,
  ISColumnsFiledsUserName: false,
  searchPlaceholder: "Group Name", // Default placeholder since not specified
  columns: [
    { key: "firstName", label: "User Name" },
    { key: "userNameAlias", label: "Login ID" },
  ],
   sortConfig: [
   
  ],
};

export const MANAGE_ONE_TIME_ASSIGNMENT_PREVIEW_CONTENT_SELECTED_ACTIVITY_TABLE_CONFIG =
  {
    showActionColumnForTheseTables: false,
    showFilterSectionForTheseTables: false,
    showSearchSectionForTheseTables: false,
    showRadioForTheseTables: false,
    showCheckboxForTheseTables: false,
    showEditIconForTheseTables: false,
    ISColumnsFiledsActivityName: true,
    ISColumnsFiledsUserName: false,
    searchPlaceholder: "Group Name", // Default placeholder since not specified
    columns: [
      { key: "activityName", label: "Activity Name" },
      { key: "activityType", label: "Activity Type" },
      { key: "createdByName", label: "Created By" },
    ],
     sortConfig: [
   
  ],
  };

export const MANAGE_ONE_TIME_ASSIGNMENT_PREVIEW_CONTENT_SELECTED_USER_TABLE_CONFIG =
  {
    showActionColumnForTheseTables: false,
    showFilterSectionForTheseTables: false,
    showSearchSectionForTheseTables: false,
    showRadioForTheseTables: false,
    showCheckboxForTheseTables: false,
    showEditIconForTheseTables: false,
    ISColumnsFiledsActivityName: false,
    ISColumnsFiledsUserName: true,
    searchPlaceholder: "Group Name", // Default placeholder since not specified
    columns: [
      { key: "firstName", label: "User Name" },
      { key: "userNameAlias", label: "Login ID" },
    ],
     sortConfig: [
   
  ],
  };

export const MANAGE_TREE_COURSES_FILES_TABLE_CONFIG = {
  showActionColumnForTheseTables: false,
  showFilterSectionForTheseTables: false,
  showSearchSectionForTheseTables: true,
  showRadioForTheseTables: true,
  showCheckboxForTheseTables: false,
  showEditIconForTheseTables: false,
  ISColumnsFiledsActivityName: false,
  ISColumnsFiledsUserName: false,
  searchPlaceholder: "File Name/Type",
  columns: [
    { key: "name", label: "File Name" },
    { key: "size", label: "Size" },
    { key: "type", label: "Type" },
    { key: "lastModified", label: "Modified Date & Time" },
  ],
   sortConfig: [
   
  ],
};

export const MANAGE_ADMIN_LEADERBOARD_USERS_TABLE_CONFIG = {
  showActionColumnForTheseTables: false,
  showFilterSectionForTheseTables: false,
  showSearchSectionForTheseTables: true,
  showRadioForTheseTables: false,
  showCheckboxForTheseTables: false,
  showEditIconForTheseTables: false,
  ISColumnsFiledsActivityName: false,
  ISColumnsFiledsUserName: false,
  searchPlaceholder: "course name",
  columns: [
    { key: "rank", label: "Rank" },
    { key: "loginID", label: "LoginId" },
    { key: "fullName", label: "Full Name" },
    { key: "score", label: "Score" },
    { key: "dateOfCompletion", label: "CompletionDate" },
  ],
   sortConfig: [
   
  ],
};

export const MANAGE_ADMIN_LEARNING_PATH_TABLE_CONFIG = {
  showActionColumnForTheseTables: true,
  showFilterSectionForTheseTables: true,
  showSearchSectionForTheseTables: true,
  showRadioForTheseTables: true,
  showCheckboxForTheseTables: false,
  showEditIconForTheseTables: true,
  ISColumnsFiledsActivityName: false,
  ISColumnsFiledsUserName: false,
  searchPlaceholder: "Learning Path Name..",
  columns: [
    { key: "curriculumName", label: "Learning Path Name" },
    { key: "modifiedByName", label: "Modified By" },
    { key: "createdByName", label: "Created By" },
    { key: "isUsedT", label: "Assigned", isUsed: true },
    { key: "isActive", label: "Status", isBadge: true },
  ],
   sortConfig: [
   
  ],
};
// export const MANAGE_COURSES_TABLE_CONFIG = {
//   showActionColumnForTheseTables: ,
//   showFilterSectionForTheseTables: ,
//   showSearchSectionForTheseTables: ,
//   showRadioForTheseTables: ,
//   showCheckboxForTheseTables: ,
//   showEditIconForTheseTables: ,
//   ISColumnsFiledsActivityName: ,
//   ISColumnsFiledsUserName: ,
//   searchPlaceholder: "",
//   columns: [

//         ],
// };


export const REPORT_LEARNER_PROGRESS_ACTIVITY_TABLE_CONFIG = {
  showActionColumnForTheseTables: false,
  showFilterSectionForTheseTables: false,
  showSearchSectionForTheseTables: false,
  showRadioForTheseTables: false,
  showCheckboxForTheseTables: true,
  showEditIconForTheseTables: false,
  ISColumnsFiledsActivityName: true,
  ISColumnsFiledsUserName: false,
  searchPlaceholder: "Activity Name",
  columns: [
    { key: "activityName", label: "Activity Name" },
    { key: "activityType", label: "Activity Type" },
    { key: "createdByName", label: "Created By" },
    { key: "dateCreated", label: "Created On" },
  ],
   sortConfig: [
   
  ],
};

export const REPORT_LEARNER_PROGRESS_TABLE_CONFIG = {
  showActionColumnForTheseTables: false,
  showFilterSectionForTheseTables: false,
  showSearchSectionForTheseTables: false,
  showRadioForTheseTables: false,
  showCheckboxForTheseTables: false,
  showEditIconForTheseTables: false,
  ISColumnsFiledsActivityName: true,
  ISColumnsFiledsUserName: false,
  searchPlaceholder: "Activity Name",
  columns: [
    { key: "learnerId", label: "Login ID" },
    { key: "userName", label: "User Name" },
    { key: "activityName", label: "Activity Name" },
    { key: "activityType", label: "Activity Type" },
    // { key: "categoryName", label: "Category Name" },
    { key: "assignmentDate", label: "Assignment Date" },
    { key: "startDate", label: "Start Date" },
    // { key: "dueDate", label: "Due Date" },
    // { key: "expiryDate", label: "Expiry Date" },
    // { key: "completionDate", label: "Completion Date" },
    // { key: "completionStatus", label: "Completion Status" },
    // { key: "score", label: "Score" },
    // { key: "assignedBy", label: "Assigned By" },
  ],
   sortConfig: [
   
  ],
};