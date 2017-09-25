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
    public class RubroesController : Controller
    {
        private readonly SGOContext _context;

        public RubroesController(SGOContext context)
        {
            _context = context;
        }

        // GET: Rubroes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rubro.ToListAsync());
        }

        // GET: Rubroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rubro = await _context.Rubro
                .SingleOrDefaultAsync(m => m.ID == id);
            if (rubro == null)
            {
                return NotFound();
            }

            return View(rubro);
        }

        // GET: Rubroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rubroes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,Numeracion")] Rubro rubro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rubro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rubro);
        }

        // GET: Rubroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rubro = await _context.Rubro.SingleOrDefaultAsync(m => m.ID == id);
            if (rubro == null)
            {
                return NotFound();
            }
            return View(rubro);
        }

        // POST: Rubroes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,Numeracion")] Rubro rubro)
        {
            if (id != rubro.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rubro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RubroExists(rubro.ID))
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
            return View(rubro);
        }

        // GET: Rubroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rubro = await _context.Rubro
                .SingleOrDefaultAsync(m => m.ID == id);
            if (rubro == null)
            {
                return NotFound();
            }

            return View(rubro);
        }

        // POST: Rubroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rubro = await _context.Rubro.SingleOrDefaultAsync(m => m.ID == id);
            _context.Rubro.Remove(rubro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RubroExists(int id)
        {
            return _context.Rubro.Any(e => e.ID == id);
        }

        public Rubro RubroByNombre(string nombre)
        {
            return _context.Rubro.SingleOrDefault(m => m.Nombre.Equals(nombre));
        }

        public Rubro Insertar(Rubro rubro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rubro);
                _context.SaveChanges();
                return rubro;
            }
            else
                return null;
        }
    }
}
