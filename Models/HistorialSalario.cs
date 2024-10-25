namespace SistemaDeGestionSalarial.Models
{
    public class HistorialSalario
    {
        public int HistorialID { get; set; } // Primary Key
        public int AsociadoID { get; set; } // Foreign Key

        public decimal SalarioAnterior { get; set; }
        public decimal SalarioNuevo { get; set; }
        public DateTime FechaAjuste { get; set; }
        public int AumentoID { get; set; } // Foreign Key
        public string CreadoPor { get; set; }

        // Relación con el modelo Asociado
        public Asociado Asociado { get; set; }

        // Relación con el modelo Aumento
        public Aumento Aumento { get; set; }
    }
}
