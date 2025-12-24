import React, { ChangeEvent, useEffect, useMemo, useState } from "react";
import { RootState } from "../../redux/store";
import { useAppDispatch, useAppSelector } from "../../hooks";
import { getCurrentAssignment } from "../../redux/Slice/learner/currentAssignment/currentAssignment.requests";
import CardFallback from "../../assets/images/card_fallback.png";
import {
  checkDueDate,
  checkExpiry,
  handleAssetPreviewTracking,
  handleDebounce,
  validateInput,
} from "../../utils/commonUtils";
import { AssignmentCard } from "../../components/shared";
import CustomPagination from "../../components/shared/CommonComponents/CustomPagination";
import CustomLoader from "../../components/shared/CommonComponents/CustomLoader";
import {
  contentModuleSession,
  getAssignmentForLaunch,
} from "../../redux/Slice/coursePlayerScormSlice/courseLaunchSlice";
import { fetchModuleSettings } from "../../api/learner/getModuleSettings.api";
import CustomBreadcrumb from "../../components/shared/CustomBreadcrumb";
import { LearnerPageRoutes } from "../../utils/Constants/Learner_PageRoutes";
import { IMainPayload } from "../../Types/commonTableTypes";
import { fetchSingleAssetDataApi } from "../../api/admin/assetLibraryApi";
import { setAssetTracking } from "../../redux/Slice/tracking/assetTracking.requests";
import { ACTIVITY_TYPES } from "../../utils/Constants/Enums";
import { getDashboardData } from "../../redux/Slice/learner/dashboard/dashboard.requests";

