namespace ProyectoFinal.Models
{
    public class Medicamento
    {
        public int Id { get; set; }
        public string NombreComercial { get; set; }
        public decimal Precio { get; set; }
        public int LaboratorioId { get; set; }

        public Laboratorio Laboratorio { get; set; } = null!;

        public ICollection<Activo> Activos { get; set; } = new List<Activo>() ;
    }
}
