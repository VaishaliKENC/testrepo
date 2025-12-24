import React, { useCallback, useEffect, useMemo, useRef, useState } from 'react';
import { debounce } from "lodash";
import { Courses, IActivateDeactivatePayload, IDeletePayload, User } from '../../../Types/commonTableTypes';
import { useAppDispatch, useAppSelector } from '../../../hooks';
import { useSelector } from 'react-redux';
import { RootState } from '../../../redux/store';
import { useNavigate } from 'react-router-dom';
import { AdminPageRoutes } from '../../../utils/Constants/Admin_PageRoutes';
import { ACTION_BUTTON_ENUM, COMMON_TABLE_TYPE } from '../../../utils/Constants/Enums';
import CommonTable from '../../../components/shared/CommonComponents/CommonTable';
import ConfirmationModal from '../../../components/shared/ConfirmationModal';
import CustomBreadcrumb from '../../../components/shared/CustomBreadcrumb';
import { activateDeactivateCourse, deleteCourses, fetchCourses } from '../../../redux/Slice/admin/contentSlice';
import AlertMessage from '../../../components/shared/AlertMessage';
import { contentModuleSession, getAssignmentForLaunch } from '../../../redux/Slice/coursePlayerScormSlice/courseLaunchSlice';
import { getGlobalData } from '../../../redux/Slice/coursePlayerScormSlice/gdSlice';
import { MANAGE_COURSES_TABLE_CONFIG } from '../../../utils/Constants/tableConfig';


