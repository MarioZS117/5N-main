using System.ComponentModel.DataAnnotations;

namespace PuntoVenta.Models.Response
{
    public class Empleados
    {
        [Key]
        public Guid idEmpleado { get; set; }
        public string? Nombre { get; set; }
        public string? Cargo { get; set; }
        public string? Usuario { get; set; }
        public string? Password { get; set; }
    }
}