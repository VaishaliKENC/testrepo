using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager.Tracking
{
    /// <summary>
    /// class ContentModuleTrackingManager
    /// </summary>
    public class ContentModuleTrackingManager : IManager<ContentModuleTracking, ContentModuleTracking.Method, ContentModuleTracking.ListMethod>, IContentModuleTrackingRepository
    {
        /// <summary>
        /// Used for read,add,update,delete transactions.
        /// </summary>
        /// <param name="pEntContModTracking"></param>
        /// <param name="pMethod"></param>
        /// <returns>ContentModuleTracking object</returns>
        public ContentModuleTracking Execute(ContentModuleTracking pEntContModTracking, ContentModuleTracking.Method pMethod)
        {
            ContentModuleTracking entContModTrackingReturn = null;
            ContentModuleTrackingAdaptor adaptorContModTracking = new ContentModuleTrackingAdaptor();

            switch (pMethod)
            {
                case ContentModuleTracking.Method.UpdateAssessmentCourse:
                    entContModTrackingReturn = adaptorContModTracking.UpdateAssessmentCourse(pEntContModTracking);
                    break;
                case ContentModuleTracking.Method.GetStatusByID:
                    entContModTrackingReturn = adaptorContModTracking.GetContentModuleTrackingStatusById_Learner(pEntContModTracking);
                    break;
                case ContentModuleTracking.Method.GetUserDataXML:
                    entContModTrackingReturn = GetUserDataXml(pEntContModTracking);

                    if (String.IsNullOrEmpty(entContModTrackingReturn.UserDataXML))
                    {
                        entContModTrackingReturn.LessonTracking = new Dictionary<string, LessonTracking>();
                    }
                    else
                    {
                        ILessonTrackingSerializer serializer = new LessonTrackingSerializerFactory().Create(pEntContModTracking.ContentType,
                            entContModTrackingReturn.ContentModuleId, entContModTrackingReturn.UserID, entContModTrackingReturn.UserFirstLastName);

                        entContModTrackingReturn.LessonTracking = serializer.ReadLessonTracking(entContModTrackingReturn.UserDataXML);

                        pEntContModTracking.NoOfPagesCompleted = entContModTrackingReturn.LessonTracking.Values.Count(l => l.IsComplete);
                    }
                    break;
                case ContentModuleTracking.Method.Get:
                    entContModTrackingReturn = adaptorContModTracking.GetContentModuleTrackingByID(pEntContModTracking);

                    break;
                case ContentModuleTracking.Method.Add:
                    entContModTrackingReturn = pEntContModTracking;
                    var result2 = adaptorContModTracking.AddContentModuleTracking(pEntContModTracking);
                    if (result2 != null)
                    {
                        if (!pEntContModTracking.IsCompleted() && result2.CurrentCompletionStatus != result2.PreviousCompletionStatus)
                        {
                            GotoDynamicAssignment(pEntContModTracking.ClientId, pEntContModTracking.ContentModuleId, pEntContModTracking.UserID, pEntContModTracking.IsForAdminPreview);
                        }
                    }
                    break;
                case ContentModuleTracking.Method.Update:
                    //Going to return basic information - Previous status, new status, and learner ID
                    //Then we can check to see if the status changed and if the new tracking is completed and 
                    //perform dynamic assignments

                    if (pEntContModTracking.LessonTracking != null)
                    {
                        ILessonTrackingSerializer serializer = new LessonTrackingSerializerFactory()
                                                                .Create(pEntContModTracking.ContentType,
                                                                        pEntContModTracking.ContentModuleId,
                                                                        pEntContModTracking.UserID,
                                                                        pEntContModTracking.UserFirstLastName);

                        if (serializer != null)
                            pEntContModTracking.UserDataXML = serializer.WriteLessonTracking(pEntContModTracking);
                    }

                    var result = EditTracking(pEntContModTracking);

                    if (result != null)
                    {
                        entContModTrackingReturn = pEntContModTracking;
                        if (pEntContModTracking.IsCompleted() && result.CurrentCompletionStatus != result.PreviousCompletionStatus)
                        {
                            GotoDynamicAssignment(pEntContModTracking.ClientId, pEntContModTracking.ContentModuleId, result.LearnerId, pEntContModTracking.IsForAdminPreview);
                        }
                    }

                    break;
                case ContentModuleTracking.Method.UpdateScannedFileName:
                    entContModTrackingReturn = adaptorContModTracking.UpdateScannedFileName(pEntContModTracking);
                    break;
                case ContentModuleTracking.Method.GetUserDataXML2004:
                    entContModTrackingReturn = GetUserDataXml(pEntContModTracking);

                    if (String.IsNullOrEmpty(entContModTrackingReturn.UserDataXML))
                    {
                        entContModTrackingReturn.LessonTracking2004 = new Dictionary<string, LessonTracking2004>();
                    }
                    else
                    {
                        //ILessonTrackingSerializer2004 serializer = new LessonTrackingSerializerFactory2004().Create(pEntContModTracking.ContentType,
                        //    entContModTrackingReturn.ContentModuleId, entContModTrackingReturn.UserID, entContModTrackingReturn.UserFirstLastName);

                        //entContModTrackingReturn.LessonTracking2004 = serializer.ReadLessonTracking(entContModTrackingReturn.UserDataXML);

                        ILessonTrackingSerializer2004 serializer2004 = new LessonTrackingSerializerFactory2004().Create(pEntContModTracking.ContentType,
                            entContModTrackingReturn.ContentModuleId, entContModTrackingReturn.UserID, entContModTrackingReturn.UserFirstLastName);
                        entContModTrackingReturn.LessonTracking2004 = serializer2004.ReadLessonTracking(entContModTrackingReturn.UserDataXML);
                        pEntContModTracking.NoOfPagesCompleted = entContModTrackingReturn.LessonTracking2004.Values.Count(l => l.IsComplete);
                    }
                    break;
                case ContentModuleTracking.Method.Update2004:
                    //Going to return basic information - Previous status, new status, and learner ID
                    //Then we can check to see if the status changed and if the new tracking is completed and 
                    //perform dynamic assignments

                    if (pEntContModTracking.LessonTracking2004 != null)
                    {
                        ILessonTrackingSerializer2004 serializer2004 = new LessonTrackingSerializerFactory2004()
                                                                .Create(pEntContModTracking.ContentType,
                                                                        pEntContModTracking.ContentModuleId,
                                                                        pEntContModTracking.UserID,
                                                                        pEntContModTracking.UserFirstLastName);

                        if (serializer2004 != null)
                            pEntContModTracking.UserDataXML = serializer2004.WriteLessonTracking(pEntContModTracking);
                    }
                    pEntContModTracking.IsScorm2004 = true;
                    var result2004 = EditTracking(pEntContModTracking);

                    if (result2004 != null)
                    {
                        entContModTrackingReturn = pEntContModTracking;
                        if (pEntContModTracking.IsCompleted() && result2004.CurrentCompletionStatus != result2004.PreviousCompletionStatus)
                        {
                            GotoDynamicAssignment(pEntContModTracking.ClientId, pEntContModTracking.ContentModuleId, result2004.LearnerId, pEntContModTracking.IsForAdminPreview);
                        }
                    }

                    break;
                default:
                    entContModTrackingReturn = null;
                    break;
            }
            return entContModTrackingReturn;
        }

        public virtual ContentModuleTracking GetUserDataXml(ContentModuleTracking pEntContModTracking)
        {
            var entContModTrackingReturn =
                new ContentModuleTrackingAdaptor().GetContentModuleTrackingByID(pEntContModTracking);
            return entContModTrackingReturn;
        }

        public virtual ContentModuleTrackingAdaptor.ContentModuleTrackingUpdateResult EditTracking(ContentModuleTracking trackingToSave)
        {
            return new ContentModuleTrackingAdaptor().EditContentModuleTracking(trackingToSave);
        }

        /// <summary>
        /// For Mark completed
        /// </summary>
        /// <param name="pEntListTracking"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public List<ContentModuleTracking> Execute(List<ContentModuleTracking> pEntListTracking, ContentModuleTracking.ListMethod pMethod)
        {
            List<ContentModuleTracking> entListTracking = new List<ContentModuleTracking>();
            ContentModuleTrackingAdaptor adaptorTracking = new ContentModuleTrackingAdaptor();
            switch (pMethod)
            {
                case ContentModuleTracking.ListMethod.MarkCompleted:
                    entListTracking = adaptorTracking.BulkUpdate(pEntListTracking, false);
                    break;
                case ContentModuleTracking.ListMethod.BulkMarkCompleted:
                    entListTracking = adaptorTracking.BulkUpdate(pEntListTracking, true);
                    break;
                default:
                    entListTracking = null;
                    break;
            }
            foreach (ContentModuleTracking entTracking in pEntListTracking)
            {
                GotoDynamicAssignment(entTracking.ClientId, entTracking.ContentModuleId, entTracking.UserID, entTracking.IsForAdminPreview);
            }
            return entListTracking;
        }

        private void GotoDynamicAssignment(string pClientId, string contentModuleId, string pSysteMUserGuId, bool isForAdminPreview)
        {
            if (!isForAdminPreview)
            {
                BackgroundBRuleAssignmentManager entMgr = new BackgroundBRuleAssignmentManager(contentModuleId, pSysteMUserGuId, ActivityContentType.Course.ToString(), pClientId);
                entMgr.AssignActivties();
            }
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <typeparam name="ContentModuleTracking"></typeparam>
        /// <param name="pEntBase">ContentModuleTracking object</param>
        /// <param name="pMethod">ContentModuleTracking.ListMethod</param>
        /// <returns>null</returns>
        public List<ContentModuleTracking> Execute(ContentModuleTracking pEntBase, ContentModuleTracking.ListMethod pMethod)
        {
            return null;

        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <typeparam name="ContentModuleTracking"></typeparam>
        /// <param name="pEntBase">ContentModuleTracking object</param>
        /// <param name="pMethod">ContentModuleTracking.ListMethod</param>
        /// <returns>null</returns>
        public DataSet ExecuteDataSet(ContentModuleTracking pEntBase, ContentModuleTracking.ListMethod pMethod)
        {
            return null;
        }

        public ContentModuleTracking GetContentModuleTracking(ContentModuleTracking trackingParameters)
        {
            return Execute(trackingParameters, ContentModuleTracking.Method.Get);
        }

        public ContentModuleTracking GetContentModuleLessonTracking(ContentModuleTracking trackingParameters)
        {
            return Execute(trackingParameters, ContentModuleTracking.Method.GetUserDataXML);
        }

        public ContentModuleTracking UpdateContentModuleTracking(ContentModuleTracking trackingParameters)
        {
            return Execute(trackingParameters, ContentModuleTracking.Method.Update);
        }
        public ContentModuleTracking UpdateContentModuleTracking2004(ContentModuleTracking trackingParameters)
        {
            return Execute(trackingParameters, ContentModuleTracking.Method.Update2004);
        }
        public ContentModuleTracking GetContentModuleLessonTracking2004(ContentModuleTracking trackingParameters)
        {
            return Execute(trackingParameters, ContentModuleTracking.Method.GetUserDataXML2004);
        }
    }
}
