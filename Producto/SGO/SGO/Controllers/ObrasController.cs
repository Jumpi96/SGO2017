using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SGO.Models;
using SGO.Models.ViewModels;

namespace SGO.Controllers
{
    public class ObrasController : Controller
    {
        private Entities db = new Entities();

        // GET: Obras
        public ActionResult Index()
        {
            var obra = db.Obra.Include(o => o.Usuario).Include(o => o.Cliente1).Include(o => o.Departamento);
            return View(obra.ToList());
        }



        // GET: Obras/Details/5/0/0/0/0/1
        public ActionResult Details(int id, int rubro, int subrubro, int item, int subitem, bool enPesos)
        {
            Obra obra = db.Obra.Find(id);
            if (obra == null)
            {
                return HttpNotFound();
            }
            ActualizarIdsDeCombos(ref rubro, ref subrubro, ref item, ref subitem);
            var obraInfoVM = new ObraInfoViewModel
            {
                ObraID = id,
                NombreObra = obra.Nombre,
                SelectedEnPesos = true,
                Rubros = GetRubros(id),
                Subrubros = GetSubRubros(id, rubro),
                Items = GetItems(id, rubro, subrubro),
                Subitems = GetSubItems(id, rubro, subrubro, item),
                Movimientos = GetMovimientos(id,rubro,subrubro,item,subitem,enPesos).ToString(),
                Entregado = 100,
                AEntregar = 20,
                SelectedItemID = item,
                SelectedRubroID = rubro,
                SelectedSubItemID = subitem,
                SelectedSubRubroID = subrubro
            };
            return View(obraInfoVM);
        }

        // GET: Obras
        public ActionResult Procesar()
        {
            Usuario usuario = new Usuario();
            ScraperService s = new ScraperService(usuario);
            s.ProcesarDocumento("D:/Code/SGO/Proyecto/Archivos/Editando1.xls", 3, 4);

            //return View(await _context.Obra.ToListAsync());
            return View();
        }

        // GET: Obras/Create
        public ActionResult Create()
        {
            ViewBag.ModUsuarioID = new SelectList(db.Usuario, "ID", "NombreUsuario");
            ViewBag.ClienteID = new SelectList(db.Cliente, "ID", "Nombre");
            ViewBag.DepartamentoID = new SelectList(db.Departamento, "ID", "Descripcion");
            return View();
        }

        // POST: Obras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Cliente,Coeficiente,InsFecha,ModUsuarioID,Nombre,Finalizada,ModFecha,DepartamentoID,ClienteID")] Obra obra)
        {
            if (ModelState.IsValid)
            {
                db.Obra.Add(obra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ModUsuarioID = new SelectList(db.Usuario, "ID", "NombreUsuario", obra.ModUsuarioID);
            ViewBag.ClienteID = new SelectList(db.Cliente, "ID", "Nombre", obra.ClienteID);
            ViewBag.DepartamentoID = new SelectList(db.Departamento, "ID", "Descripcion", obra.DepartamentoID);
            return View(obra);
        }

        // GET: Obras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Obra obra = db.Obra.Find(id);
            if (obra == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModUsuarioID = new SelectList(db.Usuario, "ID", "NombreUsuario", obra.ModUsuarioID);
            ViewBag.ClienteID = new SelectList(db.Cliente, "ID", "Nombre", obra.ClienteID);
            ViewBag.DepartamentoID = new SelectList(db.Departamento, "ID", "Descripcion", obra.DepartamentoID);
            return View(obra);
        }

        // POST: Obras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Cliente,Coeficiente,InsFecha,ModUsuarioID,Nombre,Finalizada,ModFecha,DepartamentoID,ClienteID")] Obra obra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(obra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ModUsuarioID = new SelectList(db.Usuario, "ID", "NombreUsuario", obra.ModUsuarioID);
            ViewBag.ClienteID = new SelectList(db.Cliente, "ID", "Nombre", obra.ClienteID);
            ViewBag.DepartamentoID = new SelectList(db.Departamento, "ID", "Descripcion", obra.DepartamentoID);
            return View(obra);
        }

        // GET: Obras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Obra obra = db.Obra.Find(id);
            if (obra == null)
            {
                return HttpNotFound();
            }
            return View(obra);
        }

        // POST: Obras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Obra obra = db.Obra.Find(id);
            db.Obra.Remove(obra);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public Obra Insertar(Entities context, Obra obra)
        {
            if (ModelState.IsValid)
            {
                context.Obra.Add(obra);
                context.SaveChanges();
                return obra;
            }
            else
                return null;
        }

