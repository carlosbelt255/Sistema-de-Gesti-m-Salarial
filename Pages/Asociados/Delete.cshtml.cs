using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaDeGestionSalarial.Data;
using SistemaDeGestionSalarial.Models;

namespace SistemaDeGestionSalarial.Pages.Asociados
{
    public class DeleteModel : PageModel
    {
        private readonly DatabaseContext _context;

        public DeleteModel(DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Asociado Asociado { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asociado = await _context.Asociados
                .FirstOrDefaultAsync(m => m.AsociadoID == id);

            if (asociado == null)
            {
                return NotFound();
            }
            else
            {
                Asociado = asociado;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asociado = await _context.Asociados.FindAsync(id);
            if (asociado != null)
            {
                // Parámetros para el procedimiento almacenado
                var parametros = new[]
                {
                    new SqlParameter("@AsociadoID", asociado.AsociadoID),
                    new SqlParameter("@ModificadoPor", User.Identity?.Name ?? "admin") // Usuario actual o un valor predeterminado
                };

                // Ejecutar el procedimiento almacenado para eliminación lógica
                await _context.Database.ExecuteSqlRawAsync("EXEC EliminarAsociado @AsociadoID, @ModificadoPor", parametros);

                return RedirectToPage("./Index");
            }

            return NotFound();
        }
    }
}
