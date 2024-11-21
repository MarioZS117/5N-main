using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Models.Response
{
    public class Editorial
    {
        [Key]
        public Guid idEditorial { get; set; }
        public string? Nombre { get; set; }
    }
}
