using PuntoVenta.Models.Response;
public interface IUsuario
{
	object GuardarUsuario(Usuarios IdUser);
	object ActualizarUsuario (Usuarios IdUser);
	object EliminarUsuario (Guid IdUser);
	object ListaUsuario (string busqueda);
}