using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDeGestionSalarial.Data;
using SistemaDeGestionSalarial.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaDeGestionSalarial.Pages.Aumentos
{
    public class ConsultarAumentosModel : PageModel
    {
        private readonly DatabaseContext _context;

        public ConsultarAumentosModel(DatabaseContext context)
        {
            _context = context;
        }

        // Propiedad para almacenar la lista de detalles de los aumentos
        public List<AumentoDetalle> AumentosDetalles { get; set; }

        // Método OnGet para recuperar los detalles de los aumentos
        public async Task OnGetAsync()
        {
            // Obtener los detalles de los aumentos ejecutando el procedimiento almacenado
            AumentosDetalles = await _context.AumentoDetalles
                .FromSqlRaw("EXEC ConsultarAumentos")
                .ToListAsync();
        }
    }
}
