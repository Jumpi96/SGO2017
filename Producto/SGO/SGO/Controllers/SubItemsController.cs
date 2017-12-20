using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SGO.Models;

namespace SGO.Controllers
{
    public class SubItemsController : Controller
    {
        private Entities db = new Entities();

        // GET: SubItems
        public ActionResult Index()
        {
            var subItem = db.SubItem.Include(s => s.TipoItem).Include(s => s.Unidad);
            return View(subItem.ToList());
        }

        // GET: SubItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubItem subItem = db.SubItem.Find(id);
            if (subItem == null)
            {
                return HttpNotFound();
            }
            return View(subItem);
        }

        // GET: SubItems/Create
        public ActionResult Create()
        {
            ViewBag.TipoItemID = new SelectList(db.TipoItem, "ID", "Caracter");
            ViewBag.UnidadID = new SelectList(db.Unidad, "ID", "Descripcion");
            return View();
        }

        // POST: SubItems/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,PrecioUnitario,TipoItemID,UnidadID")] SubItem subItem)
        {
            if (ModelState.IsValid)
            {
                db.SubItem.Add(subItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipoItemID = new SelectList(db.TipoItem, "ID", "Caracter", subItem.TipoItemID);
            ViewBag.UnidadID = new SelectList(db.Unidad, "ID", "Descripcion", subItem.UnidadID);
            return View(subItem);
        }

        // GET: SubItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubItem subItem = db.SubItem.Find(id);
            if (subItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoItemID = new SelectList(db.TipoItem, "ID", "Caracter", subItem.TipoItemID);
            ViewBag.UnidadID = new SelectList(db.Unidad, "ID", "Descripcion", subItem.UnidadID);
            return View(subItem);
        }

        // POST: SubItems/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,PrecioUnitario,TipoItemID,UnidadID")] SubItem subItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoItemID = new SelectList(db.TipoItem, "ID", "Caracter", subItem.TipoItemID);
            ViewBag.UnidadID = new SelectList(db.Unidad, "ID", "Descripcion", subItem.UnidadID);
            return View(subItem);
        }

        // GET: SubItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubItem subItem = db.SubItem.Find(id);
            if (subItem == null)
            {
                return HttpNotFound();
            }
            return View(subItem);
        }

        // POST: SubItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubItem subItem = db.SubItem.Find(id);
            db.SubItem.Remove(subItem);
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

        public SubItem SubItemByNombre(Entities context, string nombre)
        {
            return context.SubItem.SingleOrDefault(m => m.Nombre.Equals(nombre));
        }
    }
}
