using Eshop.Shared.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Eshop.Shared
{
    public class CommonResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public CommonResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var originalBodyStream = context.Response.Body;
                using var newResponseBody = new MemoryStream();
                context.Response.Body = newResponseBody;

                await _next(context);

                // Only wrap JSON + successful responses
                if (context.Response.ContentType != null &&
                    (context.Response.ContentType.Contains("application/json") || context.Response.ContentType.Contains("application/problem+json")))
                    
                {
                    newResponseBody.Seek(0, SeekOrigin.Begin);
                    var bodyText = await new StreamReader(newResponseBody).ReadToEndAsync();

                    var traceId = context.TraceIdentifier;

                    // Deserialize original JSON as plain object
                    var data = JsonSerializer.Deserialize<object>(bodyText);

                    ApiResponse<object> wrapped;
                    if (context.Response.StatusCode >= 200 && context.Response.StatusCode <= 299)
                    {
                        wrapped = ApiResponse<object>.SuccessResponse(data, traceId);
                    }
                    else if(context.Response.StatusCode == 400)
                    {
                        wrapped = ApiResponse<object>.FailureResponse("Bad Request",traceId);
                    }
                    else if(context.Response.StatusCode == 404)
                    {
                        wrapped = ApiResponse<object>.FailureResponse("Not Found",traceId);
                    }
                    else if(context.Response.StatusCode == 500)
                    {
                        wrapped = ApiResponse<object>.FailureResponse("Internal Server Error",traceId);
                    }
                    else
                    {
                        wrapped = ApiResponse<object>.FailureResponse($"Error {context.Response.StatusCode}", traceId);
                    }


                    // Write wrapped JSON
                    context.Response.ContentType = "application/json";
                    context.Response.Body = originalBodyStream;

                    var json = JsonSerializer.Serialize(wrapped);
                    await context.Response.WriteAsync(json);
                }
                else 
                {
                    // Non-JSON → just pass through
                    newResponseBody.Seek(0, SeekOrigin.Begin);
                    await newResponseBody.CopyToAsync(originalBodyStream);
                }
            }
            catch (Exception ex)
            {
                var exception = ex.GetType().Name;
                var originalBodyStream = context.Response.Body;
                using var newResponseBody = new MemoryStream();
                context.Response.Body = newResponseBody;
                if (exception == nameof(NotFoundException))
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }
                context.Response.ContentType = "application/json";

                var response = ApiResponse<object>.FailureResponse(
                    "An unexpected error occurred.",
                    context.TraceIdentifier
                );

                var json = JsonSerializer.Serialize(response);
               
                    await context.Response.WriteAsync(json);
                
            }
        }
    }
}
