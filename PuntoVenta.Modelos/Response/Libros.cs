using System.ComponentModel.DataAnnotations;
namespace PuntoVenta.Models.Response
{
    public class Libros
    {
        [Key]
        public Guid idLibro { get; set; }
        public string? Titulo { get; set; }
        public Guid idAutor { get; set; }
        public Guid idEditorial { get; set; }
        public DateOnly FechaPublicacion { get; set; }
        public string? Edicion { get; set; }
        public Guid idGenero { get; set; }
        public Guid idClasificacion { get; set; }
    }
}
