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
        public IActionResult LoginSubmit(UserLogin user)
        {
            // verify model state form is valid
            if (!ModelState.IsValid)
            {
                return View("Login");
            }

            try
            {
                // attempt login the user with the login model method. and user credentials in form
                user = user.loginUser(user);

                // session mapping ------------------------------------------
                // use the map method to map user claims
                MapUserClaims(user);

                // add log to the console
                Console.WriteLine("User logged in: " + user.Email + " in at " + DateTime.Now);


            } catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction("Login");
            }

            // if login is successful, redirect to the dashboard
            // TempData["feedback"] = "Login successful";
            return RedirectToAction("Index", "Home");
            // return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [Route("register")]
        public IActionResult Register()
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
        [Route("register")]
        public IActionResult RegisterSubmit(UserRegister user)
        {

            // verify model state form is valid
            if (!ModelState.IsValid)
            {
                return View("Register");
            }

            // create a new user object
            try
            {
                Console.WriteLine("Before user create function");
                // attempt to register the user with the register model method
                user.createUser(user);

                // add log to the console
                Console.WriteLine("User registered: " + user.Email + " in at " + DateTime.Now);

            } catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction("Register");
            }

            // if registration is successful, redirect to the main home page
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Route("guest-login")]
        public IActionResult GuestLogin()
        {

            // use the guest login model method to login the user

            UserLogin user = new UserLogin();

            user = user.guestLogin();

            // session mapping ------------------------------------------
            // map user claims

            // use the map method to map user claims
            MapUserClaims(user);
            
            // redirect to the home index page
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout( )
        {

            // unmap user claims using the unmap method
            UnmapUserClaims();

            /*// sign out the user from the cookie authentication scheme
            // using session authentication
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);*/

            return RedirectToAction("Index", "Home");
        }


        // private methods --------------------------------------------
        // map method to map user claims
        private ClaimsPrincipal MapUserClaims(UserLogin user)
        {
            // map user claims
            var claims = new List<Claim>
            {
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
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            

            return new ClaimsPrincipal(claimsIdentity);
        }

        // unmap method to unmap user claims and sign out the user
        private void UnmapUserClaims()
        {
            // sign out the user from the cookie authentication scheme
            // using session authentication
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        
    }
}
