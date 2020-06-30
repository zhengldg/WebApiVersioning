using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyNamespace.Controllers
{
    [RoutePrefix("ping")]
    public class PingController : ApiController
    {
        [HttpGet, Route("")]
        public IHttpActionResult Ping()
        {
            return Ok("Pong");
        }
    }
}
