using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiLab.Models;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace ApiLab.Controllers
{
    /// <summary>
    /// Handles api/v1/messages/* API logic.
    /// TODO: Validate all path parameters.
    /// TODO: Find optimal way to query SQL database.
    /// TODO: Catch more specific exceptions.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ValidateParameters]
    public class ResourceActivitiesController : Controller
    {
        private ApiLabDatabaseContext dbContext;

        public ResourceActivitiesController(ApiLabDatabaseContext context)
        {
            dbContext = context;
        }

        /// <summary>
        /// Format: GET
        /// Get all activities in database.
        /// </summary>
        [HttpGet]
        public IEnumerable<ResourceActivity> Get()
        {
            return dbContext.ResourceActivities.ToList();
        }

        /// <summary>
        /// Format: GET appName/resourceOwnerId/resourceId/actionerId
        /// A resource owner may access the activities of one user on a resource.
        /// </summary>
        /// <param name="appName">Name of app which activity is involved in.</param>
        /// <param name="resourceOwnerId">Id of user that wants to access activities performed on their resource.</param>
        /// <param name="resourceId">Resource that user performed activities in.</param>
        /// <param name="actionerId">Id of user that performed these activities.</param>
        /// <returns>List of activities performed by the user.</returns>
        [HttpGet("{appName}/{resourceOwnerId}/{resourceId}/{actionerId}")]
        public IEnumerable<ResourceActivity> GetUserActivity(string appName, string resourceOwnerId, string resourceId, string actionerId)
        {
            return (from m in dbContext.ResourceActivities
                    where string.Compare(appName, m.AppName, StringComparison.OrdinalIgnoreCase) == 0
                        && string.Compare(resourceOwnerId, m.ResourceOwnerId, StringComparison.OrdinalIgnoreCase) == 0
                        && string.Compare(resourceId, m.ResourceId, StringComparison.OrdinalIgnoreCase) == 0
                        && string.Compare(actionerId, m.ActionerId, StringComparison.OrdinalIgnoreCase) == 0
                    select m).ToList();
        }

        /// <summary>
        /// Format: GET appName/resourceOwnerId/resourceId
        /// A resource owner may access the activities of all users on a resource.
        /// </summary>
        /// <param name="appName">Name of app which activity is involved in.</param>
        /// <param name="resourceOwnerId">Id of user that wants to access activities performed on their resource.</param>
        /// <param name="resourceId">Resource that user performed activities in.</param>
        /// <returns>List of activities performed by the user.</returns>
        [HttpGet("{appName}/{resourceOwnerId}/{resourceId}")]
        public IEnumerable<ResourceActivity> GetResourceActivity(string appName, string resourceOwnerId, string resourceId)
        {
            return (from m in dbContext.ResourceActivities
                    where string.Compare(appName, m.AppName, StringComparison.OrdinalIgnoreCase) == 0
                        && string.Compare(resourceOwnerId, m.ResourceOwnerId, StringComparison.OrdinalIgnoreCase) == 0
                        && string.Compare(resourceId, m.ResourceId, StringComparison.OrdinalIgnoreCase) == 0
                    select m).ToList();
        }

        /// <summary>
        /// Format: GET appName/resourceOwnerId
        /// A user may access all activities that occur on all resources they own.
        /// </summary>
        /// <param name="appName">Name of app which activity is involved in.</param>
        /// <param name="resourceOwnerId">Id of user that wants to access activities performed on their resource.</param>
        /// <returns>List of activities performed by the user.</returns>
        [HttpGet("{appName}/{resourceOwnerId}")]
        public IEnumerable<ResourceActivity> GetOwnerResourceActivities(string appName, string resourceOwnerId)
        {
            return (from m in dbContext.ResourceActivities
                    where string.Compare(appName, m.AppName, StringComparison.OrdinalIgnoreCase) == 0
                        && string.Compare(resourceOwnerId, m.ResourceOwnerId, StringComparison.OrdinalIgnoreCase) == 0
                    select m).ToList();
        }

        /// <summary>
        /// Format: POST/appName
        /// Stores the activity parameter object into the database.
        /// </summary>
        /// <param name="activity">Activity to be stored into the database.</param>
        [HttpPost("{appName}")]
        public ApiResponse Post(string appName, [FromBody]ResourceActivity activity)
        {
            try
            {
                activity.AppName = appName;
                activity.OccurenceTime = DateTime.UtcNow;

                dbContext.Add(activity);
                dbContext.SaveChanges();

                return new ApiSuccessResponse();
            }
            catch(Exception ex)
            {
                return new ApiErrorResponse(ex.Message);
            }
        }

        /// <summary>
        /// Format:DELETE/appName/resourceOwnerId/resourceId/?deleteUpToId=value
        /// Delete activities under the user defined by parameter actionerId.
        /// Acitivities deleted will be the ones with ids lower than or equal to the parameter deleteUpToId.
        /// </summary>
        /// <param name="appName">Name of app which activity is involved in.</param>
        /// <param name="resourceOwnerId">Id of user that wants to access activities performed on their resource.</param>
        /// <param name="resourceId">Resource in which the deleted activitiies belong to.</param>
        /// <param name="deleteUpToId">Id of activity that will be deleted along with all activities with a lower id.</param>
        /// <returns></returns>
        [HttpDelete("{appName}/{resourceOwnerId}/{resourceId}")]
        public ApiResponse Delete(string appName, string resourceOwnderId, string resourceId, [FromQuery]int deleteUpToId)
        {
            try
            {
                // ToDo: find more optimal method to make deletion in database.
                List<ResourceActivity> activities = (from m in dbContext.ResourceActivities
                                                     where string.Compare(appName, m.AppName, StringComparison.OrdinalIgnoreCase) == 0
                                                        && string.Compare(resourceOwnderId, m.ResourceOwnerId, StringComparison.OrdinalIgnoreCase) == 0
                                                        && string.Compare(resourceId, m.ResourceId, StringComparison.OrdinalIgnoreCase) == 0
                                                        && m.Id <= deleteUpToId
                                                     select m).ToList();
                dbContext.RemoveRange(activities);
                dbContext.SaveChanges();

                return new ApiSuccessResponse();
            }
            catch (Exception ex)
            {
                return new ApiErrorResponse(ex.Message);
            }
        }

        /// <summary>
        /// Format: DELETE/appName/resourceOwnerId/resourceId
        /// Clear all activities of a user's file
        /// </summary>
        /// <param name="appName">Name of app which activity is involved in.</param>
        /// <param name="actionerId">Id of user whose activities will be deleted.</param>
        /// <param name="resourceId">Resource in which the deleted activitiies belong to.</param>
        [HttpDelete("clear/{appName}/{resourceOwnerId}/{resourceId}")]
        public ApiResponse Clear(string appName, string resourceOwnerId, string resourceId)
        {
            try
            {
                // ToDo: find more optimal method to make deletion in database.
                List<ResourceActivity> activities = (from m in dbContext.ResourceActivities
                                                     where string.Compare(appName, m.AppName, StringComparison.OrdinalIgnoreCase) == 0
                                                        && string.Compare(resourceOwnerId, m.ResourceOwnerId, StringComparison.OrdinalIgnoreCase) == 0
                                                        && string.Compare(resourceId, m.ResourceId, StringComparison.OrdinalIgnoreCase) == 0
                                                     select m).ToList();
                dbContext.RemoveRange(activities);
                dbContext.SaveChanges();

                return new ApiSuccessResponse();
            }
            catch(Exception ex)
            {
                return new ApiErrorResponse(ex.Message);
            }
        }
    }
}