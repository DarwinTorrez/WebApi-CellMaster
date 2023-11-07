using Microsoft.AspNetCore.Mvc;
using CellMasterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("cellmasterapi/[controller]")]
    public class ControladorMarca : ControllerBase
    {
        private readonly CellMasterContext _DbContext;

        public ControladorMarca(CellMasterContext dbContext)
        {
            this._DbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var marcas = this._DbContext.Marcas.ToList();
            return Ok(marcas);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var marca = this._DbContext.Marcas.FirstOrDefault(m => m.IdMarca == id);
            if (marca != null)
            {
                return Ok(marca);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var marca = this._DbContext.Marcas.FirstOrDefault(m => m.IdMarca == id);
            if (marca != null)
            {
                this._DbContext.Marcas.Remove(marca);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Marca _marca)
        {
            var marca = this._DbContext.Marcas.FirstOrDefault(m => m.IdMarca == _marca.IdMarca);
            if (marca != null)
            {
                return BadRequest("La entidad Marca ya existe. Utiliza la función de actualización en su lugar.");
            }

            this._DbContext.Marcas.Add(_marca);
            this._DbContext.SaveChanges();
            return Ok("La entidad Marca ha sido creada con éxito.");
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] Marca _marca)
        {
            var marca = this._DbContext.Marcas.FirstOrDefault(m => m.IdMarca == id);
            if (marca == null)
            {
                return NotFound("La entidad Marca no existe y no puede ser actualizada.");
            }
            // Actualiza los campos necesarios
            marca.IdMarca = _marca.IdMarca;
            marca.NombreMarca = _marca.NombreMarca;

            // Asegúrate de actualizar otros campos según sea necesario

            this._DbContext.SaveChanges();
            return Ok("La entidad Marca ha sido actualizada con éxito.");
        }
    }
}
