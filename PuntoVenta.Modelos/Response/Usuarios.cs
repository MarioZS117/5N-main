using System.ComponentModel.DataAnnotations;
namespace PuntoVenta.Models.Response
{
    public class Usuarios
    {
        [Key]
        public Guid IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Password { get; set; }
        public string? Usuario { get; set; }
    }
}
