using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using serverSideCapstone.Data;
using serverSideCapstone.Models;
using serverSideCapstone.Models.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace serverSideCapstone.Controllers
{
    public class UserController : Controller
    { 
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        public UserController(ApplicationDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = ctx;
        }

          public async Task<IActionResult> Index()
        {
            var id = await GetCurrentUserAsync();

            var users = await _context.UserLike
                .Where(cu => cu.CurrentUser == id && cu.IsLiked == true)
                .Select(u => u.LikedUser)
                .Distinct()
                .ToListAsync()
            ;
            return View(users);
        }

        // GET: UserLike
        //User this to Query all the liked users... on this application user 
        // Genterate the list and then display the divs 
    }
}