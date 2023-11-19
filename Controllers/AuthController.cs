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

        [HttpPost]
        [Route("login")]
        public IActionResult LoginSubmit()
        {
            return View("Login");
        }


        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult RegisterSubmit()
        {
            return View("Register");
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
