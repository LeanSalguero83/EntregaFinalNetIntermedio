using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
namespace Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly AuthorizationFilterContext _context;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
		[Authorize]
		public IActionResult Index()
        {
            return View();
        }
    }
}