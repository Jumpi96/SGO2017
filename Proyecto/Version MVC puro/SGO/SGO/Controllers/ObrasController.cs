using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGO.Models;

namespace SGO.Controllers
{
    [Authorize]
    public class ObrasController : Controller
    {
        private readonly SGOContext _context;

        public ObrasController(SGOContext context)
        {
            _context = context;
        }

        // GET: Obras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Obra.ToListAsync());
        }

        // GET: Obras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obra = await _context.Obra
                .SingleOrDefaultAsync(m => m.ID == id);
            if (obra == null)
            {
                return NotFound();
            }

            return View(obra);
        }

        // GET: Obras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Obras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,Cliente,Coeficiente,InsFecha,ModFecha,Finalizada")] Obra obra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(obra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(obra);
        }

        // GET: Obras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obra = await _context.Obra.SingleOrDefaultAsync(m => m.ID == id);
            if (obra == null)
            {
                return NotFound();
            }
            return View(obra);
        }

        // POST: Obras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,Cliente,Coeficiente,InsFecha,ModFecha,Finalizada")] Obra obra)
        {
            if (id != obra.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(obra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObraExists(obra.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(obra);
        }

        // GET: Obras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obra = await _context.Obra
                .SingleOrDefaultAsync(m => m.ID == id);
            if (obra == null)
            {
                return NotFound();
            }

            return View(obra);
        }

        // POST: Obras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var obra = await _context.Obra.SingleOrDefaultAsync(m => m.ID == id);
            _context.Obra.Remove(obra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ObraExists(int id)
        {
            return _context.Obra.Any(e => e.ID == id);
        }

        // GET: Obras
        public async Task<IActionResult> Procesar()
        {
            Usuario usuario = new Usuario();
            ScraperService s = new ScraperService(_context,usuario);
            s.ProcesarDocumento("E:/SWAP/SGO/Proyecto/Probando.xls");

            //return View(await _context.Obra.ToListAsync());
            return View();
        }

        public Obra Insertar(Obra obra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(obra);
                _context.SaveChanges();
                return obra;
            }
            else
                return null;
        }
    }
}
