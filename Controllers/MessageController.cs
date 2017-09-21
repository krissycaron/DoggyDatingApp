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
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public MessageController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Message
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Message.ToListAsync());
        }



        // GET: Message/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message
                .SingleOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Message/Create
        public IActionResult Create(string userID)
        {

            ApplicationUser rUser = _context.ApplicationUser.SingleOrDefault(r => r.Id == userID);
            MessagingReceivingViewModel newMessage = new MessagingReceivingViewModel();
            newMessage.ReceivingUserId = rUser.Id; 
            return View(newMessage);
        }

        // POST: Message/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Message, string ReceivingUserId)
        {
            ApplicationUser rUser = await _context.ApplicationUser.SingleOrDefaultAsync(r => r.Id == ReceivingUserId);
            ApplicationUser sUser = await GetCurrentUserAsync(); 

            Message sentMessage = new Message(){ReceivingUser = rUser, SendingUser = sUser, Text = Message};

            _context.Add(sentMessage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //GET ONLY user Messages for the current User: 
        public async Task<IActionResult> LoadOnlyThisUsersMessages()
        {
            var user = await GetCurrentUserAsync();


            var sentMessages = await _context.Message
            .Where(cu => cu.SendingUser == user && user == cu.ReceivingUser)
            .GroupBy(su => su.SendingUser, ru => ru.ReceivingUser)
            .ToListAsync();


            return View();
        }




        // GET: Message/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message.SingleOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message);
        }

        // POST: Message/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MessageId,Text,TimeMessageSent")] Message message)
        {
            if (id != message.MessageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.MessageId))
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
            return View(message);
        }

        // GET: Message/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message
                .SingleOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _context.Message.SingleOrDefaultAsync(m => m.MessageId == id);
            _context.Message.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id)
        {
            return _context.Message.Any(e => e.MessageId == id);
        }
    }
}
