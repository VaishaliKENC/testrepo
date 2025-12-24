import React, { useEffect, useRef, useState } from "react";
import { useSelector } from "react-redux";
import { RootState } from "../../../../redux/store";

const LaunchCourseWindow: React.FC = () => {
    const clientId: any = useSelector((state: RootState) => state.auth.clientId);
    const sessionId: any = useSelector((state: any) => state.coursePlayer?.courseLaunch?.contentModuleSession?.sessionId);
    

    useEffect(() => {
        const url = `/LaunchContentScorm?source=LW&ClientId=${clientId}&SessionId=${sessionId}`;
        window.location.href = url;
    }, []);

    return (
        <div>
            <h6>This is Launch Course Player Window page.</h6>
        </div>
    );
};

export default LaunchCourseWindow;
