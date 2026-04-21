using Eshop.Shared.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

namespace Eshop.Shared
{
    public class CommonResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CommonResponseMiddleware> _logger;

        public CommonResponseMiddleware(RequestDelegate next, ILogger<CommonResponseMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBody = context.Response.Body;
            using var newBody = new MemoryStream();
            context.Response.Body = newBody;
            ApiResponse<object> response = new ApiResponse<object>();
            try
            {
                await _next(context);

                if (context.Response.StatusCode == StatusCodes.Status204NoContent)
                {
                    context.Response.Body = originalBody;
                    return;
                }
                newBody.Seek(0, SeekOrigin.Begin);
                var bodyText = await new StreamReader(newBody).ReadToEndAsync();

                object? data = null;
                string? message = null;

                if (!string.IsNullOrWhiteSpace(bodyText))
                {
                    try
                    {
                        data = JsonSerializer.Deserialize<object>(bodyText);
                    }
                    catch
                    {
                        data = bodyText;
                    }
                }

                var traceId = context.TraceIdentifier;
                

                if (context.Response.StatusCode is >= 200 and < 300)
                    response = ApiResponse<object>.SuccessResponse(data, traceId);
                else
                response = ApiResponse<object>.FailureResponse(GetMessage(context.Response.StatusCode), traceId);
            

            context.Response.ContentType = "application/json";
                context.Response.Body = originalBody;

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
         "Unhandled exception occurred while processing request. TraceId: {TraceId}, Path: {Path}",
         context.TraceIdentifier,
         context.Request.Path);
                context.Response.Body = originalBody;
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                response = ApiResponse<object>.FailureResponse(GetMessage(context.Response.StatusCode), context.TraceIdentifier);
               
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }

        private static string GetMessage(int statusCode) =>
            statusCode switch
            {
                200 => "Success",
                201 => "Created",
                204 => "Deleted",
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => $"Error {statusCode}"
            };
    }

}
