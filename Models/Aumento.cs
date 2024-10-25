namespace SistemaDeGestionSalarial.Models
{
    public class Aumento
    {
        public int AumentoID { get; set; } // Primary Key
        public int? DepartamentoID { get; set; } // Foreign Key (Nullable)
        public decimal Porcentaje { get; set; }
        public DateTime FechaAumento { get; set; }
        public string Descripcion { get; set; }
        public string CreadoPor { get; set; }

        // Relación con el modelo Departamento
        public Departamento Departamento { get; set; }
    }
}
