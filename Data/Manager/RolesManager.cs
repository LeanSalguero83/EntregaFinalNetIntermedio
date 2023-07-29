using Data.Base;
using Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace Data.Manager
{
    public class RolesManager : BaseManager<Roles>
    {
        public RolesManager(ApplicationDbContext context) : base(context) { }

        public override Task<List<Roles>> Borrar(Roles entity)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Roles>> BuscarAsync(Roles entity)
        {
            throw new NotImplementedException();
        }

        public async override Task<List<Roles>> BuscarListaAsync()
        {
            return await context.Roles.Where(x => x.Activo == true).ToListAsync();
        }
    }
}