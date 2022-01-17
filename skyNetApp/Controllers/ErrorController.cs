using Microsoft.AspNetCore.Mvc;
using skyNetApp.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skyNetApp.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi =true)]

    public class ErrorController : BaseApiController
    {


        public ActionResult Error(int code) {
            return new ObjectResult(new ApiResponse(code));
        }

    }
}
