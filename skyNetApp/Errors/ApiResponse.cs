using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skyNetApp.Errors
{
    public class ApiResponse
    {

        public ApiResponse()
        {
                
        }
        public ApiResponse(int statusCode, string message=null)
        {
            StatusCode = statusCode;

           //and that mean if message =null execute whatt in the right of ??
            Message = message ?? getMessageForStatusCode(statusCode);

        }

        private string getMessageForStatusCode(int statuscode)
        {

            //new switch statement
            return statuscode switch
            {
                400 => "Bad request",
                401 => "you are not authorized",
                500 => "Internal server error",
                404 => "resource Not Found",
                //degfault
                _ => null

            };
        }

        public int StatusCode { get; set; }

        public string Message { get; set; }
    }
}
