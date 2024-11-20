using PuntoVenta.Models.Response;
public interface ILibro{
    public Task<object> GuardarLibro (Libros libros);
    public Task<object> ActualizarLibro (Libros libros);
    public Task<object> BorrarLibro (Guid idLibro);
    public object ConsultarLibro (string? busqueda);
}