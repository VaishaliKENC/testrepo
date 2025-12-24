using AjaxControlToolkit;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.Tls;
using SixLabors.ImageSharp;
using System.Data;
using System.Text.RegularExpressions;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.DataAccessManager.BusinessManager;
using YPLMS2._0.API.DataAccessManager.BusinessManager.Client;
using YPLMS2._0.API.DataAccessManager.BusinessManager.Content;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;
using static YPLMS2._0.API.Entity.Asset;
using static YPLMS2._0.API.Entity.UserAssetVideoTracking;


namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrackingController : ControllerBase
    {


        [HttpPost]
        [Route("getvideotrackingcount")]
        [Authorize]
        public IActionResult GetVideoTracking([FromBody] UpdateVideoTrackingRequest request)
        {
            decimal watchedInmins = 0;

            try
            {
                //string activityId = YPLMS.Services.EncryptionManager.Decrypt(request.ActivityId);
                //string activityType = YPLMS.Services.EncryptionManager.Decrypt(request.ActivityType);
                //string clientId = YPLMS.Services.EncryptionManager.Decrypt(request.ClientId);
                //string learnerId = YPLMS.Services.EncryptionManager.Decrypt(request.LearnerId);

                string activityId = request.ActivityId;
                string activityType = request.ActivityType;
                string clientId = request.ClientId;
                string learnerId = request.LearnerId;


                UserAssetTracking objTracking = new UserAssetTracking();
                UserAssetTrackingManager mgrUserAssetTracking = new UserAssetTrackingManager();

                objTracking.ID = activityId + "-" + learnerId;
                objTracking.ClientId = clientId;
                objTracking.SystemUserGUID = learnerId;
                objTracking.AssetId = learnerId;
                objTracking = mgrUserAssetTracking.Execute(objTracking, UserAssetTracking.Method.Get);

                //if video tracking is completed 100% then play video from starting
                if (Convert.ToString(objTracking.CompletionStatus).ToLower().Trim() != "completed")
                {
                    if (objTracking.WatchedInMins != null)
                        watchedInmins = (decimal)objTracking.BookmarkWatchedInMins * 60;
                }
            }
            catch (Exception ex)
            {
                // Log exception if needed
            }

            return Ok(watchedInmins);
        }

        [HttpPost]
        [Route("updatevideotracking")]
        [Authorize]
        public async Task<IActionResult> UpdateVideoTracking([FromBody] UpdateVideoTrackingRequest request)
        {
            try
            {   
                UserAssetVideoTracking entAssetVideoTracking = new UserAssetVideoTracking();
                UserAssetVideoTrackingManager entAssetVideoMgr = new UserAssetVideoTrackingManager();
                entAssetVideoTracking.ClientId = request.ClientId;
                entAssetVideoTracking.ID = request.ActivityId + "-" + request.LearnerId;
                entAssetVideoTracking = entAssetVideoMgr.Execute(entAssetVideoTracking, UserAssetVideoTracking.Method.GetVideoTracking);
                if (entAssetVideoTracking != null)
                {
                    request.Counter = entAssetVideoTracking.ListRange.TotalRows + 1;
                }

                string activityId = request.ActivityId;
                string activityType = request.ActivityType;
                string clientId = request.ClientId;
                string learnerId = request.LearnerId;
                

                UserAssetTracking tracking = new UserAssetTracking
                {
                    ID = activityId + "-" + learnerId,
                    ClientId = clientId,
                    SystemUserGUID = learnerId,
                    AssetId = activityId
                };

                UserAssetTrackingManager trackingMgr = new UserAssetTrackingManager();
                tracking = trackingMgr.Execute(tracking, UserAssetTracking.Method.Get);

                //if (tracking.CompletionStatus?.ToLower()?.Trim() != "completed")
                if (tracking.CompletionStatus != ActivityCompletionStatus.Completed)                    
                {
                    decimal watchedInMins = Math.Round(Convert.ToDecimal(request.ElaspedTime), 2);
                    decimal totalDuration = Math.Round(Convert.ToDecimal(request.TotalDuration), 2);

                    UserAssetVideoTracking videoTracking = new UserAssetVideoTracking
                    {   
                        AttemptId = activityId + "-" + learnerId,
                        ClientId = clientId,
                        SystemUserGUID = learnerId,
                        AssetId = activityId,
                        ActivityName = request.ActivityName,
                        DateOfStart = DateTime.UtcNow,
                        TotalVideoDurationInMins = Math.Round(totalDuration / 60, 2),
                        WatchedInMins = Math.Round(watchedInMins / 60, 2)
                    };

                    UserAssetVideoTrackingManager videoTrackingMgr = new UserAssetVideoTrackingManager();

                    if (request.VideoEvent?.ToLower()?.Trim() != "close")
                    {
                        videoTracking.AttemptVideoId = activityId + "-" + learnerId + "-" + request.Counter.ToString();
                        videoTracking = videoTrackingMgr.Execute(videoTracking, UserAssetVideoTracking.Method.Add);
                    }
                    else
                    {
                        // update bookmarking
                        UserAssetTracking bookmarkTracking = new UserAssetTracking
                        {
                            ClientId = clientId,
                            SystemUserGUID = learnerId,
                            AssetId = activityId,
                            BookmarkWatchedInMins = Math.Round(watchedInMins / 60, 2),
                            TotalVideoDurationInMins = Math.Round(totalDuration / 60, 2)
                        };

                        bookmarkTracking = trackingMgr.Execute(bookmarkTracking, UserAssetTracking.Method.UpdateVideoBookmark);
                    }
                }
                return Ok(new { Code = 200, Msg = "Video tracking updated successfully" });                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Code = 500, Msg = $"Internal Server Error: {ex.Message}" });                
            }
        }

    }
}
