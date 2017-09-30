using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SGO.Models;

namespace SGO.Controllers
{
    public class UnidadsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Unidads
        public IQueryable<Unidad> GetUnidad()
        {
            return db.Unidad;
        }

        // GET: api/Unidads/5
        [ResponseType(typeof(Unidad))]
        public IHttpActionResult GetUnidad(int id)
        {
            Unidad unidad = db.Unidad.Find(id);
            if (unidad == null)
            {
                return NotFound();
            }

            return Ok(unidad);
        }

        // PUT: api/Unidads/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUnidad(int id, Unidad unidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != unidad.ID)
            {
                return BadRequest();
            }

            db.Entry(unidad).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnidadExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Unidads
        [ResponseType(typeof(Unidad))]
        public IHttpActionResult PostUnidad(Unidad unidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Unidad.Add(unidad);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = unidad.ID }, unidad);
        }

        // DELETE: api/Unidads/5
        [ResponseType(typeof(Unidad))]
        public IHttpActionResult DeleteUnidad(int id)
        {
            Unidad unidad = db.Unidad.Find(id);
            if (unidad == null)
            {
                return NotFound();
            }

            db.Unidad.Remove(unidad);
            db.SaveChanges();

            return Ok(unidad);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UnidadExists(int id)
        {
            return db.Unidad.Count(e => e.ID == id) > 0;
        }
    }
}