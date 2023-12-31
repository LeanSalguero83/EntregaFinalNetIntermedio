﻿using Data.Base;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Manager
{
    public class ProductosManager : BaseManager<Productos>
    {

        public ProductosManager(ApplicationDbContext context) : base(context) { }

        public override Task<List<Productos>> Borrar(Productos entity)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Productos>> BuscarAsync(Productos entity)
        {
            throw new NotImplementedException();
        }

        public async override Task<List<Productos>> BuscarListaAsync()
        {
            return await context.Productos.Where(x => x.Activo == true).Take(4).ToListAsync();

        }
    }
}