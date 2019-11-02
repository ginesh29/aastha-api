using Newtonsoft.Json;
using System;
using System.Net;

namespace AASTHA2.Models
{
    public class CommonApiResponse
    {
        protected CommonApiResponse(HttpStatusCode statusCode, object result = null, string message = null, object validation = null, object error = null)
        {
            StatusCode = (int)statusCode;
            Result = result;
            Message = message;
            ValidationSummary = validation;
            StatusDescription = Enum.GetName(typeof(HttpStatusCode), statusCode);
            Errors = error;
        }
        public static CommonApiResponse Create(HttpStatusCode statusCode, object result = null, string message = null, object validation = null, object error = null)
        {
            return new CommonApiResponse(statusCode, result, message, validation, error);
        }

        public string Version => "1.0.0";
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Result { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object ValidationSummary { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Errors { get; set; }
    }
    public class Error
    {
        public string ErrorMessage { get; set; }
        public string ErrorDescription { get; set; }
    }
}
