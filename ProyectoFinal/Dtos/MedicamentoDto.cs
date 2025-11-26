namespace ProyectoFinal.Dtos
{
    public class MedicamentoDto
    {
        public int Id { get; set; }

        public string NombreComercial { get; set; }

        public int LaboratorioId { get; set; }

        public decimal Precio { get; set; }

       public LaboratorioDto Laboratorio { get; set; }
        public List<ActivoDto> Activos { get; set; }


    }


} 
