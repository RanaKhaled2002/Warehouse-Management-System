using System.Net;
using WarehouseBLL.Exceptions;

namespace WarehouseAPI.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException ex)
            {

                await HandleExceptionAsync(context, new
                {
                    message = ex.Message,
                    statusCode = (int)ex.StatusCode
                }, (HttpStatusCode)ex.StatusCode);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, new
                {
                    message = ex.Message,
                    innerException = ex.InnerException?.Message,
                    stackTrace = ex.StackTrace
                }, HttpStatusCode.InternalServerError);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, object errorResponse, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
