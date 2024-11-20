using System.ComponentModel.DataAnnotations;
namespace PuntoVenta.Models.Response
{
    public class Generos
    {
        [Key]
        public Guid idGenero { get; set; }
        public string? Genero { get; set; }
        public string? Descripcion { get; set; }
    }
}