        private List<SelectListItem> GetRubros(int obra)
        {
            List<SelectListItem> rubros =            
                (from R in db.Rubro
                 join SR in db.SubRubro on R.ID equals SR.RubroID
                 join I in db.Item on SR.ID equals I.SubRubroID
                 join S in db.SubItemDeItem on I.ID equals S.ItemID
                 join DSI in db.DetalleSubItem on S.ID equals DSI.SubItemDeItemID
                 where DSI.ObraID == obra
                 select R
                 ).Select(r => new SelectListItem()
                            {
                                Value = r.ID.ToString(),
                                Text = r.Nombre
                            })
                  .Distinct().ToList();
            if (rubros.Count>1)
                rubros.Insert(0, new SelectListItem() { Value = "0", Text = "--Todos los rubros--" });
            return rubros;
        }

        private List<SelectListItem> GetSubRubros(int obra, int rubro)
        {
            List<SelectListItem> subrubros =
                (from R in db.Rubro
                 join SR in db.SubRubro on R.ID equals SR.RubroID
                 join I in db.Item on SR.ID equals I.SubRubroID
                 join S in db.SubItemDeItem on I.ID equals S.ItemID
                 join DSI in db.DetalleSubItem on S.ID equals DSI.SubItemDeItemID
                 where DSI.ObraID == obra && (R.ID == rubro || rubro == 0)
                 select SR
                 ).Select(r => new SelectListItem()
                            {
                                Value = r.ID.ToString(),
                                Text = r.Nombre
                            })
                     .Distinct().ToList();
            if (subrubros.Count > 1)
                subrubros.Insert(0, new SelectListItem() { Value = "0", Text = "--Todos los subrubros--" });
            return subrubros;
        }

        private List<SelectListItem> GetItems(int obra, int rubro, int subrubro)
        {
            List<SelectListItem> items =
                (from R in db.Rubro
                 join SR in db.SubRubro on R.ID equals SR.RubroID
                 join I in db.Item on SR.ID equals I.SubRubroID
                 join S in db.SubItemDeItem on I.ID equals S.ItemID
                 join DSI in db.DetalleSubItem on S.ID equals DSI.SubItemDeItemID
                 where DSI.ObraID == obra && (R.ID == rubro || rubro == 0)
                                          && (SR.ID == subrubro || subrubro == 0)
                 select I
                 ).Select(r => new SelectListItem()
                 {
                     Value = r.ID.ToString(),
                     Text = r.Nombre
                 })
                     .Distinct().ToList();
            if (items.Count > 1)
                items.Insert(0, new SelectListItem() { Value = "0", Text = "--Todos los ítems--" });
            return items;
        }

        private List<SelectListItem> GetSubItems(int obra, int rubro, int subrubro, int item)
        {
            List<SelectListItem> subitems = 
                (from R in db.Rubro
                 join SR in db.SubRubro on R.ID equals SR.RubroID
                 join I in db.Item on SR.ID equals I.SubRubroID
                 join S in db.SubItemDeItem on I.ID equals S.ItemID
                 join DSI in db.DetalleSubItem on S.ID equals DSI.SubItemDeItemID
                 where DSI.ObraID == obra && (R.ID == rubro || rubro == 0)
                                          && (SR.ID == subrubro || subrubro == 0)
                                          && (I.ID == item || item == 0)
                 select S.SubItem
                 ).Select(r => new SelectListItem()
                 {
                     Value = r.ID.ToString(),
                     Text = r.Nombre
                 })
                    .Distinct().ToList();
            if (subitems.Count > 1)
                subitems.Insert(0, new SelectListItem() { Value = "0", Text = "--Todos los subítems--" });
            return subitems;
        }

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
        private void ActualizarIdsDeCombos(ref int rubro, ref int subrubro, ref int item, ref int subitem)
        {
            if (item != 0)
            {
                int idItem = item;
                Item itemSeleccionado = db.Item.First(i => i.ID == idItem);
                subrubro = itemSeleccionado.SubRubro.ID;
                rubro = itemSeleccionado.SubRubro.Rubro.ID;
            }
            else if (subrubro != 0)
            {
                int idSubrubro = subrubro;
                SubRubro subrubroSeleccionado = db.SubRubro.First(i => i.ID == idSubrubro);
                rubro = subrubroSeleccionado.Rubro.ID;
            }
        }
    }
}
