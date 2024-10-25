using Microsoft.AspNetCore.Mvc;
using SistemaDeGestionSalarial.Data;
using SistemaDeGestionSalarial.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace SistemaDeGestionSalarial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AumentosController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public AumentosController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost("CalcularAumento")]
        public async Task<IActionResult> CalcularAumento([FromBody] CalcularAumentoSalarioModel model)
        {
            if (ModelState.IsValid)
            {
                var parametros = new[]
                {
            new SqlParameter("@Porcentaje", model.Porcentaje),
            new SqlParameter("@DepartamentoID", model.DepartamentoID.HasValue ? model.DepartamentoID.Value : (object)DBNull.Value),
            new SqlParameter("@Usuario", model.Usuario)
        };

                await _context.Database.ExecuteSqlRawAsync("EXEC CalcularAumentoSalario @Porcentaje, @DepartamentoID, @Usuario", parametros);

                return Ok(new { message = "Aumento de salario calculado correctamente." });
            }

            return BadRequest(ModelState);
        }

        [HttpGet("ConsultarAumentos")]
        public async Task<IActionResult> ConsultarAumentos()
        {
            var detallesAumentos = await _context.AumentoDetalles
                .FromSqlRaw("EXEC ConsultarAumentos")
                .ToListAsync();

            // Devolver los detalles del aumento como un resultado JSON
            return Ok(detallesAumentos);
        }

    }
}
