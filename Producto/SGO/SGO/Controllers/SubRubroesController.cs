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
    public class SubRubroesController : Controller
    {
        private readonly SGOContext _context;

        public SubRubroesController(SGOContext context)
        {
            _context = context;
        }

        // GET: SubRubroes
        public async Task<IActionResult> Index()
        {
            return View(await _context.SubRubro.ToListAsync());
        }

        // GET: SubRubroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subRubro = await _context.SubRubro
                .SingleOrDefaultAsync(m => m.ID == id);
            if (subRubro == null)
            {
                return NotFound();
            }

            return View(subRubro);
        }

        // GET: SubRubroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubRubroes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre")] SubRubro subRubro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subRubro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subRubro);
        }

        // GET: SubRubroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subRubro = await _context.SubRubro.SingleOrDefaultAsync(m => m.ID == id);
            if (subRubro == null)
            {
                return NotFound();
            }
            return View(subRubro);
        }

        // POST: SubRubroes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre")] SubRubro subRubro)
        {
            if (id != subRubro.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subRubro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubRubroExists(subRubro.ID))
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
            return View(subRubro);
        }

        // GET: SubRubroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subRubro = await _context.SubRubro
                .SingleOrDefaultAsync(m => m.ID == id);
            if (subRubro == null)
            {
                return NotFound();
            }

            return View(subRubro);
        }

        // POST: SubRubroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subRubro = await _context.SubRubro.SingleOrDefaultAsync(m => m.ID == id);
            _context.SubRubro.Remove(subRubro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubRubroExists(int id)
        {
            return _context.SubRubro.Any(e => e.ID == id);
        }

        public SubRubro SubrubroByNombreYRubro(string nombre, Rubro rubro)
        {
            return _context.SubRubro.SingleOrDefault(m => m.Nombre.Equals(nombre) && m.Rubro.ID==rubro.ID);
        }

        public SubRubro Insertar(SubRubro subRubro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subRubro);
                _context.SaveChanges();
                return subRubro;
            }
            else
                return null;
        }
    }
}
