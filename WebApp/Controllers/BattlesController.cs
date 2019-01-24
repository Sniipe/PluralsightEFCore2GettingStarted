using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace WebApp.Controllers
{
    public class BattlesController : Controller
    {
        private readonly SamuraiContext _context;

        public BattlesController(SamuraiContext context)
        {
            _context = context;
        }

        // GET: Battles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Battles.ToListAsync());
        }

        // GET: Battles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Battle = await _context.Battles
                //.Include(s => s.SecretIdentity)
                //.Include(s => s.Quotes)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (Battle == null)
            {
                return NotFound();
            }

            return View(Battle);
        }

        // GET: Battles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Battles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartDate,Description")] Battle Battle)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(Battle);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(Battle);
        }

        // GET: Battles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Battle = await _context.Battles
                //.Include(s=>s.SecretIdentity)
                //.Include(s=>s.Quotes)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (Battle == null)
            {
                return NotFound();
            }
            return View(Battle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Battle Battle)
        public async Task<IActionResult> Edit(int id, Battle Battle)
        {
            if (id != Battle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Battle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BattleExists(Battle.Id))
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
            return View(Battle);
        }

        // GET: Battles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Battle = await _context.Battles
                .SingleOrDefaultAsync(m => m.Id == id);
            if (Battle == null)
            {
                return NotFound();
            }

            return View(Battle);
        }

        // POST: Battles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Battle = await _context.Battles.SingleOrDefaultAsync(m => m.Id == id);
            _context.Battles.Remove(Battle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BattleExists(int id)
        {
            return _context.Battles.Any(e => e.Id == id);
        }
    }
}
