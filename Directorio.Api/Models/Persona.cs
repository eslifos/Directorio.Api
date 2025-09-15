using System.ComponentModel.DataAnnotations;

namespace Directorio.Api.Models
{
    public class Persona
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string ApellidoPaterno { get; set; }

        public string? ApellidoMaterno { get; set; }

        [Required]
        public string Identificacion { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
    }
}
