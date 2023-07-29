using Api.Interfaces;
using Data.Entities;
using Data.Manager;
using Common.Helpers;

namespace Api.Services
{
    public class RolesService : IRolesService
    {
        private readonly RolesManager _manager;

        public RolesService(RolesManager manager)
        {
            _manager = manager;
        }

        public async Task<List<Roles>> BuscarRolesAsync()
        {
            try
            {
                var result = await _manager.BuscarListaAsync();
                return result;
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
        }

        public async Task<List<Roles>> GuardarRolASync(Roles rol)
        {
            try
            {
                var result = await _manager.Guardar(rol, rol.Id);
                return await _manager.BuscarListaAsync();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public async Task<List<Roles>> EliminarRolASync(Roles rol)
        {
            try
            {
                var result = await _manager.Eliminar(rol);
                return await _manager.BuscarListaAsync();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}