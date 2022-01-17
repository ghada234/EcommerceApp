using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using skyNetApp.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace skyNetApp.MiddleWare
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleWare(RequestDelegate next, ILogger<ExceptionMiddleWare> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpcontext)
        {

            // if there was no exceptionwe want the middleware to go the next piece of middleware
            try { await _next(httpcontext); }

            catch (Exception ex)
            {

                //get it in console
                _logger.LogError(ex, ex.Message);
                httpcontext.Response.ContentType = "applicaion/json";

                //get 500 as status code
                httpcontext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = _env.IsDevelopment() ? new ApiExceptions((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiExceptions((int)HttpStatusCode.InternalServerError);


                //convert to camelcase

                var options = new JsonSerializerOptions { PropertyNamingPolicy=JsonNamingPolicy.CamelCase};
                //convert response tojson
                var responseJson = JsonSerializer.Serialize(response,options);
                await httpcontext.Response.WriteAsync(responseJson);
                    
        

            }
        }
    }
}
