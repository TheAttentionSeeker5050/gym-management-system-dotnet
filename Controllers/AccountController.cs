using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gym_management_system.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {

            _logger = logger;
        }

        [HttpGet]
        [Route("account/settings")]
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
