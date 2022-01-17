using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skyNetApp.Errors
{
    public class ApiValidaionErrorResponse : ApiResponse
    {


        //error of validattion get from [apiconroller] attruibutte
        public IEnumerable<string> Errors { get; set; }
        public ApiValidaionErrorResponse() : base(400)
        {
        }
    }
}
