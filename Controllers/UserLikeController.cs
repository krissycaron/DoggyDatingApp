using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using serverSideCapstone.Data;
using serverSideCapstone.Models;
using serverSideCapstone.Models.ViewModels;

namespace serverSideCapstone.Controllers
{
    public class UserLikeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserLikeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

         private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);



        // GET: UserLike
        //User this to Query all the liked users... on this application user 
        // Genterate the list and then display the divs 
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserLike.ToListAsync());
        }

        // GET: UserLike/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Query Likeed users on this application user and populate a list of "liked users"  
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
        public async Task<IActionResult> CreateLike(LikedUserViewModel model)
        {
            var loggedUser = await GetCurrentUserAsync();
            var chosenPup = await _context.ApplicationUser
            .SingleOrDefaultAsync(l => l.Id == model.LikedUser.Id);
            //If Liked was clicked(True)
            if(model.isLikedUser == "Like")
            {
                UserLike liked = new UserLike(){CurrentUser=loggedUser, IsLiked=true, LikedUser=chosenPup };
                _context.Add(liked);
                _context.SaveChanges();
            }
            if (model.isLikedUser == "Skip")
            {
                UserLike liked = new UserLike(){CurrentUser=loggedUser, IsLiked=false, LikedUser=chosenPup };
                _context.Add(liked);
                _context.SaveChanges();
            }

            return RedirectToAction( "Index", new {controller = "RandomGenerateUser", action="Index", pupId = chosenPup.Id} );
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



        private bool UserLikeExists(int id)
        {
            return _context.UserLike.Any(e => e.UserLikeId == id);
        }
    }
}
