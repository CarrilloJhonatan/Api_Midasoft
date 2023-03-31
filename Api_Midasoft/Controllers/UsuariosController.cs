using AutoMapper;
using DALL.Data;
using DALL.DTOs;
using DALL.Models;
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
    public class UsuariosController : ApiController
    {
        private IMapper mapper;
        private readonly usuariosService usuariosService = new usuariosService(new usuariosRepository(MidasoftContext.Create()));

        public UsuariosController()
        {
            this.mapper = WebApiApplication.mapperConfiguration.CreateMapper();
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var usuarios = await usuariosService.GetAll();
            var usuariosDTO = usuarios.Select(x => mapper.Map<usuariosDTO>(x));

            return Ok(usuariosDTO);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetById(string usuario)
        {
            var usuarios = await usuariosService.GetById(usuario);

            if (usuarios == null)
                return NotFound();

            var usuariosDTO = mapper.Map<usuariosDTO>(usuarios);

            return Ok(usuariosDTO);
        }

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
 
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

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
             

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string usuario)
        {

            var user = await usuariosService.GetById(usuario);

            if (user == null)
                return NotFound();
            try
            {
                await usuariosService.Delete(usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
    }
}
