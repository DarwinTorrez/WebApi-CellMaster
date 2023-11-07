using Microsoft.AspNetCore.Mvc;
using CellMasterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("cellmasterapi/[controller]")]
    public class ControladorUsuario : ControllerBase
    {
        private readonly CellMasterContext _DbContext;

        public ControladorUsuario(CellMasterContext dbContext)
        {
            this._DbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var usuarios = this._DbContext.Usuarios.ToList();
            return Ok(usuarios);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var usuario = this._DbContext.Usuarios.FirstOrDefault(u => u.IdUsuarios == id);
            if (usuario != null)
            {
                return Ok(usuario);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var usuario = this._DbContext.Usuarios.FirstOrDefault(u => u.IdUsuarios == id);
            if (usuario != null)
            {
                this._DbContext.Usuarios.Remove(usuario);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Usuario _usuario)
        {
            var usuario = this._DbContext.Usuarios.FirstOrDefault(u => u.IdUsuarios == _usuario.IdUsuarios);
            if (usuario != null)
            {
                return BadRequest("La entidad Usuario ya existe. Utiliza la función de actualización en su lugar.");
            }

            this._DbContext.Usuarios.Add(_usuario);
            this._DbContext.SaveChanges();
            return Ok("La entidad Usuario ha sido creada con éxito.");
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] Usuario _usuario)
        {
            var usuario = this._DbContext.Usuarios.FirstOrDefault(u => u.IdUsuarios == id);
            if (usuario == null)
            {
                return NotFound("La entidad Usuario no existe y no puede ser actualizada.");
            }
            // Actualiza los campos necesarios
            usuario.IdUsuarios = _usuario.IdUsuarios;
            usuario.Cargo = _usuario.Cargo;
            usuario.Usuario1 = _usuario.Usuario1;
            usuario.Contraseña = _usuario.Contraseña;
            usuario.Estado = _usuario.Estado;
            usuario.Email = _usuario.Email;

            // Asegúrate de actualizar otros campos según sea necesario

            this._DbContext.SaveChanges();
            return Ok("La entidad Usuario ha sido actualizada con éxito.");
        }
    }
}
