namespace ApiLab.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the response of an API call.
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Initialize a success/fail response
        /// </summary>
        /// <param name="succeeded"></param>
        public ApiResponse(bool succeeded)
        {
            Succeeded = succeeded;
        }

        /// <summary>
        /// Whether the api call was successful
        /// </summary>
        public bool Succeeded { get; set; }
    }

    /// <summary>
    /// Represents the response of a successful API call.
    /// </summary>
    public class ApiSuccessResponse : ApiResponse
    {
        /// <summary>
        /// Initialize a success response with a result.
        /// </summary>
        /// <param name="result">Result of api call.</param>
        public ApiSuccessResponse(object result=null) : base(true)
        {
            Result = result;
        }

        /// <summary>
        /// Result of a successful api call.
        /// </summary>
        public object Result { get; set; }
    }

    /// <summary>
    /// Represents the response of a unsuccessful API call.
    /// </summary>
    public class ApiErrorResponse : ApiResponse
    {
        /// <summary>
        /// Creates an error response with details.
        /// </summary>
        /// <param name="errorCode">Code for this error.</param>
        /// <param name="errorMessage">Description for this error.</param>
        public ApiErrorResponse(string errorMessage, string errorCode = null) : base(false)
        {
            ErrorMessages = errorMessage;
            ErrorCode = errorCode;
        }

        /// <summary>
        /// The error code when the api call was not successful.
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// The error message when the api call was not successful.
        /// </summary>
        public string ErrorMessages { get; set; }
    }
}
