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
    public class TipoItemsController : Controller
    {
        private readonly SGOContext _context;

        public TipoItemsController(SGOContext context)
        {
            _context = context;
        }

        // GET: TipoItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoItem.ToListAsync());
        }

        // GET: TipoItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoItem = await _context.TipoItem
                .SingleOrDefaultAsync(m => m.ID == id);
            if (tipoItem == null)
            {
                return NotFound();
            }

            return View(tipoItem);
        }

        // GET: TipoItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,Caracter")] TipoItem tipoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoItem);
        }

        // GET: TipoItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoItem = await _context.TipoItem.SingleOrDefaultAsync(m => m.ID == id);
            if (tipoItem == null)
            {
                return NotFound();
            }
            return View(tipoItem);
        }

        // POST: TipoItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,Caracter")] TipoItem tipoItem)
        {
            if (id != tipoItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoItemExists(tipoItem.ID))
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
            return View(tipoItem);
        }

        // GET: TipoItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoItem = await _context.TipoItem
                .SingleOrDefaultAsync(m => m.ID == id);
            if (tipoItem == null)
            {
                return NotFound();
            }

            return View(tipoItem);
        }

        // POST: TipoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoItem = await _context.TipoItem.SingleOrDefaultAsync(m => m.ID == id);
            _context.TipoItem.Remove(tipoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoItemExists(int id)
        {
            return _context.TipoItem.Any(e => e.ID == id);
        }
    }
}
