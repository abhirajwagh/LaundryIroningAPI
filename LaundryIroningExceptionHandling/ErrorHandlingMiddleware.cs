using LaundryIroningLogging;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LaundryIroningExceptionHandling
{
    public class ErrorHandlingMiddleware
    {
        #region Exception Messages
        public const string UnauthorizedAccessException = "You are not authorized user, Please contact site admin";
        public const string UnauthorizedAccessExceptionForEdit = "You are not authorized to perform edit, Please contact site admin";
        public const string InvalidAccessTokenException = "Invalid Token Details";
        #region ExceptionType
        public const string UnauthorizedAccess = "UnauthorizedAccess";
        public const string UnauthorizedAccessForEdit = "UnauthorizedAccessForEdit";
        public const string InvalidAccessToken = "InvalidAccessToken";
        public const string InternalServerError = "InternalServerError";
        #endregion
        #endregion
        #region Private Variables
        private readonly RequestDelegate _next;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next">It will get populated by framework</param>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        #endregion

        #region Public Methods

        public async Task Invoke(HttpContext context /* other scoped dependencies */)
        {
            try
            {
                // call next middleware
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handle exception and provide appropriate response
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new { error = exception.Message });


            //LoggerTest.Log(LogTypes.ERROR, "ErrorHandlingMiddleware", "HandleExceptionAsync", exception.InnerException == null ? exception.Message : exception.InnerException.Message, exception.StackTrace);

            switch (exception.Message)
            {
                case UnauthorizedAccess:
                    code = HttpStatusCode.Unauthorized;
                    result = JsonConvert.SerializeObject(new { error = UnauthorizedAccessException });
                    Logger.Log(LogType.ERROR, "ErrorHandlingMiddleware", "HandleExceptionAsync", exception.InnerException == null ? exception.Message : exception.InnerException.Message, exception.StackTrace);
                    break;
                case InvalidAccessToken:
                    code = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(new { error = InvalidAccessTokenException });
                    Logger.Log(LogType.ERROR, "ErrorHandlingMiddleware", "HandleExceptionAsync", exception.InnerException == null ? exception.Message : exception.InnerException.Message, exception.StackTrace);
                    break;
                case InternalServerError:
                    code = HttpStatusCode.InternalServerError;
                    result = JsonConvert.SerializeObject(new { error = exception.InnerException.Message });
                    Logger.Log(LogType.ERROR, "ErrorHandlingMiddleware", "HandleExceptionAsync", exception.InnerException == null ? exception.Message : exception.InnerException.Message, exception.StackTrace);
                    break;
                case UnauthorizedAccessForEdit:
                    code = HttpStatusCode.Unauthorized;
                    result = JsonConvert.SerializeObject(new { error = UnauthorizedAccessExceptionForEdit });
                    Logger.Log(LogType.ERROR, "ErrorHandlingMiddleware", "HandleExceptionAsync", exception.InnerException == null ? exception.Message : exception.InnerException.Message, exception.StackTrace);
                    break;

                default:
                    Logger.Log(LogType.ERROR, "ErrorHandlingMiddleware", "HandleExceptionAsync", exception.InnerException == null ? exception.Message : exception.InnerException.Message, exception.StackTrace);
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(result);
        }
        #endregion
    }
}
