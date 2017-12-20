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
    public class TipoItemsController : Controller
    {
        private Entities db = new Entities();

        // GET: TipoItems
        public ActionResult Index()
        {
            return View(db.TipoItem.ToList());
        }

        // GET: TipoItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoItem tipoItem = db.TipoItem.Find(id);
            if (tipoItem == null)
            {
                return HttpNotFound();
            }
            return View(tipoItem);
        }

        // GET: TipoItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoItems/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Caracter,Nombre")] TipoItem tipoItem)
        {
            if (ModelState.IsValid)
            {
                db.TipoItem.Add(tipoItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoItem);
        }

        // GET: TipoItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoItem tipoItem = db.TipoItem.Find(id);
            if (tipoItem == null)
            {
                return HttpNotFound();
            }
            return View(tipoItem);
        }

        // POST: TipoItems/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Caracter,Nombre")] TipoItem tipoItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoItem);
        }

        // GET: TipoItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoItem tipoItem = db.TipoItem.Find(id);
            if (tipoItem == null)
            {
                return HttpNotFound();
            }
            return View(tipoItem);
        }

        // POST: TipoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoItem tipoItem = db.TipoItem.Find(id);
            db.TipoItem.Remove(tipoItem);
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

        public TipoItem TipoItemByCaracter(Entities context, string caracter)
        {
            return context.TipoItem.SingleOrDefault(m => m.Caracter.Equals(caracter));
        }
    }
}
