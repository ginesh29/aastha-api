using AASTHA2.Common;
using AASTHA2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AASTHA2.Middleware
{
    public class ExceptionWrapper
    {
        private readonly RequestDelegate _next;

        public ExceptionWrapper(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                var statusCode = (int)HttpStatusCode.InternalServerError;
                response.ContentType = "application/json";
                response.StatusCode = statusCode;
                var result = CommonApiResponse.Create((HttpStatusCode)context.Response.StatusCode, null, Messages.INTERNAL_SERVER_ERROR, null, new Error { ErrorMessage = ex.Message, ErrorDescription = ex.StackTrace });
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }
    }
    public static class ExceptionWrapperExtensions
    {
        public static IApplicationBuilder UseExceptionWrapper(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionWrapper>();
        }
    }
}
