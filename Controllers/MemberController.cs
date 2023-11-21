using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gym_management_system.Controllers
{
    public class MemberController : Controller
    {

        private readonly ILogger<MemberController> _logger;

        public MemberController(ILogger<MemberController> logger)
        {

            _logger = logger;
        }


        public ActionResult Index()
        {
            return View();
        }

    }
}
