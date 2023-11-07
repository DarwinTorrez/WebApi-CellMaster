using Microsoft.AspNetCore.Mvc;
using CellMasterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("cellmasterapi/[controller]")]
    public class ControladorFactura : ControllerBase
    {
        private readonly CellMasterContext _DbContext;

        public ControladorFactura(CellMasterContext dbContext)
        {
            this._DbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var facturas = this._DbContext.Facturas.ToList();
            return Ok(facturas);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var factura = this._DbContext.Facturas.FirstOrDefault(f => f.IdFactura == id);
            if (factura != null)
            {
                return Ok(factura);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var factura = this._DbContext.Facturas.FirstOrDefault(f => f.IdFactura == id);
            if (factura != null)
            {
                this._DbContext.Facturas.Remove(factura);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Factura _factura)
        {
            var factura = this._DbContext.Facturas.FirstOrDefault(f => f.IdFactura == _factura.IdFactura);
            if (factura != null)
            {
                return BadRequest("La entidad Factura ya existe. Utiliza la función de actualización en su lugar.");
            }

            this._DbContext.Facturas.Add(_factura);
            this._DbContext.SaveChanges();
            return Ok("La entidad Factura ha sido creada con éxito.");
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] Factura _factura)
        {
            var factura = this._DbContext.Facturas.FirstOrDefault(f => f.IdFactura == id);
            if (factura == null)
            {
                return NotFound("La entidad Factura no existe y no puede ser actualizada.");
            }
            // Actualiza los campos necesarios
            factura.IdFactura = _factura.IdFactura;
            factura.Fecha = _factura.Fecha;
            factura.Total = _factura.Total;
            factura.Estado = _factura.Estado;
            factura.IdUsuarios = _factura.IdUsuarios;
            factura.IdClientes = _factura.IdClientes;

            // Asegúrate de actualizar otros campos según sea necesario

            this._DbContext.SaveChanges();
            return Ok("La entidad Factura ha sido actualizada con éxito.");
        }
    }
}
