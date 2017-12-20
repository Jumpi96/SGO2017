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
    public class ObrasController : Controller
    {
        private Entities db = new Entities();

        // GET: Obras
        public ActionResult Index()
        {
            var obra = db.Obra.Include(o => o.Usuario).Include(o => o.Cliente1).Include(o => o.Departamento);
            return View(obra.ToList());
        }

        // GET: Obras/Details/5
        public ActionResult Details(int? id)
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

        
    }
}
