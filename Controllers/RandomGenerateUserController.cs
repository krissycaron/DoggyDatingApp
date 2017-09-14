using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using serverSideCapstone.Data;
using serverSideCapstone.Models;

namespace serverSideCapstone.Controllers
{
    public class RandomGenerateUserController: Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public RandomGenerateUserController(ApplicationDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = ctx;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public async Task<IActionResult> Index(string searchString, bool LocalDelivery)
        {
            // Get all user Query Database. 
            var id = await GetCurrentUserAsync();
            //Get a List of users, MINUS logged in user
            var ListOfUsers = _context.ApplicationUser
            .Where(cu => cu != id)
            .ToList();
            // currentUserDisplayed = GetRandomuser out of able list of users 
            Random random = new Random();
            var GetRandomuser = random.Next(ListOfUsers.Count);

            return View(ListOfUsers);
        }
        // Genearate a random # 
        // GEt user out of the list. 
        
    }
}