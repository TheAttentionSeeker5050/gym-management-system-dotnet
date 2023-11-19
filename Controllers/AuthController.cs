using Microsoft.AspNetCore.Mvc;
using gym_management_system.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

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
        public async Task<IActionResult> LoginSubmitAsync(User user)
        {

            try
            {
                // attempt login the user with the login model method. and user credentials in form
                user = user.loginUser(user);

                // session mapping ------------------------------------------
                // map user claims
                var claims = new List<Claim> { 
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                // claims identity
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = true,
                    RedirectUri = "/"
                };

                // sign in the user to the cookie authentication scheme
                // using session authentication
                // method is asynchronous, so await it
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                // add log to the console
                Console.WriteLine("User logged in: " + user.Email + "in at " + DateTime.Now);


            } catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction("Login");
            }

            // if login is successful, redirect to the dashboard
            TempData["feedback"] = "Login successful";
            return RedirectToAction("Index", "Home");
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
