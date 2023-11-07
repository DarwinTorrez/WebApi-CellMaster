using Microsoft.AspNetCore.Mvc;
using CellMasterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("cellmasterapi/[controller]")]
    public class ControladorDetalleFactura : ControllerBase
    {
        private readonly CellMasterContext _DbContext;

        public ControladorDetalleFactura(CellMasterContext dbContext)
        {
            this._DbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var detallesFactura = this._DbContext.DetalleFacuras.ToList();
            return Ok(detallesFactura);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var detalleFactura = this._DbContext.DetalleFacuras.FirstOrDefault(d => d.IdDetalleFactura == id);
            if (detalleFactura != null)
            {
                return Ok(detalleFactura);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var detalleFactura = this._DbContext.DetalleFacuras.FirstOrDefault(d => d.IdDetalleFactura == id);
            if (detalleFactura != null)
            {
                this._DbContext.DetalleFacuras.Remove(detalleFactura);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] DetalleFacura _detalleFactura)
        {
            var detalleFactura = this._DbContext.DetalleFacuras.FirstOrDefault(d => d.IdDetalleFactura == _detalleFactura.IdDetalleFactura);
            if (detalleFactura != null)
            {
                return BadRequest("La entidad DetalleFactura ya existe. Utiliza la función de actualización en su lugar.");
            }

            this._DbContext.DetalleFacuras.Add(_detalleFactura);
            this._DbContext.SaveChanges();
            return Ok("La entidad DetalleFactura ha sido creada con éxito.");
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] DetalleFacura _detalleFactura)
        {
            var detalleFactura = this._DbContext.DetalleFacuras.FirstOrDefault(d => d.IdDetalleFactura == id);
            if (detalleFactura == null)
            {
                return NotFound("La entidad DetalleFactura no existe y no puede ser actualizada.");
            }
            // Actualiza los campos necesarios
            detalleFactura.IdDetalleFactura = _detalleFactura.IdDetalleFactura;
            detalleFactura.Cantidad = _detalleFactura.Cantidad;
            detalleFactura.Subtotal = _detalleFactura.Subtotal;
            detalleFactura.Total = _detalleFactura.Total;
            detalleFactura.IdFactura = _detalleFactura.IdFactura;
            detalleFactura.IdProducto = _detalleFactura.IdProducto;
            detalleFactura.IdtipoCambio = _detalleFactura.IdtipoCambio;

            // Asegúrate de actualizar otros campos según sea necesario

            this._DbContext.SaveChanges();
            return Ok("La entidad DetalleFactura ha sido actualizada con éxito.");
        }
    }
}
