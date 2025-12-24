import { ASSETTYPES } from "./Constants/AssetTypes";

export const getAssetType = (filetype: string) => {
  for (const [type, extensions] of Object.entries(ASSETTYPES)) {
    if (extensions.includes(filetype)) {
      return type;
    }
  }
  return null;
};

export const mapLeaderBoardCourseList = (list: any[]) => {
  return list.map((item: any) => ({
    label: item.courseName,
    value: item.activityId,
  }));
};

export const getRankHolders = (courseLeaders: any[], position: string) => {
  return courseLeaders.filter((rank) => rank.rank === position);
};

// export const getActivityId = (option: any, list: any) => {
//   const x=list.find((item: any) => item.courseName === option);
//   return x;
// };
//  const filterMyRank = () => {
//     return rankList.filter((rank: any) => {
//       return rank.systemUserGUID === userId;
//     });
//   };