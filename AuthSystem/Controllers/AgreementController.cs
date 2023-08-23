using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Models;
using AuthSystem.Data;
using AuthSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace AuthSystem.Controllers
{
    [Authorize]
    public class AgreementController : Controller
    {
        private readonly AuthDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AgreementController(AuthDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Agreement
        public async Task<IActionResult> Index()
        {
            var authDbContext = _context.Agreements.Include(a => a.ApplicationUser).Include(a => a.Product).Include(a => a.ProductGroup);
            return View(await authDbContext.ToListAsync());
        }

        // GET: Agreement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Agreements == null)
            {
                return NotFound();
            }

            var agreement = await _context.Agreements
                .Include(a => a.ApplicationUser)
                .Include(a => a.Product)
                .Include(a => a.ProductGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agreement == null)
            {
                return NotFound();
            }

            return View(agreement);
        }

        // GET: Agreement/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            ViewData["ProductGroupId"] = new SelectList(_context.ProductGroups, "Id", "Id");
            return View();
        }

        // POST: Agreement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ProductGroupId,ProductId,EffectiveDate,ExpirationDate,ProductPrice,NewPrice")] Agreement agreement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agreement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", agreement.UserId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", agreement.ProductId);
            ViewData["ProductGroupId"] = new SelectList(_context.ProductGroups, "Id", "Id", agreement.ProductGroupId);
            return View(agreement);
        }

        [HttpGet]
        public ActionResult GetProductByGroup(int id = 0)
        {
            var products = _context.Products.Where(x => x.ProductGroupId == id).ToList();


            return Ok(products);
        }

        // GET: Agreement/Edit/5
        public async Task<IActionResult> AddorEdit(int id = 0)
        {
            var productGroups = _context.ProductGroups.ToList();
            ViewBag.ProductGroups = productGroups;
            if (id == 0)
            {
                Agreement agreement = new Agreement();
                agreement.EffectiveDate = DateTime.Now;
                agreement.ExpirationDate = DateTime.Now;
                return View(agreement);
            }
            else
            {
                var agreementModel = await _context.Agreements.FindAsync(id);
                if (agreementModel == null)
                {
                    return NotFound();
                }
                return View(agreementModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Id,ProductGroupId,ProductId,EffectiveDate,ExpirationDate,ProductPrice,NewPrice,Active")] Agreement agreement)
        {
            agreement.UserId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {

                    _context.Add(agreement);
                    await _context.SaveChangesAsync();

                }
                //Update
                else
                {
                    try
                    {
                        _context.Update(agreement);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AgreementExists(agreement.Id))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true });
            }
            return Json(new { isValid = false });
        }

        // POST: Agreement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ProductGroupId,ProductId,EffectiveDate,ExpirationDate,ProductPrice,NewPrice")] Agreement agreement)
        {
            if (id != agreement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agreement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgreementExists(agreement.Id))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", agreement.UserId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", agreement.ProductId);
            ViewData["ProductGroupId"] = new SelectList(_context.ProductGroups, "Id", "Id", agreement.ProductGroupId);
            return View(agreement);
        }

        // GET: Agreement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Agreements == null)
            {
                return NotFound();
            }

            var agreement = await _context.Agreements
                .Include(a => a.ApplicationUser)
                .Include(a => a.Product)
                .Include(a => a.ProductGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agreement == null)
            {
                return NotFound();
            }

            return View(agreement);
        }

        // POST: Agreement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Agreements == null)
            {
                return Problem("Entity set 'AuthDbContext.Agreements'  is null.");
            }
            var agreement = await _context.Agreements.FindAsync(id);
            if (agreement != null)
            {
                _context.Agreements.Remove(agreement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgreementExists(int id)
        {
            return (_context.Agreements?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
