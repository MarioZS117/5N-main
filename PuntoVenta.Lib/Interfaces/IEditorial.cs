using PuntoVenta.Models.Response;

public interface IEditorial
{
    public Task<object> GuardarEditorial(Editorial editorial);
    public Task<object> BorrarEditorial(Guid idEditorial);
    public Task<object> ActualizarEditorial(Editorial editorial);
    public object ConsultarEditorial(string? Nombre);
}
