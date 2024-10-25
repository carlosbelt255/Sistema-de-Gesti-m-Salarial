using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeGestionSalarial.Models
{
    public class CalcularAumentoSalarioModel
    {
        [Required(ErrorMessage = "El porcentaje es obligatorio")]
        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100")]
        public decimal Porcentaje { get; set; }

        // Si es nulo o 0, se aplicará a todos los departamentos
        public int? DepartamentoID { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string Usuario { get; set; }

        // Esta lista solo se usa en la vista y no debe ser requerida en la validación POST
        [BindProperty(SupportsGet = true)] // Solo para GET
        public List<Departamento> Departamentos { get; set; } = new List<Departamento>(); // Inicializar la lista para evitar null
    }
}
