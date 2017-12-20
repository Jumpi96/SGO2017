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
    public class ItemsController : Controller
    {
        private Entities db = new Entities();

        // GET: Items
        public ActionResult Index()
        {
            var item = db.Item.Include(i => i.SubRubro).Include(i => i.Unidad);
            return View(item.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.SubRubroID = new SelectList(db.SubRubro, "ID", "Nombre");
            ViewBag.UnidadID = new SelectList(db.Unidad, "ID", "Descripcion");
            return View();
        }

        // POST: Items/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,SubRubroID,UnidadID")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Item.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubRubroID = new SelectList(db.SubRubro, "ID", "Nombre", item.SubRubroID);
            ViewBag.UnidadID = new SelectList(db.Unidad, "ID", "Descripcion", item.UnidadID);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubRubroID = new SelectList(db.SubRubro, "ID", "Nombre", item.SubRubroID);
            ViewBag.UnidadID = new SelectList(db.Unidad, "ID", "Descripcion", item.UnidadID);
            return View(item);
        }

        // POST: Items/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,SubRubroID,UnidadID")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubRubroID = new SelectList(db.SubRubro, "ID", "Nombre", item.SubRubroID);
            ViewBag.UnidadID = new SelectList(db.Unidad, "ID", "Descripcion", item.UnidadID);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Item.Find(id);
            db.Item.Remove(item);
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

        public Item ItemByNombreYSubrubro(Entities context, string nombre, SubRubro subRubro)
        {
            return context.Item.SingleOrDefault(m => m.Nombre.Equals(nombre) && m.SubRubro.ID == subRubro.ID);
        }

        public Item Insertar(Entities context, Item item)
        {
            if (ModelState.IsValid)
            {
                context.Item.Add(item);
                context.SaveChanges();
                return item;
            }
            else
                return null;
        }
    }
}
