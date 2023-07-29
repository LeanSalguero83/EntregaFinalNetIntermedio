using Api.Interfaces;
using Api.Services;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController
    {
        private readonly IUsuariosService _usuariosService;

        public UsuariosController(IUsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }
        [Authorize]
        [HttpGet]
        [Route("BuscarUsuarios")]
        public async Task<List<Usuarios>> BuscarUsuarios()
        {

            return await _usuariosService.BuscarUsuariosAsync();
        }

        [HttpPost]
        [Route("GuardarUsuario")]
        public async Task<List<Usuarios>> GuardarUsuario(Usuarios usuarios)
        {
            
            return await _usuariosService.GuardarUsuarioASync(usuarios);
        }

        [HttpPost]
        [Route("EliminarUsuario")]
        public async Task<List<Usuarios>> EliminarUsuario(Usuarios usuarios)
        {
            
            return await _usuariosService.EliminarUsuarioASync(usuarios);
        }


    }
}