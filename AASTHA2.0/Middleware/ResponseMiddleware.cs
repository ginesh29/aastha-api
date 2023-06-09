﻿using AASTHA2.Common;
using AASTHA2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
//using Serilog;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AASTHA2.Middleware
{
    public class ResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                var currentBody = context.Response.Body;

                using (var memoryStream = new MemoryStream())
                {
                    //set the current response to the memorystream.
                    context.Response.Body = memoryStream;
                    await _next(context);
                    var status = context.Response.StatusCode;
                    //reset the body 
                    context.Response.Body = currentBody;
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var readToEnd = new StreamReader(memoryStream).ReadToEnd();
                    object objResult = null;
                    string message = string.Empty;
                    object validation = null;
                    object error = null;
                    var method = context.Request.Method;
                    if (readToEnd == "[]" || status == (int)HttpStatusCode.NotFound)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        message = Messages.NO_DATA_FOUND;
                        objResult = null;
                    }
                    else if (status == (int)HttpStatusCode.OK)
                    {
                        objResult = JsonConvert.DeserializeObject(readToEnd);
                        message = Messages.FETCH_SUCCESS;
                    }
                    else if (status == (int)HttpStatusCode.Created)
                    {
                        objResult = JsonConvert.DeserializeObject(readToEnd);
                        message = method == "POST" ? Messages.RECORD_ADD : method == "PUT" ? Messages.RECORD_UPDATE : Messages.RECORD_DELETE;
                    }
                    else if (status == (int)HttpStatusCode.Unauthorized)
                    {
                        message = JsonConvert.DeserializeObject(readToEnd).ToString();
                        //Log.Warning(message);
                    }
                    else if (status == (int)HttpStatusCode.BadRequest)
                    {
                        message = Messages.VALIDATION_ERROR;
                        validation = ((dynamic)JsonConvert.DeserializeObject(readToEnd)).errors;
                        //Log.Warning(validation.ToString());
                    }
                    else if (status == (int)HttpStatusCode.InternalServerError)
                    {
                        message = Messages.INTERNAL_SERVER_ERROR;
                        error = ((dynamic)JsonConvert.DeserializeObject(readToEnd)).Errors;
                        //Log.Error(error.ToString());
                    }
                    var result = CommonApiResponse.Create((HttpStatusCode)context.Response.StatusCode, objResult, message, validation, error);
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                }
            }
            catch (Exception ex)
            {
                //Log.Error($"Message : { ex.Message},Stacktrace :{ex.StackTrace}");
            }
        }
    }

    public static class ResponseWrapperExtensions
    {
        public static IApplicationBuilder UseResponseWrapper(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseMiddleware>();
        }
    }
}
