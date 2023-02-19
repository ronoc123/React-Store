using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace react_store.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : BaseApiController
    {


        [HttpGet("not-found")]
        public  ActionResult GetNotFound()
        {
            return NotFound();
        }
        [HttpGet("bad-request")]

        public  ActionResult GetBadRequest()
        {
            return BadRequest("This is a Bad Request");
        }
        [HttpGet("unauthorized")]
        public  ActionResult GetUnauthorized()
        {
            return Unauthorized();
        }
        [HttpGet("validation-error")]
        public  ActionResult GetValidationError()
        {
            ModelState.AddModelError("Problem1", "This is the first Error");
            ModelState.AddModelError("Problem2", "This is a second Error");
            return ValidationProblem();
        }
        [HttpGet("server-error")]
        public  ActionResult GetServerError()
        {
            throw new Exception("THis is a server error");
        }
    }
}