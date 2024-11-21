using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Models.Response;

public class ClasificacionesService : IClasificaciones
{
    public async Task<object> ActualizarClasificacion(Clasificaciones clasificaciones)
    {
        var Response = new SimpleResponse();
        var conexionBD = new BDBiblioteca();
        string connetionString = conexionBD.Database.GetDbConnection().ConnectionString;
        using (var conexion = new SqlConnection(connetionString))
        {
            using var command = new SqlCommand();
            command.Connection = conexion;
            command.CommandText = "dbo.AdministrarClasificaciones";
            command.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros
            command.Parameters.Add(new SqlParameter("@Op", SqlDbType.VarChar) { Value = "Actualizar" });
            command.Parameters.Add(new SqlParameter("@idClasificacion", SqlDbType.UniqueIdentifier) { Value = clasificaciones.idClasificacion });
            command.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar) { Value = clasificaciones.Descripcion });
            command.Parameters.Add(new SqlParameter("@Tipo", SqlDbType.VarChar) { Value = clasificaciones.Tipo });
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

    public async Task<object> BorrarClasificacion(Guid idClasificacion)
    {
         var response = new SimpleResponse();

    // Validar entrada
    if (idClasificacion == Guid.Empty)
    {
        response.Exito = false;
        response.Mensaje = "El ID de la clasificacion no puede ser vacío.";
        return response;
    }

    try
    {
        using var dbContext = new BDBiblioteca();

        var clasificacion = await dbContext.Clasificaciones.FirstOrDefaultAsync(a => a.idClasificacion == idClasificacion);

        if (clasificacion == null)
        {
            response.Exito = false;
            response.Mensaje = "El autor no existe.";
            return response;
        }

        // Eliminar la Clasificacion
        dbContext.Clasificaciones.Remove(clasificacion);
        await dbContext.SaveChangesAsync();

        response.Exito = true;
        response.Mensaje = "La clasificacion ha sido eliminado exitosamente.";
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

    public object ConsultarClasificacion(string? busqueda)
    {
        List<Clasificaciones> ListaClasificacion = new List<Clasificaciones>();
        using (var conexion = new BDBiblioteca())
        {
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                var consulta = (from c in conexion.Clasificaciones select c).ToList();
                return consulta;
            }
            else
            {
                var consulta = (from c in conexion.Clasificaciones where c.Tipo.Contains(busqueda) select c).ToList();
                return consulta;
            }
        }
    }

    public async Task<object> GuardarClasificacion(Clasificaciones clasificaciones)
    {
        var Response = new SimpleResponse();
        var conexionBD = new BDBiblioteca();
        string connetionString = conexionBD.Database.GetDbConnection().ConnectionString;
        using (var conexion = new SqlConnection(connetionString))
        {
            Guid llave = Guid.NewGuid();
            clasificaciones.idClasificacion = llave;
            using var command = new SqlCommand();
            command.Connection = conexion;
            command.CommandText = "dbo.AdministrarClasificaciones";
            command.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros
            command.Parameters.Add(new SqlParameter("@Op", SqlDbType.VarChar) { Value = "Insertar" });
            command.Parameters.Add(new SqlParameter("@idClasificacion", SqlDbType.UniqueIdentifier) { Value = clasificaciones.idClasificacion });
            command.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar) { Value = clasificaciones.Descripcion });
            command.Parameters.Add(new SqlParameter("@Tipo", SqlDbType.VarChar) { Value = clasificaciones.Tipo });

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