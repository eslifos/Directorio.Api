using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Directorio.Api.Models
{
    public class Factura
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NumeroFactura { get; set; }

        [Required]
        public decimal Monto { get; set; }

        public int PersonaId { get; set; }

        [ForeignKey("PersonaId")]
        public virtual Persona Persona { get; set; }
    }
}
