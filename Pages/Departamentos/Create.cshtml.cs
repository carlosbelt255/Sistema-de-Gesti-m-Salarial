using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDeGestionSalarial.Data;
using SistemaDeGestionSalarial.Models;
using System.Threading.Tasks;

namespace SistemaDeGestionSalarial.Pages.Departamentos
{
    public class CreateModel : PageModel
    {
        private readonly DatabaseContext _context;

        public CreateModel(DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Departamento Departamento { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Log para depurar
            Console.WriteLine("Ejecutando POST para crear un departamento");

            try
            {
                _context.Departamentos.Add(Departamento);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "El departamento ha sido creado correctamente.";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                // Capturar excepciones y mostrarlas para depuración
                ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                return Page();
            }
        }
    }
}
