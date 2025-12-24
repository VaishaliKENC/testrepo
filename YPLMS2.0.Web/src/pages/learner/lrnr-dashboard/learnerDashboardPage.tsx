import React, { useEffect, useState } from "react";
import {
  AssignmentCard,
  LearnerDashboardCarousel,
  LearnerLeaderBoard,
  StatisticsCards,
} from "../../../components/shared";
import {
  AssignedCirriculumsIcon,
  CompletedCourseIcon,
  AssignedCourseIcon,
} from "../../../assets/icons";
import { useSelector } from "react-redux";
import { RootState } from "../../../redux/store";
import {
  getDashboardCompletedAssignment,
  getDashboardCurrentAssignment,
  getDashboardData,
} from "../../../redux/Slice/learner/dashboard/dashboard.requests";
import { useAppDispatch, useAppSelector } from "../../../hooks";
import {
  checkDueDate,
  checkExpiry,
  handleAssetPreviewTracking,
  validateInput,
} from "../../../utils/commonUtils";
import CardFallback from "../../../assets/images/card_fallback.png";
import CustomLoader from "../../../components/shared/CommonComponents/CustomLoader";
import {
  contentModuleSession,
  getAssignmentForLaunch,
} from "../../../redux/Slice/coursePlayerScormSlice/courseLaunchSlice";
import { fetchModuleSettings } from "../../../api/learner/getModuleSettings.api";
import { LearnerPageRoutes } from "../../../utils/Constants/Learner_PageRoutes";
import { Link } from "react-router-dom";
import { setAssetTracking } from "../../../redux/Slice/tracking/assetTracking.requests";
import { fetchSingleAssetDataApi } from "../../../api/admin/assetLibraryApi";
import { IMainPayload } from "../../../Types/commonTableTypes";
import { ACTIVITY_TYPES } from "../../../utils/Constants/Enums";

