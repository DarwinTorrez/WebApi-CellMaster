using Microsoft.AspNetCore.Mvc;
using CellMasterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("cellmasterapi/[controller]")]
    public class ControladorTipoCambio : ControllerBase
    {
        private readonly CellMasterContext _DbContext;

        public ControladorTipoCambio(CellMasterContext dbContext)
        {
            this._DbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var tiposCambio = this._DbContext.TipoCambios.ToList();
            return Ok(tiposCambio);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var tipoCambio = this._DbContext.TipoCambios.FirstOrDefault(t => t.IdtipoCambio == id);
            if (tipoCambio != null)
            {
                return Ok(tipoCambio);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var tipoCambio = this._DbContext.TipoCambios.FirstOrDefault(t => t.IdtipoCambio == id);
            if (tipoCambio != null)
            {
                this._DbContext.TipoCambios.Remove(tipoCambio);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] TipoCambio _tipoCambio)
        {
            var tipoCambio = this._DbContext.TipoCambios.FirstOrDefault(t => t.IdtipoCambio == _tipoCambio.IdtipoCambio);
            if (tipoCambio != null)
            {
                return BadRequest("La entidad TipoCambio ya existe. Utiliza la función de actualización en su lugar.");
            }

            this._DbContext.TipoCambios.Add(_tipoCambio);
            this._DbContext.SaveChanges();
            return Ok("La entidad TipoCambio ha sido creada con éxito.");
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] TipoCambio _tipoCambio)
        {
            var tipoCambio = this._DbContext.TipoCambios.FirstOrDefault(t => t.IdtipoCambio == id);
            if (tipoCambio == null)
            {
                return NotFound("La entidad TipoCambio no existe y no puede ser actualizada.");
            }
            // Actualiza los campos necesarios
            tipoCambio.IdtipoCambio = _tipoCambio.IdtipoCambio;
            tipoCambio.PrecioCambio = _tipoCambio.PrecioCambio;
            tipoCambio.FechaC = _tipoCambio.FechaC;

            // Asegúrate de actualizar otros campos según sea necesario

            this._DbContext.SaveChanges();
            return Ok("La entidad TipoCambio ha sido actualizada con éxito.");
        }
    }
}
