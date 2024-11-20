using PuntoVenta.Models.Response;

public interface IAutor{
    public Task<object> GuardarAutor(Autores autores);
    public Task<object> ActualizarAutor(Autores autores);
    public Task<object> EliminarAutor(Guid idAutor);
    object ConsultarAutor( string? busqueda);

}