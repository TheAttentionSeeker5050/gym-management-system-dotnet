using Microsoft.AspNetCore.Mvc;
using gym_management_system.Models;
using Microsoft.AspNetCore.Authorization;

namespace gym_management_system.Controllers
{
    public class AuthController : Controller
    {

        // private fields ---------------------------------------------
        
        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            // check feedback and error messages from the login form
            if (TempData["feedback"] != null)
            {
                ViewBag.feedback = TempData["feedback"];
            }
            if (TempData["error"] != null)
            {
                ViewBag.error = TempData["error"];
            }
            return View();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult LoginSubmit(User user)
        {

            try
            {
                // attempt login the user with the login model method. and user credentials in form
                user = user.loginUser(user);

                // now add the user to the session
                
            } catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction("Login");
            }

            // if login is successful, redirect to the dashboard
            TempData["feedback"] = "Login successful";
            return RedirectToAction("Login");
            // return RedirectToAction("Index", "Home");
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
