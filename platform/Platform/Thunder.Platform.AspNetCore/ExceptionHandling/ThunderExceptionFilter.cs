using System;
using System.Net;
using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using Thunder.Platform.AspNetCore.Extensions;
using Thunder.Platform.Core.Exceptions;
using Thunder.Platform.Core.Helpers;

namespace Thunder.Platform.AspNetCore.ExceptionHandling
{
    public class ThunderExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }

            var exception = context.Exception;
            if (!HandleAuthenticationException(exception, out ErrorResponse errorResponse) &&
                !HandleBusinessLogicException(exception, out errorResponse) &&
                !UnhandledException(exception, out errorResponse))
            {
                errorResponse = new ErrorResponse(
                    new ErrorInfo
                    {
                        Code = "InternalServerException",
                        Message = exception.ToString(),
                    },
                    HttpStatusCode.BadRequest);
            }

            context.Result = new JsonResult(errorResponse);
            context.HttpContext.Response.StatusCode = errorResponse.StatusCode;
            context.ExceptionHandled = true;
        }

        private bool HandleBusinessLogicException(Exception exception, out ErrorResponse errorResponse)
        {
            var businessLogicException = ExceptionHelper.GetFirstExceptionOfType<BusinessLogicException>(exception);
            if (businessLogicException != null)
            {
                if (businessLogicException is DataValidationException dataValidationError)
                {
                    Log.Logger.Error(exception, $"There is a {nameof(DataValidationException)} during the processing of the request.");
                    errorResponse = new ErrorResponse(dataValidationError.ValidationError, HttpStatusCode.BadRequest);
                }
                else
                {
                    Log.Logger.Error(exception, $"There is a {nameof(BusinessLogicException)} during the processing of the request.");
                    errorResponse = new ErrorResponse(businessLogicException.ToDisplayError(), HttpStatusCode.BadRequest);
                }

                return true;
            }

            errorResponse = null;
            return false;
        }

        private bool UnhandledException(Exception exception, out ErrorResponse errorResponse)
        {
            GeneralException generalException = ExceptionHelper.GetFirstExceptionOfType<GeneralException>(exception);
            if (generalException != null)
            {
                Log.Logger.Error(exception, $"There is a {nameof(GeneralException)} during the processing of the request.");
                errorResponse = new ErrorResponse(generalException.ToDisplayError(), HttpStatusCode.BadRequest);

                return true;
            }

            errorResponse = null;

            return false;
        }

        private bool HandleAuthenticationException(Exception exception, out ErrorResponse errorResponse)
        {
            AuthenticationException authenticationException = ExceptionHelper.GetFirstExceptionOfType<AuthenticationException>(exception);
            if (authenticationException != null)
            {
                Log.Logger.Error(exception, $"There is a {nameof(AuthenticationException)} during the processing of the request.");
                errorResponse = new ErrorResponse(authenticationException.ToDisplayError(), HttpStatusCode.Unauthorized);

                return true;
            }

            errorResponse = null;

            return false;
        }
    }
}
