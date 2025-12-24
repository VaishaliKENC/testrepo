import React, { useEffect, useMemo, useRef, useState } from 'react';
import { useSelector } from "react-redux";
import { GlobalData } from '../../../../Types/coursePlayerScormType/globalDataType';

import { addManifestNode, arrManifestNodes, getScoStatus, lessonStatuses, writeScoTracking } from '../../../../utils/coursePlayerScormArrays';
import { CSCO } from '../course-ScormLibraries/Master';
import { RTEMaster, arrSCO } from '../course-ScormLibraries/RTEMaster';


interface GlobalDataProps {
  gDataObj: GlobalData;
  openCourse: () => void;
  onReady: () => void;
}


const CoursePage: React.FC<GlobalDataProps> = ({ gDataObj: initialGDataObj, openCourse, onReady }) => {
  const initializedRef = useRef(false);

  const [gDataObj, setGDataObj] = useState<GlobalData>(initialGDataObj);

  const gDataObjUpdated = useSelector((state: any) => state.updatedGlobalData);
  const globalData = useSelector((state: any) => state.globalData);
  const entContent = globalData?.contentModule;
  const contModTracking = globalData.contentModuleTracking;
  //const rtemaster = new RTEMaster(globalData);
  const rtemaster = useMemo(() => new RTEMaster(globalData), [globalData]);


  let courseSection: any[] = [];
  let iMasteryScore: number = -1;
  let strIMSCourseBasePath = "";


  if (entContent && entContent.absoluteFolderUrl) {
    iMasteryScore = entContent.masteryScore || -1;
    strIMSCourseBasePath = entContent.absoluteFolderUrl + "/";

    courseSection = Object.values(entContent.sections || {}).map((section: any) => ({
      identifier: section.identifier,
      title: section.title,
      sortOrder: section.sortOrder || 0,
      lessons: Object.values(section.lessons || {}).map((lesson: any) => {
        const siTempMasterScore = iMasteryScore > -1
          ? String(iMasteryScore)
          : lesson.masteryScore?.toString() || "0";

        const baseHref = lesson.resourceSco?.href || "index_lms.html";
        const scormType = lesson.resourceSco?.scormType || "sco";

        let launchUrl = baseHref;
        if (!launchUrl.includes(strIMSCourseBasePath)) {
          const separator = launchUrl.includes('?') ? '&' : '?';
          launchUrl = `${launchUrl}${separator}passingScore=${siTempMasterScore}&LMSID=${lesson.identifier}&ClientName=yp02-dev-local&lang=en-US&rip=`;
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
  }


  useEffect(() => {
    const fetchCourseData = async () => {
      console.log("CoursePage loading course data...");
      await new Promise((resolve) => setTimeout(resolve, 1000));
      console.log("CoursePage data ready");
      onReady();
    };
    fetchCourseData();
  }, [onReady]);

  useEffect(() => {
    setGDataObj(initialGDataObj);
  }, [initialGDataObj]);


  useEffect(() => {
    if (initializedRef.current) return;
    initializedRef.current = true;
    courseSection.forEach((section: any) => {
      section.lessons.forEach((lesson: any) => {
        const siTempMasterScore = iMasteryScore > -1 ? String(iMasteryScore) : lesson.masteryScore?.toString() || "0";

        const manifestNodeParams = [
          lesson.identifier, "", "", "", `LMSID=${lesson.identifier}&lang=en-US`,
          siTempMasterScore, lesson.maxTimeAllowed || "", lesson.timeLimitAction || "",
          "", "", "", "",
          lesson.resourceSco?.href
            ? `${lesson.resourceSco.href}?passingScore=${siTempMasterScore}&LMSID=${lesson.identifier}&ClientName=yp16-dev-local&lang=en-US&rip=`
            : `index_lms.html?passingScore=${siTempMasterScore}&LMSID=${lesson.identifier}&ClientName=yp15-dev-local&lang=en-US&rip=`
        ];
        addManifestNode(manifestNodeParams);
        debugger;
        const tracking = contModTracking.lessonTracking?.[lesson.identifier];
        lessonStatuses[lesson.identifier] = tracking ? getScoStatus(tracking.lessonStatus) : getScoStatus("not-started");
        if (tracking) writeScoTracking(tracking);

        const cscoParams: string[] = [
          lesson.identifier,
          gDataObjUpdated.gStudentId,
          gDataObjUpdated.gLearnerName,
          "", "", tracking?.lessonLocation || "",
          tracking?.credit || "credit",
          tracking?.lessonStatus || "Not started",
          "ab-initio",
          tracking?.rawScore || 0,
          "", "", tracking?.totalTime || "00:00:00",
          tracking?.lessonMode || "normal",
          "", tracking?.sessionTime || "00:00:00",
          tracking?.suspendData || "",
          `LMSID=${lesson.identifier}&lang=en-US`,
          "", "", tracking?.objectives?.length || 0,
          lesson.masteryScore?.toString() || "95",
          lesson.maxTimeAllowed || null,
          lesson.timeLimitAction || null,
          "en-US",
          tracking?.totalpages || 0,
          tracking?.completedpages || 0
        ];
        if (!arrSCO.find(sco => sco.identifier === lesson.identifier)) {
          arrSCO.push(new CSCO(cscoParams));
        }

        //arrSCO.push(new CSCO(cscoParams));

        if (arrManifestNodes.length === 1) {
          rtemaster.fGoToLaunchSco(arrManifestNodes[0].identifier);
          // rtemaster.LMSIntInitialize("");
        }
      });
    });
  }, []);

  if (!gDataObj || !entContent || !entContent.absoluteFolderUrl) {
    return <p>Loading...</p>;
  }


  // let iMasteryScore: number = entContent?.masteryScore || -1;
  // const strIMSCourseBasePath = entContent.absoluteFolderUrl + "/";

  // const courseSection = entContent && Object.values(entContent.sections || {}).map((section: any) => ({
  //   identifier: section.identifier,
  //   title: section.title,
  //   sortOrder: section.sortOrder || 0,
  //   lessons: Object.values(section.lessons || {}).map((lesson: any) => {
  //     const siTempMasterScore = iMasteryScore > -1
  //       ? String(iMasteryScore)
  //       : lesson.masteryScore?.toString() || "0";

  //     const baseHref = lesson.resourceSco?.href || "index_lms.html";
  //     const scormType = lesson.resourceSco?.scormType || "sco";

  //     let launchUrl = baseHref;
  //     if (!launchUrl.includes(strIMSCourseBasePath)) {
  //       const separator = launchUrl.includes('?') ? '&' : '?';
  //       launchUrl = `${launchUrl}${separator}passingScore=${siTempMasterScore}&LMSID=${lesson.identifier}&ClientName=yp02-dev-local&lang=en-US&rip=`;
  //     }

  //     return {
  //       identifier: lesson.identifier,
  //       title: lesson.title,
  //       masteryScore: lesson.masteryScore || 0,
  //       maxTimeAllowed: lesson.maxTimeAllowed || null,
  //       timeLimitAction: lesson.timeLimitAction || "",
  //       sortOrder: lesson.sortOrder || 0,
  //       launchUrl,
  //       scormType
  //     };
  //   }),
  // }));

  //   if (!courseSection || courseSection.length === 0) {
  //   return <p>No Sections available.</p>;
  // }

  return (
    <div className='m-3'>
      {/* <div style={{ fontFamily: 'Verdana, Arial, Helvetica, sans-serif' }}>
        <div style={{ textAlign: 'left', width: '100%' }}>
          <strong>
            &nbsp; <span style={{ fontSize: '10pt' }}>Welcome</span>
            <span id="lblWelcome" style={{ fontSize: '10pt' }}> {gDataObjUpdated.gLearnerName}</span>
          </strong>
        </div>
        <br />
        <div style={{ border: 'none', textAlign: 'center', width: '100%' }}>
          <div
            style={{
              border: '1px solid #4b5053',
              backgroundColor: '#efefef',
              padding: '10px',
              width: '496px',
              margin: '0 auto',
            }}
          >
            <div className="dvh">
              <span id="lblErr" style={{ color: 'Red' }}></span>
            </div>
            <div className="dvh">
              Course Name:&nbsp;
              <span id="lblContentModuleName">{gDataObjUpdated.gCourseName}</span>
            </div>
            <div className="dvh">Instructions:</div>
            <div className="dvh">
              <span id="lblMessage">
                To receive your certificate of completion, you must view each screen in each section.
              </span>
            </div>
            <div className="dvh">1&nbsp;Lessons</div>
            <br />
            <br />
            <table className="gridbox" cellSpacing={0} cellPadding={3} style={{ borderCollapse: "collapse" }}>
              <thead>
                <tr className="gridtitlebgleft">
                  <th>Lesson</th>
                  <th align="center">Lesson Status</th>
                </tr>
              </thead>
              <tbody>
                {courseSection?.map((section: any) => (
                  <React.Fragment key={section.identifier}>
                    <tr>
                      <td colSpan={2} style={{ fontWeight: "bold" }}>{section.title}</td>
                    </tr>
                    {section.lessons.map((lesson: any) => (
                      <tr key={lesson.identifier}>
                        <td>
                          <div className="SpanWordRap">
                            <a href="#" onClick={(e) => {
                              e.preventDefault();
                              rtemaster.fOpenScoAsset(
                                `${gDataObjUpdated.gContentPath}/${lesson.launchUrl}`,
                                lesson.identifier,
                                lesson.scormType
                              );
                            }}>
                              {lesson.title}
                            </a>
                          </div>
                        </td>
                        <td align="center">{lessonStatuses[lesson.identifier]}</td>
                      </tr>
                    ))}
                  </React.Fragment>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div> */}
    </div>
  );
};

export default CoursePage;
