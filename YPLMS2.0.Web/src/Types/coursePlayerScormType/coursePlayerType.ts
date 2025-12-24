export interface ISendDataLMSPayload {

  identifier: string,
  studentId: string,
  studentName: string,
  lessonLocation: string, 
  credit: string, 
  lessonStatus: string,
  entry: string,
  rawScore: Number,  
  minScore: Number,  
  maxScore: Number,  
  exit: any,
  sessionTime: Number,
  totalTime: Number,
  lessonMode: string,
  suspendData: string,
  launchData: string,
  comments: string,
  commentsFromLms: string,
  masteryScore: Number,
  maxTimeAllowed: Number,
  timeLimitAction: string,
  totalpages: Number,
  completedpages: Number,
  courseId: string,
  clientId: string,
  audio: boolean,
  language: string ,
  speed: Number,
  text: string ,
  interactionCount: Number,
  sessionId: string
};

export interface ICourseLaunchSessionPayload {
    clientId: string,
    contentModuleId: string,
    systemUserGuid: string,
    attempt: number,
    launchSite: number,
    isReview: boolean,
    ssoLogin: boolean,
    sameWindow: boolean,
    returnUrl: string,
    gridPageSize: number
  
}


export interface ICoursePlayerPayload {
  clientId: string,
  contentModuleId: string,
  SystemUserGUID: string,
  LaunchType: string,
}
