using System.ComponentModel.DataAnnotations;
namespace PuntoVenta.Models.Response{
    public class Prestamo{
    [Key]
    public Guid idPrestamo { get; set; }
    public Guid idEmpleado {get;set;}
    public Guid idLibro { get; set; }
    public DateTime Fecha { get; set; }
    public Guid idUsuario { get; set; }
}
}
