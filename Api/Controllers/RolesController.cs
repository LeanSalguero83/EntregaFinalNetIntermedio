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
    public class RolesController
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        [Route("BuscarRoles")]
        public async Task<List<Roles>> BuscarRoles()
        {
            
            return await _rolesService.BuscarRolesAsync();
        }

        [HttpPost]
        [Route("GuardarRol")]
        public async Task<List<Roles>> GuardarRol(Roles roles)
        {
           
            return await _rolesService.GuardarRolASync(roles);
        }

        [HttpPost]
        [Route("EliminarRol")]
        public async Task<List<Roles>> EliminarRol(Roles roles)
        {
            
            return await _rolesService.EliminarRolASync(roles);
        }


    }
}
