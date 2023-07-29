using Data.Base;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Manager
{
    public class ServiciosManager : BaseManager<Servicios>
    {
        public ServiciosManager(ApplicationDbContext context) : base(context)
        {
        }

        public async override Task<List<Servicios>> Borrar(Servicios servicio)
        {
            await context.Database.ExecuteSqlRawAsync($"EXEC EliminarServicio @p0", servicio.Id);

            return await context.Servicios.FromSqlRaw("EXEC ObtenerServicios").ToListAsync();
        }

        public override Task<List<Servicios>> BuscarAsync(Servicios entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Servicios>> GuardarAsync(Servicios servicio)
        {
            var p = await context.Database.ExecuteSqlRawAsync($"EXEC GuardaroActualizarServicios @p0, @p1, @p2",
                servicio.Id, servicio.Nombre, servicio.Activo);

            return await context.Servicios.FromSqlRaw("EXEC ObtenerServicios").ToListAsync();
        }

        public async  override Task<List<Servicios>> BuscarListaAsync()
        {
            return  context.Servicios.FromSqlRaw("ObtenerServicios").ToList();
		}
    }
}
