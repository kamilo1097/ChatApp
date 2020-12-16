using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        public readonly UserManager<AppUser> _userManager;
        public ChatHub(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);

            var DateNow = DateTime.Now;
            var userId = _userManager.GetUserId(Context.User);
             var msg = new Message
             {
                 UserID = userId,
                 Text = message,
                 UserName = user,
                 When = DateNow
             };

             await _context.Messages.AddAsync(msg);
             await _context.SaveChangesAsync();
             

            //await _context.Messages.AddAsync(msg);
        }

        public override Task OnConnectedAsync()
        {
            var messages = _context.Messages.Where(m=>m.When>DateTime.Now.AddDays(-2));
            foreach (var comment in messages)
            {
                Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", comment.UserName, comment.Text);
            }
            return base.OnConnectedAsync();
        }
    }
}
