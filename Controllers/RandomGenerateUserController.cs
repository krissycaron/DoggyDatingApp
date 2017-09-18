using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using serverSideCapstone.Data;
using serverSideCapstone.Models;
using serverSideCapstone.Models.ViewModels;

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
        public async Task<IActionResult> Index(string pupId)
        {
            LikedUserViewModel model = new LikedUserViewModel();
            // Get all user Query Database. 
            var id = await GetCurrentUserAsync();
            //Get a List of users, MINUS logged in user
            var dontShow =  _context.UserLike
            .Where(cu => cu.CurrentUser == id && cu.IsLiked == false)
            .GroupBy(lu => lu.LikedUser, (key, g) => new {userId = key, count = g.Count()})
            .ToList();

            var ListOfUsers = _context.ApplicationUser
            //This is where you would query the skipped 3X and the already liked users. 
            //where current user (cu)
            .Where(cu => cu != id)
            .ToList();
            foreach(var noShow in dontShow)
              {
                  if(noShow.count >= 3)
                  {
                    ApplicationUser singleUser = _context.ApplicationUser.Single(ns => ns.Id == noShow.userId.Id);
                    ListOfUsers.Remove(singleUser);
                  } 
              }   

            if(ListOfUsers.Count <= 0)
            {
                return View("NoPuppiesToLove");
            }
            
            model.LikedUser = await RandomlyGenerateUser();
            //checking to see if the newly generated dog is equal to the dog that was liked (or skipped) (ChosenPup == PupId)
            while (model.LikedUser.Id == pupId)
            {
                model.LikedUser = await RandomlyGenerateUser();
            }

            return View(model);
        }

        public async Task<ApplicationUser> RandomlyGenerateUser()
        {
            var id = await GetCurrentUserAsync();
            var ListOfUsers = _context.ApplicationUser
             .Where(cu => cu != id)
            .ToList();
            // currentUserDisplayed = GetRandomIndex out of able list of users 
            // Genearate a random # 
            Random random = new Random();
            // Get user out of the list. 
            var GetRandomIndex = random.Next(ListOfUsers.Count);
            var randomUser = ListOfUsers[GetRandomIndex];
            //Json is an Iaction reslut method. returning of same type ... 
            return randomUser;
        }
    }
}