using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using gym_management_system.Models;

namespace gym_management_system.Controllers
{
    public class MemberController : Controller
    {

        private readonly ILogger<MemberController> _logger;

        public MemberController(ILogger<MemberController> logger)
        {

            _logger = logger;
        }

        [HttpGet]
        [Route("admin/members")]
        public ActionResult Index()
        {
            // if user is not logged in, redirect to login page
            if (User.Identity.IsAuthenticated == false)
            {
                ViewBag.IsAuthenticated = false;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.IsAuthenticated = true;

            // the members array
            List<GymMember> members = new List<GymMember>();

            try {
                // use member manager to get all members
                MemberManager memberManager = new MemberManager();
                memberManager.GetAllMembers();
                members = memberManager.Members;

            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                TempData["error"] = e.Message;
                return RedirectToAction("Index", "Home");
            }

            return View(members);

        }

        // create member object
        [HttpGet]
        [Route("admin/members/create")]
        public ActionResult Create()
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
