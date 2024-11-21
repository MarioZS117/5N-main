using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Models.Response
{
    public class Copias
    {
        [Key]
        public Guid idCopia { get; set; }
        public Guid idLibro { get; set; }
        public int Cantidad { get; set; }
        public bool Estado { get; set; }
    }
}
