using PuntoVenta.Models.Response;

public interface ICopias{
    public Task<object> GuadarCopia(Copias copias);
    public Task<object> BorrarCopia(Guid idCopia);
    public Task<object> ActualizarCopia(Copias copias);
    public object ConsultarCopias(string? Estado);
}