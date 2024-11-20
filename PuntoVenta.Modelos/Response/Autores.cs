using System.ComponentModel.DataAnnotations;
namespace PuntoVenta.Models.Response{
public class Autores{
    [Key]
    public Guid idAutor { get; set; }
    public string? Nombre{ get; set; }
    public string? Apellido { get; set; }
    
}
}
