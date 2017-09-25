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
    public class SubItemDeItemsController : Controller
    {
        private readonly SGOContext _context;

        public SubItemDeItemsController(SGOContext context)
        {
            _context = context;
        }

        // GET: SubItemDeItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.SubItemDeItem.ToListAsync());
        }

        // GET: SubItemDeItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subItemDeItem = await _context.SubItemDeItem
                .SingleOrDefaultAsync(m => m.ID == id);
            if (subItemDeItem == null)
            {
                return NotFound();
            }

            return View(subItemDeItem);
        }

        // GET: SubItemDeItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubItemDeItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID")] SubItemDeItem subItemDeItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subItemDeItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subItemDeItem);
        }

        // GET: SubItemDeItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subItemDeItem = await _context.SubItemDeItem.SingleOrDefaultAsync(m => m.ID == id);
            if (subItemDeItem == null)
            {
                return NotFound();
            }
            return View(subItemDeItem);
        }

        // POST: SubItemDeItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID")] SubItemDeItem subItemDeItem)
        {
            if (id != subItemDeItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subItemDeItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubItemDeItemExists(subItemDeItem.ID))
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
            return View(subItemDeItem);
        }

        // GET: SubItemDeItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subItemDeItem = await _context.SubItemDeItem
                .SingleOrDefaultAsync(m => m.ID == id);
            if (subItemDeItem == null)
            {
                return NotFound();
            }

            return View(subItemDeItem);
        }

        // POST: SubItemDeItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subItemDeItem = await _context.SubItemDeItem.SingleOrDefaultAsync(m => m.ID == id);
            _context.SubItemDeItem.Remove(subItemDeItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubItemDeItemExists(int id)
        {
            return _context.SubItemDeItem.Any(e => e.ID == id);
        }

        public SubItemDeItem SubItemDeItemByItemSubItem(Item item, SubItem subItem)
        {
            return _context.SubItemDeItem.SingleOrDefault(m => m.Item.ID == item.ID && m.SubItem.ID == subItem.ID);
        }

        public SubItemDeItem Insertar(SubItemDeItem subItemDeItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subItemDeItem);
                _context.SaveChanges();
                return subItemDeItem;
            }
            else
                return null;
        }
    }
}
