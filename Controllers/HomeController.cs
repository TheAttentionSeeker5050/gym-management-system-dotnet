using gym_management_system.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Authorization;


namespace gym_management_system.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {

            _logger = logger;
        }


        public IActionResult Index()
        {
            ViewBag.IsAuthenticated = false;
            // add log to the console
            if (User.Identity.IsAuthenticated)
            {
                Console.WriteLine("User logged in: " + User.Identity.Name + " at " + DateTime.Now);
                // add is auth Viewbag
                ViewBag.IsAuthenticated = true;
            }
            else
            {
                Console.WriteLine("User not logged in");
                // add is auth Viewbag
                ViewBag.IsAuthenticated = false;
            }

            return View();
        }

        
    }
}