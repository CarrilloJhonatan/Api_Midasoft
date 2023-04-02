using AutoMapper;
using DALL.Data;
using DALL.DTOs;
using DALL.Models;
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
    public class UsuariosController : ApiController
    {
        private IMapper mapper;
        private readonly usuariosService usuariosService = new usuariosService(new usuariosRepository(MidasoftContext.Create()));
        private readonly ILogger _logger;

        public UsuariosController()
        {
            this.mapper = WebApiApplication.mapperConfiguration.CreateMapper();
            _logger = Log.Logger;
        }

        //Peticion que me trae todos los registros de usuarios
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var usuarios = await usuariosService.GetAll();
                var usuariosDTO = usuarios.Select(x => mapper.Map<usuariosDTO>(x));
                _logger.Information("Petición exitosa a GetAll Usuraios");
                return Ok(usuariosDTO);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Petición fallida a GetAll Usuraios: {MensajeDeError}", ex.Message);
                return InternalServerError(ex);
            }

        }
        //Peticion que me trae los registros por cedula de usuarios
        [HttpGet]
        public async Task<IHttpActionResult> GetById(string usuario)
        {
            try
            {
                var usuarios = await usuariosService.GetById(usuario);

                if (usuarios == null)
                    return NotFound();

                var usuariosDTO = mapper.Map<usuariosDTO>(usuarios);
                _logger.Information("Petición exitosa a GetById Usuarios");
                return Ok(usuariosDTO);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Petición fallida a GetById Usuraios: {MensajeDeError}", ex.Message);
                return InternalServerError(ex);
            }

        }
        //Peticion que me inserta registro de usuarios
        [HttpPost]
        public async Task<IHttpActionResult> Insert(usuariosDTO usuariosDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var usuarios = mapper.Map<usuarios>(usuariosDTO);
                usuarios.fecsys = DateTime.Now;
                usuarios = await usuariosService.Insert(usuarios);
                _logger.Information("Petición exitosa a Insert Usuarios");
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Petición fallida a Insert Usuarios: {MensajeDeError}", ex.Message);
                return InternalServerError(ex);
            }

        }
        //Peticion que me actualiza registro por cedula de usuarios
        [HttpPut]
        public async Task<IHttpActionResult> Update(usuariosDTO usuariosDTO, string usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (usuariosDTO.usuario != usuario)
                return BadRequest();
            var user = await usuariosService.GetById(usuario);

            if (user == null)
                return NotFound();
            try
            {
                var usuarios = mapper.Map<usuarios>(usuariosDTO);

                usuarios.fecsys = DateTime.Now;

                usuarios = await usuariosService.Update(usuarios);

                _logger.Information("Petición exitosa a Update Usuarios");
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Petición fallida a Update Usuarios: {MensajeDeError}", ex.Message);
                return InternalServerError(ex);
            }

        }
        //Peticion que me elimina registro por cedula de usuarios
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string usuario)
        {

            var user = await usuariosService.GetById(usuario);

            if (user == null)
                return NotFound();
            try
            {
                await usuariosService.Delete(usuario);
                _logger.Information("Petición exitosa a Delete Usuarios");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Petición fallida a Delete Usuarios: {MensajeDeError}", ex.Message);
                return InternalServerError(ex);
            }

        }
    }
}
