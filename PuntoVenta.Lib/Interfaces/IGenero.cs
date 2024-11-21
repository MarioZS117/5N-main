using PuntoVenta.Models.Response;

public interface IGenero{
    public Task<object> GuardarGenero(Generos generos);
    public Task<object> ActualizarGenero(Generos generos);
    public Task<object> BorrarGenero(Guid idGenero);
    public object ConsultarGenero(string? genero);
    }