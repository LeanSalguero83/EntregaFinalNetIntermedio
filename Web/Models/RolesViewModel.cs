using Data.Entities;
namespace Web.Models
{
    public class RolesViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public static implicit operator RolesViewModel(Roles rol)
        {
            var rolViewModel = new RolesViewModel();
            rolViewModel.Id = rol.Id;
            rolViewModel.Nombre = rol.Nombre;
            rolViewModel.Activo = rol.Activo;
            return rolViewModel;

        }

    }
}
