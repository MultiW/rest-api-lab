using ApiLab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLab
{
    /// <summary>
    /// Attribute for controllers to validate request parameters before every HTTP request.
    /// </summary>
    public class ValidateParametersAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Perform validation of parameters before every HTTP request.
        /// Returns to request origin a list of errors if validation fails.
        /// </summary>
        /// <param name="context">Context of this HTTP request.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                IEnumerable<ModelError> errorList = context.ModelState.SelectMany(x => x.Value.Errors);
                string result = "";
                foreach (ModelError error in errorList)
                {
                    result += error.ErrorMessage;
                    result += Environment.NewLine;
                }
                context.Result = new BadRequestObjectResult(new ApiErrorResponse(result));
            }

            base.OnActionExecuting(context);
        }
    }
}
