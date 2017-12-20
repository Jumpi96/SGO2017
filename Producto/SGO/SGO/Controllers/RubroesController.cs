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
    public class RubroesController : Controller
    {
        private Entities db = new Entities();

        // GET: Rubroes
        public ActionResult Index()
        {
            return View(db.Rubro.ToList());
        }

        // GET: Rubroes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rubro rubro = db.Rubro.Find(id);
            if (rubro == null)
            {
                return HttpNotFound();
            }
            return View(rubro);
        }

        // GET: Rubroes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rubroes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,Numeracion")] Rubro rubro)
        {
            if (ModelState.IsValid)
            {
                db.Rubro.Add(rubro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rubro);
        }

        // GET: Rubroes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rubro rubro = db.Rubro.Find(id);
            if (rubro == null)
            {
                return HttpNotFound();
            }
            return View(rubro);
        }

        // POST: Rubroes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,Numeracion")] Rubro rubro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rubro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rubro);
        }

        // GET: Rubroes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rubro rubro = db.Rubro.Find(id);
            if (rubro == null)
            {
                return HttpNotFound();
            }
            return View(rubro);
        }

        // POST: Rubroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rubro rubro = db.Rubro.Find(id);
            db.Rubro.Remove(rubro);
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

        public Rubro RubroByNombre(Entities context, string nombre)
        {
            return context.Rubro.SingleOrDefault(m => m.Nombre.Equals(nombre));
        }

        public Rubro Insertar(Entities context, Rubro rubro)
        {
            if (ModelState.IsValid)
            {
                context.Rubro.Add(rubro);
                context.SaveChanges();
                return rubro;
            }
            else
                return null;
        }
    }
}
