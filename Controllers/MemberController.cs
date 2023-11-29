using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using gym_management_system.Models;
using MongoDB.Bson;

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
            MemberManager memberManager = new MemberManager();

            try {
                // use member manager to get all members
                memberManager.GetAllMembers();
                // assign the members array
                // members = memberManager.Members;

            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                TempData["error"] = e.Message;
                return RedirectToAction("Index", "Home");
            }

            return View(memberManager.Members);

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

        // submit member object
        [HttpPost]
        [Route("admin/members/create")]
        public ActionResult CreateSubmit(GymMember gymMember)
        {
            // if user is not logged in, redirect to login page
            if (User.Identity.IsAuthenticated == false)
            {
                ViewBag.IsAuthenticated = false;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.IsAuthenticated = true;

            try
            {
                // create a new member manager
                MemberManager memberManager = new MemberManager();

                // add the member to the database
                memberManager.CreateMember(gymMember);

                // redirect to the members page
                return RedirectToAction("Index", "Member");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                TempData["error"] = e.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        // edit member object
        [HttpGet]
        [Route("admin/members/edit/{id}")]
        public ActionResult Edit(string id)
        {
            // if user is not logged in, redirect to login page
            if (User.Identity.IsAuthenticated == false)
            {
                ViewBag.IsAuthenticated = false;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.IsAuthenticated = true;

            // create a new member manager
            MemberManager memberManager = new MemberManager();

            // create object id from the id string
            ObjectId objectId = new ObjectId(id);

            // get the member from the database
            memberManager.GetMemberById(objectId);

            // assign the member to the viewbag
            ViewBag.Member = memberManager.Member;

            return View();
        }

        // submit edit member object
        [HttpPost]
        [Route("admin/members/edit/{id}")]
        public ActionResult EditSubmit(string id, GymMember gymMember)
        {
            // if user is not logged in, redirect to login page
            if (User.Identity.IsAuthenticated == false)
            {
                ViewBag.IsAuthenticated = false;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.IsAuthenticated = true;

            try
            {
                // create a new member manager
                MemberManager memberManager = new MemberManager();

                // create object id from the id string
                ObjectId objectId = new ObjectId(id);

                // get the member from the database
                memberManager.GetMemberById(objectId);

                // assign the member to the viewbag
                ViewBag.Member = memberManager.Member;

                // update the member
                memberManager.UpdateMemberInfo(objectId, gymMember);

                // redirect to the members page
                return RedirectToAction("Index", "Member");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                TempData["error"] = e.Message;
                return RedirectToAction("Index", "Home");
            }
        }


        // delete member object
        [HttpGet]
        [Route("admin/members/delete/{id}")]
        public ActionResult Delete(string id)
        {
            // if user is not logged in, redirect to login page
            if (User.Identity.IsAuthenticated == false)
            {
                ViewBag.IsAuthenticated = false;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.IsAuthenticated = true;

            // create a new member manager
            MemberManager memberManager = new MemberManager();

            // create object id from the id string
            ObjectId objectId = new ObjectId(id);

            // get the member from the database
            memberManager.GetMemberById(objectId);

            // assign the member to the viewbag
            ViewBag.Member = memberManager.Member;

            return View();

        }


        // submit delete member object
        [HttpPost]
        [Route("admin/members/delete/{id}")]
        public ActionResult DeleteSubmit(string id)
        {
            // if user is not logged in, redirect to login page
            if (User.Identity.IsAuthenticated == false)
            {
                ViewBag.IsAuthenticated = false;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.IsAuthenticated = true;

            try
            {
                // create a new member manager
                MemberManager memberManager = new MemberManager();

                // create object id from the id string
                ObjectId objectId = new ObjectId(id);

                // get the member from the database
                memberManager.GetMemberById(objectId);

                // assign the member to the viewbag
                ViewBag.Member = memberManager.Member;

                // delete the member
                memberManager.DeleteMember(objectId);

                // redirect to the members page
                return RedirectToAction("Index", "Member");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                TempData["error"] = e.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        // view member object details
        [HttpGet]
        [Route("admin/members/details/{id}")]
        public ActionResult Details(string id)
        {
            // if user is not logged in, redirect to login page
            if (User.Identity.IsAuthenticated == false)
            {
                ViewBag.IsAuthenticated = false;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.IsAuthenticated = true;

            // create a new member manager
            MemberManager memberManager = new MemberManager();

            // create object id from the id string
            ObjectId objectId = new ObjectId(id);

            // get the member from the database
            memberManager.GetMemberById(objectId);

            // assign the member to the viewbag
            ViewBag.Member = memberManager.Member;

            return View();

        }

    }
}
