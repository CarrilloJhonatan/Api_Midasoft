using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DALL.Data;
using DALL.Services.Implements;
using DALL.Repositories.Implements;
using DALL.DTOs;
using System.Threading.Tasks;
using AutoMapper;
using DALL.Models;
using Serilog;

namespace Api_Midasoft.Controllers
{
    [Authorize]
    public class GrupoFamiliarController : ApiController
    {
        private IMapper mapper;
        private readonly grupo_familiarService grupo_FamiliarService = new grupo_familiarService(new grupo_familiarRepository(MidasoftContext.Create()));
        private readonly ILogger _logger;
        public GrupoFamiliarController()
        {
            this.mapper = WebApiApplication.mapperConfiguration.CreateMapper();
            _logger = Log.Logger;
        }
            
        //Peticion que me trae todos los registros de grupo_familiar
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var grupofamiliar = await grupo_FamiliarService.GetAll();
                var grupofamiliarDTO = grupofamiliar.Select(x => mapper.Map<grupo_familiarDTO>(x));
                _logger.Information("Petición exitosa a GetAll GrupoFamiliar");
                return Ok(grupofamiliarDTO);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Petición fallida a GetAll GrupoFamiliar: {MensajeDeError}", ex.Message);
                return InternalServerError(ex);
            }

        }
        //Peticion que me trae los registros por cedula de grupo_familiar
        [HttpGet]
        public async Task<IHttpActionResult> GetById(string cedula)
        {
            try
            {
                var grupofamiliar = await grupo_FamiliarService.GetById(cedula);

                if (grupofamiliar == null)
                    return NotFound();

                var grupofamiliarDTO = mapper.Map<grupo_familiarDTO>(grupofamiliar);
                _logger.Information("Petición exitosa a GetById GrupoFamiliar");
                return Ok(grupofamiliarDTO);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Petición fallida a GetById GrupoFamiliar: {MensajeDeError}", ex.Message);
                return InternalServerError(ex);
            }

        }
        //Peticion que me inserta registro de grupo_familiar
        [HttpPost]
        public async Task<IHttpActionResult> Insert(grupo_familiarDTO grupo_FamiliarDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (grupo_FamiliarDTO.edad < 18 && grupo_FamiliarDTO.fecha_nacimiento == null)
                return BadRequest("Es menor de edad, el campo fecha_nacimiento es Requerido");
            try
            {
                var grupofamiliar = mapper.Map<grupo_familiar>(grupo_FamiliarDTO);
                if (grupofamiliar.edad < 18)
                {
                    grupofamiliar.fecsys = DateTime.Now;
                    grupofamiliar.menor_edad = "SI";
                    grupofamiliar = await grupo_FamiliarService.Insert(grupofamiliar);
                }
                else
                {
                    grupofamiliar.menor_edad = "";
                    grupofamiliar.fecha_nacimiento = null;
                    grupofamiliar.fecsys = DateTime.Now;
                    grupofamiliar = await grupo_FamiliarService.Insert(grupofamiliar);
                }
                _logger.Information("Petición exitosa a Insert GrupoFamiliar");
                return Ok(grupofamiliar);
            } catch (Exception ex)
            {
                _logger.Error(ex, "Petición fallida a Insert GrupoFamiliar: {MensajeDeError}", ex.Message);
                return InternalServerError(ex);
            }
          
        }
        //Peticion que me actualiza registro por cedula de grupo_familiar
        [HttpPut]
        public async Task<IHttpActionResult> Update(grupo_familiarDTO grupo_FamiliarDTO, string cedula)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (grupo_FamiliarDTO.edad < 18 && grupo_FamiliarDTO.fecha_nacimiento == null)
                return BadRequest("Es menor de edad, el campo fecha_nacimiento es Requerido");

            if (grupo_FamiliarDTO.cedula != cedula)
                return BadRequest();
            var familiar = await grupo_FamiliarService.GetById(cedula);

            if (familiar == null)
                return NotFound();
            try
                {
                      var  grupofamiliar = mapper.Map<grupo_familiar>(grupo_FamiliarDTO);
                    if (grupofamiliar.edad < 18)
                    {
                        grupofamiliar.fecsys = DateTime.Now;
                        grupofamiliar.menor_edad = "SI";
                        grupofamiliar = await grupo_FamiliarService.Update(grupofamiliar);
                    }
                    else
                    {
                        grupofamiliar.menor_edad = "";
                        grupofamiliar.fecha_nacimiento = null;
                        grupofamiliar.fecsys = DateTime.Now;
                        grupofamiliar = await grupo_FamiliarService.Update(grupofamiliar);
                    }
                _logger.Information("Petición exitosa a Update GrupoFamiliar");
                return Ok(grupofamiliar);
                }
            catch (Exception ex)
            {
                _logger.Error(ex, "Petición fallida a Update GrupoFamiliar: {MensajeDeError}", ex.Message);
                return InternalServerError(ex);
            }

        }
        //Peticion que me elimina registro por cedula de grupo_familiar
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string cedula)
        {
        
            var familiar = await grupo_FamiliarService.GetById(cedula);

            if (familiar == null)
                return NotFound();
            try
            {
                await grupo_FamiliarService.Delete(cedula);
                _logger.Information("Petición exitosa a Delete GrupoFamiliar");
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Petición fallida a Delete GrupoFamiliar: {MensajeDeError}", ex.Message);
                return InternalServerError(ex);
            }

        }

    }
}
