using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.DataAccessManager.BusinessManager.Client;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager.Tracking
{
    public interface ICourseTrackingUpdater
    {
        ContentModuleTracking UpdateTracking(LessonTracking tracking, ContentModuleTracking contModTracking);
        ContentModuleTracking UpdateTracking(LessonTracking2004 tracking, ContentModuleTracking contModTracking);
    }

    public class CourseTrackingUpdater : ICourseTrackingUpdater
    {
        public readonly IContentModuleSessionRepository ContentModuleSessionRepository;

        public ContentModuleTracking UpdateTracking(LessonTracking tracking, ContentModuleTracking contModTracking)
        {
            var forCredit = tracking.ForCredit;
            var browseMode = tracking.BrowseMode;
            var trackingExists = contModTracking.LessonTracking.ContainsKey(tracking.Identifier);
            var masteryScore = tracking.MasteryScore;
            var currentScore = tracking.RawScore;
            var currentStatus = tracking.LessonStatus;
            var totalTime = new TimeSpan(0);

            if (trackingExists)
            {
                var existingTracking = contModTracking.LessonTracking[tracking.Identifier];
                //masteryScore = existingTracking.MasteryScore; commented bez masteryScore of YPLMS will active.              
                currentStatus = existingTracking.LessonStatus;
                currentScore = existingTracking.RawScore;
                totalTime = existingTracking.TotalTime;
            }
            else if (browseMode || !forCredit)
            {
                currentStatus = "browsed";
                currentScore = null;
            }

            //Note: site had only been calculating status based on mastery score for AICC courses, 
            //but we should make it do that for all courses eventually.
            if (contModTracking.ContentType == ActivityContentType.AICC.ToString() && masteryScore.HasValue && tracking.RawScore.HasValue && masteryScore.Value != -1)
            {
                tracking.LessonStatus = (tracking.RawScore.Value >= masteryScore.Value) ? "passed" : "failed";
            }

            if (contModTracking.ContentType == ActivityContentType.Scorm12.ToString() && masteryScore.HasValue && tracking.RawScore.HasValue && masteryScore.Value > 0) // this if statement added to recalcualte the completion status as completed or failed.
            {
                contModTracking.CompletionStatus = (tracking.RawScore.Value >= masteryScore.Value) ? ActivityCompletionStatus.Completed : ActivityCompletionStatus.Failed;

                // captivate course if course is incomplete and score is zero then make course status  "Started"
                if (tracking.RawScore.Value == 0)
                {
                    contModTracking.CompletionStatus = ActivityCompletionStatus.Started;
                }

                if (contModTracking.CompletionStatus == ActivityCompletionStatus.Completed && Convert.ToString(tracking.LessonStatus).ToLower() == "incomplete")  //Course status is incomplete, and LMS calculate status is Completed then update status with started.
                {
                    contModTracking.CompletionStatus = ActivityCompletionStatus.Started;
                }

            }

            // if mastery score is NULL & SCORM 1.2 -- GRSI
            // To check GRSI Course Assessment Feature is ON/OFF
            ClientFeatureManager _objClientCourseFeatureManager = new ClientFeatureManager();

            ClientFeature _entClientAssLock = new ClientFeature();
            _entClientAssLock.ClientId = contModTracking.ClientId;
            _entClientAssLock.ClientFeatureID = ClientFeature.FEA_ID_ASSESSMENT_LOCK;

            _entClientAssLock = _objClientCourseFeatureManager.Execute(_entClientAssLock, ClientFeature.Method.Get);
            //if (_entClientAssLock != null)
            //{
            //    if (_entClientAssLock.IsActive)
            //    {
                    if (contModTracking.ContentType == ActivityContentType.Scorm12.ToString() && masteryScore.HasValue && masteryScore.Value != -1)
                    {
                        if (Convert.ToString(tracking.LessonStatus).ToLower() == "incomplete")
                        {
                            contModTracking.CompletionStatus = ActivityCompletionStatus.Started;
                        }
                        else if (Convert.ToString(tracking.LessonStatus).ToLower() == "failed")
                        {
                            contModTracking.CompletionStatus = ActivityCompletionStatus.Failed;
                        }
                        else if (Convert.ToString(tracking.LessonStatus).ToLower() == "passed")
                        {
                            contModTracking.CompletionStatus = ActivityCompletionStatus.Completed;
                        }
                    }
            //    }
            //}
            //when taking course not for credit, keep existing score/status/time, or if none exists, use "browsed"/null/zero time
            if (browseMode || !forCredit)
            {
                tracking.LessonStatus = currentStatus;
                tracking.RawScore = currentScore;
                tracking.TotalTime = totalTime;
            }
            else
            {
                tracking.TotalTime = tracking.SessionTime.HasValue ? tracking.SessionTime.Value + totalTime : totalTime;
            }

            contModTracking.LessonTracking[tracking.Identifier] = tracking;
            contModTracking.Bookmark = tracking.Identifier;

            return SetContentModuleTracking(contModTracking);
        }

        private ContentModuleTracking SetContentModuleTracking(ContentModuleTracking contModTracking)
        {
            var scos = contModTracking.LessonTracking;

            Func<LessonTracking, bool> lessonIsComplete = l => l.IsComplete;

            int numberOfPagesCompleted = scos.Values.Count(lessonIsComplete);

            contModTracking.NoOfPagesCompleted = numberOfPagesCompleted;

            if (contModTracking.CompletionStatus == ActivityCompletionStatus.Failed && contModTracking.NoOfPagesCompleted > 0) // this if block added. bez in some cases progress get 100% for failed course .
            {
                contModTracking.NoOfPagesCompleted = contModTracking.NoOfPagesCompleted - 1;
            }

            if (numberOfPagesCompleted == contModTracking.TotalNoOfPages && !contModTracking.IsCompleted() && contModTracking.CompletionStatus != ActivityCompletionStatus.Failed)  // this failed status added in if clause, for avoiding overwrting of failed status to completed.
            {
                contModTracking.DateOfCompletion = DateTime.UtcNow;
                contModTracking.CompletionStatus = ActivityCompletionStatus.Completed;
                var lastScore = scos.Select(sco => sco.Value.RawScore).LastOrDefault(score => score.HasValue) ?? (decimal?)100;
                contModTracking.Score = Math.Round(lastScore.GetValueOrDefault(100)).ToString("F0");
            }
            else
            {
                // contModTracking.CompletionStatus = ActivityCompletionStatus.Started; commented. bez this line making completed course status as "Started" 
                if (!contModTracking.IsCompleted() && contModTracking.CompletionStatus != ActivityCompletionStatus.Failed) // if block added to set the status as started when we lunch the course.
                {
                    contModTracking.CompletionStatus = ActivityCompletionStatus.Started;
                }
                //Add AICC course Failed condition 
                if (!contModTracking.IsCompleted() && contModTracking.CompletionStatus != ActivityCompletionStatus.Failed && contModTracking.ContentType == ActivityContentType.AICC.ToString())
                {
                    if (scos.Select(sco => sco.Value.LessonStatus).LastOrDefault().ToString().ToLower().Trim() == "f" || scos.Select(sco => sco.Value.LessonStatus).LastOrDefault().ToString().ToLower().Trim() == "failed")
                    {
                        contModTracking.CompletionStatus = ActivityCompletionStatus.Failed;
                    }
                }
                else if (numberOfPagesCompleted == contModTracking.TotalNoOfPages && !contModTracking.IsCompleted() && contModTracking.ContentType == ActivityContentType.AICC.ToString())
                {
                    if (scos.Select(sco => sco.Value.LessonStatus).LastOrDefault().ToString().ToLower().Trim() == "p" || scos.Select(sco => sco.Value.LessonStatus).LastOrDefault().ToString().ToLower().Trim() == "c" || scos.Select(sco => sco.Value.LessonStatus).LastOrDefault().ToString().ToLower().Trim() == "passed")
                    {
                        contModTracking.DateOfCompletion = DateTime.UtcNow;
                        contModTracking.CompletionStatus = ActivityCompletionStatus.Completed;
                    }
                }
                var lastScore = scos.Select(sco => sco.Value.RawScore).LastOrDefault(score => score.HasValue) ?? null;
                if (lastScore != null)
                {
                    contModTracking.Score = Math.Round(lastScore.GetValueOrDefault(100)).ToString("F0");
                }
                else
                {
                    contModTracking.Score = null;
                }
            }

            //// Changes by Prajakta 
            if (contModTracking.LessonTracking[contModTracking.Bookmark].totalpages != null)
            {
                contModTracking.CustomTotalNoOfPages = Convert.ToInt32(contModTracking.LessonTracking[contModTracking.Bookmark].totalpages);
            }
            if (contModTracking.LessonTracking[contModTracking.Bookmark].completedpages != null)
            {
                contModTracking.CustomNoOfPagesCompleted = Convert.ToInt32(contModTracking.LessonTracking[contModTracking.Bookmark].completedpages);
            }
            if (contModTracking.CustomTotalNoOfPages != null && contModTracking.CustomTotalNoOfPages != 0 && contModTracking.CustomNoOfPagesCompleted != null && contModTracking.CustomNoOfPagesCompleted != 0)
            {
                if (contModTracking.CustomTotalNoOfPages == contModTracking.CustomNoOfPagesCompleted)
                {
                    contModTracking.DateOfCompletion = DateTime.UtcNow;
                    contModTracking.CompletionStatus = ActivityCompletionStatus.Completed;
                    var lastScore = scos.Select(sco => sco.Value.RawScore).LastOrDefault(score => score.HasValue) ?? (decimal?)100;
                    contModTracking.Score = Math.Round(lastScore.GetValueOrDefault(100)).ToString("F0");
                }
            }

            //// end Prajakta
            if (contModTracking.CompletionStatus == ActivityCompletionStatus.Completed)
            {
                contModTracking.DateOfCompletion = DateTime.UtcNow;
            }
            return contModTracking;
        }

        public ContentModuleTracking UpdateTracking(LessonTracking2004 tracking, ContentModuleTracking contModTracking)
        {
            var forCredit = tracking.ForCredit;
            var browseMode = tracking.BrowseMode;
            var trackingExists = contModTracking.LessonTracking2004.ContainsKey(tracking.Identifier);
            var masteryScore = tracking.MasteryScore;
            var currentScore = tracking.RawScore;
            var currentStatus = tracking.LessonStatus;
            var totalTime = "";// new TimeSpan(0);

            if (trackingExists)
            {
                var existingTracking = contModTracking.LessonTracking2004[tracking.Identifier];
                //masteryScore = existingTracking.MasteryScore;
                masteryScore = tracking.MasteryScore;
                currentStatus = existingTracking.LessonStatus;
                currentScore = existingTracking.RawScore;
                //totalTime = existingTracking.TotalTime;
                totalTime = existingTracking.TotalTime.ToString();
            }
            else if (browseMode || !forCredit)
            {
                currentStatus = "browsed";
                currentScore = null;
            }

            //Note: site had only been calculating status based on mastery score for AICC courses, 
            //but we should make it do that for all courses eventually.
            if (contModTracking.ContentType == ActivityContentType.AICC.ToString() && masteryScore.HasValue && tracking.RawScore.HasValue && masteryScore.Value != -1)
            {
                tracking.LessonStatus = (tracking.RawScore.Value >= masteryScore.Value) ? "passed" : "failed";
            }

            if (contModTracking.ContentType == ActivityContentType.Scorm2004.ToString() && masteryScore.HasValue && !(string.IsNullOrEmpty(tracking.SuccessStatus)) && tracking.RawScore.HasValue && masteryScore.Value > 0)
            {
                contModTracking.CompletionStatus = (tracking.RawScore.Value >= masteryScore.Value) ? ActivityCompletionStatus.Completed : ActivityCompletionStatus.Failed;

                if (Convert.ToString(tracking.LessonStatus).ToLower() == "completed" && Convert.ToString(tracking.SuccessStatus).ToLower() == "failed" && contModTracking.CompletionStatus == ActivityCompletionStatus.Failed)
                {
                    contModTracking.CompletionStatus = ActivityCompletionStatus.Failed;
                }
                else if (Convert.ToString(tracking.LessonStatus).ToLower() == "completed" && Convert.ToString(tracking.SuccessStatus).ToLower() == "failed" && contModTracking.CompletionStatus == ActivityCompletionStatus.Completed)
                {
                    contModTracking.CompletionStatus = ActivityCompletionStatus.Completed;
                }
                else if (Convert.ToString(tracking.LessonStatus).ToLower() == "completed" && Convert.ToString(tracking.SuccessStatus).ToLower() == "passed" && contModTracking.CompletionStatus == ActivityCompletionStatus.Failed)
                {
                    contModTracking.CompletionStatus = ActivityCompletionStatus.Failed;
                }
                else if (Convert.ToString(tracking.LessonStatus).ToLower() == "completed" && Convert.ToString(tracking.SuccessStatus).ToLower() == "passed" && contModTracking.CompletionStatus == ActivityCompletionStatus.Completed)
                {
                    contModTracking.CompletionStatus = ActivityCompletionStatus.Completed;
                }
                else if (Convert.ToString(tracking.LessonStatus).ToLower() == "incomplete" && Convert.ToString(tracking.SuccessStatus).ToLower() == "failed" && contModTracking.CompletionStatus == ActivityCompletionStatus.Failed)
                {
                    contModTracking.CompletionStatus = ActivityCompletionStatus.Failed;
                }
                else if (Convert.ToString(tracking.LessonStatus).ToLower() == "incomplete" && Convert.ToString(tracking.SuccessStatus).ToLower() == "failed" && contModTracking.CompletionStatus == ActivityCompletionStatus.Completed)
                {
                    contModTracking.CompletionStatus = ActivityCompletionStatus.Started;
                }
                else if (Convert.ToString(tracking.LessonStatus).ToLower() == "incomplete" && Convert.ToString(tracking.SuccessStatus).ToLower() == "passed" && contModTracking.CompletionStatus == ActivityCompletionStatus.Completed)
                {
                    contModTracking.CompletionStatus = ActivityCompletionStatus.Started;
                }
                else if (Convert.ToString(tracking.LessonStatus).ToLower() == "incomplete" && Convert.ToString(tracking.SuccessStatus).ToLower() == "passed" && contModTracking.CompletionStatus == ActivityCompletionStatus.Failed)
                {
                    contModTracking.CompletionStatus = ActivityCompletionStatus.Started;
                }

            }
            else
            {
                contModTracking.CompletionStatus = ActivityCompletionStatus.Started;
            }

            //when taking course not for credit, keep existing score/status/time, or if none exists, use "browsed"/null/zero time
            if (browseMode || !forCredit)
            {
                tracking.LessonStatus = currentStatus;
                tracking.RawScore = currentScore;
                tracking.TotalTime = totalTime;
            }
            else
            {
                //tracking.TotalTime = tracking.SessionTime.HasValue ? tracking.SessionTime.Value + totalTime : totalTime;
                // tracking.TotalTime = tracking.SessionTime;  commented by sarita. as totaltime calculation done in RTEMaster
            }

            contModTracking.LessonTracking2004[tracking.Identifier] = tracking;
            contModTracking.Bookmark = tracking.Identifier;

            return SetContentModuleTracking2004(contModTracking);
        }

        private ContentModuleTracking SetContentModuleTracking2004(ContentModuleTracking contModTracking)
        {
            var scos = contModTracking.LessonTracking2004;

            Func<LessonTracking2004, bool> lessonIsComplete = l => l.IsComplete;

            int numberOfPagesCompleted = scos.Values.Count(lessonIsComplete);

            contModTracking.NoOfPagesCompleted = numberOfPagesCompleted;

            if ((contModTracking.CompletionStatus == ActivityCompletionStatus.Failed || contModTracking.CompletionStatus == ActivityCompletionStatus.Started) && contModTracking.NoOfPagesCompleted > 0) // this if block added bez in some cases progress get 100% for failed course .
            {
                contModTracking.NoOfPagesCompleted = contModTracking.NoOfPagesCompleted - 1;
                numberOfPagesCompleted = numberOfPagesCompleted - 1;
            }

            /* if (numberOfPagesCompleted == contModTracking.TotalNoOfPages && !contModTracking.IsCompleted() && contModTracking.CompletionStatus != ActivityCompletionStatus.Failed)    // this failed status added in if clause, for avoiding overwrting of failed status to completed.
             {
                 if(contModTracking.CompletionStatus == ActivityCompletionStatus.Completed 
                 contModTracking.DateOfCompletion = DateTime.UtcNow;
                 contModTracking.CompletionStatus = ActivityCompletionStatus.Completed;

                //var lastScore = scos.Select(sco => sco.Value.RawScore).LastOrDefault(score => score.HasValue) ?? (decimal?)100;
                 //contModTracking.Score = Math.Round(lastScore.GetValueOrDefault(100)).ToString("F0");
                 var lastScore = scos.Select(sco => sco.Value.RawScore).LastOrDefault(score => score.HasValue) ?? null;
                 if (lastScore != null)
                 {
                     contModTracking.Score = Math.Round(lastScore.GetValueOrDefault(100)).ToString("F0");
                 }
                 else
                 {
                     contModTracking.Score = null;
                 }

             }
             else
             {  */
            // contModTracking.CompletionStatus = ActivityCompletionStatus.Started; commented. bez this line making completed course status as "Started" 
            if (!contModTracking.IsCompleted() && contModTracking.CompletionStatus != ActivityCompletionStatus.Failed) // if block added to set the status as started when we lunch the course.
            {
                contModTracking.CompletionStatus = ActivityCompletionStatus.Started;
            }
            //var lastScore = scos.Select(sco => sco.Value.RawScore).LastOrDefault(score => score.HasValue) ?? (decimal?)0;
            //contModTracking.Score = Math.Round(lastScore.GetValueOrDefault(100)).ToString("F0");
            var lastScore = scos.Select(sco => sco.Value.RawScore).LastOrDefault(score => score.HasValue) ?? null;
            if (lastScore != null)
            {
                contModTracking.Score = Math.Round(lastScore.GetValueOrDefault(100)).ToString("F0");
            }
            else
            {
                contModTracking.Score = null;
            }


            //   }

            //// Changes by Prajakta 
            if (contModTracking.LessonTracking2004[contModTracking.Bookmark].totalpages != null)
            {
                contModTracking.CustomTotalNoOfPages = Convert.ToInt32(contModTracking.LessonTracking2004[contModTracking.Bookmark].totalpages);
            }
            if (contModTracking.LessonTracking2004[contModTracking.Bookmark].completedpages != null)
            {
                contModTracking.CustomNoOfPagesCompleted = Convert.ToInt32(contModTracking.LessonTracking2004[contModTracking.Bookmark].completedpages);
            }

            /* commented by sarita to avoid over writing of totalnumberofpages  as done in main branch 
            if (contModTracking.CompletionStatus == ActivityCompletionStatus.Completed)
            {
                contModTracking.CustomTotalNoOfPages = 1;
                contModTracking.CustomNoOfPagesCompleted = 1;
            }
             commented by sarita to avoid over writing of totalnumberofpages  as done in main branch  */
            if (contModTracking.CustomTotalNoOfPages != null && contModTracking.CustomTotalNoOfPages != 0 && contModTracking.CustomNoOfPagesCompleted != null && contModTracking.CustomNoOfPagesCompleted != 0)
            {
                if (contModTracking.CustomTotalNoOfPages == contModTracking.CustomNoOfPagesCompleted)
                {
                    contModTracking.DateOfCompletion = DateTime.UtcNow;
                    contModTracking.CompletionStatus = ActivityCompletionStatus.Completed;
                    var lastScore1 = scos.Select(sco => sco.Value.RawScore).LastOrDefault(score => score.HasValue) ?? (decimal?)100;
                    contModTracking.Score = Math.Round(lastScore1.GetValueOrDefault(100)).ToString("F0");
                }
            }

            //// end Prajakta
            if (contModTracking.CompletionStatus == ActivityCompletionStatus.Completed)
            {
                contModTracking.DateOfCompletion = DateTime.UtcNow;
            }
            return contModTracking;
        }
    }
}
