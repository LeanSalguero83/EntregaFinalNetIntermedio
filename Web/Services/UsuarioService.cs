using Data.Dtos;
using Data.Entities;
using Data.Manager;
using Web.Interfaces;

namespace Web.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuariosManager _manager;

       
        public UsuarioService(UsuariosManager manager)
        {
            _manager = manager;
        }
        public async Task<Usuarios> BuscarUsuario(LoginDto usuario)
        {
            var result = await _manager.BuscarUsuarioGoogleAsync(usuario);
            return result;
        }
    }
}
