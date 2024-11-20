using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Models.Response
{
    public class Clasificaciones{
        [Key]
        public Guid idClasificacion { get; set; }
        public string? Tipo { get; set; }
        public string? Descripcion { get; set; }
    }
}
