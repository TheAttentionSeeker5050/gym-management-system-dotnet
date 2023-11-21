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

        public ActionResult Index()
        {
            return View();
        }

        
    }
}
