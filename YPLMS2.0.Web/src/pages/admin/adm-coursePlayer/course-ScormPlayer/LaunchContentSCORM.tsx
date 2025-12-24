// LaunchContentSCORM.tsx
import React, { useEffect, useState, useRef, useMemo } from "react";
import CoursePage from "./CoursePage";
import { GlobalData } from '../../../../Types/coursePlayerScormType/globalDataType';
import { SCORMApi } from "../course-ScormLibraries/SCORMApi";
import { CSCO } from '../course-ScormLibraries/Master';

import { useLocation, useNavigate } from "react-router-dom";

import { useAppDispatch, useAppSelector } from '../../../../hooks';
import { getGlobalData, setCoursesGlobalData } from '../../../../redux/Slice/coursePlayerScormSlice/gdSlice';
import { setUpdatedGlobalData } from '../../../../redux/Slice/coursePlayerScormSlice/updatedGlobalDataSlice';
import { RTEMaster, arrSCO } from '../course-ScormLibraries/RTEMaster';

import { ManifestNode } from "../../../../utils/coursePlayerScormArrays";
import { useSelector } from "react-redux";
import { RootState } from "../../../../redux/store";
import { AdminPageRoutes } from "../../../../utils/Constants/Admin_PageRoutes";



window.addEventListener("message", (event) => {
  if (event.data.type === "LMSInitializeResponse") {
    if (event.data.result === "true") {
      alert("LMS Initialized Successfully");
    } else {
      alert("LMS Initialization Failed");
    }
  }
});

interface Lesson {
  identifier: string;
  title: string;
  masteryScore: number;
  maxTimeAllowed: string | null;
  timeLimitAction: string;
  sortOrder: number;
  launchUrl?: string;
  scormType: string;
}

interface Section {
  identifier: string;
  title: string;
  sortOrder: number;
  lessons: Lesson[];
}


