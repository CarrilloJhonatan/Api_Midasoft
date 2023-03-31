using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DALL.Data;
using DALL.Services.Implements;
using DALL.Repositories.Implements;
using System.Threading.Tasks;

namespace Api_Midasoft.Controllers
{
    public class Grupo_FamiliarController : ApiController
    {
        private readonly grupo_familiarService grupo_FamiliarService = new grupo_familiarService(new grupo_familiarRepository(MidasoftContext.Create()));

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {

            var grupofamiliar = await grupo_FamiliarService.GetAll();
            

            return Ok(grupofamiliar);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetById(string cedula)
        {
            var grupofamiliar = await grupo_FamiliarService.GetById(cedula);

            if (grupofamiliar == null)
                return NotFound();


            return Ok(grupofamiliar);
        }
    }
}
