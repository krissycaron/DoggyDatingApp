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
    public class UserLikeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserLikeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserLike
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserLike.ToListAsync());
        }

        // GET: UserLike/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLike = await _context.UserLike
                .SingleOrDefaultAsync(m => m.UserLikeId == id);
            if (userLike == null)
            {
                return NotFound();
            }

            return View(userLike);
        }

        // GET: UserLike/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserLike/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserLikeId,IsLiked")] UserLike userLike)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userLike);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userLike);
        }

        // GET: UserLike/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLike = await _context.UserLike.SingleOrDefaultAsync(m => m.UserLikeId == id);
            if (userLike == null)
            {
                return NotFound();
            }
            return View(userLike);
        }

        // POST: UserLike/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserLikeId,IsLiked")] UserLike userLike)
        {
            if (id != userLike.UserLikeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userLike);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserLikeExists(userLike.UserLikeId))
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
            return View(userLike);
        }

        // GET: UserLike/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLike = await _context.UserLike
                .SingleOrDefaultAsync(m => m.UserLikeId == id);
            if (userLike == null)
            {
                return NotFound();
            }

            return View(userLike);
        }

        // POST: UserLike/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userLike = await _context.UserLike.SingleOrDefaultAsync(m => m.UserLikeId == id);
            _context.UserLike.Remove(userLike);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserLikeExists(int id)
        {
            return _context.UserLike.Any(e => e.UserLikeId == id);
        }
    }
}
