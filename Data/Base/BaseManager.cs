using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Data.Base
{
    public abstract class BaseManager<T> where T : class
    {
        protected readonly ApplicationDbContext context;

        public BaseManager(ApplicationDbContext context)
        {
            this.context = context;
        }

        public abstract Task<List<T>> BuscarListaAsync();
        public abstract Task<List<T>> BuscarAsync(T entity);
        public abstract Task<List<T>> Borrar(T entity);

        public async Task<bool> Guardar(T entity, int id)
        {
            try
            {
                if (context.Entry(entity).State == EntityState.Detached)
                {
                    if (id == 0)
                        context.Entry(entity).State = EntityState.Added;
                    else
                        context.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    context.ChangeTracker.Clear();
                    if (id == 0)
                        context.Entry(entity).State = EntityState.Added;
                    else
                        context.Entry(entity).State = EntityState.Modified;
                }

                var resultado = await context.SaveChangesAsync() > 0;
                context.Entry(entity).State = EntityState.Detached;
                return resultado;
            }
            catch (Exception ex)
            {
                // Consider logging the exception message here.
                return false;
            }
        }

        public async Task<bool> Eliminar(T entity)
        {
            try
            {
                if (context.Entry(entity).State == EntityState.Detached)
                {
                    context.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    context.ChangeTracker.Clear();
                    context.Entry(entity).State = EntityState.Modified;
                }

                var resultado = await context.SaveChangesAsync() > 0;
                return resultado;
            }
            catch (Exception ex)
            {
                // Consider logging the exception message here.
                return false;
            }
        }

        public void Detach(T entity)
        {
            context.Entry(entity).State = EntityState.Detached;
        }
    }
}
