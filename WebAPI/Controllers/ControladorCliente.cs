using Microsoft.AspNetCore.Mvc;
using CellMasterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("cellmasterapi/[controller]")]
    public class ControladorCliente : ControllerBase
    {
        private readonly CellMasterContext _DbContext;

        public ControladorCliente(CellMasterContext dbContext)
        {
            this._DbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var clientes = this._DbContext.Clientes.ToList();
            return Ok(clientes);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var cliente = this._DbContext.Clientes.FirstOrDefault(c => c.IdClientes == id);
            if (cliente != null)
            {
                return Ok(cliente);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var cliente = this._DbContext.Clientes.FirstOrDefault(c => c.IdClientes == id);
            if (cliente != null)
            {
                this._DbContext.Clientes.Remove(cliente);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Cliente _cliente)
        {
            var cliente = this._DbContext.Clientes.FirstOrDefault(c => c.IdClientes == _cliente.IdClientes);
            if (cliente != null)
            {
                return BadRequest("La entidad Cliente ya existe. Utiliza la función de actualización en su lugar.");
            }

            this._DbContext.Clientes.Add(_cliente);
            this._DbContext.SaveChanges();
            return Ok("La entidad Cliente ha sido creada con éxito.");
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] Cliente _cliente)
        {
            var cliente = this._DbContext.Clientes.FirstOrDefault(c => c.IdClientes == id);
            if (cliente == null)
            {
                return NotFound("La entidad Cliente no existe y no puede ser actualizada.");
            }
            // Actualiza los campos necesarios
            cliente.IdClientes = _cliente.IdClientes;

            // Asegúrate de actualizar otros campos según sea necesario

            this._DbContext.SaveChanges();
            return Ok("La entidad Cliente ha sido actualizada con éxito.");
        }
    }
}
