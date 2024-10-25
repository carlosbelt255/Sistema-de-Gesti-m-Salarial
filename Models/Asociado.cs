using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDeGestionSalarial.Models
{
    public class Asociado
    {
        public int AsociadoID { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El salario es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "El salario debe ser mayor a 0")]
        public decimal Salario { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El estado es obligatorio")]
        [RegularExpression("Activo|Inactivo", ErrorMessage = "El estado debe ser Activo o Inactivo")]
        public string Estado { get; set; }

        public int? DepartamentoID { get; set; }

        // Nueva propiedad para mostrar el nombre del departamento
        [NotMapped] // Esto indica que la propiedad no corresponde a una columna en la tabla de la base de datos
        public string NombreDpto { get; set; }

        public DateTime? FechaUltimoAumento { get; set; }
        public decimal? SalarioAnterior { get; set; }

        [Required]
        public string CreadoPor { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }

        [ForeignKey("DepartamentoID")]
        public Departamento Departamento { get; set; }
    }
}
