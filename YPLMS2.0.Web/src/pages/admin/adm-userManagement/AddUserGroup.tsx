import React, { useEffect, useRef, useState } from 'react';
import { useNavigate, useParams } from "react-router-dom";
import { AdminPageRoutes } from "../../../utils/Constants/Admin_PageRoutes";
import CustomBreadcrumb from "../../../components/shared/CustomBreadcrumb";
import { RootState } from "../../../redux/store";
import { useAppDispatch, useAppSelector } from '../../../hooks';
import { useSelector } from 'react-redux';
import * as bootstrap from 'bootstrap'; // Import Bootstrap
import AlertMessage from '../../../components/shared/AlertMessage';
import TabContainer from "../../../components/shared/Tabs/TabContainer";
import TabGroupDetails from './TabGroupDetails';
import TabCriteriaType from './TabCriteriaType';
import TabGetUsers from './TabGetUsers';

const AddUserGroup: React.FC = () => {
    const navigate = useNavigate();
    const dispatch = useAppDispatch();
    const loggedInUserId = useSelector((state: RootState) => state.auth.id);
    const clientId: any = useSelector((state: RootState) => state.auth.clientId);
    const { loading, error } = useAppSelector((state: RootState) => state.course);
    const { userGroupId } = useParams<{ userGroupId: string }>();
    let isAddForm: boolean = userGroupId ? false : true;
    const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);

    const breadcrumbItems = [
        { iconClass: 'fa-classic fa-solid fa-house', path: AdminPageRoutes.ADMIN_DASHBOARD.FULL_PATH },
        { label: 'User Groups', path: AdminPageRoutes.ADMIN_MANAGE_GROUP.FULL_PATH },
        { label: isAddForm ? 'Add User Group' : 'Edit User Group' },
    ];
    const pageTitle: string = isAddForm ? 'Add User Group' : 'Edit User Group';
    document.title = pageTitle;

    const [activeTab, setActiveTab] = useState<string>('tabGroupDetails');

    // Group Details Tab
    const handleSetActiveTab = (tabKey: string) => {
        setActiveTab(tabKey);
    };

    const handleCancel = () => {
        
    };
    // END Group Details Tab

    return(
        <>
            {/* Loading Overlay */}
            {loading && (
                <div className='yp-loading-overlay'>
                    <div className='yp-spinner'>
                        <div className='spinner-border' role='status'>
                            <span className='visually-hidden'>Loading...</span>
                        </div>
                    </div>
                </div>
            )}

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
                    disableNextTabs={false} 
                    tabs={[
                        {
                        id: 'tabGroupDetails',
                        title: 'Group Details',
                        content: (
                            <TabGroupDetails
                                // activeTabOnAPI={activeTab}
                                // onSetActiveTab={handleSetActiveTab}
                                // onCancel={handleCancel}
                                // onSelectedActivitiesChange={handleSetSelectedActivities}
                                // resetForm={resetForm}
                                // onResetComplete={() => setResetForm(false)}
                            />
                        ),
                        },
                        {
                        id: 'tabCriteriaType',
                        title: 'Criteria Type',
                        content: (
                            <TabCriteriaType
                                // activeTabOnAPI={activeTab}
                                // onSetActiveTab={handleSetActiveTab}
                                // onCancel={handleCancel}
                                // onSelectedUsersChange={handleSetSelectedUsers}
                                // onSelectedBusinessRuleChange={handleSetSelectedBusinessRule}
                            />
                        ),
                        },
                        {
                        id: 'tabGetUsers',
                        title: 'Get Users',
                        content: (
                            <TabGetUsers
                                // activeTabOnAPI={activeTab}
                                // onSetActiveTab={handleSetActiveTab}
                                // onCancel={handleCancel}
                                // selectedBusinessRuleName={selectedBusinessRuleName}
                                // hasVisitedDefineTab={hasVisitedDefineTab}
                                // setHasVisitedDefineTab={setHasVisitedDefineTab}
                            />
                        ),
                        }
                    ]}
                    />

                {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}
            </div>
        </>
    )
}

export default AddUserGroup;