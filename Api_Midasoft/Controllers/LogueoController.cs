using AutoMapper;
using DALL.Data;
using DALL.DTOs;
using DALL.Repositories.Implements;
using DALL.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Api_Midasoft.Controllers
{
    [AllowAnonymous]
    public class LogueoController : ApiController
    {
        private IMapper mapper;
        private readonly usuariosService usuariosService = new usuariosService(new usuariosRepository(MidasoftContext.Create()));

        public LogueoController()
        {
            this.mapper = WebApiApplication.mapperConfiguration.CreateMapper();
        }
        [HttpPost]
        public async Task<IHttpActionResult> Login(usuariosDTO usuariosDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var usuarios = await usuariosService.GetAll();
            bool credencial = false;
            foreach (var user in usuarios)
            {
                if(usuariosDTO.contrasena == user.contrasena && usuariosDTO.usuario == user.usuario)
                {
                    credencial = true;
                    break;
                }  
            }
            if (credencial == true)
            {
                var token = TokenGenerator.GenerateTokenJwt(usuariosDTO.usuario);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }

        }
    }
}
