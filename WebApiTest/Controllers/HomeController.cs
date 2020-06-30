using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiTest.Controllers
{
    [RoutePrefix("api/Home2")]
    public class HomeController : ApiController
    {
        [HttpGet]
        [Route("Index2")]
        public IHttpActionResult Index()
        {
            return Ok("trust");
        }
    }
}
