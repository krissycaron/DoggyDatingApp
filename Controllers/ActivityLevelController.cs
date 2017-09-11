using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using serverSideCapstone.Data;
using serverSideCapstone.Models;

namespace serverSideCapstone.Controllers
{
    public class ActivityLevelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivityLevelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ActivityLevel
        public async Task<IActionResult> Index()
        {
            return View(await _context.ActivityLevel.ToListAsync());
        }

        // GET: ActivityLevel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityLevel = await _context.ActivityLevel
                .SingleOrDefaultAsync(m => m.ActivityLevelId == id);
            if (activityLevel == null)
            {
                return NotFound();
            }

            return View(activityLevel);
        }

        // GET: ActivityLevel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ActivityLevel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivityLevelId,Name")] ActivityLevel activityLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activityLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(activityLevel);
        }

        // GET: ActivityLevel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityLevel = await _context.ActivityLevel.SingleOrDefaultAsync(m => m.ActivityLevelId == id);
            if (activityLevel == null)
            {
                return NotFound();
            }
            return View(activityLevel);
        }

        // POST: ActivityLevel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActivityLevelId,Name")] ActivityLevel activityLevel)
        {
            if (id != activityLevel.ActivityLevelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activityLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityLevelExists(activityLevel.ActivityLevelId))
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
            return View(activityLevel);
        }

        // GET: ActivityLevel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityLevel = await _context.ActivityLevel
                .SingleOrDefaultAsync(m => m.ActivityLevelId == id);
            if (activityLevel == null)
            {
                return NotFound();
            }

            return View(activityLevel);
        }

        // POST: ActivityLevel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activityLevel = await _context.ActivityLevel.SingleOrDefaultAsync(m => m.ActivityLevelId == id);
            _context.ActivityLevel.Remove(activityLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityLevelExists(int id)
        {
            return _context.ActivityLevel.Any(e => e.ActivityLevelId == id);
        }
    }
}