const ManageCourses: React.FC = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const clientId: any = useSelector((state: RootState) => state.auth.clientId);
  const id: any = useSelector((state: RootState) => state.auth.id);
  const userTypeId: any = useSelector((state: RootState) => state.auth.userTypeId);
  const courseList: any = useSelector((state: RootState) => state.course.courses);
  const { loading } = useAppSelector((state: RootState) => state.course);
  const { coursePlayerLoading } = useAppSelector((state: RootState) => state.coursePlayer);
  const isLoading = loading || coursePlayerLoading;

  // Local states
  const [Courses, setCourses] = useState<Courses[]>([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [selectedUsers, setSelectedUsers] = useState<string[]>([]);
  const [showModal, setShowModal] = useState(false);
  const [searchKeyword, setSearchKeyword] = useState<string>('');

  // Action buttons configuration
  const actionButtonList = [
    { type: "button", className: "btn btn-primary", buttonName: "Activate", actionType: ACTION_BUTTON_ENUM?.ACTIVATE, disabled: Courses.length === 0 },
    { type: "button", className: "btn btn-secondary", buttonName: "Deactivate", actionType: ACTION_BUTTON_ENUM?.DEACTIVATE, disabled: Courses.length === 0 },
    { type: "button", className: "btn btn-danger", buttonName: "Delete", actionType: ACTION_BUTTON_ENUM?.DELETE, disabled: Courses.length === 0 }
  ];

  const [modalConfig, setModalConfig] = useState({
    title: '',
    message: '',
    primaryBtnText: '',
  });
  const [modalAlertMessageConfig, setModalAlertMessageConfig] = useState<{
    type: "success" | "error";
    message: string;
    duration?: string;
    autoClose?: boolean;
  }>({
    type: 'success',
    message: '',
    duration: '',
    autoClose: false
  });
  const [onConfirmAction, setOnConfirmAction] = useState<() => void>(() => () => { });
  const [alert, setAlert] = useState<{ type: "success" | "warning" | "error"; message: string; duration?: string; autoClose?: boolean; } | null>(null);

  /** Breadcrumb navigation items*/
  const breadcrumbItems = [
    { iconClass: 'fa-classic fa-solid fa-house', path: AdminPageRoutes.ADMIN_DASHBOARD.FULL_PATH },
    { label: 'Manage Courses' },
  ];
  const pageTitle: string = 'Manage Courses';
  document.title = pageTitle;

  const ClientId = useSelector((state: RootState) => state.auth.clientId);
  const SessionId = useSelector((state: any) => state.coursePlayer?.courseLaunch?.contentModuleSession?.sessionId);

  const courseWindowWidth = useSelector((state: RootState) => state.globalData.courseWindowWidth);
  const courseWindowHeight = useSelector((state: RootState) => state.globalData.courseWindowHeight);
  const courseLaunchSameWindow = useSelector((state: RootState) => state.globalData.courseLaunchSameWindow);
  const courseLoading = useSelector((state: RootState) => state.globalData.loading);

  let coursePopup: Window | null = null;
  const [isCoursePopupOpen, setIsCoursePopupOpen] = useState(false);


  useEffect(() => {
    if (ClientId && SessionId) {
      dispatch(getGlobalData({ ClientId, SessionId }));
    }
  }, []);
  useEffect(() => {
    if (!courseLoading) {
      // console.log("courseWindowWidth:", courseWindowWidth);
      // console.log("courseWindowHeight:", courseWindowHeight);
      // console.log("courseLaunchSameWindow:", courseLaunchSameWindow);
    }
  }, [courseLoading]);


  // useEffect(() => {
  //   if (!id || !clientId) return;
  //   const params = {
  //     id: id,
  //     clientId: clientId,
  //     listRange: {
  //       pageIndex: 1,
  //       pageSize: pageSize,
  //       sortExpression: 'LastModifiedDate desc',
  //       // requestedById: "USR7cbc556be7f64ac8",
  //     },
  //     keyWord: ""
  //   } as const;

  //   dispatch(fetchCourses(params));
  // }, [id, clientId]);

  // useEffect(() => {
  //   if (!courseList?.contentModuleList?.length) {
  //     setCourses([])
  //     return
  //   };
  //   setCourses(courseList?.contentModuleList)
  // }, [courseList])


  /** Fetch the initial course list when the component mounts. */
  useEffect(() => {
    if (!id || !clientId) return;
    fetchCourseList(1, pageSize);
  }, [id, clientId]);

  /** Update the local course state when userList changes. */
  useEffect(() => {
    setCourses(courseList?.contentModuleList || []);
  }, [courseList]);

  /** Fetch user list with pagination and filters. */
  const fetchCourseList = (pageIndex: number, pageSize: number) => {
    const params = {
      id, clientId,
      listRange: { pageIndex, pageSize, sortExpression: 'LastModifiedDate desc' },
      keyWord: ""
    };
    dispatch(fetchCourses(params));
  };


  // Lunch course window closed - page reload

  //   useEffect(() => {
  //   window.addEventListener('beforeunload', () => {
  //     if (window.opener) {
  //       window.opener.postMessage({ type: 'WINDOW_CLOSED' }, '*');
  //     }
  //   });
  // }, []);

  // useEffect(() => {
  //   const handleMessage = (event: MessageEvent) => {
  //     if (event.data?.type === 'WINDOW_CLOSED') {
  //       console.log('WINDOW_CLOSED received from child window');
  //       setSelectedUsers([]);
  //       setCurrentPage(1);
  //       refreshCourseList(1);
  //     }
  //   };

  //   window.addEventListener('message', handleMessage);

  //   return () => {
  //     window.removeEventListener('message', handleMessage);
  //   };
  // }, []);



  /** Handle individual user selection via checkbox.*/
  const handleCheckboxChange = (userId: string) => {
    setSelectedUsers((prevSelected) => {
      if (prevSelected.includes(userId)) {
        return prevSelected.filter((id) => id !== userId); // Deselect if already selected
      }
      return [...prevSelected, userId]; // Select if not already selected
    });
  };

  /** Handle selecting/deselecting all courses. */
  const handleSelectAll = (selectAll: boolean) => {
    if (selectAll) {
      const allUserIds = Courses.map((user) => user.id);
      setSelectedUsers(allUserIds);
    } else {
      setSelectedUsers([]);
    }
  };

  /** 
   * Activate selected courses.
    */
  const onActivate = (usersToActivate: IActivateDeactivatePayload[]) => {
    dispatch(activateDeactivateCourse(usersToActivate)).then(() => {
      setSelectedUsers([]);
      setCurrentPage(1);
      refreshCourseList(1);
      setModalAlertMessageConfig({
        type: 'success',
        message: 'Course activated successfully!'
      });
    }).catch((error) => {
      console.error("Error activating Courses:", error);
      setModalAlertMessageConfig({
        type: 'error',
        message: "Error activating Courses: " + error
      });
    });
  };
  /** Handle activate button click.  */
  const handleActivate = () => {
    if (selectedUsers.length <= 0) {
      setAlert({ type: "warning", message: "Please select a record." });
      return;
    }

    const selectedUsersWithInactiveStatus = Courses.filter(user => selectedUsers.includes(user.id) && !user.isActive);
    // If any selected user is already active, show an error message
    if (selectedUsers.length !== selectedUsersWithInactiveStatus.length) {
      setAlert({ type: "warning", message: "Please select only inactive record(s)." });
      return;
    }
    const confirmActivate = () => {
      const usersToActivate: IActivateDeactivatePayload[] = Courses
        .filter((user) => selectedUsers.includes(user.id))
        .map((user) => ({
          id: user.id,
          clientId: clientId,
          lastModifiedById: id,
          isActive: true,
        }));

      onActivate(usersToActivate);
    };

    setModalConfig({
      title: 'Activate Course Confirmation',
      message: 'Are you sure you want to activate the selected courses?',
      primaryBtnText: 'Activate',
    });
    setOnConfirmAction(() => confirmActivate);
    setShowModal(true);
  };

  /** 
   * Deactivate selected courses. 
    */
  const onDeactivate = (usersToDeactivate: IActivateDeactivatePayload[]) => {
    dispatch(activateDeactivateCourse(usersToDeactivate)).then(() => {
      setSelectedUsers([]);
      setCurrentPage(1);
      refreshCourseList(1);
      setModalAlertMessageConfig({
        type: 'success',
        message: 'Course deactivated successfully!'
      });
    }).catch((error) => {
      console.error("Error deactivating Courses:", error);
      setModalAlertMessageConfig({
        type: 'error',
        message: "Error deactivating Courses: " + error
      });
    });
  };

  /** Handle Deactivate button click.  */
  const handleDeactivate = () => {
    if (selectedUsers.length <= 0) {
      setAlert({ type: "warning", message: "Please select a record." });
      return;
    }

    const selectedUsersWithActiveStatus = Courses.filter(user => selectedUsers.includes(user.id) && user.isActive);
    // If any selected user is already inactive, show an error message
    if (selectedUsers.length !== selectedUsersWithActiveStatus.length) {
      setAlert({ type: "warning", message: "Please select only active record(s)." });
      return;
    }

    const confirmDeactivate = () => {
      const usersToDeactivate: IActivateDeactivatePayload[] = Courses
        .filter((user) => selectedUsers.includes(user.id))
        .map((user) => ({
          id: user.id,
          clientId: clientId,
          lastModifiedById: id,
          isActive: false,
        }));
      onDeactivate(usersToDeactivate);
    };

    setModalConfig({
      title: 'Deactivate Course Confirmation',
      message: 'Are you sure you want to deactivate the selected Courses?',
      primaryBtnText: 'Deactivate',
    });
    setOnConfirmAction(() => confirmDeactivate);
    setShowModal(true);
  };

  /** 
  Delete selected courses.
 */
  const onDelete = (courseToDelete: IDeletePayload[]) => {
    dispatch(deleteCourses(courseToDelete)).unwrap()
      .then((response) => {
        // If the response has a message, log or alert it
        if (response?.msg) {
          setModalAlertMessageConfig({
            type: 'success',
            message: response.msg
          });
        }
        setSelectedUsers([]);
        setCurrentPage(1);
        refreshCourseList(1);
      }).catch((error) => {
        console.error("Error deleting courses:", error);
        setModalAlertMessageConfig({
          type: 'error',
          message: error?.msg || "Failed to delete courses"
        });
      });
  };

  /** Handle Delete button click.  */
  const handleDelete = () => {
    if (selectedUsers.length <= 0) {
      setAlert({ type: "warning", message: "Please select a record.", autoClose: true });
      return;
    }
    const confirmDelete = () => {
      const courseToDelete: IDeletePayload[] = Courses
        .filter((user) => selectedUsers.includes(user.id))
        .map((user) => ({
          id: user.id,
          clientId: clientId,
          lastModifiedById: id
        }));

      onDelete(courseToDelete);
    };
    setModalConfig({
      title: 'Delete Course Confirmation',
      message: 'Are you sure you want to delete the selected courses?',
      primaryBtnText: 'Delete',
    });
    setOnConfirmAction(() => confirmDelete);
    setShowModal(true);
  };

  /** Handle user action button clicks (Activate/Deactivate). */
  const handleActionButtonClick = (actionType: string) => {
    if (actionType === ACTION_BUTTON_ENUM?.ACTIVATE) {
      handleActivate()
    } else if (actionType === ACTION_BUTTON_ENUM?.DEACTIVATE) {
      handleDeactivate()
    } else if (actionType === ACTION_BUTTON_ENUM?.DELETE) {
      handleDelete()
    }
  }

  /** Handle user search. */
  // const handleSearch = (keyword: string) => {
  //   const trimmedKeyword = keyword.trim();

  //   setSearchKeyword(trimmedKeyword);
  //   // Always reset current page to 1 before calling fetch
  //   const updatedPage = 1;
  //   setCurrentPage(updatedPage);

  //   const params = {
  //     id, clientId,
  //     listRange: { pageIndex: updatedPage, pageSize, sortExpression: 'LastModifiedDate desc' },
  //     keyWord: trimmedKeyword,
  //   };
  //   dispatch(fetchCourses(params));

  // };

  const debouncedSearch = useMemo(
    () =>
      debounce((keyword: string) => {
        const trimmedKeyword = keyword.trim();
        setSearchKeyword(trimmedKeyword);

        // Always reset current page to 1 before calling fetch
        const updatedPage = 1;
        setCurrentPage(updatedPage);

        const params = {
          id,
          clientId,
          listRange: {
            pageIndex: updatedPage,
            pageSize,
            sortExpression: "LastModifiedDate desc",
          },
          keyWord: trimmedKeyword,
        };

        dispatch(fetchCourses(params));
      }, 500), // 500ms debounce
    [dispatch, id, clientId, pageSize]
  );

  //  Use this in input onChange or search call
  const handleSearch = useCallback(
    (keyword: string) => {
      debouncedSearch(keyword);
    },
    [debouncedSearch]
  );

  //  Cleanup debounce on unmount
  useEffect(() => {
    return () => {
      debouncedSearch.cancel();
    };
  }, [debouncedSearch]);

  /**Navigate to Add User page.   */
  const handleAddCourse = () => {
    navigate(AdminPageRoutes.ADMIN_CONTENT_ADD_COURSE.FULL_PATH);
  };

  /** Handle pagination changes. */
  const handlePageChange = (page: number, newPageSize?: number) => {
    if (newPageSize) setPageSize(newPageSize); // Update page size if provided
    setCurrentPage(page); // Update the current page state
    setSelectedUsers([]);//  Clear selected users when changing page
    const params = {
      id: id,
      clientId: clientId,
      listRange: {
        pageIndex: page,
        pageSize: newPageSize || pageSize,
        sortExpression: 'LastModifiedDate desc',
        requestedById: id,
      },
      // keyWord: "",
      keyWord: searchKeyword,
    };
    dispatch(fetchCourses(params));
  };

  /** Refresh course list. */
  const refreshCourseList = (overridePageIndex?: number) => {
    const params = {
      id, clientId,
      listRange: {
        pageIndex: overridePageIndex ?? currentPage,  // Use overridePageIndex if provided
        pageSize,
        sortExpression: 'LastModifiedDate desc',
        // requestedById: "USR7cbc556be7f64ac8" 
      },
      keyWord: "",
    };
    dispatch(fetchCourses(params));
  };




  const handleCourseLaunch = async (courseGroupId: string) => {
    if (!courseGroupId || !courseList?.contentModuleList?.length) {
      console.error("Invalid courseGroupId or no courses found");
      return;
    }

    const selectedModule = courseList.contentModuleList.find((course: any) => course.courseGroupId === courseGroupId);

    if (!selectedModule) {
      console.error(`Course module not found for ID: ${courseGroupId}`);
      return;
    }

    const { courseLaunchSameWindow } = selectedModule;

    const params = {
      clientId,
      contentModuleId: courseGroupId,
      systemUserGuid: id,
      attempt: 1,
      launchSite: 1,
      isReview: false,
      ssoLogin: false,
      sameWindow: courseLaunchSameWindow,
      returnUrl: AdminPageRoutes.ADMIN_CONTENT_MANAGE_COURSES.FULL_PATH,
      gridPageSize: pageSize
    };

    const paramsCoursePlayer = {
      clientId,
      contentModuleId: courseGroupId,
      SystemUserGUID: id,
      LaunchType: userTypeId
    };

    const launchUrl = '/LaunchCourse';
    const launchFeatures = `width=${courseWindowWidth},height=${courseWindowHeight},resizable=yes,scrollbars=yes`;

    try {
      // Dispatch course session
      const result = await dispatch(
        contentModuleSession({
          sessionDataPayload: params,
          courseWindowWidth,
          courseWindowHeight,
          courseLaunchSameWindow,
        })
      );

      // Dispatch assignment for launch
      await dispatch(getAssignmentForLaunch(paramsCoursePlayer));

      if (courseLaunchSameWindow) {
        window.location.href = launchUrl; // Open in same tab
      } else {
        const popup = window.open(launchUrl, '_blank', launchFeatures);

        if (popup) {
          setIsCoursePopupOpen(true);
          const popupMonitor = setInterval(() => {
            if (popup.closed) {
              clearInterval(popupMonitor);
              console.log('Popup closed, reloading parent...');
              window.location.reload(); // Refresh or update state
            }
          }, 200);
        } else {
          console.error('Failed to open popup window');
        }
      }
    } catch (error) {
      console.error('Unexpected error in handleCourseLaunch:', error);
    }
  };

  // const handleCourseLaunch = async (courseGroupId: string) => {
  //   if (!courseGroupId || !courseList?.contentModuleList?.length) {
  //     console.error("Invalid courseGroupId or no courses found");
  //     return;
  //   }
  //   const selectedModule = courseList.contentModuleList.find((course: any) => course.courseGroupId === courseGroupId);

  //   if (!selectedModule) {
  //     console.error(`Course module not found for ID: ${courseGroupId}`);
  //     return;
  //   }

  //   const { courseLaunchSameWindow } = selectedModule;

  //   const params = {
  //     clientId,
  //     contentModuleId: courseGroupId,
  //     systemUserGuid: id,
  //     attempt: 1,
  //     launchSite: 1,
  //     isReview: false,
  //     ssoLogin: false,
  //     sameWindow: courseLaunchSameWindow,
  //     returnUrl: AdminPageRoutes.ADMIN_CONTENT_MANAGE_COURSES.FULL_PATH,
  //     gridPageSize: pageSize
  //   };

  //   const paramsCoursePlayer = {
  //     clientId,
  //     contentModuleId: courseGroupId,
  //     SystemUserGUID: id,
  //     LaunchType: userTypeId
  //   }

  //   // console.log("Launching with params:", paramsCoursePlayer);

  //   try {
  //     // await dispatch(contentModuleSession(params));
  //     await dispatch(contentModuleSession({
  //       sessionDataPayload: params,
  //       courseWindowWidth, courseWindowHeight, courseLaunchSameWindow
  //     }));

  //     await dispatch(getAssignmentForLaunch(paramsCoursePlayer));

  //     // if (contentModuleSession.fulfilled.match(resultAction)) {
  //     //   const launchUrl = '/LaunchCourse';
  //     //   const launchFeatures = "width=1200,height=800,top=100,resizable=yes,scrollbars=yes";
  //     //   courseLaunchSameWindow ? window.location.href = launchUrl : window.open(launchUrl, '_blank', launchFeatures);
  //     // } else {
  //     //   console.error("API call failed:", resultAction);
  //     // }


  //   } catch (error) {
  //     console.error("Unexpected error in handleCourseLunch:", error);
  //   }
  // };

  return (
    <>
      {/* Loading spinner overlay */}
      {isLoading && <div className="yp-loading-overlay"><div className="yp-spinner"><div className="spinner-border"></div></div></div>}

      <div className="yp-page-title-button-section" id="yp-page-title-breadcrumb-section">
        <div className="yp-page-title-breadcrumb">
          <div className="yp-page-title">{pageTitle}</div>
          <CustomBreadcrumb items={breadcrumbItems} />
        </div>
        <div className="yp-page-button">
          <button className="btn btn-primary" onClick={handleAddCourse}>
            <i className="fa fa-plus"></i>
            Add Course
          </button>
        </div>
      </div>

      {/* Common list here */}

      <div className="yp-card p-0 pb-3" id="yp-card-main-content-section">
        <div className="yp-custom-table-section">
          <CommonTable
            type={COMMON_TABLE_TYPE.MANAGE_COURSES}
             tableConfig={MANAGE_COURSES_TABLE_CONFIG}
            // filterComponent={
            //   <ManageUserFilter 
            //   onSubmit={handleFilterSubmit} 
            //   onClear={handleClearFilters} /> }
            // onFilterSubmit={handleFilterSubmit}
            data={Courses}
            onSearch={handleSearch}
            onPageChange={handlePageChange}
            currentPage={currentPage}
            totalRecords={courseList?.totalRows}
            pageSize={pageSize}
            selectedUsers={selectedUsers}
            handleCheckboxChange={handleCheckboxChange}
            handleSelectAll={handleSelectAll}
            actionButtonList={actionButtonList}
            actionButtonClick={handleActionButtonClick}
            onCourseLaunch={handleCourseLaunch}
          />
        </div>
      </div>

      {alert && <AlertMessage type={alert.type} message={alert.message} duration={alert.duration} autoClose={alert.autoClose} onClose={() => setAlert(null)} />}

      {/* Confirmation Modal */}
      <ConfirmationModal
        show={showModal}
        onConfirm={onConfirmAction}
        onCancel={() => {
          setShowModal(false);
          setModalAlertMessageConfig({ type: 'success', message: '', duration: '4000', autoClose: true });
        }
        }
        modalConfig={modalConfig}
        modalAlertMessageConfig={modalAlertMessageConfig}
      />
      {isCoursePopupOpen && (
        <div className="modal-backdrop fade show" />
      )}

    </>
  );
};

export default ManageCourses;
