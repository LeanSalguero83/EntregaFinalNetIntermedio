using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;


namespace Data.Entities
{
    public class Productos
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string? Imagen { get; set; }
        public bool Activo { get; set; }
        [NotMapped]
        public IFormFile? Imagen_Archivo { get; set; }
    }
}