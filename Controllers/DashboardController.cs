using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gym_management_system.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {

            _logger = logger;
        }

        [HttpGet]
        [Route("dashboard")]
        public ActionResult Index()
        {
            // if user is not logged in, redirect to login page
            if (User.Identity.IsAuthenticated == false)
            {
                ViewBag.IsAuthenticated = false;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.IsAuthenticated = true;
            return View();
        }

    }
}
