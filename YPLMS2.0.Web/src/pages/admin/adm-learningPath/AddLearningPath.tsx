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
import BasicDetailsContent from './BasicDetailsContent';
import AddModulesActivitiesContent from './AddModulesActivitiesContent';
import SettingsContent from './SettingsContent';

const AddLearningPath: React.FC = () => {
    const navigate = useNavigate();
    const dispatch = useAppDispatch();
    const loggedInUserId = useSelector((state: RootState) => state.auth.id);
    const clientId: any = useSelector((state: RootState) => state.auth.clientId);
    const { loading, error } = useAppSelector((state: RootState) => state.course);
    const { learningPathId } = useParams<{ learningPathId: string }>();
    let isAddForm: boolean = learningPathId ? false : true;
    const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);

    const breadcrumbItems = [
        { iconClass: 'fa-classic fa-solid fa-house', path: AdminPageRoutes.ADMIN_DASHBOARD.FULL_PATH },
        { label: 'Manage Learning Path', path: AdminPageRoutes.ADMIN_MANAGE_LEARNING_PATH.FULL_PATH },
        { label: isAddForm ? 'Add Learning Path' : 'Edit Learning Path' },
    ];
    const pageTitle: string = isAddForm ? 'Add Learning Path' : 'Edit Learning Path';
    document.title = pageTitle;

    const [activeTab, setActiveTab] = useState<string>('tabBasicDetails');

    // Basic Details Tab    
    const handleSetSelectedActivities = (data: any) => {
        
    };

    const handleSetActiveTab = (tabKey: string) => {
        setActiveTab(tabKey);
    };

    const handleCancel = () => {
        
    };
    // END Basic Details Tab

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
                        id: 'tabBasicDetails',
                        title: 'Basic Details',
                        content: (
                            <BasicDetailsContent
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
                        id: 'tabAddModulesActivities',
                        title: 'Modules and Activities',
                        content: (
                            <AddModulesActivitiesContent
                                // activeTabOnAPI={activeTab}
                                // onSetActiveTab={handleSetActiveTab}
                                // onCancel={handleCancel}
                                // onSelectedUsersChange={handleSetSelectedUsers}
                                // onSelectedBusinessRuleChange={handleSetSelectedBusinessRule}
                            />
                        ),
                        },
                        {
                        id: 'tabSettings',
                        title: 'Settings',
                        content: (
                            <SettingsContent
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

export default AddLearningPath;