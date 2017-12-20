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
    public class SubItemDeItemsController : Controller
    {
        private Entities db = new Entities();

        // GET: SubItemDeItems
        public ActionResult Index()
        {
            var subItemDeItem = db.SubItemDeItem.Include(s => s.Item).Include(s => s.SubItem);
            return View(subItemDeItem.ToList());
        }

        // GET: SubItemDeItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubItemDeItem subItemDeItem = db.SubItemDeItem.Find(id);
            if (subItemDeItem == null)
            {
                return HttpNotFound();
            }
            return View(subItemDeItem);
        }

        // GET: SubItemDeItems/Create
        public ActionResult Create()
        {
            ViewBag.ItemID = new SelectList(db.Item, "ID", "Nombre");
            ViewBag.SubItemID = new SelectList(db.SubItem, "ID", "Nombre");
            return View();
        }

        // POST: SubItemDeItems/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ItemID,SubItemID")] SubItemDeItem subItemDeItem)
        {
            if (ModelState.IsValid)
            {
                db.SubItemDeItem.Add(subItemDeItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemID = new SelectList(db.Item, "ID", "Nombre", subItemDeItem.ItemID);
            ViewBag.SubItemID = new SelectList(db.SubItem, "ID", "Nombre", subItemDeItem.SubItemID);
            return View(subItemDeItem);
        }

        // GET: SubItemDeItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubItemDeItem subItemDeItem = db.SubItemDeItem.Find(id);
            if (subItemDeItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.Item, "ID", "Nombre", subItemDeItem.ItemID);
            ViewBag.SubItemID = new SelectList(db.SubItem, "ID", "Nombre", subItemDeItem.SubItemID);
            return View(subItemDeItem);
        }

        // POST: SubItemDeItems/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ItemID,SubItemID")] SubItemDeItem subItemDeItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subItemDeItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemID = new SelectList(db.Item, "ID", "Nombre", subItemDeItem.ItemID);
            ViewBag.SubItemID = new SelectList(db.SubItem, "ID", "Nombre", subItemDeItem.SubItemID);
            return View(subItemDeItem);
        }

        // GET: SubItemDeItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubItemDeItem subItemDeItem = db.SubItemDeItem.Find(id);
            if (subItemDeItem == null)
            {
                return HttpNotFound();
            }
            return View(subItemDeItem);
        }

        // POST: SubItemDeItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubItemDeItem subItemDeItem = db.SubItemDeItem.Find(id);
            db.SubItemDeItem.Remove(subItemDeItem);
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

        public SubItemDeItem SubItemDeItemByItemSubItem(Entities context, Item item, SubItem subItem)
        {
            return context.SubItemDeItem.SingleOrDefault(m => m.Item.ID == item.ID && m.SubItem.ID == subItem.ID);
        }

        public SubItemDeItem Insertar(Entities context, SubItemDeItem subItemDeItem)
        {
            if (ModelState.IsValid)
            {
                context.SubItemDeItem.Add(subItemDeItem);
                context.SaveChanges();
                return subItemDeItem;
            }
            else
                return null;
        }
    }
}
