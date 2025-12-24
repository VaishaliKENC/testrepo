import React, { useEffect, useRef, useState } from 'react';
import { useAppDispatch, useAppSelector } from '../../../hooks';
import { fetchAdminDashboardCountSlice, fetchRecentAssignmentSlice, fetchTopAssignmentSlice } from '../../../redux/Slice/admin/adminDashboardSlice';
import { useSelector } from 'react-redux';
import { RootState } from '../../../redux/store';
import TabContainer from "../../../components/shared/Tabs/TabContainer";
import TabRecentlyAccessedChart from './TabRecentlyAccessedChart';
import TabTopAssignedChart from './TabTopAssignedChart';

import AdminLeaderBoard from './AdminLeaderBoard';

const Dashboard: React.FC = () => {
  const pageTitle: string = 'Dashboard';
  document.title = pageTitle;

  const dispatch = useAppDispatch();
  const clientId: any = useSelector((state: RootState) => state.auth.clientId);
  const currentUserId: any = useSelector((state: RootState) => state.auth.id);

  const { totalUsers, activeUsers, totalCourses, totalCurriculums } = useSelector( (state: RootState) => state.adminDashboard.data);
  const recentAssignment: any = useSelector((state: RootState) => state.adminDashboard.recentAssignment);
  const topAssignment: any = useSelector((state: RootState) => state.adminDashboard.topAssignment);

  const [activeTab, setActiveTab] = useState<string>("tabRecentlyAccessed");

    //  Load count & default tab data on page load
  useEffect(() => {
    if (!clientId || !currentUserId) return;

    dispatch(fetchAdminDashboardCountSlice({ clientId, currentUserId }));

    if (activeTab === "tabRecentlyAccessed") {
      dispatch(fetchRecentAssignmentSlice({ clientId, currentUserId }));
    } else if (activeTab === "tabTopAssigned") {
      dispatch(fetchTopAssignmentSlice({ clientId, currentUserId }));
    }
  }, [clientId, currentUserId]);

  //  Handle tab change and fetch API accordingly
  const handleSetActiveTab = (tabKey: string) => {
    setActiveTab(tabKey);

    if (!clientId || !currentUserId) return;

    if (tabKey === "tabRecentlyAccessed") {
      dispatch(fetchRecentAssignmentSlice({ clientId, currentUserId }));
    } else if (tabKey === "tabTopAssigned") {
      dispatch(fetchTopAssignmentSlice({ clientId, currentUserId }));
    }
  };
  return (
    <>
      <div className="yp-page-title-button-section" id="yp-page-title-breadcrumb-section">
        <div className="yp-page-title-breadcrumb">
          <div className="yp-page-title">{pageTitle}</div>
        </div>
      </div>

      <div className="yp-admin-dashboard-stat-boxes">
        <div className="row row-gap-3">
          <div className="col-lg-3 col-md-6 col-sm-6 col-6">
            <div className="yp-admin-dashboard-stat-box">
              <div className="yp-admin-dashboard-stat-box-icon yp-dash-icon-purple">
                <svg xmlns="http://www.w3.org/2000/svg" width="36" height="26" viewBox="0 0 36 26" fill="none">
                  <path d="M5.4 11.1429C7.38562 11.1429 9 9.47723 9 7.42857C9 5.37991 7.38562 3.71429 5.4 3.71429C3.41438 3.71429 1.8 5.37991 1.8 7.42857C1.8 9.47723 3.41438 11.1429 5.4 11.1429ZM30.6 11.1429C32.5856 11.1429 34.2 9.47723 34.2 7.42857C34.2 5.37991 32.5856 3.71429 30.6 3.71429C28.6144 3.71429 27 5.37991 27 7.42857C27 9.47723 28.6144 11.1429 30.6 11.1429ZM32.4 13H28.8C27.81 13 26.9156 13.4121 26.2631 14.0795C28.53 15.3621 30.1388 17.6777 30.4875 20.4286H34.2C35.1956 20.4286 36 19.5987 36 18.5714V16.7143C36 14.6656 34.3856 13 32.4 13ZM18 13C21.4819 13 24.3 10.0924 24.3 6.5C24.3 2.90759 21.4819 0 18 0C14.5181 0 11.7 2.90759 11.7 6.5C11.7 10.0924 14.5181 13 18 13ZM22.32 14.8571H21.8531C20.6831 15.4375 19.3837 15.7857 18 15.7857C16.6162 15.7857 15.3225 15.4375 14.1469 14.8571H13.68C10.1025 14.8571 7.2 17.8518 7.2 21.5429V23.2143C7.2 24.7522 8.40938 26 9.9 26H26.1C27.5906 26 28.8 24.7522 28.8 23.2143V21.5429C28.8 17.8518 25.8975 14.8571 22.32 14.8571ZM9.73688 14.0795C9.08438 13.4121 8.19 13 7.2 13H3.6C1.61437 13 0 14.6656 0 16.7143V18.5714C0 19.5987 0.804375 20.4286 1.8 20.4286H5.50688C5.86125 17.6777 7.47 15.3621 9.73688 14.0795Z" fill="white" />
                </svg>
              </div>
              <div className="yp-admin-dashboard-stat-box-info">
                <div className="yp-admin-dashboard-stat-box-count">{totalUsers}</div>
                <div className="yp-admin-dashboard-stat-box-name">Total Users</div>
              </div>
            </div>
          </div>
          <div className="col-lg-3 col-md-6 col-sm-6 col-6">
            <div className="yp-admin-dashboard-stat-box">
              <div className="yp-admin-dashboard-stat-box-icon yp-dash-icon-blue">
                <svg xmlns="http://www.w3.org/2000/svg" width="39" height="33" viewBox="0 0 39 33" fill="none">
                  <path d="M9.55519 12.3391C12.5551 12.3391 14.9832 10.0266 14.9832 7.16955C14.9832 4.31245 12.5551 2 9.55519 2C6.55523 2 4.12716 4.31245 4.12716 7.16955C4.12716 10.0266 6.55523 12.3391 9.55519 12.3391ZM13.2773 13.8161H12.875C11.8669 14.2777 10.7474 14.5546 9.55519 14.5546C8.36296 14.5546 7.24827 14.2777 6.23537 13.8161H5.83311C2.75077 13.8161 0.25 16.1978 0.25 19.1334V20.4627C0.25 21.6858 1.29199 22.6782 2.5763 22.6782H16.5341C17.8184 22.6782 18.8604 21.6858 18.8604 20.4627V19.1334C18.8604 16.1978 16.3596 13.8161 13.2773 13.8161Z" fill="white" />
                  <path d="M23.8418 13.5176L24.04 13.6074C24.9371 14.0161 25.9113 14.2559 26.9453 14.2559C27.9801 14.2558 28.9584 14.016 29.8486 13.6084L30.0469 13.5176H30.667C34.255 13.5176 37.25 16.3017 37.25 19.835V21.1641C37.2499 22.9848 35.7138 24.3789 33.9238 24.3789H19.9658C18.1759 24.3788 16.6398 22.9848 16.6396 21.1641V19.835C16.6396 16.3017 19.6346 13.5176 23.2227 13.5176H23.8418ZM26.9453 1.70117C30.4508 1.70142 33.373 4.41645 33.373 7.87109C33.3728 11.3256 30.4507 14.0398 26.9453 14.04C23.4397 14.04 20.5168 11.3257 20.5166 7.87109C20.5166 4.41629 23.4396 1.70117 26.9453 1.70117Z" fill="white" stroke="url(#paint0_linear_3002_3249)" strokeWidth="2" />
                  <circle cx="18.7501" cy="23.5" r="9" fill="#7ABEEA" stroke="white" />
                  <path d="M22.7677 20.0938C23.1529 19.7018 23.78 19.7018 24.1652 20.0938L24.4991 20.4346H24.381C24.5197 20.7933 24.4472 21.2167 24.1622 21.5068L17.8781 27.9062C17.4929 28.2983 16.8659 28.298 16.4806 27.9062L13.338 24.7061C12.9547 24.3158 12.9547 23.6842 13.338 23.2939C13.7233 22.9018 14.3502 22.9017 14.7355 23.2939L17.1788 25.7803L22.7677 20.0938Z" fill="white" stroke="white" strokeWidth="0.4" />
                  <defs>
                    <linearGradient id="paint0_linear_3002_3249" x1="26.9448" y1="2.70117" x2="26.9448" y2="23.3794" gradientUnits="userSpaceOnUse">
                      <stop stopColor="#7EC1ED" />
                      <stop offset="0.5" stopColor="#74B9E7" />
                      <stop offset="1" stopColor="#6CB2E1" />
                    </linearGradient>
                  </defs>
                </svg>
              </div>
              <div className="yp-admin-dashboard-stat-box-info">
                <div className="yp-admin-dashboard-stat-box-count">{activeUsers}</div>
                <div className="yp-admin-dashboard-stat-box-name">Active Users</div>
              </div>
            </div>
          </div>
          <div className="col-lg-3 col-md-6 col-sm-6 col-6">
            <div className="yp-admin-dashboard-stat-box">
              <div className="yp-admin-dashboard-stat-box-icon yp-dash-icon-green">
                <svg xmlns="http://www.w3.org/2000/svg" width="32" height="30" viewBox="0 0 32 30" fill="none">
                  <g opacity="0.95">
                    <path d="M4 0C1.79375 0 0 1.79375 0 4V20C0 22.2062 1.79375 24 4 24H6V29C6 29.3813 6.2125 29.725 6.55 29.8937C6.8875 30.0625 7.29375 30.025 7.6 29.8L15.3312 24H28C30.2062 24 32 22.2062 32 20V4C32 1.79375 30.2062 0 28 0H4Z" fill="white" />
                    <rect x="3" y="3" width="26" height="18" rx="4" fill="#95B864" />
                    <path d="M9 12C9 10.1435 9.7375 8.36301 11.0503 7.05025C12.363 5.7375 14.1435 5 16 5C17.8565 5 19.637 5.7375 20.9497 7.05025C22.2625 8.36301 23 10.1435 23 12C23 13.8565 22.2625 15.637 20.9497 16.9497C19.637 18.2625 17.8565 19 16 19C14.1435 19 12.363 18.2625 11.0503 16.9497C9.7375 15.637 9 13.8565 9 12ZM14.1488 9.02227C13.941 9.13711 13.8125 9.35859 13.8125 9.59375V14.4062C13.8125 14.6441 13.941 14.8629 14.1488 14.9777C14.3566 15.0926 14.6082 15.0898 14.8133 14.9641L18.7508 12.5578C18.9449 12.4375 19.0652 12.227 19.0652 11.9973C19.0652 11.7676 18.9449 11.557 18.7508 11.4367L14.8133 9.03047C14.6109 8.90742 14.3566 8.90195 14.1488 9.0168V9.02227Z" fill="white" />
                  </g>
                </svg>
              </div>
              <div className="yp-admin-dashboard-stat-box-info">
                <div className="yp-admin-dashboard-stat-box-count">{totalCourses}</div>
                <div className="yp-admin-dashboard-stat-box-name">Total Courses</div>
              </div>
            </div>
          </div>
          <div className="col-lg-3 col-md-6 col-sm-6 col-6">
            <div className="yp-admin-dashboard-stat-box">
              <div className="yp-admin-dashboard-stat-box-icon yp-dash-icon-orange">
                <svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" viewBox="0 0 30 30" fill="none">
                  <path d="M2 1H20.1055C20.6577 1.00011 21.1055 1.44778 21.1055 2V20.1055C21.1054 20.6576 20.6576 21.1054 20.1055 21.1055H2C1.44778 21.1055 1.00011 20.6577 1 20.1055V2C1 1.44772 1.44771 1 2 1Z" fill="#E1943D" fillOpacity="0.4" stroke="#F5CFA5" strokeWidth="2" />
                  <path d="M5.94751 4.94727H24.053C24.6052 4.94737 25.053 5.39505 25.053 5.94727V24.0527C25.0529 24.6049 24.6051 25.0526 24.053 25.0527H5.94751C5.39529 25.0527 4.94762 24.6049 4.94751 24.0527V5.94727C4.94751 5.39498 5.39522 4.94727 5.94751 4.94727Z" fill="#E69A44" stroke="#FAEBDA" strokeWidth="2" />
                  <rect x="8.89453" y="8.89453" width="20.1053" height="20.1053" rx="1" fill="white" stroke="white" strokeWidth="2" />
                  <path d="M15.8861 18.9282C15.8861 17.4409 15.8913 15.9518 15.8827 14.4645C15.881 14.0966 15.9563 13.7839 16.3073 13.6042C16.6583 13.4246 16.9561 13.5559 17.2472 13.7701C19.2759 15.2626 21.3081 16.7499 23.3368 18.2407C23.9257 18.6725 23.924 19.2288 23.3334 19.6623C21.2961 21.1583 19.2537 22.6508 17.2164 24.1467C16.9305 24.3558 16.6326 24.4611 16.2987 24.2884C15.9666 24.1156 15.881 23.8133 15.8827 23.454C15.8896 21.946 15.8861 20.4379 15.8861 18.9299V18.9282Z" fill="#E1943D" />
                </svg>
              </div>
              <div className="yp-admin-dashboard-stat-box-info">
                {/* <div className="yp-admin-dashboard-stat-box-count">{totalCurriculums}</div> */}
                <div className="yp-admin-dashboard-stat-box-count">0</div>
                <div className="yp-admin-dashboard-stat-box-name">Total Curriculums</div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div className="row row-gap-5">
        <div className="col-lg-6 col-md-12">
         <AdminLeaderBoard />
        </div>

        <div className="col-lg-6 col-md-12">
          <div className="yp-dashboard-cards-wrapper">
            <div className="yp-text-16-400 yp-color-482F58 mb-3">Top Activities</div>
            <div className="yp-card yp-admin-dashboard-activities-card">
              <TabContainer
                activeTab={activeTab}
                onTabChange={handleSetActiveTab}
                disableNextTabs={false}
                tabs={[
                  {
                    id: 'tabRecentlyAccessed',
                    title: 'Recently Accessed',
                    content: (
                      <TabRecentlyAccessedChart activeTab={activeTab} RecentDataAssignment={recentAssignment?.recentAssignmentdata || []} />
                    )
                  },
                  {
                    id: 'tabTopAssigned',
                    title: 'Top Assigned',
                    content: (
                      <TabTopAssignedChart activeTab={activeTab} TopDataAssignment={topAssignment?.topAssignmentdata || []} />
                    )
                  }
                ]}
              />
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default Dashboard;
