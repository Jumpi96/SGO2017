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
using SGO.Models.ViewModels;

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

        // GET: api/Obras/5/0/0/0/0
        [ResponseType(typeof(ObraInfoViewModel))]
        [Route("api/Obras/{id}/{rubro}/{subrubro}/{item}/{subitem}/{unidad}")]
        public IHttpActionResult GetInfoObra(int id, int rubro, int subrubro, int item, int subitem, int enPesos)
        {
            Obra obra = db.Obra.Find(id);
            if (obra == null)
            {
                return NotFound();
            }

            Dictionary<int,String> rubros = db.Rubro.ToDictionary(x => x.ID, x => x.Nombre);
            Dictionary<int, String> subrubros = db.SubRubro.ToDictionary(x => x.ID, x => x.Nombre);
            Dictionary<int, String> items = db.Item.ToDictionary(x => x.ID, x => x.Nombre);
            Dictionary<int, String> subitems = db.SubItem.ToDictionary(x => x.ID, x => x.Nombre);
            String aEntregar = GetAEntregar(id, rubro, subrubro, item, subitem, enPesos==1);
            String entregado = GetEntregado(id, rubro, subrubro, item, subitem, enPesos == 1);
            String movimientos = GetMovimientos(id, rubro, subrubro, item, subitem, enPesos == 1).ToString();
            String unidad = enPesos==1 ? "$" : GetUnidad(subitem);

            ObraInfoViewModel infoObra = new ObraInfoViewModel
            {
                rubros = rubros,
                subrubros = subrubros,
                items = items,
                subitems = subitems,
                aEntregar = aEntregar,
                entregado = entregado,
                movimientos = movimientos,
                unidad = unidad
            };

            return Ok(infoObra);
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

        /*
         * Devuelve cantidad de un token que se debe entregar en la obra, en una unidad determinada o en $.
         * Token: un subitem, los subitems de un item, todos los subitems de una obra, etc.
        */
        private String GetAEntregar(int obra, int rubro, int subrubro, int item, int subitem, bool enPesos)
        {
            var query =
                    from DS in db.DetalleSubItem
                    join S in db.SubItemDeItem on DS.SubItemDeItemID equals S.ID
                    where DS.ObraID == obra && (S.SubItemID == subitem || subitem == 0)
                                            && (S.ItemID == item || item == 0)
                                            && (S.Item.SubRubroID == subrubro || subrubro == 0)
                                            && (S.Item.SubRubro.RubroID == rubro || rubro == 0)
                    select DS;
            List<DetalleSubItem> detallesSubItem = query.ToList();

            double sumatoria;
            if (enPesos)
            {
                sumatoria = detallesSubItem.Sum(d => d.Cantidad*d.PrecioUnitario);
            }
            else
            {
                sumatoria = detallesSubItem.Sum(d => d.Cantidad);
            }

            return sumatoria.ToString("N2");
        }

        //Devuelve cantidad de un token entregado en la obra, en una unidad determinada o en $.
        private String GetEntregado(int obra, int rubro, int subrubro, int item, int subitem, bool enPesos)
        {
            var queryM =
                from M in db.Movimiento
                join S in db.SubItemDeItem on M.SubItemID equals S.SubItemID
                where M.ObraID == obra && (S.SubItemID == subitem || subitem == 0)
                                        && (S.ItemID == item || item == 0)
                                        && (S.Item.SubRubroID == subrubro || subrubro == 0)
                                        && (S.Item.SubRubro.RubroID == rubro || rubro == 0)
                select M;
            List<Movimiento> movimientos = queryM.ToList();

            double sumatoria;
            if (enPesos)
            {
                sumatoria = movimientos.Sum(m => m.Cantidad *
                                            (from DS in db.DetalleSubItem
                                                 //join SI in db.SubItemDeItem on DS.SubItemDeItemID equals 
                                             where m.SubItemID == DS.SubItemDeItem.SubItemID
                                             orderby DS.PrecioUnitario descending
                                             select DS.PrecioUnitario).First());
            }
            else
            {
                sumatoria = movimientos.Sum(m => m.Cantidad);
            }

            return sumatoria.ToString("N2");
        }

        //Devuelve cantidad de entregas en la obra.
        private int GetMovimientos(int obra, int rubro, int subrubro, int item, int subitem, bool enPesos)
        {
            return
                (from M in db.Movimiento
                 join S in db.SubItemDeItem on M.SubItemID equals S.SubItemID
                 where M.ObraID == obra && (S.SubItemID == subitem || subitem == 0)
                                         && (S.ItemID == item || item == 0)
                                         && (S.Item.SubRubroID == subrubro || subrubro == 0)
                                         && (S.Item.SubRubro.RubroID == rubro || rubro == 0)
                 select M.ID).Distinct().Count();

        }

        private String GetUnidad(int idSubitem)
        {
            SubItem subitem = db.SubItem.Find(idSubitem);
            if (subitem == null)
            {
                return "";
            }
            return subitem.Unidad.Descripcion;
        }
    }
}