const LaunchContentSCORM: React.FC = () => {
  const initialDataObj: GlobalData = {
    gContentPath: '',
    gLearnerId: '',
    gStudentId: '',
    gManagerEmail: '',
    gStudentEmail: '',
    sessionId: '',
    clientId: '',
    gTrackScoreSettingFromLMS: '',
    gTotalNoOfPages: '',
    gNoOfCompletedPages: '',
    gLearnerName: '',
    gCourseName: '',
    gManifestId: '',
    gCourseSection: [],
  };
  const [lessonStatus, setLessonStatus] = useState<string>("");
  const [isInitialized, setIsInitialized] = useState<boolean>(false);
  const [launchPage, setLaunchPage] = useState<boolean>(true);


  // State for managing global data
  const [gDataObj, setGDataObj] = useState<GlobalData>(initialDataObj);

  const location = useLocation();
  const queryParams = new URLSearchParams(location.search);
  const source = queryParams.get("source"); // "LaunchCourse" or "LaunchCourseWindow"
  const ClientId: any =
    useSelector((state: RootState) => state.auth.clientId) || queryParams.get("ClientId");

  const SessionId: any =
    useSelector((state: any) => state.coursePlayer?.courseLaunch?.contentModuleSession?.sessionId) ||
    queryParams.get("SessionId");

  // const ClientId: any = useSelector((state: RootState) => state.auth.clientId);
  // const SessionId: any = useSelector((state: any) => state.coursePlayer?.courseLaunch?.contentModuleSession?.sessionId);
  const courseLaunchSameWindow: any = useSelector((state: any) => state.globalData?.courseLaunchSameWindow);
  const courseReturnURL = useSelector((state: any) => state.globalData?.returnUrl);
 


  const [iframeSrc, setIframeSrc] = useState<string>("");
  const [entContentLunch, setEntContentLunch] = useState<boolean>(false);
  const [showPopupMessage, setShowPopupMessage] = useState<boolean>(false);

  const [courseWindowWidth, setEntCourseWindowWidth] = useState<boolean>(false);
  const [courseWindowHeight, setEntCourseWindowHeight] = useState<boolean>(false);
  const [showPopupOverlay, setShowPopupOverlay] = useState(false);



  const scormApi = useMemo(() => new SCORMApi(), []);
  const dispatch = useAppDispatch();
  const isApiInitialized = useRef(false);
  const navigate = useNavigate();



  const [isCoursePageReady, setIsCoursePageReady] = useState(false);

  // When both CoursePage and SCORM API are ready, allow iframe
  const [iframeVisible, setIframeVisible] = useState(false);


  // const fullUrl = `${gContentPathData}/${launchUrl}`;

  // useEffect(() => {
  //   const handleBeforeUnload = () => {
  //     if (window.opener) {
  //       window.opener.postMessage({ type: "WINDOW_CLOSED" }, "*");
  //     }
  //   };

  //   window.addEventListener("beforeunload", handleBeforeUnload);

  //   return () => {
  //     window.removeEventListener("beforeunload", handleBeforeUnload);
  //   };
  // }, []);



  // Auto-close the original launch window when course opens in new window
  // useEffect(() => {
  //   if (source === "LW" && window.opener) {
  //     window.opener.postMessage({ type: "CLOSE_LAUNCH_PAGE" }, "*");
  //   }
  // }, [source]);

  // // Listen for close command in parent (source=L)
  // useEffect(() => {
  //   const handleMessage = (event: MessageEvent) => {
  //     if (event.data.type === "CLOSE_LAUNCH_PAGE") {
  //       window.close(); // Close the main window after new window opens
  //     }
  //   };
  //   window.addEventListener("message", handleMessage);
  //   return () => window.removeEventListener("message", handleMessage);
  // }, []);


  useEffect(() => {
    const lesson = gDataObj.gCourseSection?.[0]?.lessons?.[0] as Lesson | undefined;
    const launchUrl = lesson?.launchUrl ?? '';
    const fullUrl = `${gDataObj.gContentPath}/${launchUrl}`;
    console.log("fullUrl in useEffect", gDataObj);
    if (isApiInitialized && isCoursePageReady) {
      const timer = setTimeout(() => {
        setIframeVisible(true);
        setIframeSrc(fullUrl);
        //setIframeSrc("test/main.html");
      }, 100); // optional buffer
      return () => clearTimeout(timer);
    }
  }, [isApiInitialized, isCoursePageReady]);// [isApiInitialized, isCoursePageReady]);

  useEffect(() => {
    const iframe = document.getElementById('iframe') as HTMLIFrameElement | null;
    const onLoad = () => {
      // If the iframe loads something, call the SCORM API or mark it ready
      callAPI();
    };
    if (iframe) {
      iframe.addEventListener('load', onLoad);
    }
    return () => {
      if (iframe) iframe.removeEventListener('load', onLoad);
    };
  }, [iframeSrc]);


  useEffect(() => {
    // if (!isApiInitialized.current && launchPage) {
    if (ClientId && SessionId && !isApiInitialized.current && launchPage) {
      window.API = scormApi;
      console.log("window.API set in LaunchCourse:", window.API);
      isApiInitialized.current = true;

      (window as any).API = scormApi;
      (window.parent as any).API = scormApi;
      (window.top as any).API = scormApi;

      setIsInitialized(true);
      callAPI();
    }
    // }, [launchPage]);
  }, [ClientId, SessionId, launchPage]);



  const callAPI = () => {
    {
      console.log("callApi");
      const fetchGlobalData = async () => {
        const response = await dispatch(getGlobalData({ ClientId, SessionId }));
        if (response.meta.requestStatus === 'fulfilled') {
          const globalData = response.payload.session;
          const entAssignment = globalData.assignment;
          const entLearner = globalData.learner;
          const entContent = globalData.contentModule;
          const contModTracking = globalData.contentModuleTracking;
          setEntContentLunch(entContent.isMiddlePage);
          setEntCourseWindowWidth(entContent.courseWindowWidth)
          setEntCourseWindowHeight(entContent.courseWindowHeight);;

          // setCourseWindowWidth(entContent.courseWindowWidth || 1000);
          // setCourseWindowHeight(entContent.courseWindowHeight || 800);

          // console.log("courseWindowWidth :", entContent.courseWindowWidth);
          // console.log("courseWindowHeight :", entContent.courseWindowHeight);

          console.log("Content Module Tracking :", contModTracking);

          let iMasteryScore: number = entContent?.masteryScore || -1;
          const strIMSCourseBasePath = entContent.absoluteFolderUrl + "/";
          const courseSection: Section[] = entContent && Object.values(entContent.sections || {}).map((section: any) => ({
            identifier: section.identifier,
            title: section.title,
            sortOrder: section.sortOrder || 0,
            lessons: Object.values(section.lessons || {}).map((lesson: any): Lesson => {
              const siTempMasterScore = iMasteryScore > -1
                ? String(iMasteryScore)
                : lesson.masteryScore?.toString() || "0";

              const baseHref = lesson.resourceSco?.href || "index_lms.html";
              const scormType = lesson.resourceSco?.scormType || "sco";

              let launchUrl = baseHref;
              if (!launchUrl.includes(strIMSCourseBasePath)) {
                const separator = launchUrl.includes('?') ? '&' : '?';
                launchUrl = `${launchUrl}${separator}passingScore=${siTempMasterScore}&LMSID=${lesson.identifier}&ClientName=yp15-dev-local&lang=en-US&rip=`;
              }

              return {
                identifier: lesson.identifier,
                title: lesson.title,
                masteryScore: lesson.masteryScore || 0,
                maxTimeAllowed: lesson.maxTimeAllowed || null,
                timeLimitAction: lesson.timeLimitAction || "",
                sortOrder: lesson.sortOrder || 0,
                launchUrl,
                scormType
              };
            }),
          }));

          const updatedGDataObj = {
            ...gDataObj, // Use the current state as a base
            gLearnerId: entLearner.userNameAlias,
            gStudentId: entLearner.userNameAlias,
            gContentPath: entContent.absoluteFolderUrl,
            gManagerEmail: entLearner.managerEmailId,
            gStudentEmail: entLearner.emailID,
            sessionId: entLearner.sessionId,
            clientId: entLearner.clientId,
            gTrackScoreSettingFromLMS: entContent.ScoreTracking,
            gTotalNoOfPages: entContent.totalLessons,
            gNoOfCompletedPages: "",
            gLearnerName: `${entLearner.firstName} ${entLearner.lastName}`,
            // gCourseName: entAssignment.activityName,
            gCourseName: entContent.contentModuleEnglishName,
            gManifestId: entContent.id,
            gCourseSection: courseSection, // Assign the transformed array here
          };


          // Pass updated data directly to avoid timing issues
          dispatch(setCoursesGlobalData(globalData));
          dispatch(setUpdatedGlobalData(updatedGDataObj)); // Save data to Redux

          // Update the state
          // setGDataObj(gDataObj);
          setGDataObj(updatedGDataObj);
          // setGDataObj(globalData);

          const lesson = updatedGDataObj.gCourseSection?.[0]?.lessons?.[0] as Lesson | undefined;
          const launchUrl = lesson?.launchUrl ?? '';
          const fullUrl = `${updatedGDataObj.gContentPath}/${launchUrl}`;
          console.log("fullUrl in useEffect", fullUrl);
          // setIframeSrc(fullUrl);
          const rtemaster = new RTEMaster(globalData);
          // console.log("RTEMaster initialized with temp data:", rtemaster);

          setLaunchPage(true);
        } else {
          console.error('Error fetching global data.');
        }
      };
      fetchGlobalData();
    }
  };

  const openCourse = () => {
    // LMSInitialize();

    const newWindow = window.open(
      '/LaunchCourseWindow',
      // `/LaunchCourseWindow?ClientId=${ClientId}&SessionId=${SessionId}`,
      "CourseWindow",
      `width=${courseWindowWidth},height=${courseWindowHeight}`
    );
    if (!newWindow || newWindow.closed || typeof newWindow.closed === "undefined") {
      setShowPopupMessage(true);
    } else {
      newWindow.focus();

      setShowPopupOverlay(true); // block background

      // Check every 300ms if window is closed
      const interval = setInterval(() => {
        if (newWindow.closed) {
          clearInterval(interval);
          setShowPopupOverlay(false);
          window.close(); // Close this window when popup is closed
        }
      }, 300);
    }
  };


  if (entContentLunch === null) {
    return <p>Loading...</p>;
  }


  const handleClose = () => {
    navigate(courseReturnURL); 
  };






  return (
    <div>
      <div>
        {source === "L" ? (
          <div className="lunch-screen">

            {!entContentLunch ? (
              <>
                {/* {console.log("source === launch")} */}
                {/* {isInitialized && (<iframe id="iframe" width="100%" height="500px" src="https://YP15-dev-local.encora.com/content/Clients/CLIW6hJcmeHlLrN/Courses/COUNanjoPQy/main.html?passingScore=0&LMSID=Page1"></iframe>)} */}
                {/* {gDataObj.clientId !== "" ? <CoursePage gDataObj={gDataObj} key={JSON.stringify(gDataObj)} openCourse={() => setIframeSrc("test/main.html")}  onReady={() => setIsCoursePageReady(true)} /> : <p>Loading...</p>} */}
                {/* {gDataObj.clientId !== "" ? <CoursePage gDataObj={gDataObj} key={JSON.stringify(gDataObj)} openCourse={() => setIframeSrc("test/main.html")}  onReady={() => setIsCoursePageReady(true)} /> : <p>Loading...</p>}
                {isInitialized && isCoursePageReady && ( <iframe id="iframe" width="100%" height="500px" src="test/main.html"></iframe>)} */}
                {/* {gDataObj.clientId !== "" ? <CoursePage gDataObj={gDataObj} key={JSON.stringify(gDataObj)} openCourse={() => setIframeSrc("test/main.html")} /> : <p>Loading...</p>} */}
                {/* Step 1: Load CoursePage */}

                {gDataObj.clientId !== "" ?
                  <CoursePage gDataObj={gDataObj} openCourse={() => setIframeSrc("test/main.html")} onReady={() => setIsCoursePageReady(true)} /> : <p>Loading...</p>
                }

                {/* Step 2: Load iframe only after CoursePage + SCORM API are ready */}
                {/* {iframeVisible && isInitialized && isCoursePageReady && (
                  <iframe id="iframe" width="100%" height="500px" src={iframeSrc}></iframe>
                )} */}
                {iframeVisible && isInitialized && isCoursePageReady && iframeSrc ? (
                  <>
                    {courseLaunchSameWindow && (
                      <button
                        onClick={handleClose}
                        style={{
                          alignSelf: "flex-end",
                          float: "right",
                          margin: 16,
                          background: "#dc3545",
                          color: "#fff",
                          padding: "10px 15px",
                          border: "none",
                          borderRadius: "4px",
                          fontWeight: "bold",
                          cursor: "pointer",
                          zIndex: 9999,
                        }}
                      >
                        Back to Home
                      </button>
                    )}
                    <iframe id="iframe" width="100%" height="500px" src={iframeSrc} style={{ display: 'block' }} />
                  </>
                  // <iframe id="iframe" width={courseWindowWidth} height={courseWindowHeight} src={iframeSrc} style={{ display: 'block' }}/>

                ) : (
                  <div className="yp-loading-overlay">
                    <div className="yp-spinner">
                      <div className="spinner-border" />
                    </div>
                  </div>
                )}

              </>
            ) : (
              gDataObj.clientId !== "" ? (
                <>
                  {isInitialized && isCoursePageReady && iframeSrc &&
                    <>
                      <iframe id="iframe" width="100%" height="500px" src={iframeSrc} />
                    </>
                  }
                  {courseLaunchSameWindow && (
                    <button
                      onClick={handleClose}
                      style={{
                        alignSelf: "flex-end",
                        margin: 16,
                        float: "right",
                        background: "#dc3545",
                        color: "#fff",
                        padding: "10px 15px",
                        border: "none",
                        borderRadius: "4px",
                        fontWeight: "bold",
                        cursor: "pointer",
                        zIndex: 9999,
                      }}
                    >
                      Back to Home
                    </button>
                  )}
                  {/* <iframe id="iframe" width={courseWindowWidth} height={courseWindowHeight} src={iframeSrc} style={{ display: 'block' }} />} */}
                  <div className="d-flex justify-content-center align-items-center vh-100">
                    <div className="bg-light p-4 border border-secondary rounded shadow text-center" style={{ maxWidth: "600px" }}>
                      <p className="text-dark">Popup Blocked. Please disable popup blockers for this site.</p>
                      <p className="text-dark">Click <span className="fw-bold">Launch Course</span> to bypass the popup blocker.</p>
                      <button onClick={openCourse} className="btn btn-primary px-4 py-2 fw-bold">Launch Course</button>
                      <p className="text-danger mt-3 fw-bold fst-italic">Note - Do not close this window to ensure course progress is tracked.</p>
                    </div>
                  </div>
                </>
              ) : <p className="text-center">Loading...</p>
            )}
          </div>
        ) : (
          // <div className="video-screen">
          //   {/* {isInitialized && ( <iframe id="iframe" width="100%" height="500px" src="https://YP15-dev-local.encora.com/content/Clients/CLIW6hJcmeHlLrN/Courses/COUNanjoPQy/main.html?passingScore=0&LMSID=Page1"></iframe>)} */}
          // <CoursePage gDataObj={gDataObj} key={JSON.stringify(gDataObj)} openCourse={() => setIframeSrc("test/main.html")}  onReady={() => setIsCoursePageReady(true)}/>
          //  {isInitialized && isCoursePageReady && (<iframe id="iframe" width="100%" height="500px" src="test/main.html"></iframe>)}

          // </div>

          <div className="video-screen">
            {/* Step 1: Load CoursePage */}
            <CoursePage gDataObj={gDataObj} key={JSON.stringify(gDataObj)} openCourse={() => setIframeSrc("test/main.html")} onReady={() => setIsCoursePageReady(true)} />
            {/* Step 2: Load iframe only after CoursePage + SCORM API are ready */}
            {iframeVisible && isInitialized && isCoursePageReady && iframeSrc && (
              // <iframe id="iframe" width="100%" height="500px" src={iframeSrc} />
              <>
                <iframe id="iframe" width="100%" height="500px" src={iframeSrc} />
              </>
            )}
            {/* {courseLaunchSameWindow && (
              <button
                onClick={handleClose}
                style={{
                  alignSelf: "flex-end",
                  margin: 16,
                  float: "right",
                  background: "#dc3545",
                  color: "#fff",
                  padding: "10px 15px",
                  border: "none",
                  borderRadius: "4px",
                  fontWeight: "bold",
                  cursor: "pointer",
                  zIndex: 9999,

                }}
              >
                Back to Home
              </button>
            )} */}
          </div>
        )}

      </div>
    </div>
  );
};

export default LaunchContentSCORM;