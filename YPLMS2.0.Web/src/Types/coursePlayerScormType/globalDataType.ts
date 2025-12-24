// Define the type for gDataObj (important for type safety)
export interface GlobalData {
    gContentPath: string;
    gLearnerId: string;
    gStudentId: string;
    gManagerEmail: string;
    gStudentEmail: string;
    sessionId: string;
    clientId: string;
    gTrackScoreSettingFromLMS: string;
    gTotalNoOfPages: string;
    gNoOfCompletedPages: string;
    gLearnerName: string;
    gCourseName : string;
    gManifestId : string;
    gCourseSection: { identifier: string; title: string; sortOrder: number,
      lessons : { identifier: string; title: string; sortOrder: number }[] }[];
    
  }

  // Initialize a default global data object
export const gDataObj: GlobalData = {
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
  gCourseSection:[],
};


//   export interface GlobalDataProps {
//     gDataObj: GlobalData;
// }