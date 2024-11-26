using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using MDW_Back_ops.Data;
using Microsoft.EntityFrameworkCore;


namespace MDW_Back_ops.Controllers
{

    namespace MDW_Back_ops.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class ScannersController : ControllerBase
        {
            private readonly ScannersDbContext _context;

            public ScannersController(ScannersDbContext context)
            {
                _context = context;
            }

            [HttpGet("available")]
            public async Task<IActionResult> GetAvailableScanners()
            {
                var availableScanners = await _context.Scanners
                    .Where(s => s.Enable)
                    .ToListAsync();

                return Ok(availableScanners);
            }

            [HttpPost("toggle/{id}")]
            public async Task<IActionResult> ToggleScanner(Guid id, [FromQuery] bool enable)
            {
                var scanner = await _context.Scanners.FindAsync(id);
                if (scanner == null)
                    return NotFound($"No se encontró el escáner con ID {id}.");

                scanner.Enable = enable;
                scanner.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return Ok(new { Message = $"El escáner con ID {id} ha sido {(enable ? "habilitado" : "deshabilitado")}." });
            }

            [HttpPost("toggle-all")]
            public async Task<IActionResult> ToggleAllScanners([FromQuery] bool enable)
            {
                var scanners = await _context.Scanners.ToListAsync();

                foreach (var scanner in scanners)
                {
                    scanner.Enable = enable;
                    scanner.UpdatedAt = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();

                return Ok(new { Message = $"Todos los escáneres han sido {(enable ? "habilitados" : "deshabilitados")}." });
            }
        }
    }
}

