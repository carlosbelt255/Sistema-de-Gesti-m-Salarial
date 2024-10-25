using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDeGestionSalarial.Data;
using SistemaDeGestionSalarial.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SistemaDeGestionSalarial.Pages.Aumentos
{
    public class CalcularAumentoModel : PageModel
    {
        private readonly DatabaseContext _context;

        public CalcularAumentoModel(DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CalcularAumentoSalarioModel AumentoModel { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Asegurarnos de inicializar el modelo si es nulo
            if (AumentoModel == null)
            {
                AumentoModel = new CalcularAumentoSalarioModel();
            }

            // Cargar la lista de departamentos desde la base de datos
            AumentoModel.Departamentos = await _context.Departamentos.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                AumentoModel.Departamentos = await _context.Departamentos.ToListAsync();
                return Page();
            }

            // Llamada al procedimiento almacenado para calcular aumento de salario
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC CalcularAumentoSalario @p0, @p1, @p2",
                AumentoModel.Porcentaje,
                AumentoModel.DepartamentoID.HasValue ? (object)AumentoModel.DepartamentoID : DBNull.Value,
                AumentoModel.Usuario
            );

            // Redirigir a la página de consulta de aumentos después de crear el aumento
            return RedirectToPage("/Aumentos/ConsultarAumentos");
        }
    }
}
