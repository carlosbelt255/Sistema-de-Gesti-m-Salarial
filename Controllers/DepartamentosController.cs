using Microsoft.AspNetCore.Mvc;
using SistemaDeGestionSalarial.Data; // Espacio de nombres del contexto de base de datos
using SistemaDeGestionSalarial.Models; // Espacio de nombres de los modelos
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SistemaDeGestionSalarial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public DepartamentosController(DatabaseContext context)
        {
            _context = context;
        }

        // Método GET para obtener la lista de departamentos
        [HttpGet]
        public async Task<IActionResult> GetDepartamentos()
        {
            var departamentos = await _context.Departamentos.ToListAsync();
            return Ok(departamentos);
        }

        // Método GET para obtener un departamento por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartamento(int id)
        {
            var departamento = await _context.Departamentos.FindAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }
            return Ok(departamento);
        }

        // Método POST para crear un nuevo departamento
        [HttpPost]
        public async Task<IActionResult> CreateDepartamento([FromBody] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _context.Departamentos.Add(departamento);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetDepartamento), new { id = departamento.DepartamentoID }, departamento);
            }
            return BadRequest(ModelState);
        }

        // Método PUT para editar un departamento existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartamento(int id, [FromBody] Departamento departamento)
        {
            if (id != departamento.DepartamentoID)
            {
                return BadRequest();
            }

            _context.Entry(departamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartamentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Método DELETE para eliminar un departamento
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartamento(int id)
        {
            var departamento = await _context.Departamentos.FindAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }

            _context.Departamentos.Remove(departamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Método auxiliar para verificar si un departamento existe
        private bool DepartamentoExists(int id)
        {
            return _context.Departamentos.Any(e => e.DepartamentoID == id);
        }
    }
}
