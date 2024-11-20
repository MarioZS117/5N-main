using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Models.Response;
public class LibrosService : ILibro
{
    public async Task<object> ActualizarLibro(Libros libros)
    {
        var Response = new SimpleResponse();
        var conexionBD = new BDBiblioteca();
        string connetionString = conexionBD.Database.GetDbConnection().ConnectionString;
        using (var conexion = new SqlConnection(connetionString))
        {
            using var command = new SqlCommand();
            command.Connection = conexion;
            command.CommandText = "dbo.AdministrarLibros";
            command.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros
            command.Parameters.Add(new SqlParameter("@Op", SqlDbType.VarChar) { Value = "Actualizar" });
            command.Parameters.Add(new SqlParameter("@idLibro", SqlDbType.UniqueIdentifier) { Value = libros.idLibro});
            command.Parameters.Add(new SqlParameter("@Titulo", SqlDbType.VarChar) { Value = libros.Titulo});
            command.Parameters.Add(new SqlParameter("@idAutor", SqlDbType.UniqueIdentifier) { Value = libros.idAutor });
            command.Parameters.Add(new SqlParameter("@idEditorial", SqlDbType.UniqueIdentifier) { Value = libros.idEditorial });
            command.Parameters.Add(new SqlParameter("@FechaPublicacion", SqlDbType.Date) { Value = libros.FechaPublicacion });
            command.Parameters.Add(new SqlParameter("@Edicion", SqlDbType.VarChar) { Value = libros.Edicion });
            command.Parameters.Add(new SqlParameter("@idGenero", SqlDbType.UniqueIdentifier) { Value = libros.idGenero });
            command.Parameters.Add(new SqlParameter("@idClasificacion", SqlDbType.UniqueIdentifier) { Value = libros.idClasificacion });

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

    public async Task<object> BorrarLibro(Guid idLibro)
    {
          var response = new SimpleResponse();

    // Validar entrada
    if (idLibro == Guid.Empty)
    {
        response.Exito = false;
        response.Mensaje = "El ID del libro no puede ser vacío.";
        return response;
    }

    try
    {
        using var dbContext = new BDBiblioteca();

        var Libro = await dbContext.Libros.FirstOrDefaultAsync(a => a.idLibro == idLibro);

        if (Libro == null)
        {
            response.Exito = false;
            response.Mensaje = "El libro no existe.";
            return response;
        }

        dbContext.Libros.Remove(Libro);
        await dbContext.SaveChangesAsync();

        response.Exito = true;
        response.Mensaje = "El libro ha sido eliminado exitosamente.";
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

    public object ConsultarLibro(string? busqueda)
    {
          List<Libros> ListaClasificacion = new List<Libros>();
        using (var conexion = new BDBiblioteca())
        {
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                var consulta = (from c in conexion.Libros select c).ToList();
                return consulta;
            }
            else
            {
                var consulta = (from c in conexion.Libros where c.Titulo.Contains(busqueda) select c).ToList();
                return consulta;
            }
        }
    }

    async Task<object> ILibro.GuardarLibro(Libros libros)
    {
        var Response = new SimpleResponse();
        var conexionBD = new BDBiblioteca();
        string connetionString = conexionBD.Database.GetDbConnection().ConnectionString;
        using (var conexion = new SqlConnection(connetionString))
        {
            using var command = new SqlCommand();
            command.Connection = conexion;
            command.CommandText = "dbo.AdministrarLibros";
            command.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros
            command.Parameters.Add(new SqlParameter("@Op", SqlDbType.VarChar) { Value = "Insertar" });
            command.Parameters.Add(new SqlParameter("@idLibro", SqlDbType.UniqueIdentifier) { Value = libros.idLibro});
            command.Parameters.Add(new SqlParameter("@Titulo", SqlDbType.VarChar) { Value = libros.Titulo});
            command.Parameters.Add(new SqlParameter("@idAutor", SqlDbType.UniqueIdentifier) { Value = libros.idAutor });
            command.Parameters.Add(new SqlParameter("@idEditorial", SqlDbType.UniqueIdentifier) { Value = libros.idEditorial });
            command.Parameters.Add(new SqlParameter("@FechaPublicacion", SqlDbType.Date) { Value = libros.FechaPublicacion });
            command.Parameters.Add(new SqlParameter("@Edicion", SqlDbType.VarChar) { Value = libros.Edicion });
            command.Parameters.Add(new SqlParameter("@idGenero", SqlDbType.UniqueIdentifier) { Value = libros.idGenero });
            command.Parameters.Add(new SqlParameter("@idClasificacion", SqlDbType.UniqueIdentifier) { Value = libros.idClasificacion });

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