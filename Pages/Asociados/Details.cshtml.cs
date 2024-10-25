using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SistemaDeGestionSalarial.Data;
using SistemaDeGestionSalarial.Models;

namespace SistemaDeGestionSalarial.Pages.Asociados
{
    public class DetailsModel : PageModel
    {
        private readonly SistemaDeGestionSalarial.Data.DatabaseContext _context;

        public DetailsModel(SistemaDeGestionSalarial.Data.DatabaseContext context)
        {
            _context = context;
        }

        public Asociado Asociado { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Incluir el departamento en la consulta para que cargue la relación
            Asociado = await _context.Asociados
                .Include(a => a.Departamento) // Esto carga el Departamento relacionado
                .FirstOrDefaultAsync(m => m.AsociadoID == id);

            if (Asociado == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