const CurrentAssignmentPage: React.FC = () => {
  document.title = 'Current Assignment';
  const dispatch = useAppDispatch();
  const pageSize = 9;
  const [currentPage, setCurrentPage] = useState(1);
  const [searchTerm, setSearchTerm] = useState<string>("");
  const clientId: any = useAppSelector(
    (state: RootState) => state.auth.clientId
  );
  const userId: any = useAppSelector((state: RootState) => state.auth.id);
  const userTypeId: any = useAppSelector(
    (state: RootState) => state.auth.userTypeId
  );
  const contentServerURL: any = useAppSelector(
    (state: RootState) => state.auth.serverUrl
  );

  const { currentAssignment: assignData, loading: assignLoader } =
    useAppSelector((state: RootState) => state.currentAssignment);

  const totalRecords = useAppSelector(
    (state: RootState) => state.learnerDashboard.assignedCourses
  );
  // const totalRecords = useAppSelector(
  //   (state: RootState) => state.learnerDashboard.assignedCourses
  // );
  const [isCoursePopupOpen, setIsCoursePopupOpen] = useState(false);
  const breadcrumbItems = [
    {
      iconClass: "fa-classic fa-solid fa-house",
      path: LearnerPageRoutes.LEARNER_DASHBOARD.FULL_PATH,
    },
    { label: "Current Assignments" },
  ];
  useEffect(() => {
    dispatch(getDashboardData({ clientId: clientId, userId: userId }));    //get total assigned courses number for pagination
  }, []);

  useEffect(() => {
    dispatch(
      getCurrentAssignment({
        pageIndex: currentPage,
        pageSize,
        sortExpression: null,
        clientId: clientId,
        userId: userId,
      })
    );
  }, [dispatch, clientId, userId, currentPage]);
  const cardData = assignData?.map((card: Record<string, any>) => {
    return {
      id: card.ID,
      imageSrc: card?.ThumbnailImgRelativePath
        ? process.env.REACT_APP_CONTENT_SERVER_URL +
        card.ThumbnailImgRelativePath
        : CardFallback,
      title: card?.ActivityName,
      courseType: card.ActivityType,
      assignedDate: validateInput(card.AssignmentDateSet),
      showProgress: true,
      progressBar: Math.round(card?.Progress ?? 0),
      description: card?.ActivityDescription,
      activityStatus: card?.ActivityStatus,
      dueDate: validateInput(card.DueDateSet),
      isDueDate: checkDueDate(validateInput(card.DueDateSet)),
      expiryDate: validateInput(card.ExpiryDateSet),
      expiringSoon: checkExpiry(validateInput(card.DueDateSet)),
    };
  });

  useEffect(() => {
    const handleMessage = (event: MessageEvent) => {
      if (event.data === "assetPreviewClosed") {
        window.location.reload(); // Reload parent when popup sends message
      }
    };

    window.addEventListener("message", handleMessage);
    return () => window.removeEventListener("message", handleMessage);
  }, []);


  const handleCourseLaunch = async (data: any) => {
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
              systemUserGuid: userId,
              attempt: 1,
              launchSite: 0,
              isReview: false,
              ssoLogin: false,
              sameWindow: response?.data?.contentModule?.courseLaunchSameWindow, //courseLaunchSameWindow,
              returnUrl: LearnerPageRoutes.CURRENT_ASSIGNMENT.FULL_PATH,
              gridPageSize: 9,
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
            SystemUserGUID: userId,
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

  const onPageChange = (page: number, pagesize: number) => {
    setCurrentPage(page);
  };

  const handlePageClick = (page: number) => {
    if (page !== currentPage) {
      onPageChange(page, pageSize);
    }
  };

  const handleSearch = (event: ChangeEvent<HTMLInputElement>) => {
    setSearchTerm(event?.target?.value);
    debouncedSearch(event?.target?.value);
  };
  const searchAssignment = (value: string) => {
    dispatch(
      getCurrentAssignment({
        pageIndex: currentPage,
        pageSize,
        sortExpression: null,
        clientId: clientId,
        userId: userId,
        keyWord: value,
      })
    );
  };

  const debouncedSearch = useMemo(
    () => handleDebounce(searchAssignment, 1000),
    []
  );
  const handleAssetPreview = async (card: Record<string, any>) => {
    const assetSinglePayload: IMainPayload = {
      clientId: clientId,
      id: card?.id,
    };
    try {
      const response = await fetchSingleAssetDataApi(assetSinglePayload);
      if (response.code === 200 || response?.asset) {
        const assetData = response?.asset;
        const isMp4 = assetData?.assetFileName?.split(".")?.pop();
        // if (isMp4 !== "mp4") {    //if asked that do pause etc tracking for all videos then use assetData.assetFileType!==VIDEO
        //mp4 tracking will be handled in previewFile component
        // dispatch(
        //   setAssetTracking({
        //     clientId: clientId,
        //     currentUserID: userId,
        //     activityId: card?.id,
        //     activityType: ACTIVITY_TYPES.ASSET,
        //     isForAdminPreview: false,
        //     progress: 100,
        //   })
        // )

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
          assetName: assetData.assetName, //used in showing name of audio/video file in preview
          assetFileType: assetData.assetFileType, //type of file audio/video image ppt etc
          relativePath: assetData.relativePath,
          isMp4: isMp4 === "mp4" ? "true" : "false",
          id: assetData.id,
          clientId: clientId,
          userId: userId,
          watchedInMins
        };
        handleAssetPreviewTracking(payload);

        // ✅ Open popup with watchedSeconds in URL
        //   const launchUrl = `/training/currentAssignment?file=${encodeURIComponent(
        //     assetData.relativePath
        //   )}&server=${encodeURIComponent(contentServerURL)}&type=${assetData.assetFileType}&name=${
        //     assetData.assetName
        //   }&isMp4=${isMp4 === "mp4" ? "true" : "false"}&id=${assetData.id}&clientId=${clientId}&userId=${userId}&watchedSeconds=${watchedInMins}`;

        //   const popup = window.open(launchUrl);

        //   if (popup) {
        //     setIsCoursePopupOpen(true);
        //     const popupMonitor = setInterval(() => {
        //       if (popup.closed) {
        //         clearInterval(popupMonitor);
        //         console.log("Popup closed, reloading parent...");
        //         window.location.reload();
        //       }
        //     }, 200);
        //   } else {
        //     console.error("Failed to open popup window");
        //   }
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
      {assignLoader && <CustomLoader />}

      <div
        className="yp-page-title-button-section"
        id="yp-page-title-breadcrumb-section"
      >
        <div className="yp-page-title-breadcrumb">
          <div className="yp-page-title">
            Current Assignment ({cardData.length ?? 0})
          </div>
          <CustomBreadcrumb items={breadcrumbItems} />
        </div>
        <div className="yp-page-button">
          <div className="yp-width-335-px">
            <div className="yp-form-control-with-icon">
              <div className="form-group mb-0">
                <div className="yp-form-control-wrapper">
                  <input
                    type="text"
                    name="tableSearchInput"
                    //placeholder="Assignment Name..."
                    placeholder=""
                    className="form-control yp-form-control"
                    value={searchTerm}
                    onChange={(event) => handleSearch(event)}
                  />
                  <label className="form-label">Assignment Name</label>
                  <span className="yp-form-control-icon">
                    <i className="fa fa-search"></i>
                  </span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div className="yp-card" id="yp-card-main-content-section">
        <div className="yp-learner-assgn-boxes">
          <div className="row row-gap-4">
            {/*             
            {cardData.map((card, index) => (
              <AssignmentCard
                key={card.title}
                data={card}
                id={index}
                handleAssignments={() => handleAssignments(card)}
              />
            ))} */}
            {cardData && cardData.length > 0 ? (
              cardData.map((card, index) => (
                <AssignmentCard
                  key={card.title}
                  data={card}
                  id={index}
                  handleAssignments={() => handleAssignments(card)}
                />
              ))
            ) : (
              <div className="col-12 text-center py-5">
                <p className="mb-0">No assignments found.</p>
              </div>
            )}
          </div>
        </div>

        {cardData && cardData.length > 0 && (
          <CustomPagination
            totalRecords={totalRecords}
            currentPage={currentPage}
            onPageChange={onPageChange}
            pageSize={pageSize}
            handlePageClick={handlePageClick}
          />
        )}
      </div>
      {isCoursePopupOpen && <div className="modal-backdrop fade show"></div>}
    </>
  );
};

export default CurrentAssignmentPage;
