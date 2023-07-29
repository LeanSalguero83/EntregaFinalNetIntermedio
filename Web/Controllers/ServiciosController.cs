using Data.Base;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Microsoft.AspNetCore.Authorization;
namespace Web.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        public ServiciosController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
		[Authorize]
		public IActionResult Servicios()
        {
            return View();
        }

        public IActionResult ServiciosAddPartial([FromBody] Servicios servicio)
        {
            var usuViewModel = new ServiciosViewModel();
            if (servicio != null)
                usuViewModel = servicio;
            return PartialView("~/Views/Servicios/Partial/ServiciosAddPartial.cshtml", usuViewModel);
        }

        public async Task<IActionResult> GuardarServicio(Servicios servicio)
        {
            var baseApi = new BaseApi(_httpClient);
            var token = HttpContext.Session.GetString("Token");
            var servicios = await baseApi.PostToApi("Servicios/GuardarServicio", servicio, token);
            return RedirectToAction("Servicios", "Servicios");
        }

        public async Task<IActionResult> EliminarServicio([FromBody] Servicios servicio)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            servicio.Activo = false;
            var servicios = await baseApi.PostToApi("Servicios/EliminarServicio", servicio, token);
            return RedirectToAction("Servicios", "Servicios");
        }
    }
}
