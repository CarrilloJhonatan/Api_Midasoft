using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api_Midasoft.Controllers
{
    [AllowAnonymous]
    public class LogueoController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Login()
        {
            return Ok();
        }
    }
}
