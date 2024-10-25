namespace SistemaDeGestionSalarial.Models
{
    public class AumentoDetalle
    {
        public int AumentoID { get; set; }
        public string NombreDepartamento { get; set; }
        public decimal Porcentaje { get; set; }
        public DateTime FechaAumento { get; set; }
        public string Descripcion { get; set; }
        public string CreadoPor { get; set; }
        public int AsociadoID { get; set; }
        public string NombreAsociado { get; set; }
        public decimal SalarioActual { get; set; }
        public decimal? SalarioAnterior { get; set; }
    }
}
