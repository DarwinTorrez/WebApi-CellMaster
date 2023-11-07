using Microsoft.AspNetCore.Mvc;
using CellMasterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("cellmasterapi/[controller]")]
    public class ControladorProveedore : ControllerBase
    {
        private readonly CellMasterContext _DbContext;

        public ControladorProveedore(CellMasterContext dbContext)
        {
            this._DbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var proveedores = this._DbContext.Proveedores.ToList();
            return Ok(proveedores);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var proveedor = this._DbContext.Proveedores.FirstOrDefault(p => p.IdProveedores == id);
            if (proveedor != null)
            {
                return Ok(proveedor);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var proveedor = this._DbContext.Proveedores.FirstOrDefault(p => p.IdProveedores == id);
            if (proveedor != null)
            {
                this._DbContext.Proveedores.Remove(proveedor);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Proveedore _proveedor)
        {
            var proveedor = this._DbContext.Proveedores.FirstOrDefault(p => p.IdProveedores == _proveedor.IdProveedores);
            if (proveedor != null)
            {
                return BadRequest("La entidad Proveedore ya existe. Utiliza la función de actualización en su lugar.");
            }

            this._DbContext.Proveedores.Add(_proveedor);
            this._DbContext.SaveChanges();
            return Ok("La entidad Proveedore ha sido creada con éxito.");
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] Proveedore _proveedor)
        {
            var proveedor = this._DbContext.Proveedores.FirstOrDefault(p => p.IdProveedores == id);
            if (proveedor == null)
            {
                return NotFound("La entidad Proveedore no existe y no puede ser actualizada.");
            }
            // Actualiza los campos necesarios
            proveedor.IdProveedores = _proveedor.IdProveedores;
            proveedor.NombreEmpresa = _proveedor.NombreEmpresa;
            proveedor.Departamento = _proveedor.Departamento;
            proveedor.Direccion = _proveedor.Direccion;
            proveedor.Email = _proveedor.Email;

            // Asegúrate de actualizar otros campos según sea necesario

            this._DbContext.SaveChanges();
            return Ok("La entidad Proveedore ha sido actualizada con éxito.");
        }
    }
}
