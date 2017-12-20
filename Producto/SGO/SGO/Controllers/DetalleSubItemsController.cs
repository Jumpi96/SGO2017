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
    public class DetalleSubItemsController : Controller
    {
        private Entities db = new Entities();

        // GET: DetalleSubItems
        public ActionResult Index()
        {
            var detalleSubItem = db.DetalleSubItem.Include(d => d.Obra).Include(d => d.SubItemDeItem);
            return View(detalleSubItem.ToList());
        }

        // GET: DetalleSubItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleSubItem detalleSubItem = db.DetalleSubItem.Find(id);
            if (detalleSubItem == null)
            {
                return HttpNotFound();
            }
            return View(detalleSubItem);
        }

        // GET: DetalleSubItems/Create
        public ActionResult Create()
        {
            ViewBag.ObraID = new SelectList(db.Obra, "ID", "Nombre");
            ViewBag.SubItemDeItemID = new SelectList(db.SubItemDeItem, "ID", "ID");
            return View();
        }

        // POST: DetalleSubItems/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Cantidad,ObraID,PrecioUnitario,SubItemDeItemID")] DetalleSubItem detalleSubItem)
        {
            if (ModelState.IsValid)
            {
                db.DetalleSubItem.Add(detalleSubItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ObraID = new SelectList(db.Obra, "ID", "Nombre", detalleSubItem.ObraID);
            ViewBag.SubItemDeItemID = new SelectList(db.SubItemDeItem, "ID", "ID", detalleSubItem.SubItemDeItemID);
            return View(detalleSubItem);
        }

        // GET: DetalleSubItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleSubItem detalleSubItem = db.DetalleSubItem.Find(id);
            if (detalleSubItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ObraID = new SelectList(db.Obra, "ID", "Nombre", detalleSubItem.ObraID);
            ViewBag.SubItemDeItemID = new SelectList(db.SubItemDeItem, "ID", "ID", detalleSubItem.SubItemDeItemID);
            return View(detalleSubItem);
        }

        // POST: DetalleSubItems/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Cantidad,ObraID,PrecioUnitario,SubItemDeItemID")] DetalleSubItem detalleSubItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleSubItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ObraID = new SelectList(db.Obra, "ID", "Nombre", detalleSubItem.ObraID);
            ViewBag.SubItemDeItemID = new SelectList(db.SubItemDeItem, "ID", "ID", detalleSubItem.SubItemDeItemID);
            return View(detalleSubItem);
        }

        // GET: DetalleSubItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleSubItem detalleSubItem = db.DetalleSubItem.Find(id);
            if (detalleSubItem == null)
            {
                return HttpNotFound();
            }
            return View(detalleSubItem);
        }

        // POST: DetalleSubItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleSubItem detalleSubItem = db.DetalleSubItem.Find(id);
            db.DetalleSubItem.Remove(detalleSubItem);
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

        public DetalleSubItem Insertar(Entities context, DetalleSubItem detalleSubItem)
        {
            if (ModelState.IsValid)
            {
                context.DetalleSubItem.Add(detalleSubItem);
                context.SaveChanges();
                return detalleSubItem;
            }
            else
                return null;
        }
    }
}
