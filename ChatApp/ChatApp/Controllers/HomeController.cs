using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChatApp.Models;
using Microsoft.AspNetCore.Identity;
using ChatApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public readonly UserManager<AppUser> _userManager;
        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
        
        public IActionResult Index()
        {
            var loggedUser = _userManager.GetUserId(User);
            ViewBag.UserHashId = loggedUser;
            ViewBag.UserName = User.Identity.Name;

            var messages = _context.Messages.ToList();
            return View(messages);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
