using PuntoVenta.Models.Response;
public interface IUsuario
{
	public Task<object> GuardarUsuario(Usuarios IdUser);
	public Task<object> ActualizarUsuario (Usuarios IdUser);
	object EliminarUsuario (Guid IdUser);
	object ListaUsuario (string busqueda);
}