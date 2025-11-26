namespace ProyectoFinal.Dtos
{
    public class MedicamentoCreateDto
    {
        public string NombreComercial { get; set; }
        public int LaboratorioId { get; set; }
        public decimal Precio { get; set; }
        public List<int> ActivosIds { get; set; } = new List<int>();
    }
}
