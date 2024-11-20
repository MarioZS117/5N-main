using PuntoVenta.Models.Response;

public interface IEmpleado{
    public Task<object> GuadarEmpleado(Empleados empleados);
    public Task<object> ActualizarEmpleado(Empleados empleados);
    public Task<object> BorrarEmpleado(Guid idEmpleado);
    public object ConsultarEmpleado(string? Nombre);
}