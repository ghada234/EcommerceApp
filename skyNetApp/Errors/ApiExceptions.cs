using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skyNetApp.Errors
{
    public class ApiExceptions:ApiResponse
    {

        public string Details{ get; set; }
        public ApiExceptions(int statusCode,string message=null,string details=null):base(statusCode,message)
        {
            Details = details;        
        }


    }
}
