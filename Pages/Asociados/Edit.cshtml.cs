using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using SistemaDeGestionSalarial.Data;
using SistemaDeGestionSalarial.Models;

namespace SistemaDeGestionSalarial.Pages.Asociados
{
    public class EditModel : PageModel
    {
        private readonly SistemaDeGestionSalarial.Data.DatabaseContext _context;

        public EditModel(SistemaDeGestionSalarial.Data.DatabaseContext context)
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
                .Include(a => a.Departamento) 
                .FirstOrDefaultAsync(m => m.AsociadoID == id);
            if (asociado == null)
            {
                return NotFound();
            }
            Asociado = asociado;
            ViewData["DepartamentoID"] = new SelectList(_context.Departamentos, "DepartamentoID", "Nombre", Asociado.DepartamentoID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["DepartamentoID"] = new SelectList(_context.Departamentos, "DepartamentoID", "Nombre", Asociado.DepartamentoID);
                return Page();
            }

            // Parámetros para el procedimiento almacenado
            var parametros = new[]
            {
                new SqlParameter("@AsociadoID", Asociado.AsociadoID),
                new SqlParameter("@Nombre", Asociado.Nombre),
                new SqlParameter("@Salario", Asociado.Salario),
                new SqlParameter("@FechaIngreso", Asociado.FechaIngreso),
                new SqlParameter("@Estado", Asociado.Estado),
                new SqlParameter("@DepartamentoID", Asociado.DepartamentoID),
                new SqlParameter("@FechaUltimoAumento", (object)Asociado.FechaUltimoAumento ?? DBNull.Value),
                new SqlParameter("@SalarioAnterior", (object)Asociado.SalarioAnterior ?? DBNull.Value),
                new SqlParameter("@ModificadoPor", Asociado.ModificadoPor ?? "Sistema") 
            };

            // Llamar al procedimiento almacenado
            await _context.Database.ExecuteSqlRawAsync("EXEC ModificarAsociado @AsociadoID, @Nombre, @Salario, @FechaIngreso, @Estado, @DepartamentoID, @FechaUltimoAumento, @SalarioAnterior, @ModificadoPor", parametros);

            return RedirectToPage("./Index");
        }

        private bool AsociadoExists(int id)
        {
            return _context.Asociados.Any(e => e.AsociadoID == id);
        }
    }
}
