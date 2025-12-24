import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { RootState } from "../../../../redux/store";
import { useAppDispatch, useAppSelector } from "../../../../hooks";
import AlertMessage from "../../../../components/shared/AlertMessage";
import CustomBreadcrumb from "../../../../components/shared/CustomBreadcrumb";
import TabContainer from "../../../../components/shared/Tabs/TabContainer";
import SelectActivityContent from "./SelectActivityContent";
import SelectUsersContent from "./SelectUsersContent";
import DefinePropertiesContent from "./DefinePropertiesContent";
import SelectedUsersContent from "./SelectedUsersContent";
import PreviewContent from "./PreviewContent";
import { AdminPageRoutes } from '../../../../utils/Constants/Admin_PageRoutes';
import { resetOneTimeAssignment } from "../../../../redux/Slice/admin/oneTimeAssignmentsSlice";
import { fetchDefaultAssignmentListSlice } from "../../../../redux/Slice/admin/defaultAssignmentsSlice";


const OneTimeAssignment: React.FC = () => {
  const dispatch = useAppDispatch();
  const defaultAssignmentData = useSelector((state: RootState) => state.defaultAssignments.defaultDataList);
  const clientId: any = useSelector((state: RootState) => state.auth.clientId);


  const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);
  const { loading } = useAppSelector((state: RootState) => state.oneTimeAssignments);
  const [resetForm, setResetForm] = useState(false);

  /** Breadcrumb navigation items*/
  const breadcrumbItems = [
    { iconClass: 'fa-classic fa-solid fa-house', path: AdminPageRoutes.ADMIN_DASHBOARD.FULL_PATH },
    { label: 'One-Time Assignments' },
  ];
  const pageTitle: string = 'One-Time Assignments';
  document.title = pageTitle;

  const [activeTab, setActiveTab] = useState<string>('tabSelectActivity');

  // Select Activity Tab
  const [selectedActivities, setSelectedActivities] = useState<string[]>([]);
  const handleSetSelectedActivities = (data: any) => {
    setSelectedActivities(data);
  };

  const handleSetActiveTab = (tabKey: string) => {
    setActiveTab(tabKey);
  };

  const handleCancel = () => {
    setSelectedActivities([]);
    setSelectedUsers([]);
    setSelectedBusinessRuleName('0');
    setSelectedBusinessRuleValue('0');
  };
  // END Select Activity Tab

  // Select User Tab
  const [selectedUsers, setSelectedUsers] = useState<string[]>([]);
  const [selectedBusinessRuleName, setSelectedBusinessRuleName] = useState<string>('');
  const [selectedBusinessRuleValue, setSelectedBusinessRuleValue] = useState<string>('');
  const handleSetSelectedUsers = (data: any) => {
    setSelectedUsers(data);
  };
  const handleSetSelectedBusinessRule = (name: string, value: string) => {
    if (value != '') {
      setSelectedBusinessRuleName(name);
      setSelectedBusinessRuleValue(value);
    }
    else {
      setSelectedBusinessRuleName('0');
      setSelectedBusinessRuleValue('0');
    }
  };
  // END Select User Tab

  // Final Save / Submit
  const handleResetAfterSubmit = () => {
    setSelectedActivities([]);
    setSelectedUsers([]);
    setSelectedBusinessRuleName('0');
    setSelectedBusinessRuleValue('0');
    dispatch(resetOneTimeAssignment());
    dispatch(fetchDefaultAssignmentListSlice(clientId))
    setResetForm(true);
  };
  const [hasVisitedDefineTab, setHasVisitedDefineTab] = useState(false);


  return (
    <>
      {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}

      {loading ? (<div className="yp-loading-overlay"><div className="yp-spinner"><div className="spinner-border"></div></div></div>) : null}

      <div className="yp-page-title-button-section" id="yp-page-title-breadcrumb-section">
        <div className="yp-page-title-breadcrumb">
          <div className="yp-page-title">{pageTitle}</div>
          <CustomBreadcrumb items={breadcrumbItems} />
        </div>
      </div>

      <div className="yp-card p-0 pb-3" id="yp-card-main-content-section">
        <TabContainer
          activeTab={activeTab}
          onTabChange={setActiveTab}
          disableNextTabs={true} 
          tabs={[
            {
              id: 'tabSelectActivity',
              title: '1. Select Activity',
              content: (
                <SelectActivityContent
                  activeTabOnAPI={activeTab}
                  onSetActiveTab={handleSetActiveTab}
                  onCancel={handleCancel}
                  onSelectedActivitiesChange={handleSetSelectedActivities}
                  resetForm={resetForm}
                  onResetComplete={() => setResetForm(false)}
                />
              ),
            },
            {
              id: 'tabSelectUsers',
              title: '2. User Selection',
              content: (
                <SelectUsersContent
                  activeTabOnAPI={activeTab}
                  onSetActiveTab={handleSetActiveTab}
                  onCancel={handleCancel}
                  onSelectedUsersChange={handleSetSelectedUsers}
                  onSelectedBusinessRuleChange={handleSetSelectedBusinessRule}
                />
              ),
            },
            {
              id: 'tabSelectedUsers',
              title: '3. Selected Users',
              content: (
                <SelectedUsersContent
                  activeTabOnAPI={activeTab}
                  onSetActiveTab={handleSetActiveTab}
                  onCancel={handleCancel}
                  selectedBusinessRuleName={selectedBusinessRuleName}
                  hasVisitedDefineTab={hasVisitedDefineTab}
                  setHasVisitedDefineTab={setHasVisitedDefineTab}
                />
              ),
            },
            {
              id: 'tabDefineProperties',
              title: '4. Define Properties',
              content: (
                <DefinePropertiesContent
                  onSetActiveTab={handleSetActiveTab}
                  onCancel={handleCancel}
                  selectedBusinessRuleValue={selectedBusinessRuleValue}
                  resetForm={resetForm}
                  onResetComplete={() => setResetForm(false)}
                  defaultAssignmentData={defaultAssignmentData}

                />
              ),
            },
            {
              id: 'tabPreview',
              title: '5. Preview',
              content: (
                <PreviewContent
                  onSetActiveTab={handleSetActiveTab}
                  onCancel={handleCancel}
                  onResetAfterSubmit={handleResetAfterSubmit}
                  selectedBusinessRuleName={selectedBusinessRuleName}
                />
              ),
            },
          ]}
        />
      </div>
    </>
  );
};

export default OneTimeAssignment;
