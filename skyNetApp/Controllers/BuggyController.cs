using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using skyNetApp.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skyNetApp.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")]


        //404
        public ActionResult getNoFoundRequest() {
            var thing = _context.Products.Find(42);
            if (thing == null) {
                return NotFound(new ApiResponse(404));
            }
            return Ok();

        }

        //

        //500
        [HttpGet("servererror")]
        public ActionResult getServerErorr()
        {
            var thing = _context.Products.Find(42);
            //null.tostring==> object reference not set to an object //server error
            var thingToReturn = thing.ToString();
            return Ok();

        }

        //400
        [HttpGet("badrequest")]
        public ActionResult getBadRequest() {
            return BadRequest(new ApiResponse(400));
        }


        //badrequest related to validations

        [HttpGet("badrequest/{id}")]
        public ActionResult getBadRequest(int id) {
            return Ok();
        }


    }
}
