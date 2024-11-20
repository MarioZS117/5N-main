using PuntoVenta.Models.Response;
public class UsuarioServices : IUsuario
{
    public object ActualizarUsuario(Usuarios User)
    {
        using (var conexion = new BDBiblioteca())
        {
            var consulta = (from c in conexion.Usuarios where c.IdUsuario == User.IdUsuario select c).FirstOrDefault();
            if (consulta != null)
            {
                consulta.Apellidos = User.Apellidos;
                consulta.Nombre = User.Nombre;
                consulta.Direccion = User.Direccion;
                consulta.Correo = User.Correo;
                consulta.Password = User.Password;
                consulta.Telefono = User.Telefono;
                consulta.Usuario = User.Usuario;
            }
            return conexion.SaveChanges() == 1;
        }
    }

    public object EliminarUsuario(Guid IdUser)
    {
        using (var conexion = new BDBiblioteca())
        {
            var consulta = (from c in conexion.Usuarios where c.IdUsuario == IdUser select c).FirstOrDefault();
            if (consulta != null)
            {
                conexion.Usuarios.Remove(consulta);
            }
            return conexion.SaveChanges() == 1;
        }
    }

    public object GuardarUsuario(Usuarios User)
    {
        using (var conexion = new BDBiblioteca())
        {
            Guid llave = Guid.NewGuid();
            User.IdUsuario = llave;
            conexion.Usuarios.Add(User);
            return conexion.SaveChanges() == 1;
        }
    }

    public object ListaUsuario(string? busqueda)
    {
        List<Usuarios> ListaUsuarios = new List<Usuarios>();
        using (var conexion = new BDBiblioteca())
        {
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                ListaUsuarios = (from c in conexion.Usuarios select c).ToList();
            }
            else
            {
                ListaUsuarios = (from c in conexion.Usuarios where c.Nombre.Contains(busqueda) select c).ToList();
            }

        }
        return ListaUsuarios;
    }
}