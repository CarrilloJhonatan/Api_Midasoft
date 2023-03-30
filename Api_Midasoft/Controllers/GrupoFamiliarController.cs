using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DALL;
using Newtonsoft.Json;

namespace Api_Midasoft.Controllers
{
    public class GrupoFamiliarController : ApiController
    {
        // GET: api/GrupoFamiliar
        public IEnumerable<grupo_familiar> Get()
        {
            using (prueba_midasoftEntities db = new prueba_midasoftEntities())
            {
                return db.grupo_familiar.ToList();
            }
        }

        // GET: api/GrupoFamiliar/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GrupoFamiliar
        public HttpResponseMessage Post([FromBody]grupo_familiar fami)
        {
            int resp = 0;
            HttpResponseMessage ms = null;
            try
            {
                using (prueba_midasoftEntities entities = new prueba_midasoftEntities())
                {
                    if (fami.edad < 18)
                    {
                        fami.fecsys = DateTime.Now;
                        fami.menor_edad = "SI";
                        entities.Entry(fami).State = EntityState.Added;
                        resp = entities.SaveChanges();
                        ms = Request.CreateResponse(HttpStatusCode.OK, resp);
                    }
                    else
                    {
                        fami.menor_edad = "";
                        fami.fecha_nacimiento = null;
                        fami.fecsys = DateTime.Now;
                        entities.Entry(fami).State = EntityState.Added;
                        resp = entities.SaveChanges();
                        ms = Request.CreateResponse(HttpStatusCode.OK, resp);
                    }
                }
            }
            catch (Exception ex)
            {
                ms = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return ms;
        }

        // PUT: api/GrupoFamiliar/5
        public HttpResponseMessage Put([FromBody] grupo_familiar fami)
        {
            int resp = 0;
            HttpResponseMessage ms = null;
            try
            {
                using (prueba_midasoftEntities entities = new prueba_midasoftEntities())
                {
                    if (fami.edad < 18)
                    {
                        fami.fecsys = DateTime.Now;
                        fami.menor_edad = "SI";
                        entities.Entry(fami).State = EntityState.Modified;
                        resp = entities.SaveChanges();
                        ms = Request.CreateResponse(HttpStatusCode.OK, resp);
                    }
                    else
                    {
                        fami.menor_edad = "";
                        fami.fecha_nacimiento = null;
                        fami.fecsys = DateTime.Now;
                        entities.Entry(fami).State = EntityState.Modified;
                        resp = entities.SaveChanges();
                        ms = Request.CreateResponse(HttpStatusCode.OK, resp);
                    }
                }
            }
            catch (Exception ex)
            {
                ms = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return ms;
        }

        // DELETE: api/GrupoFamiliar/5
        public HttpResponseMessage Delete([FromBody] grupo_familiar fami)
        {
            int resp = 0;
            HttpResponseMessage ms = null;
            try
            {
                using (prueba_midasoftEntities entities = new prueba_midasoftEntities())
                {

                        entities.Entry(fami).State = EntityState.Deleted;
                        resp = entities.SaveChanges();
                        ms = Request.CreateResponse(HttpStatusCode.OK, resp);
                }
            }
            catch (Exception ex)
            {
                ms = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return ms;
        }
    }
}