const LearnerDashboardPage: React.FC = () => {
  document.title = 'Dashboard';
  const clientId: any = useSelector((state: RootState) => state.auth.clientId);
  const id: any = useSelector((state: RootState) => state.auth.id);
  const userId: any = useAppSelector((state: RootState) => state.auth.id);
  const dispatch = useAppDispatch();
  const statsData = useAppSelector(
    (state: RootState) => state.learnerDashboard
  );
  const currentAssgn = useAppSelector(
    (state: RootState) => state.learnerDashboard.currentAssignmentCards ?? []
  );
  const completedAssgn = useAppSelector(
    (state: RootState) => state.learnerDashboard.completedAssignmentCards ?? []
  );
  const userTypeId: any = useAppSelector(
    (state: RootState) => state.auth.userTypeId
  );

  const userInfo = useAppSelector((state: RootState) => state.auth);
  const contentServerURL: any = useAppSelector(
    (state: RootState) => state.auth.serverUrl
  );
  const [isCoursePopupOpen, setIsCoursePopupOpen] = useState(false);
  const statsCardsData = [
    {
      label: "Assigned Curriculums",
      count: statsData.assignedCirriculumns,
      icon: <AssignedCirriculumsIcon />,
      bgClass: "yp-bg-color-E5E8F8",
      iconBgClass: "yp-bg-color-7CBDCC",
    },
    {
      label: "Assigned Courses",
      count: statsData.totalCourseAssigned,
      icon: <AssignedCourseIcon />,
      bgClass: "yp-bg-color-FBECD6",
      iconBgClass: "yp-bg-color-F7C842",
    },
    {
      label: "Completed Courses",
      count: statsData.totalCourseCompleted,
      icon: <CompletedCourseIcon />,
      bgClass: "yp-bg-color-E7F6D2",
      iconBgClass: "yp-bg-color-A4C475",
    },
  ];

  const currentAssignmentData = currentAssgn?.map(
    (card: Record<string, any>) => {
      return {
        id: card.ID,
        imageSrc: card?.ThumbnailImgRelativePath
          ? process.env.REACT_APP_CONTENT_SERVER_URL +
          card.ThumbnailImgRelativePath
          : CardFallback,
        title: card?.ActivityName,
        courseType: card.ActivityType,
        assignedDate: validateInput(card.AssignmentDateSet),
        dueDate: validateInput(card.DueDateSet),
        isDueDate: checkDueDate(validateInput(card.DueDateSet)),
        expiringSoon: checkExpiry(validateInput(card.DueDateSet)),
      };
    }
  );
  const completedAssignData = completedAssgn?.map(
    (card: Record<string, any>) => {
      return {
        imageSrc: card?.ThumbnailImgRelativePath
          ? process.env.REACT_APP_CONTENT_SERVER_URL +
          card.ThumbnailImgRelativePath
          : CardFallback,
        title: card?.ActivityName,
        courseType: card.ActivityType,
        assignedDate: validateInput(card.AssignmentDateSet),
        completionDate: validateInput(card.DateOfCompletion),
      };
    }
  );
  useEffect(() => {
    dispatch(getDashboardData({ clientId: clientId, userId: id }));
    dispatch(getDashboardCurrentAssignment({ clientId: clientId, userId: id }));
    dispatch(
      getDashboardCompletedAssignment({ clientId: clientId, userId: id })
    );
  }, [dispatch, clientId, id]);

  useEffect(() => {
    const handleMessage = (event: MessageEvent) => {
      if (event.data === "assetPreviewClosed") {
        window.location.reload(); // Reload parent when popup sends message
      }
    };

    window.addEventListener("message", handleMessage);
    return () => window.removeEventListener("message", handleMessage);
  }, []);

  const handleCourseLaunch = async (data: Record<string, any>) => {
    try {
      const response = await fetchModuleSettings({
        moduleId: data.id,
        clientId,
      });
      if (response.status === 200 || response.data.contentModule) {
        await dispatch(
          contentModuleSession({
            sessionDataPayload: {
              clientId,
              contentModuleId: data.id,
              systemUserGuid: id,
              attempt: 1,
              launchSite: 0,
              isReview: false,
              ssoLogin: false,
              sameWindow: response?.data?.contentModule?.courseLaunchSameWindow,
              returnUrl: LearnerPageRoutes.LEARNER_DASHBOARD.FULL_PATH,
              gridPageSize: 5,
            },
            courseWindowWidth: response?.data?.contentModule?.courseWindowWidth,
            courseWindowHeight:
              response?.data?.contentModule?.courseWindowHeight,
            courseLaunchSameWindow:
              response?.data?.contentModule?.courseLaunchSameWindow,
          })
        );
        await dispatch(
          getAssignmentForLaunch({
            clientId,
            contentModuleId: data.id,
            SystemUserGUID: id,
            LaunchType: userTypeId,
          })
        );
        //LOGIC FOR SHOWING OVERLAY AND REFRESH ON COURSE WINDOW CLOSE
        const launchUrl = "/LaunchCourse";
        const launchFeatures = `width=${response?.data?.contentModule?.courseWindowWidth},height=${response?.data?.contentModule?.courseWindowHeight},resizable=yes,scrollbars=yes`;
        if (response?.data?.contentModule?.courseLaunchSameWindow) {
          window.location.href = launchUrl; // Open in same tab
        } else {
          const popup = window.open(launchUrl, "_blank", launchFeatures);

          if (popup) {
            setIsCoursePopupOpen(true);
            const popupMonitor = setInterval(() => {
              if (popup.closed) {
                clearInterval(popupMonitor);
                console.log("Popup closed, reloading parent...");
                window.location.reload(); // Refresh or update state
              }
            }, 200);
          } else {
            console.error("Failed to open popup window");
          }
        }
      }
    } catch (error) {
      console.error("Error fetching module settings:", error);
    }
  };
  const handleAssetPreview = async (card: Record<string, any>) => {
    const assetSinglePayload: IMainPayload = {
      clientId: clientId,
      id: card?.id,
    };
    try {
      const response = await fetchSingleAssetDataApi(assetSinglePayload);
      if (response.code === 200 || response.asset) {
        const assetData = response.asset;
        const isMp4 = assetData?.assetFileName?.split(".")?.pop();
        // if (isMp4 !== "mp4") {   //mp4 tracking will be handled in previewFile component

        const trackingData = await dispatch(
          setAssetTracking({
            clientId,
            currentUserID: userId,
            activityId: card?.id,
            activityType: ACTIVITY_TYPES.ASSET,
            isForAdminPreview: false,
            progress: 100,
          })
        ).unwrap?.();

        const watchedInMins = trackingData?.asset?.trackingData?.watchedInMins ?? 0;

        const payload = {
          contentServerURL,
          isDownload: assetData.isDownload,
          assetName: assetData.assetName,
          assetFileName: assetData.assetFileName, //used in showing name of audio/video file in preview
          assetFileType: assetData.assetFileType, //type of file audio/video/video/image etc
          relativePath: assetData.relativePath,
          isMp4: isMp4 === "mp4" ? "true" : "false",
          id: assetData.id,
          clientId: clientId,
          userId: userId,
          watchedInMins
        };
        handleAssetPreviewTracking(payload);
      }
    } catch (error) {
      console.log("error", error);
    }
  };

  const handleAssignments = (card: Record<string, any>) => {
    if (card?.courseType === ACTIVITY_TYPES.SCROM12) handleCourseLaunch(card);
    else if (card?.courseType === ACTIVITY_TYPES.ASSET) {
      handleAssetPreview(card);
    }
  };
  return (
    <>
      {statsData.loading && <CustomLoader />}
      <div
        className="yp-page-title-button-section mb-2"
        id="yp-page-title-breadcrumb-section"
      >
        <div className="yp-page-title-breadcrumb">
          <div className="yp-page-title">Dashboard</div>
        </div>
        <div className="yp-page-button">
          <div className="yp-learner-welcome-badge">
            {/* <i className="fa-solid fa-handshake"></i> */}
            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="14" viewBox="0 0 20 14" fill="none">
              <path d="M15.59 7.73766L11.1642 4.84491L10.2497 6.64136C9.60135 7.91491 8.13339 8.33637 6.97744 7.58083C5.82149 6.82529 5.40848 5.17441 6.0568 3.90086L8.04256 0L5.51598 0.0159702C4.76622 0.0175808 4.08133 0.463472 3.71553 1.18205L2.94311 2.69941L1.05444 2.65898C0.465241 2.6501 -0.0091542 3.17799 0.000134103 3.83819L0.06612 9.8069C0.0724966 10.4652 0.558275 11.005 1.14747 11.0139L3.93794 11.0666L7.91534 13.6662C8.94318 14.3381 10.2475 13.9636 10.824 12.8312L11.2899 13.1357C11.8052 13.4725 12.4551 13.2859 12.7442 12.7181L13.7893 10.665L14.0223 10.8173C14.4095 11.0704 14.8958 10.9308 15.113 10.5041L15.8969 8.96432C16.114 8.53766 15.9772 7.99077 15.59 7.73766Z" fill="#6C5C7D"/>
              <path d="M19.9999 9.78621L19.9312 3.6427C19.9246 2.96513 19.4189 2.4095 18.8056 2.40036L15.9009 2.34613L12.8033 0.344174C12.4396 0.109111 12.0272 -0.00781079 11.6109 0.000404777L10.2403 0.0342832C9.8568 0.0416602 9.51198 0.272059 9.32499 0.635267L7.17803 4.80556C6.80235 5.53527 7.04061 6.47695 7.71044 6.90986C8.38027 7.34277 9.22709 7.10236 9.60276 6.37264L11.0987 3.46698L16.6756 7.07128C17.6121 7.67657 17.9462 8.99702 17.4209 10.0173L16.9365 10.9583L18.9025 11C19.5175 11.0058 20.0066 10.4638 19.9999 9.78621Z" fill="#8F6CB3"/>
            </svg>

            {`Welcome to Encora, ${userInfo.firstName
              ?.charAt(0)
              ?.toUpperCase()
              ?.concat(userInfo.firstName.slice(1))}
           ${userInfo.lastName
                ?.charAt(0)
                ?.toUpperCase()
                ?.concat(userInfo.lastName?.slice(1))}`}
          </div>
        </div>
      </div>

      <LearnerDashboardCarousel />

      <div className="yp-card" id="yp-card-main-content-section">
        <div className="yp-learner-dashboard-stat-boxes mb-5">
          <div className="yp-learner-dashboard-stat-boxes-wrapper">
            {statsCardsData?.map((cards) => (
              <StatisticsCards key={cards.label} data={cards} />
            ))}
          </div>

       < LearnerLeaderBoard/>
        </div>

        <div className="mb-0">
          <ul
            className="nav nav-tabs yp-learner-tabs yp-learner-tabs-with-btn-view-all"
            id="myTab"
            role="tablist"
          >
            <li className="nav-item" role="presentation">
              <button
                type="button"
                className="nav-link nav-link-with-count active"
                data-bs-toggle="tab"
                data-bs-target="#currentAssignmentsTab"
              >
                Current Assignments
                <span className="nav-link-count">
                  {statsData.assignedCourses}
                </span>
              </button>
            </li>
            <li className="nav-item" role="presentation">
              <button
                type="button"
                className="nav-link nav-link-with-count"
                data-bs-toggle="tab"
                data-bs-target="#completedAssignmentsTab"
              >
                Completed Assignments
                <span className="nav-link-count">
                  {statsData.completedCourses}
                </span>
              </button>
            </li>
          </ul>
          <div className="tab-content">
            {/* 1st TAB */}
            <div
              className={`tab-pane fade show active`}
              id="currentAssignmentsTab"
              role="tabpanel"
            >
              <div className="yp-learner-assgn-boxes">
                <div className="row row-gap-4">
                  {currentAssignmentData?.slice(0, 6)?.map((item, index) => (
                    <AssignmentCard
                      key={item.title}
                      data={item}
                      id={index}
                      handleAssignments={() => handleAssignments(item)}
                    />
                  ))}
                </div>
              </div>
              <div className="yp-tab-content-btn-view-all">
                <Link
                  to={`${LearnerPageRoutes.CURRENT_ASSIGNMENT.FULL_PATH}`}
                  className="btn btn-sm btn-primary"
                >
                  {" "}
                  View All
                </Link>
              </div>
            </div>
            {/* 2nd TAB */}
            <div
              className={`tab-pane fade`}
              id="completedAssignmentsTab"
              role="tabpanel"
            >
              <div className="yp-learner-assgn-boxes">
                <div className="row row-gap-4">
                  {completedAssignData?.slice(0, 6)?.map((item, index) => (
                    <AssignmentCard key={item.title} data={item} id={index} />
                  ))}
                </div>
              </div>
              <div className="yp-tab-content-btn-view-all">
                <Link
                  to={`${LearnerPageRoutes.COMPLETED_ASSIGNMENT.FULL_PATH}`}
                  className="btn btn-sm btn-primary"
                >
                  {" "}
                  View All
                </Link>
              </div>
            </div>
          </div>
        </div>
      </div>
      {isCoursePopupOpen && <div className="modal-backdrop fade show"></div>}
    </>
  );
};

export default LearnerDashboardPage;
