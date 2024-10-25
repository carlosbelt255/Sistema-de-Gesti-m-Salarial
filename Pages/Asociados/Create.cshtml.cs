using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaDeGestionSalarial.Data;
using SistemaDeGestionSalarial.Models;

namespace SistemaDeGestionSalarial.Pages.Asociados
{
    public class CreateModel : PageModel
    {
        private readonly DatabaseContext _context;

        public CreateModel(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // Cargar la lista de departamentos
            ViewData["DepartamentoID"] = new SelectList(_context.Departamentos, "DepartamentoID", "Nombre");
            return Page();
        }

        [BindProperty]
        public Asociado Asociado { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, cargar nuevamente los departamentos y volver a la página
                ViewData["DepartamentoID"] = new SelectList(_context.Departamentos, "DepartamentoID", "Nombre");
                return Page();
            }

            // Parámetros del procedimiento almacenado
            var parametros = new[]
            {
                new SqlParameter("@Nombre", Asociado.Nombre),
                new SqlParameter("@Salario", Asociado.Salario),
                new SqlParameter("@FechaIngreso", Asociado.FechaIngreso),
                new SqlParameter("@Estado", Asociado.Estado),
                new SqlParameter("@DepartamentoID", Asociado.DepartamentoID),
                new SqlParameter("@CreadoPor", Asociado.CreadoPor)
            };

            // Ejecutar el procedimiento almacenado para insertar el asociado
            await _context.Database.ExecuteSqlRawAsync("EXEC InsertarAsociado @Nombre, @Salario, @FechaIngreso, @Estado, @DepartamentoID, @CreadoPor", parametros);

            // Redirigir a la página de lista de asociados después de guardar
            return RedirectToPage("./Index");
        }
    }
}
