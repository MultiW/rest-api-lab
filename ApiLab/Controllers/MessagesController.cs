namespace ApiLab.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using ApiLab.Models;

    /// <summary>
    /// The class handles api/v1/messages/* API logic.
    /// TODO: Validate all path parameters.
    /// TODO: Find optimal way to query SQL database.
    /// TODO: Catch more specific exceptions.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ValidateParameters]
    public class MessagesController : Controller
    {
        private ApiLabDatabaseContext dbContext;

        public MessagesController(ApiLabDatabaseContext context)
        {
            dbContext = context;
        }

        /// <summary>
        /// Gets all messages. 
        /// Format: GET
        /// </summary>
        /// <returns>All messages in the database.</returns>
        [HttpGet]
        public IEnumerable<Message> Get()
        {
            var messages = dbContext.Messages.ToList();
            return messages;
        }

        /// <summary>
        /// Gets new messages of the receipant for a given app.
        /// Format: GET/{appName}/{receiverId}/{ignoreMessageId}
        /// 
        /// </summary>
        /// <param name="appName">The app name</param>
        /// <param name="receiverId">The receipant id</param>
        /// <param name="afterMessageId">The last message Id the client app already received.</param>
        /// <returns>A list of new messages for the receipant since the last message Id.</returns>
        [HttpGet("{appName}/{receiverId}")]
        public IEnumerable<Message> Get(string appName, string receiverId, [FromQuery]int afterMessageId)
        {
            var messages = (from m in dbContext.Messages
                            where string.Compare(appName, m.AppName, StringComparison.OrdinalIgnoreCase) == 0 &&
                                    string.Compare(receiverId, m.ReceiverId, StringComparison.OrdinalIgnoreCase) == 0 &&
                                    m.Id > afterMessageId
                            select m).ToList();

            return messages;
        }

        /// <summary>
        /// Post a message
        /// Format: POST/appName/receiverId
        /// </summary>
        /// <param name="message">A message</param>
        /// <returns>The response indicating whether the operation was successful.</returns>
        [HttpPost("{appName}/{receiverId}")]
        public ApiResponse Post(string appName, string receiverId, [FromBody]Message message)
        {
            try
            {
                message.AppName = appName;
                message.ReceiverId = receiverId;
                message.PostedTime = DateTime.UtcNow;

                this.dbContext.Messages.Add(message);
                this.dbContext.SaveChanges();

                return new ApiSuccessResponse();
            }
            catch (Exception ex)
            {
                // ToDo: We should only capture specific exception type.
                return new ApiErrorResponse(ex.Message);
            }
        }

        /// <summary>
        /// Format: DELETE/appName/receiverId/?deleteUpToId=value
        /// Delete messages received by the user with id receiverId.
        /// All messages with id lower than or equal to parameter deleteUpToId will be deleted.
        /// </summary>
        /// <param name="appName">Name of app which activity is involved in.</param>
        /// <param name="receiverId">Represents the id of the user whose messages will be deleted</param>
        /// <param name="deleteUpToId">All messages with id lower and equal to this will be deleted.</param>
        [HttpDelete("{appName}/{receiverId}")]
        public ApiResponse Delete(string appName, string receiverId, [FromQuery]int deleteUpToId)
        {
            try
            {
                // ToDo: find more optimal method to make deletion in database.
                List<Message> messagesToDelete = (from m in dbContext.Messages
                                                  where string.Compare(appName, m.AppName, StringComparison.OrdinalIgnoreCase) == 0 &&
                                                      string.Compare(receiverId, m.ReceiverId, StringComparison.OrdinalIgnoreCase) == 0 &&
                                                      m.Id <= deleteUpToId
                                                  select m).ToList();

                dbContext.Messages.RemoveRange(messagesToDelete);
                dbContext.SaveChanges();

                return new ApiSuccessResponse();
            }
            catch (Exception ex)
            {
                // ToDo: We should only capture specific exception type.
                return new ApiErrorResponse(ex.Message);
            }
        }
    }
}
