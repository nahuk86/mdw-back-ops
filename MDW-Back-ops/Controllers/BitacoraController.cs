using MDW_Back_ops.Data;
using MDW_Back_ops.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MDW_Back_ops.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BitacoraController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BitacoraController(AppDbContext context)
        {
            _context = context;
        }

        // Endpoint para registrar una entrada en la bitácora
        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarBitacora([FromBody] Bitacora bitacora)
        {
            if (bitacora == null)
                return BadRequest("La información de la bitácora no puede ser nula.");

            bitacora.FechaHora = DateTime.UtcNow; // Establece la fecha y hora actual
            _context.Bitacoras.Add(bitacora);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Bitácora registrada exitosamente" });
        }

        // Endpoint para leer las entradas de la bitácora con filtros opcionales
        [HttpGet("consultar")]
        public IActionResult ConsultarBitacora([FromQuery] string email, [FromQuery] DateTime? desde, [FromQuery] DateTime? hasta)
        {
            var query = _context.Bitacoras.AsQueryable();

            if (!string.IsNullOrEmpty(email))
                query = query.Where(b => b.Email == email);

            if (desde.HasValue)
                query = query.Where(b => b.FechaHora >= desde);

            if (hasta.HasValue)
                query = query.Where(b => b.FechaHora <= hasta);

            var bitacoras = query.OrderByDescending(b => b.FechaHora).ToList();
            return Ok(bitacoras);
        }


        [HttpGet("todos")]
        public IActionResult ObtenerTodos()
        {
            var bitacoras = _context.Bitacoras.OrderByDescending(b => b.FechaHora).ToList();
            return Ok(bitacoras);
        }
    }
}
