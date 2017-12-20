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
    public class SubRubroesController : Controller
    {
        private Entities db = new Entities();

        // GET: SubRubroes
        public ActionResult Index()
        {
            var subRubro = db.SubRubro.Include(s => s.Rubro);
            return View(subRubro.ToList());
        }

        // GET: SubRubroes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubRubro subRubro = db.SubRubro.Find(id);
            if (subRubro == null)
            {
                return HttpNotFound();
            }
            return View(subRubro);
        }

        // GET: SubRubroes/Create
        public ActionResult Create()
        {
            ViewBag.RubroID = new SelectList(db.Rubro, "ID", "Nombre");
            return View();
        }

        // POST: SubRubroes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,RubroID")] SubRubro subRubro)
        {
            if (ModelState.IsValid)
            {
                db.SubRubro.Add(subRubro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RubroID = new SelectList(db.Rubro, "ID", "Nombre", subRubro.RubroID);
            return View(subRubro);
        }

        // GET: SubRubroes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubRubro subRubro = db.SubRubro.Find(id);
            if (subRubro == null)
            {
                return HttpNotFound();
            }
            ViewBag.RubroID = new SelectList(db.Rubro, "ID", "Nombre", subRubro.RubroID);
            return View(subRubro);
        }

        // POST: SubRubroes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,RubroID")] SubRubro subRubro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subRubro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RubroID = new SelectList(db.Rubro, "ID", "Nombre", subRubro.RubroID);
            return View(subRubro);
        }

        // GET: SubRubroes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubRubro subRubro = db.SubRubro.Find(id);
            if (subRubro == null)
            {
                return HttpNotFound();
            }
            return View(subRubro);
        }

        // POST: SubRubroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubRubro subRubro = db.SubRubro.Find(id);
            db.SubRubro.Remove(subRubro);
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

        public SubRubro SubrubroByNombreYRubro(Entities context, string nombre, Rubro rubro)
        {
            return context.SubRubro.SingleOrDefault(m => m.Nombre.Equals(nombre) && m.Rubro.ID == rubro.ID);
        }

        public SubRubro Insertar(Entities context, SubRubro subRubro)
        {
            if (ModelState.IsValid)
            {
                context.SubRubro.Add(subRubro);
                context.SaveChanges();
                return subRubro;
            }
            else
                return null;
        }
    }
}
