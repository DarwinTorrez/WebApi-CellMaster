using Microsoft.AspNetCore.Mvc;
using CellMasterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("cellmasterapi[controller]")]
    public class ControladorProducto : ControllerBase
    {
        private readonly CellMasterContext _DbContext;

        public ControladorProducto(CellMasterContext dbContext)
        {
            this._DbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var productos = this._DbContext.Productos.ToList();
            return Ok(productos);
        }

        [HttpGet("GetByCode/{code}")]
        public IActionResult GetByCode(int code)
        {
            var producto = this._DbContext.Productos.FirstOrDefault(o => o.IdProducto == code);
            if (producto != null)
            {
                return Ok(producto);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{code}")]
        public IActionResult Remove(int code)
        {
            var producto = this._DbContext.Productos.FirstOrDefault(o => o.IdProducto == code);
            if (producto != null)
            {
                this._DbContext.Productos.Remove(producto);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct([FromBody] Producto _producto)
        {
        var producto = this._DbContext.Productos.FirstOrDefault(o => o.IdProducto == _producto.IdProducto);
            if (producto != null)
            {
                producto.IdProducto = _producto.IdProducto;
                producto.CodigoProd = _producto.CodigoProd;
                producto.NombreProducto = _producto.NombreProducto;
                producto.PrecioVenta = _producto.PrecioVenta;
                producto.Stock = _producto.Stock;
                producto.StockMinimo = _producto.StockMinimo;
                producto.IdCategorias = _producto.IdCategorias;
                producto.IdMarca = _producto.IdMarca;
            }
            else
            {
                this._DbContext.Productos.Add(_producto);
            }
                this._DbContext.SaveChanges();
            return Ok(true);
        }

        // Medodo Crear
        [HttpPost("Create")]
        public IActionResult Create([FromBody] Producto _producto)
        {
            var Producto = this._DbContext.Productos.FirstOrDefault(o => o.IdProducto == _producto.IdProducto);
            if (Producto != null)
            {
                return BadRequest("La entidad Componente ya existe. Utiliza la función de actualización en su lugar.");
            }

            this._DbContext.Productos.Add(_producto);
            this._DbContext.SaveChanges();
            return Ok("La entidad Productos ha sido creada con éxito.");
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] Producto _producto)
            {
                var producto = this._DbContext.Productos.FirstOrDefault(o => o.IdProducto == id);
                if (producto == null)
            {
            return NotFound("La entidad Producto no existe y no puede ser actualizada.");
            }
            // Actualiza los campos necesarios
                producto.IdProducto = _producto.IdProducto;
                producto.CodigoProd = _producto.CodigoProd;
                producto.NombreProducto = _producto.NombreProducto;
                producto.PrecioVenta = _producto.PrecioVenta;
                producto.Stock = _producto.Stock;
                producto.StockMinimo = _producto.StockMinimo;
                producto.IdCategorias = _producto.IdCategorias;
                producto.IdMarca = _producto.IdMarca;

                this._DbContext.SaveChanges();
            return Ok("La entidad Producto ha sido actualizada con éxito.");
        }
    }
}
