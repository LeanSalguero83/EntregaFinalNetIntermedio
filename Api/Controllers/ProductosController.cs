using Api.Interfaces;
using Api.Services;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController 
    {

        private readonly IProductosService _productosService;

        public ProductosController(IProductosService productosService)
        {
            _productosService = productosService;
        }

        [HttpGet]
        [Route("BuscarProductos")]
        public async Task<List<Productos>> BuscarProductos()
        {
            return await _productosService.BuscarProductosAsync();
        }

        [HttpPost]
        [Route("GuardarProducto")]
        public async Task<List<Productos>> GuardarProducto(Productos productos)
        {
            return await _productosService.GuardarProductoASync(productos);
        }

        [HttpPost]
        [Route("EliminarProducto")]
        public async Task<List<Productos>> EliminarProducto(Productos productos)
        {
            return await _productosService.EliminarProductoASync(productos);
        }
    }
}
