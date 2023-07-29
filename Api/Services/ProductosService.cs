using Api.Interfaces;
using Common.Helpers;
using Data.Entities;
using Data.Manager;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class ProductosService : IProductosService
    {
        private readonly ProductosManager _manager;

        public ProductosService(ProductosManager manager)
        {
            _manager = manager;
        }

        public async Task<List<Productos>> BuscarProductosAsync()
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

        public async Task<List<Productos>> GuardarProductoASync(Productos producto)
        {
            try
            {
                var result = await _manager.Guardar(producto, producto.Id);
                return await _manager.BuscarListaAsync();
            }
            catch (Exception ex)
            {
               
                throw ex;
            }

        }

        public async Task<List<Productos>> EliminarProductoASync(Productos producto)
        {
            try
            {
                var result = await _manager.Eliminar(producto);
                return await _manager.BuscarListaAsync();
            }
            catch (Exception ex)
            {
               
                throw ex;
            }

        }
    }
}