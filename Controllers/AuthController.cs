using Microsoft.AspNetCore.Mvc;

namespace gym_management_system.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }


        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        [Route("guest-login")]
        public IActionResult GuestLogin()
        {
            return View();
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
