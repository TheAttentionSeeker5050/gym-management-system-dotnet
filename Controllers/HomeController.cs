using gym_management_system.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System;

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
            var user = new User();
            var userDBHandler = new UserDBHandler();
            userDBHandler.createUser(user);
            return View();
        }

        
    }
}