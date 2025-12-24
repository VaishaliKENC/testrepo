import React, { useEffect, useMemo, useRef, useState } from "react";
import { useSelector } from "react-redux";
import { RootState } from "../../../../redux/store";
import { SCORMApi } from "../course-ScormLibraries/SCORMApi";
import { useAppDispatch } from "../../../../hooks";


const LaunchCourse: React.FC = () => {
  
const clientId: any = useSelector((state: RootState) => state.auth.clientId);
const sessionId: any = useSelector((state: any) => state.coursePlayer?.courseLaunch?.contentModuleSession?.sessionId);

const scormApi = useMemo(() => new SCORMApi(), []);
const dispatch = useAppDispatch();
const isApiInitialized = useRef(false);
const [setIsInitialized] = useState<boolean>(false);

useEffect(() => {
    InitializeScorm();
    const url = `/LaunchContentScorm?source=L&ClientId=${clientId}&SessionId=${sessionId}`;
    window.location.href = url;
}, []);




const InitializeScorm = () => {
    if (!isApiInitialized.current) {
      window.API = scormApi;
      console.log("window.API set in LaunchCourse:", window.API);
      isApiInitialized.current = true;
      //setIsInitialized(true);
    }
  };


   

    return (
        <div>
            <h6>Redirecting to course...</h6>
        </div>
    );
};

export default LaunchCourse;




// const LaunchCourse: React.FC = () => {
//     const formRef = useRef<HTMLFormElement | null>(null);
//     const [ClientId] = useState('CLIYPRevamp_New');
//     const [SessionId] = useState('68B53A8A-CE1E-4903-9CF1-020312270032');

//     useEffect(() => {
//         if (formRef.current) {
//             // formRef.current.action = "/LaunchContentScorm?source=withOutLaunchWindow";
//             formRef.current.action = '/LaunchContentScorm?source=L&ClientId=' + `${ClientId}` + '&SessionId=' + `${SessionId}`
//             formRef.current.submit();
//         } else {
//             console.error("Form 'CourseLaunch' not found.");
//         }
//     }, []);

//     return (
//         <div>
//             <h6>This is Launch Course Player ASPX page.</h6>

//             <form ref={formRef} name="CourseLaunch" method="post">
//                 <input type="hidden" name="SessionId" id="SessionId" value="" />
//                 <input type="hidden" name="ClientId" id="ClientId" value="" />
//             </form>
//         </div>
//     );
// };

// export default LaunchCourse;