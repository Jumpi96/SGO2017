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
    public class ReceptorsController : Controller
    {
        private readonly SGOContext _context;

        public ReceptorsController(SGOContext context)
        {
            _context = context;
        }

        // GET: Receptors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Receptor.ToListAsync());
        }

        // GET: Receptors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receptor = await _context.Receptor
                .SingleOrDefaultAsync(m => m.ID == id);
            if (receptor == null)
            {
                return NotFound();
            }

            return View(receptor);
        }

        // GET: Receptors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Receptors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,Observaciones")] Receptor receptor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receptor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(receptor);
        }

        // GET: Receptors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receptor = await _context.Receptor.SingleOrDefaultAsync(m => m.ID == id);
            if (receptor == null)
            {
                return NotFound();
            }
            return View(receptor);
        }

        // POST: Receptors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,Observaciones")] Receptor receptor)
        {
            if (id != receptor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receptor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceptorExists(receptor.ID))
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
            return View(receptor);
        }

        // GET: Receptors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receptor = await _context.Receptor
                .SingleOrDefaultAsync(m => m.ID == id);
            if (receptor == null)
            {
                return NotFound();
            }

            return View(receptor);
        }

        // POST: Receptors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receptor = await _context.Receptor.SingleOrDefaultAsync(m => m.ID == id);
            _context.Receptor.Remove(receptor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceptorExists(int id)
        {
            return _context.Receptor.Any(e => e.ID == id);
        }
    }
}
