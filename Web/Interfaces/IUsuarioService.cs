using Data.Dtos;
using Data.Entities;
namespace Web.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuarios> BuscarUsuario(LoginDto login);
    }
}
