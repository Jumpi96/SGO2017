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

namespace SGO.Controllers.api
{
    public class ObrasController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Obras
        public IQueryable<Obra> GetObra()
        {
            return db.Obra;
        }

        // GET: api/Obras/5
        [ResponseType(typeof(Obra))]
        public IHttpActionResult GetObra(int id)
        {
            Obra obra = db.Obra.Find(id);
            if (obra == null)
            {
                return NotFound();
            }

            return Ok(obra);
        }

        // PUT: api/Obras/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutObra(int id, Obra obra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obra.ID)
            {
                return BadRequest();
            }

            db.Entry(obra).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObraExists(id))
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

        // POST: api/Obras
        [ResponseType(typeof(Obra))]
        public IHttpActionResult PostObra(Obra obra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Obra.Add(obra);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = obra.ID }, obra);
        }

        // DELETE: api/Obras/5
        [ResponseType(typeof(Obra))]
        public IHttpActionResult DeleteObra(int id)
        {
            Obra obra = db.Obra.Find(id);
            if (obra == null)
            {
                return NotFound();
            }

            db.Obra.Remove(obra);
            db.SaveChanges();

            return Ok(obra);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ObraExists(int id)
        {
            return db.Obra.Count(e => e.ID == id) > 0;
        }
    }
}