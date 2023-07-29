
using Data.Base;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Microsoft.AspNetCore.Authorization;
namespace Web.Controllers
{
    public class RolesController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        public RolesController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
		[Authorize]
		public IActionResult Roles()
        {
            return View();
        }

        public IActionResult RolesAddPartial([FromBody] Roles rol)
        {
            var usuViewModel = new RolesViewModel();
            if (rol != null)
                usuViewModel = rol;
            return PartialView("~/Views/Roles/Partial/RolesAddPartial.cshtml", usuViewModel);
        }

        public async Task<IActionResult> GuardarRol(Roles rol)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var roles = await baseApi.PostToApi("Roles/GuardarRol", rol,token);
            return RedirectToAction("Roles", "Roles");
        }

        public async Task<IActionResult> EliminarRol([FromBody] Roles rol)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            rol.Activo = false;
            var roles = await baseApi.PostToApi("Roles/EliminarRol", rol, token);
            return RedirectToAction("Roles", "Roles");
        }
    }
}
