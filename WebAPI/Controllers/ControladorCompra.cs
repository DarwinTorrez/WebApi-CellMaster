using Microsoft.AspNetCore.Mvc;
using CellMasterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("cellmasterapi/[controller]")]
    public class ControladorCompra : ControllerBase
    {
        private readonly CellMasterContext _DbContext;

        public ControladorCompra(CellMasterContext dbContext)
        {
            this._DbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var compras = this._DbContext.Compras.ToList();
            return Ok(compras);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var compra = this._DbContext.Compras.FirstOrDefault(c => c.IdCompras == id);
            if (compra != null)
            {
                return Ok(compra);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var compra = this._DbContext.Compras.FirstOrDefault(c => c.IdCompras == id);
            if (compra != null)
            {
                this._DbContext.Compras.Remove(compra);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Compra _compra)
        {
            var compra = this._DbContext.Compras.FirstOrDefault(c => c.IdCompras == _compra.IdCompras);
            if (compra != null)
            {
                return BadRequest("La entidad Compra ya existe. Utiliza la función de actualización en su lugar.");
            }

            this._DbContext.Compras.Add(_compra);
            this._DbContext.SaveChanges();
            return Ok("La entidad Compra ha sido creada con éxito.");
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] Compra _compra)
        {
            var compra = this._DbContext.Compras.FirstOrDefault(c => c.IdCompras == id);
            if (compra == null)
            {
                return NotFound("La entidad Compra no existe y no puede ser actualizada.");
            }
            // Actualiza los campos necesarios
            compra.IdCompras = _compra.IdCompras;
            compra.FechaCompra = _compra.FechaCompra;
            compra.IdUsuarios = _compra.IdUsuarios;
            compra.IdProveedores = _compra.IdProveedores;

            // Asegúrate de actualizar otros campos según sea necesario

            this._DbContext.SaveChanges();
            return Ok("La entidad Compra ha sido actualizada con éxito.");
        }
    }
}
