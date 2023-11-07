using Microsoft.AspNetCore.Mvc;
using CellMasterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("cellmasterapi/[controller]")]
    public class ControladorCategoria : ControllerBase
    {
        private readonly CellMasterContext _DbContext;

        public ControladorCategoria(CellMasterContext dbContext)
        {
            this._DbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var categorias = this._DbContext.Categorias.ToList();
            return Ok(categorias);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var categoria = this._DbContext.Categorias.FirstOrDefault(c => c.IdCategorias == id);
            if (categoria != null)
            {
                return Ok(categoria);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var categoria = this._DbContext.Categorias.FirstOrDefault(c => c.IdCategorias == id);
            if (categoria != null)
            {
                this._DbContext.Categorias.Remove(categoria);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Categoria _categoria)
        {
            var categoria = this._DbContext.Categorias.FirstOrDefault(c => c.IdCategorias == _categoria.IdCategorias);
            if (categoria != null)
            {
                return BadRequest("La entidad Categoría ya existe. Utiliza la función de actualización en su lugar.");
            }

            this._DbContext.Categorias.Add(_categoria);
            this._DbContext.SaveChanges();
            return Ok("La entidad Categoría ha sido creada con éxito.");
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] Categoria _categoria)
        {
            var categoria = this._DbContext.Categorias.FirstOrDefault(c => c.IdCategorias == id);
            if (categoria == null)
            {
                return NotFound("La entidad Categoría no existe y no puede ser actualizada.");
            }
            // Actualiza los campos necesarios
            categoria.IdCategorias = _categoria.IdCategorias;
            categoria.Nombre = _categoria.Nombre;
            categoria.Descripción = _categoria.Descripción;

            this._DbContext.SaveChanges();
            return Ok("La entidad Categoría ha sido actualizada con éxito.");
        }
    }
}
