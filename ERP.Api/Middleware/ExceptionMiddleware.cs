
using ERP.Api.Presentation.Contracts.Responses;
using ERP.Shared.Exceptions;
using System.Text.Json;

namespace ERP.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException ex)
            {
                await HandleBusinessException(context, ex);
            }
            catch (Exception)
            {
                await HandleException(context);
            }
        }

        private static async Task HandleBusinessException(HttpContext context, BusinessException ex)
        {
            context.Response.StatusCode = ex._status_code;
            context.Response.ContentType = "application/json";

            var response = new ApiResponse<object>
            {
                success = false,
                status_code = ex._status_code,
                message = ex.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static async Task HandleException(HttpContext context)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var response = new ApiResponse<object>
            {
                success = false,
                status_code = 500,
                message = "Error interno del servidor"
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
