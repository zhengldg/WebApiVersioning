using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiVersioning.UriRoutingAttributes.Routes;

namespace SS.Hits.Controllers
{
    [Version1RoutePrefix("Test")]
    public class TestController : ApiController
    {
        [HttpGet, Route("List")]
        public IHttpActionResult List()
        {
            return Json( new { name = "zld" });
        }
    }
}
