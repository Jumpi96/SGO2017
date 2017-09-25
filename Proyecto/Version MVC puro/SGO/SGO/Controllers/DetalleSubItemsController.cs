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
    public class DetalleSubItemsController : Controller
    {
        private readonly SGOContext _context;

        public DetalleSubItemsController(SGOContext context)
        {
            _context = context;
        }

        // GET: DetalleSubItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.DetalleSubItem.ToListAsync());
        }

        // GET: DetalleSubItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleSubItem = await _context.DetalleSubItem
                .SingleOrDefaultAsync(m => m.ID == id);
            if (detalleSubItem == null)
            {
                return NotFound();
            }

            return View(detalleSubItem);
        }

        // GET: DetalleSubItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DetalleSubItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Cantidad,PrecioUnitario")] DetalleSubItem detalleSubItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleSubItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(detalleSubItem);
        }

        // GET: DetalleSubItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleSubItem = await _context.DetalleSubItem.SingleOrDefaultAsync(m => m.ID == id);
            if (detalleSubItem == null)
            {
                return NotFound();
            }
            return View(detalleSubItem);
        }

        // POST: DetalleSubItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Cantidad,PrecioUnitario")] DetalleSubItem detalleSubItem)
        {
            if (id != detalleSubItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleSubItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleSubItemExists(detalleSubItem.ID))
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
            return View(detalleSubItem);
        }

        // GET: DetalleSubItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleSubItem = await _context.DetalleSubItem
                .SingleOrDefaultAsync(m => m.ID == id);
            if (detalleSubItem == null)
            {
                return NotFound();
            }

            return View(detalleSubItem);
        }

        // POST: DetalleSubItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleSubItem = await _context.DetalleSubItem.SingleOrDefaultAsync(m => m.ID == id);
            _context.DetalleSubItem.Remove(detalleSubItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleSubItemExists(int id)
        {
            return _context.DetalleSubItem.Any(e => e.ID == id);
        }

        public DetalleSubItem Insertar(DetalleSubItem detalleSubItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleSubItem);
                _context.SaveChanges();
                return detalleSubItem;
            }
            else
                return null;
        }
    }
}
