using Microsoft.AspNetCore.Mvc;
using SistemaDeGestionSalarial.Data;
using SistemaDeGestionSalarial.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SistemaDeGestionSalarial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsociadosController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public AsociadosController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Asociados
        [HttpGet]
        public async Task<IActionResult> GetAsociados()
        {
            var asociados = await _context.Asociados.Include(a => a.Departamento).ToListAsync();
            return Ok(asociados);
        }

        // GET: api/Asociados/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsociado(int id)
        {
            var asociado = await _context.Asociados.Include(a => a.Departamento).FirstOrDefaultAsync(a => a.AsociadoID == id);
            if (asociado == null)
            {
                return NotFound();
            }
            return Ok(asociado);
        }

        // POST: api/Asociados
        [HttpPost]
        public async Task<IActionResult> CreateAsociado([FromBody] Asociado asociado)
        {
            if (ModelState.IsValid)
            {
                _context.Asociados.Add(asociado);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Asociado creado correctamente." });
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Asociados/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsociado(int id, [FromBody] Asociado asociado)
        {
            if (id != asociado.AsociadoID)
            {
                return BadRequest();
            }

            _context.Entry(asociado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Asociado actualizado correctamente." });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsociadoExists(id))
                {
                    return NotFound();
                }
                throw;
            }
        }

        // DELETE: api/Asociados/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsociado(int id)
        {
            var asociado = await _context.Asociados.FindAsync(id);
            if (asociado == null)
            {
                return NotFound();
            }

            _context.Asociados.Remove(asociado);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Asociado eliminado correctamente." });
        }

        private bool AsociadoExists(int id)
        {
            return _context.Asociados.Any(a => a.AsociadoID == id);
        }
    }
}
