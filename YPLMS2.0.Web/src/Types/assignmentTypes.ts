import { LookupTextValue } from "./commonTableTypes";


export interface selectActivityContentProps {
  activities: Activity[];
  activityList: any;
  currentPage: number;
  pageSize: number;
  searchMode: 'keyword' | 'filter' | null;
  isFilterVisible: boolean;
  selectedActivities: string[];
  handleCheckboxChange: (id: string) => void;
  handleSelectAll: (select: boolean) => void;
  handleFilterSubmit: (filter: any) => void;
  handleClearFilters: () => void;
  handleSearch: (keyword: string) => void;
  handlePageChange: (page: number, newPageSize?: number) => void;
  setSearchMode: React.Dispatch<React.SetStateAction<'keyword' | 'filter' | null>>;
  setFilterVisible: React.Dispatch<React.SetStateAction<boolean>>;
  activityTypeList: LookupTextValue[];
  categoryList: LookupTextValue[];
  subCategoryList: LookupTextValue[];
  handleCategoryChange: (categoryId: string) => void;
  setActiveTab: (tabKey: string) => void;
  onNext: () => void;
  onCancel: () => void;
}

export interface IMainPayloadOneTimeAssignment {
  id?: string;
  clientId: string;
  listRange?: {
    pageIndex: number,
    pageSize: number,
    sortExpression?: string,
    requestedById?: string,
    totalRows?: number,
  }
  keyWord?: string,

  activityCriteria?: {
    activityTypeId?: number,
    activityName?: string,
    categoryId?: string,
    subCategoryId?: string,
  }
  isActiveActivity?: boolean,
  IsActive?: boolean,
}

export type TabItem = {
  id: string;
  title: string;
  content: React.ReactNode;
};

// One Time Assignment - Activity
export interface Activity {
  id: string;
  activityId?: string;
  activityName: string;
  activityType: string;
  createdByName: string;
}

export interface User {
  id: string;
  userNameAlias?: string;
  firstName?: string;
  lastName?: string;
}


export interface AssignmentDefineProperties {
  AssignmentDateText?: string;
  DueDateText?: string;
  ExpiryDateText?: string;
  LastModifiedById?: string;
  CurrentUserID?: string;
  IsReassignmentChecked: boolean;
  BusinessRuleId: string;
  IsMailChecked: boolean;
  IsAutoMail: boolean;
  IsDirectSendMail: boolean;
  IsAssignmentBasedOnHireDate: boolean;
  IsAssignmentBasedOnCreationDate: boolean;
  AssignAfterDaysOf: number | "";
  IsNoDueDate: boolean;
  IsDueBasedOnAssignDate: boolean;
  IsDueBasedOnHireDate: boolean;
  IsDueBasedOnCreationDate: boolean;
  IsDueBasedOnStartDate: boolean;
  DueAfterDaysOf: number | "";
  IsNoExpiryDate: boolean;
  IsExpiryBasedOnAssignDate: boolean;
  IsExpiryBasedOnStartDate: boolean;
  IsExpiryBasedOnDueDate: boolean;
  ExpireAfterDaysOf: number | "";
}

export interface ISaveOnetimeAssignment {
  clientId: string;
  activities: { id: string; name: string; type: any; isSelected: boolean }[],
  learners: { id: string; isSelected: boolean; registrationDate: "" }[],
  AssignmentDateText?: string;
  DueDateText?: string;
  ExpiryDateText?: string;
  LastModifiedById?: string;
  CurrentUserID?: string;
  IsReassignmentChecked: boolean;
  BusinessRuleId: string;
  IsMailChecked: boolean;
  IsAutoMail: boolean;
  IsDirectSendMail: boolean;
  IsAssignmentBasedOnHireDate: boolean;
  IsAssignmentBasedOnCreationDate: boolean;
  AssignAfterDaysOf: number | "";
  IsNoDueDate: boolean;
  IsDueBasedOnAssignDate: boolean;
  IsDueBasedOnHireDate: boolean;
  IsDueBasedOnCreationDate: boolean;
  IsDueBasedOnStartDate: boolean;
  DueAfterDaysOf: number | "";
  IsNoExpiryDate: boolean;
  IsExpiryBasedOnAssignDate: boolean;
  IsExpiryBasedOnStartDate: boolean;
  IsExpiryBasedOnDueDate: boolean;
  ExpireAfterDaysOf: number | "";
}


export interface IMainDefaultassignment {
 
  clientId: string,
  currentUrl: string,
  createdById: string,
 
  chkAssignmentDates?: boolean,
 
  rbAbsoluteDate?: boolean,
  txtDefaultAssignmnetDays?: string,
 
  txtAssignmentDays?: string,  
  ddlAssignmentDate?: string,
 
  rbAbsoluteDueDate?: boolean,
  txtDefaultDueDays?: string,
 
  rbRelativeDueDate?: boolean,
  txtDueDays?: string,
  ddlDueDate?: string,
  rbNoDueDate?: boolean,
 
  rbAbsoluteExpiryDate?: boolean,
  txtDefaultExpDays?: string,
  rbRelativeExpiryDate?: boolean,
  txtExprDays?: string,
  ddlExprDate?: string
  rbNoExpiryDate?: boolean,  
 
  chkIsForDynamic?: boolean
}