using System.ComponentModel.DataAnnotations;

public class Departamento : IValidatableObject
{
    public int DepartamentoID { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "La fecha de creación es obligatoria")]
    [DataType(DataType.Date)]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El estado es obligatorio")]
    [RegularExpression("Activo|Inactivo", ErrorMessage = "El estado debe ser Activo o Inactivo")]
    public string Estado { get; set; }

    [Required(ErrorMessage = "El campo 'CreadoPor' es obligatorio")]
    public string CreadoPor { get; set; }

    public string ModificadoPor { get; set; }

    [DataType(DataType.Date)]
    public DateTime? FechaModificacion { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DepartamentoID > 0 && FechaModificacion == null)
        {
            yield return new ValidationResult(
                "La fecha de modificación es obligatoria al editar el departamento.",
                new[] { nameof(FechaModificacion) });
        }
    }
}
