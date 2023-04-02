using AutoMapper;
using DALL.Data;
using DALL.DTOs;
using DALL.Repositories.Implements;
using DALL.Services.Implements;
using Serilog;
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
        private readonly ILogger _logger;
        public LogueoController()
        {
            this.mapper = WebApiApplication.mapperConfiguration.CreateMapper();
            _logger = Log.Logger;
        }
        //Peticion que manda a datos a comparar con la tabla usuario y validar si existen para generar toquen
        [HttpPost]
        public async Task<IHttpActionResult> Login(usuariosDTO usuariosDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try {
                var usuarios = await usuariosService.GetAll();
                bool credencial = false;
                foreach (var user in usuarios)
                {
                    if (usuariosDTO.contrasena == user.contrasena && usuariosDTO.usuario == user.usuario)
                    {
                        credencial = true;
                        break;
                    }
                }
                if (credencial == true)
                {
                    var token = TokenGenerator.GenerateTokenJwt(usuariosDTO.usuario);
                    _logger.Information("Petición exitosa a Login Logueo");
                    return Ok(token);
                }
                else
                {
                    _logger.Information("Petición rechazada a Login Logueo (Usuario o Contrasena incorrectos)");
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Petición fallida a Login Logueo: {MensajeDeError}", ex.Message);
                return InternalServerError(ex);
            }


        }
    }
}
