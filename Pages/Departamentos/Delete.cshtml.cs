using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDeGestionSalarial.Data;
using SistemaDeGestionSalarial.Models;
using System.Threading.Tasks;

namespace SistemaDeGestionSalarial.Pages.Departamentos
{
    public class DeleteModel : PageModel
    {
        private readonly DatabaseContext _context;

        public DeleteModel(DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Departamento Departamento { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Departamento = await _context.Departamentos.FindAsync(id);

            if (Departamento == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var departamento = await _context.Departamentos.FindAsync(id);

            if (departamento == null)
            {
                return NotFound();
            }

            _context.Departamentos.Remove(departamento);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "El departamento ha sido eliminado correctamente.";
            return RedirectToPage("Index");
        }
    }
}
