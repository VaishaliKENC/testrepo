import { useLocation } from "react-router-dom";
import { useRef, useState , useEffect} from "react";
import { updateVideoTracking } from "../../../redux/Slice/tracking/assetTracking.requests";
import { ACTIVITY_TYPES } from "../../../utils/Constants/Enums";
import { useAppDispatch } from "../../../hooks";

const PreviewFile = () => {
  const searchParams = new URLSearchParams(useLocation().search);

  const fileInfo = {
    fileUrl: decodeURIComponent(searchParams?.get("file") ?? ""),
    fileServer: decodeURIComponent(searchParams?.get("server") ?? ""),
    fileType: searchParams?.get?.("type"),
    fileName: searchParams?.get("name"),
    isMp4: searchParams?.get?.("isMp4"),
  };

  const [isSeeking, setIsSeeking] = useState(false);
  const videoRef = useRef<any>(null);
  const dispatch = useAppDispatch();
  const initialSeekRef = useRef(true);
  const watchedInMins = Number(searchParams.get("watchedInMins"));
  //USE IF WANT TO PLAY THAT VIDEO FROM A PARTICULAR TIME ,TIME IS IS IN SEC
  //   useEffect(() => {
  //   if (videoRef?.current) {
  //     videoRef.current.currentTime = 2.2;    //time at which I want to make player start from
  //     // videoRef.current.play();
  //   }
  // }, []);

  // Common function to track video progress
  const sendVideoTracking = (videoEvent: string) => {
    if (!videoRef.current) return;

    const currentTime = videoRef.current.currentTime;
    const totalDuration = videoRef.current.duration;

    const videoTrackingPayload = {
      activityId: searchParams.get("id") ?? "",
      activityType: ACTIVITY_TYPES.ASSET,
      clientId: searchParams.get("clientId") ?? "",
      learnerId: searchParams.get("userId") ?? "",
      totalDuration,
      elaspedTime: currentTime,
      videoEvent,
      activityName: fileInfo.fileName ?? "",
    };

    console.log("videoTrackingPayload", videoEvent, videoTrackingPayload);
    dispatch(updateVideoTracking(videoTrackingPayload));
  };

  useEffect(() => {
    if (videoRef?.current && watchedInMins > 0) {
      videoRef.current.currentTime = watchedInMins * 60;
    }
  }, [watchedInMins]);

  // LISTENS WHEN USER DRAGS THE SLEEK OF VIDEO PLAYER
  const handleSeeking = () => {
    if (fileInfo.isMp4 === "true") {
      if (initialSeekRef.current) {
        // This is the first seeking event caused by set watchedinMinutes
        initialSeekRef.current = false;
        return;
      }
      if (!isSeeking) {
        console.log('inside seek')
        setIsSeeking(true);
      }
    }
  };
  useEffect(() => {
    if (isSeeking) {
      console.log('currenttime',(videoRef?.current?.currentTime)/60,'watched time',watchedInMins)
      if((videoRef?.current?.currentTime)/60 <watchedInMins){
        return 
      }
      sendVideoTracking("Seek");
    }
  }, [isSeeking]);

  const handlePause = () => {
    if (!isSeeking && fileInfo.isMp4 === "true") {
      sendVideoTracking("Pause");
    }
  };

  const handlePlay = () => {
    if (!isSeeking && fileInfo.isMp4 === "true") {
      sendVideoTracking("Play");
    }
  };

  useEffect(() => {
    const handleBeforeUnload = (e: any) => {
      if (window.opener && fileInfo.isMp4 === "true" && !isSeeking) {
        window.opener.postMessage("assetPreviewClosed", "*"); // Notify parent
        sendVideoTracking("Exit")
      }
      //ADD VIDEO TRACKING API WHEN WINDOW CLOSED
      // if (fileInfo.isMp4 === "true" && !isSeeking) sendVideoTracking("Exit");
    };

    window.addEventListener("beforeunload", handleBeforeUnload);
    return () => {
      window.removeEventListener("beforeunload", handleBeforeUnload);
    };
  }, [fileInfo]);

  useEffect(() => {
    const video = videoRef?.current;
    if (video) {
      video.addEventListener("seeking", handleSeeking);
    }
    return () => {
      if (video) {
        video.addEventListener("seeking", handleSeeking);
      }
    };
  }, []);

  if (!fileInfo.fileUrl) return <div>No file selected</div>;

  const getFilePreview = () => {
    // const type = getAssetType(fileInfo?.fileType ?? "");  /prev used not required now
    const type = fileInfo?.fileType ?? "";
    switch (type) {
      case "VIDEO":
        return (
          <div className="m-3">
            <div className="mb-2">{fileInfo.fileName}</div>
            <video
              ref={videoRef}
              width="600"
              controls
              // autoPlay={true}
              className="shadow-lg"
              poster="vidPlayerImage.jpg"
              onPause={handlePause}
              onPlay={handlePlay}
            >
              <source
                src={`${fileInfo.fileServer}${fileInfo.fileUrl}`}
                type="video/mp4"
              />
            </video>
          </div>
        );
      case "AUDIO":
        return (
          <div className="m-3">
            <div className="mb-2">{fileInfo.fileName}</div>
            <audio controls autoPlay>
              <source
                src={`${fileInfo.fileServer}${fileInfo.fileUrl}`}
                type="audio/ogg"
              />
            </audio>
          </div>
        );
      default:
        return <>Nothing to view</>;
    }
  };
  return <>{getFilePreview()} </>;
};

export default PreviewFile;
