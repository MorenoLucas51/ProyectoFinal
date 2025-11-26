using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Activo
    {
        public int Id { get; set; }
       public string Nombre { get; set; } 

        public ICollection<Medicamento>  Medicamentos { get; set; } = new List<Medicamento>();
    }
}
