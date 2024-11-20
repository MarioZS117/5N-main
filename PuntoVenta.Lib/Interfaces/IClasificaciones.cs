using PuntoVenta.Models.Response;

public interface IClasificaciones{
    public  Task<object> GuardarClasificacion(Clasificaciones clasificaciones);
    public Task<object> ActualizarClasificacion(Clasificaciones clasificaciones);
    public Task<object> BorrarClasificacion(Guid idClasificacion);
    public object ConsultarClasificacion(string? busqueda);
}