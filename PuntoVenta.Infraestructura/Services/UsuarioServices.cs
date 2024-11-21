using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Models.Response;
public class UsuarioServices : IUsuario
{
    public async Task<object> ActualizarUsuario(Usuarios User)
    {
        var Response = new SimpleResponse();
        var conexionBD = new BDBiblioteca();
        string connetionString = conexionBD.Database.GetDbConnection().ConnectionString;
        using (var conexion = new SqlConnection(connetionString))
        {
            using var command = new SqlCommand();
            command.Connection = conexion;
            command.CommandText = "dbo.AdministrarUsuarios";
            command.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros
            command.Parameters.Add(new SqlParameter("@Op", SqlDbType.VarChar) { Value = "Actualizar" });
            command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.UniqueIdentifier) { Value = User.IdUsuario });
            command.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = User.Nombre });
            command.Parameters.Add(new SqlParameter("@Apellidos", SqlDbType.VarChar) { Value = User.Apellidos });
            command.Parameters.Add(new SqlParameter("@Direccion", SqlDbType.VarChar) { Value = User.Direccion });
            command.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar) { Value = User.Telefono });
            command.Parameters.Add(new SqlParameter("@Correo", SqlDbType.VarChar) { Value = User.Correo });
            command.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar) { Value = User.Password });
            command.Parameters.Add(new SqlParameter("@Usuario", SqlDbType.VarChar) { Value = User.Usuario });
            command.Parameters.Add(new SqlParameter("@Rol", SqlDbType.Binary) { Value = User.Rol });

            try
            {
                conexion.Open();

                var reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    if (await reader.ReadAsync())
                    {
                        Response.Exito = reader.GetBoolean(reader.GetOrdinal("Exito"));
                        Response.Mensaje = reader.GetString(reader.GetOrdinal("Mensaje"));

                    }
                }
            }
            catch (SqlException ex)
            {
                Response.Exito = false;
                Response.Mensaje = ex.Message;
            }
            catch (Exception e)
            {
                Response.Exito = false;
                Response.Mensaje = e.Message;
            }
            conexion.Close();
        }
        return Response;
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

    public async Task<object> GuardarUsuario(Usuarios User)
    {
        var Response = new SimpleResponse();
        var conexionBD = new BDBiblioteca();
        string connetionString = conexionBD.Database.GetDbConnection().ConnectionString;
        using (var conexion = new SqlConnection(connetionString))
        {
            Guid llave = Guid.NewGuid();
            User.IdUsuario = llave;
            using var command = new SqlCommand();
            command.Connection = conexion;
            command.CommandText = "dbo.AdministrarUsuarios";
            command.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros
            command.Parameters.Add(new SqlParameter("@Op", SqlDbType.VarChar) { Value = "Insertar" });
            command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.UniqueIdentifier) { Value = User.IdUsuario });
            command.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = User.Nombre });
            command.Parameters.Add(new SqlParameter("@Apellidos", SqlDbType.VarChar) { Value = User.Apellidos });
            command.Parameters.Add(new SqlParameter("@Direccion", SqlDbType.VarChar) { Value = User.Direccion });
            command.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar) { Value = User.Telefono });
            command.Parameters.Add(new SqlParameter("@Correo", SqlDbType.VarChar) { Value = User.Correo });
            command.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar) { Value = User.Password });
            command.Parameters.Add(new SqlParameter("@Usuario", SqlDbType.VarChar) { Value = User.Usuario });
            command.Parameters.Add(new SqlParameter("@Rol", SqlDbType.Binary) { Value = User.Rol });

            try
            {
                conexion.Open();

                var reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    if (await reader.ReadAsync())
                    {
                        Response.Exito = reader.GetBoolean(reader.GetOrdinal("Exito"));
                        Response.Mensaje = reader.GetString(reader.GetOrdinal("Mensaje"));

                    }
                }
            }
            catch (SqlException ex)
            {
                Response.Exito = false;
                Response.Mensaje = ex.Message;
            }
            catch (Exception e)
            {
                Response.Exito = false;
                Response.Mensaje = e.Message;
            }
            conexion.Close();
        }
        return Response;
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
                ListaUsuarios = (from c in conexion.Usuarios where c.Usuario.Contains(busqueda) select c).ToList();
            }

        }
        return ListaUsuarios;
    }
}