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
    public class SubItemsController : Controller
    {
        private readonly SGOContext _context;

        public SubItemsController(SGOContext context)
        {
            _context = context;
        }

        // GET: SubItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.SubItem.ToListAsync());
        }

        // GET: SubItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subItem = await _context.SubItem
                .SingleOrDefaultAsync(m => m.ID == id);
            if (subItem == null)
            {
                return NotFound();
            }

            return View(subItem);
        }

        // GET: SubItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,PrecioUnitario")] SubItem subItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subItem);
        }

        // GET: SubItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subItem = await _context.SubItem.SingleOrDefaultAsync(m => m.ID == id);
            if (subItem == null)
            {
                return NotFound();
            }
            return View(subItem);
        }

        // POST: SubItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,PrecioUnitario")] SubItem subItem)
        {
            if (id != subItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubItemExists(subItem.ID))
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
            return View(subItem);
        }

        // GET: SubItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subItem = await _context.SubItem
                .SingleOrDefaultAsync(m => m.ID == id);
            if (subItem == null)
            {
                return NotFound();
            }

            return View(subItem);
        }

        // POST: SubItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subItem = await _context.SubItem.SingleOrDefaultAsync(m => m.ID == id);
            _context.SubItem.Remove(subItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubItemExists(int id)
        {
            return _context.SubItem.Any(e => e.ID == id);
        }
    }
}
