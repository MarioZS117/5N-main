using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Models.Response;

public class AutorService : IAutor
{
    public async Task<object> ActualizarAutor(Autores autores)
    {
        var Response = new SimpleResponse();
        var conexionBD = new BDBiblioteca();
        string connetionString = conexionBD.Database.GetDbConnection().ConnectionString;
        using (var conexion = new SqlConnection(connetionString))
        {
            using var command = new SqlCommand();
            command.Connection = conexion;
            command.CommandText = "dbo.AdministrarAutores";
            command.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros
            command.Parameters.Add(new SqlParameter("@Op", SqlDbType.VarChar) { Value = "Actualizar" });
            command.Parameters.Add(new SqlParameter("@idAutor", SqlDbType.UniqueIdentifier) { Value = autores.idAutor });
            command.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = autores.Nombre });
            command.Parameters.Add(new SqlParameter("@Apellido", SqlDbType.VarChar) { Value = autores.Apellido });

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

    public object ConsultarAutor(string? busqueda)
    {
        List<Autores> ListaAutores = new List<Autores>();
        using (var conexion = new BDBiblioteca())
        {
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                var consulta = (from c in conexion.Autores select c).ToList();
                return consulta;
            }
            else
            {
                var consulta = (from c in conexion.Autores where c.Nombre.Contains(busqueda) select c).ToList();
                return consulta;
            }
        }
    }

    public async Task<object> EliminarAutor(Guid idAutor)
{
    var response = new SimpleResponse();

    // Validar entrada
    if (idAutor == Guid.Empty)
    {
        response.Exito = false;
        response.Mensaje = "El ID del autor no puede ser vacío.";
        return response;
    }

    try
    {
        using var dbContext = new BDBiblioteca();

        // Buscar el autor por ID
        var autor = await dbContext.Autores.FirstOrDefaultAsync(a => a.idAutor == idAutor);

        if (autor == null)
        {
            response.Exito = false;
            response.Mensaje = "El autor no existe.";
            return response;
        }

        // Eliminar el autor
        dbContext.Autores.Remove(autor);
        await dbContext.SaveChangesAsync();

        response.Exito = true;
        response.Mensaje = "El autor ha sido eliminado exitosamente.";
    }
    catch (DbUpdateException ex)
    {
        response.Exito = false;
        response.Mensaje = $"Error al actualizar la base de datos: {ex.Message}";
    }
    catch (Exception ex)
    {
        response.Exito = false;
        response.Mensaje = $"Error inesperado: {ex.Message}";
    }

    return response;
}


    public async Task<object> GuardarAutor(Autores autores)
    {
        var Response = new SimpleResponse();
        var conexionBD = new BDBiblioteca();
        string connetionString = conexionBD.Database.GetDbConnection().ConnectionString;
        using (var conexion = new SqlConnection(connetionString))
        {
            Guid llave = Guid.NewGuid();
            autores.idAutor = llave;
            using var command = new SqlCommand();
            command.Connection = conexion;
            command.CommandText = "dbo.AdministrarAutores";
            command.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros
            command.Parameters.Add(new SqlParameter("@Op", SqlDbType.VarChar) { Value = "Insertar" });
            command.Parameters.Add(new SqlParameter("@idAutor", SqlDbType.UniqueIdentifier) { Value = autores.idAutor });
            command.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = autores.Nombre });
            command.Parameters.Add(new SqlParameter("@Apellido", SqlDbType.VarChar) { Value = autores.Apellido });

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

}
