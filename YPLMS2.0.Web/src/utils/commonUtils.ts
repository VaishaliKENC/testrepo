export const validateInput = (input: any) => {
  if (!input || input === null) {
    return "";
  } else if (input && Object.keys(input).length === 0) {
    return "";
  } else return input;
};

export const checkDueDate = (date: string) => {
  const currentDate = new Date().setHours(0, 0, 0, 0);
  const dueDate = new Date(date).setHours(0, 0, 0, 0);
  const difference = (dueDate - currentDate) / (1000 * 60 * 60 * 24);
  if (difference > 0 && (difference === 6 || difference < 6)) {
    return true;
  }
  return false;
};

export const checkExpiry = (date: string) => {
  const currentDate = new Date().setHours(0, 0, 0, 0);
  const dueDate = new Date(date).setHours(0, 0, 0, 0);
  if (currentDate > dueDate) {
    return true;
  }
  return false;
};
export const formatDate = (date: string) => {
  const newDate = new Date(date);
  const year = newDate.getFullYear();
  const month = (newDate.getMonth() + 1).toString().padStart(2, "0");
  const day = newDate.getDate().toString().padStart(2, "0");
  const customFormat = `${day}/${month}/${year}`;
  return customFormat;
};

export const handleDebounce = (
  func: (value: string) => void,
  timeout = 1000
) => {
  let timer: any;
  return (...args: any) => {
    clearTimeout(timer);
    timer = setTimeout(() => {
      func.apply(this, args);
    }, timeout);
  };
};
export interface AssetTrackingDataProps {
  contentServerURL: string;
  isDownload: boolean;
  assetName: string;
  assetFileType: string;
  relativePath: string;
  isMp4: string;
  id?: string;
  clientId?: string;
  userId?: string;
  watchedInMins?: number;
}

export const handleAssetPreviewTracking = (data: AssetTrackingDataProps) => {
  let newTab: Window | null = null;

  if (data.assetFileType === "PDF") {
    const fileUrl = data.contentServerURL + data.relativePath;
    const viewerUrl = `/web/viewer.html?file=${fileUrl}&IsDownload=${
      data.isDownload ? "visible" : "hidden"
    }`;
    newTab = window.open(viewerUrl, "_blank");
  } else if (data.assetFileType === "VIDEO" || data.assetFileType === "AUDIO") {
    const mediaUrl = `/preview?file=${encodeURIComponent(
      data.relativePath
    )}&server=${encodeURIComponent(data.contentServerURL)}&type=${
      data.assetFileType
    }&name=${encodeURIComponent(data.assetName)}&isMp4=${data.isMp4}&id=${
      data.id ?? ""
    }&clientId=${data.clientId ?? ""}&userId=${
      data.userId ?? ""
    }&watchedInMins=${data.watchedInMins ?? 0}`;

    newTab = window.open(mediaUrl, "_blank");
  } else {
    const fileUrl = data.contentServerURL + data.relativePath;
    newTab = window.open(fileUrl, "_blank");
  }
  if (localStorage.getItem("userTypeId") === "Learner") {
    if (newTab) {
      const closeCheck = setInterval(() => {
        if (newTab?.closed) {
          clearInterval(closeCheck);
          window.location.reload();
        }
      }, 500);
    }
  }
};

export const sortContents = (
  list: any,
  item: { key: string; sort: string }
) => {
  return [...list].sort((a, b) => {
    if (item?.key === null) {
      return 0; // No sorting
    }
    //IF NEED TO DEAL WITH BOOLEAN ,USE BELOW BLOCK ALSO
    // if (typeof a[item?.key] === "boolean") {
    //   const valueA = a[item?.key] ? 1 : 0;
    //   const valueB = b[item?.key] ? 1 : 0;
    //   if (valueA < valueB) {
    //     return item?.sort === "asc" ? 1: -1;
    //   }
    //   if (valueA > valueB) {
    //     return item?.sort === "asc" ? -1 : 1;
    //   }
    //   return 0;
    // } else {
    const valueA = a[item?.key] ? a[item?.key].toString().toLowerCase() : "";
    const valueB = b[item?.key] ? b[item?.key].toString().toLowerCase() : "";
    if (valueA < valueB) {
      return item?.sort === "asc" ? -1 : 1;
    }
    if (valueA > valueB) {
      return item?.sort === "asc" ? 1 : -1;
    }
    return 0;
    // }
  });
};
// export const handleAssetPreviewTracking = (data: AssetTrackingDataProps) => {
//   if (data.assetFileType === "PDF") {
//     const fileUrl = data.contentServerURL + data.relativePath;
//     const viewerUrl = `/web/viewer.html?file=${fileUrl}&IsDownload=${data.isDownload ? "visible" : "hidden"
//       }`;
//     window.open(viewerUrl, "_blank", "noopener,noreferrer");
//   }
//   else if (data.assetFileType === "VIDEO" || data.assetFileType === "AUDIO") {
//     const mediaUrl = `/preview?file=${encodeURIComponent(
//       data.relativePath
//     )}&server=${encodeURIComponent(data.contentServerURL)}&type=${data.assetFileType
//       }&name=${encodeURIComponent(data.assetName)}&isMp4=${data.isMp4 === "mp4" ? "true" : "false"
//       }&id=${data.id ?? ""}&clientId=${data.clientId ?? ""}&userId=${data.userId ?? ""}&watchedInMins=${data.watchedInMins ?? 0}`;

//     window.open(mediaUrl, "_blank");
//   }
//   else {
//     const fileUrl = data.contentServerURL + data.relativePath;
//     window.open(fileUrl, "_blank", "noopener,noreferrer");
//   }
// };

// export const handleAssetPreviewTracking = (
//   data: AssetTrackingDataProps,
// ) => {
//   if (data.assetFileType === "PDF") {
//     const fileUrl = data.contentServerURL + data?.relativePath;
//     const viewerUrl = `/web/viewer.html?file=${fileUrl}&IsDownload=${data.isDownload ? "visible" : "hidden"
//       }`;
//     window.open(viewerUrl, "_blank", "noopener,noreferrer");
//   } else if (data.assetFileType === "VIDEO" || data.assetFileType === 'AUDIO') {
//     const mediaUrl = `/preview?file=${encodeURIComponent(
//       data.relativePath
//     )}&server=${encodeURIComponent(data.contentServerURL)}&type=${data.assetFileType
//       }&name=${data.assetName}&isMp4=${data.isMp4 === "mp4" ? "true" : "false"}`;

//     window.open(mediaUrl, "_blank");
//   } else {
//     const fileUrl = data.contentServerURL + data?.relativePath;
//     window.open(fileUrl, "_blank", "noopener,noreferrer");
//   }
// };
