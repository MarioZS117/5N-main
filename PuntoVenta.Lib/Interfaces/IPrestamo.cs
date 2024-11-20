using PuntoVenta.Models.Response;

public interface IPrestamo{
    object DetallePrestamo (string? Consulta);
    object DetalleLibro (string? Consulta);
    public Task<object> GuardarPrestamo (Prestamo prestamos);
    public Task<object> ActualizarPrestamo(Prestamo prestamo);
    public Task<object> BorrarPrestamo(Guid idPrestamo);
}