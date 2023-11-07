using Microsoft.AspNetCore.Mvc;
using CellMasterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("cellmasterapi/[controller]")]
    public class ControladorDetalleCompra : ControllerBase
    {
        private readonly CellMasterContext _DbContext;

        public ControladorDetalleCompra(CellMasterContext dbContext)
        {
            this._DbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var detallesCompra = this._DbContext.DetalleCompras.ToList();
            return Ok(detallesCompra);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var detalleCompra = this._DbContext.DetalleCompras.FirstOrDefault(d => d.IdDetalleCompra == id);
            if (detalleCompra != null)
            {
                return Ok(detalleCompra);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var detalleCompra = this._DbContext.DetalleCompras.FirstOrDefault(d => d.IdDetalleCompra == id);
            if (detalleCompra != null)
            {
                this._DbContext.DetalleCompras.Remove(detalleCompra);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] DetalleCompra _detalleCompra)
        {
            var detalleCompra = this._DbContext.DetalleCompras.FirstOrDefault(d => d.IdDetalleCompra == _detalleCompra.IdDetalleCompra);
            if (detalleCompra != null)
            {
                return BadRequest("La entidad DetalleCompra ya existe. Utiliza la función de actualización en su lugar.");
            }

            this._DbContext.DetalleCompras.Add(_detalleCompra);
            this._DbContext.SaveChanges();
            return Ok("La entidad DetalleCompra ha sido creada con éxito.");
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] DetalleCompra _detalleCompra)
        {
            var detalleCompra = this._DbContext.DetalleCompras.FirstOrDefault(d => d.IdDetalleCompra == id);
            if (detalleCompra == null)
            {
                return NotFound("La entidad DetalleCompra no existe y no puede ser actualizada.");
            }
            // Actualiza los campos necesarios
            detalleCompra.IdDetalleCompra = _detalleCompra.IdDetalleCompra;
            detalleCompra.PrecioCompra = _detalleCompra.PrecioCompra;
            detalleCompra.CantidadProducto = _detalleCompra.CantidadProducto;
            detalleCompra.TotalCompra = _detalleCompra.TotalCompra;
            detalleCompra.IdCompras = _detalleCompra.IdCompras;
            detalleCompra.IdProducto = _detalleCompra.IdProducto;

            // Asegúrate de actualizar otros campos según sea necesario

            this._DbContext.SaveChanges();
            return Ok("La entidad DetalleCompra ha sido actualizada con éxito.");
        }
    }
}
