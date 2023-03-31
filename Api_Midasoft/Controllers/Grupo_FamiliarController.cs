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

namespace Api_Midasoft.Controllers
{
    public class Grupo_FamiliarController : ApiController
    {
        private IMapper mapper;
        private readonly grupo_familiarService grupo_FamiliarService = new grupo_familiarService(new grupo_familiarRepository(MidasoftContext.Create()));

        public Grupo_FamiliarController()
        {
            this.mapper = WebApiApplication.mapperConfiguration.CreateMapper();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var grupofamiliar = await grupo_FamiliarService.GetAll();
            var grupofamiliarDTO = grupofamiliar.Select(x => mapper.Map<grupo_familiarDTO>(x));

            return Ok(grupofamiliarDTO);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetById(string cedula)
        {
            var grupofamiliar = await grupo_FamiliarService.GetById(cedula);

            if (grupofamiliar == null)
                return NotFound();

            var grupofamiliarDTO = mapper.Map<grupo_familiarDTO>(grupofamiliar);

            return Ok(grupofamiliarDTO);
        }

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

                return Ok(grupofamiliar);
            } catch (Exception ex)
            {
                return InternalServerError(ex);
            }
          
        }

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

                    return Ok(grupofamiliar);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }

        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string cedula)
        {
        
            var familiar = await grupo_FamiliarService.GetById(cedula);

            if (familiar == null)
                return NotFound();
            try
            {
                await grupo_FamiliarService.Delete(cedula);
                return Ok();
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }

        }

    }
}
