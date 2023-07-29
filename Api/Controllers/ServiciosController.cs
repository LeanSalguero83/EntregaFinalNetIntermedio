using Api.Interfaces;
using Api.Services;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ServiciosController
    {
        private readonly IServiciosService _serviciosService;

        public ServiciosController(IServiciosService serviciosService)
        {
            _serviciosService = serviciosService;
        }
        [Authorize]
        [HttpGet]
        [Route("BuscarServicios")]
        public async Task<List<Servicios>> BuscarServicios()
        {
            
            return await _serviciosService.BuscarServiciosAsync();
        }

        [HttpPost]
        [Route("GuardarServicio")]
        public async Task<List<Servicios>> GuardarServicio(Servicios servicios)
        {
            
            return await _serviciosService.GuardarServicioASync(servicios);
        }

        [HttpPost]
        [Route("EliminarServicio")]
        public async Task<List<Servicios>> EliminarServicio(Servicios servicios)
        {
            
            return await _serviciosService.EliminarServicioASync(servicios);
        }


    }
}