using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Data;
using AuthSystem.Models;

namespace AuthSystem.Controllers
{
    public class ProductGroupController : Controller
    {
        private readonly AuthDbContext _context;

        public ProductGroupController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: ProductGroup
        public async Task<IActionResult> Index()
        {
              return _context.ProductGroups != null ? 
                          View(await _context.ProductGroups.ToListAsync()) :
                          Problem("Entity set 'AuthDbContext.ProductGroups'  is null.");
        }

        // GET: ProductGroup/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductGroups == null)
            {
                return NotFound();
            }

            var productGroup = await _context.ProductGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productGroup == null)
            {
                return NotFound();
            }

            return View(productGroup);
        }

        // GET: ProductGroup/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductGroup/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GroupDescription,GroupCode,Active")] ProductGroup productGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productGroup);
        }

        // GET: ProductGroup/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductGroups == null)
            {
                return NotFound();
            }

            var productGroup = await _context.ProductGroups.FindAsync(id);
            if (productGroup == null)
            {
                return NotFound();
            }
            return View(productGroup);
        }

        // POST: ProductGroup/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GroupDescription,GroupCode,Active")] ProductGroup productGroup)
        {
            if (id != productGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductGroupExists(productGroup.Id))
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
            return View(productGroup);
        }

        // GET: ProductGroup/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductGroups == null)
            {
                return NotFound();
            }

            var productGroup = await _context.ProductGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productGroup == null)
            {
                return NotFound();
            }

            return View(productGroup);
        }

        // POST: ProductGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductGroups == null)
            {
                return Problem("Entity set 'AuthDbContext.ProductGroups'  is null.");
            }
            var productGroup = await _context.ProductGroups.FindAsync(id);
            if (productGroup != null)
            {
                _context.ProductGroups.Remove(productGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductGroupExists(int id)
        {
          return (_context.ProductGroups?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
