using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Models.Response;

public class GeneroService : IGenero
{
    public async Task<object> ActualizarGenero(Generos generos)
    {
        var Response = new SimpleResponse();
        var conexionBD = new BDBiblioteca();
        string connetionString = conexionBD.Database.GetDbConnection().ConnectionString;
        using (var conexion = new SqlConnection(connetionString))
        {
            using var command = new SqlCommand();
            command.Connection = conexion;
            command.CommandText = "dbo.AdministrarGeneros";
            command.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros
            command.Parameters.Add(new SqlParameter("@Op", SqlDbType.VarChar) { Value = "Guardar" });
            command.Parameters.Add(new SqlParameter("@idGenero", SqlDbType.UniqueIdentifier) { Value = generos.idGenero });
            command.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar) { Value = generos.Descripcion });
            command.Parameters.Add(new SqlParameter("@Genero", SqlDbType.VarChar) { Value = generos.Genero });

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

    public async Task<object> BorrarGenero(Guid idGenero)
    {
        var response = new SimpleResponse();

        // Validar entrada
        if (idGenero == Guid.Empty)
        {
            response.Exito = false;
            response.Mensaje = "El ID del genero no puede ser vacío.";
            return response;
        }

        try
        {
            using var dbContext = new BDBiblioteca();

            var generos = await dbContext.Generos.FirstOrDefaultAsync(a => a.idGenero == idGenero);

            if (generos == null)
            {
                response.Exito = false;
                response.Mensaje = "El genero no existe.";
                return response;
            }

            // Eliminar la Clasificacion
            dbContext.Generos.Remove(generos);
            await dbContext.SaveChangesAsync();

            response.Exito = true;
            response.Mensaje = "EL genero ha sido eliminado exitosamente.";
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

    public object ConsultarGenero(string? genero)
    {
        List<Generos> ListaGeneros = new List<Generos>();
        using (var conexion = new BDBiblioteca())
        {
            if (string.IsNullOrWhiteSpace(genero))
            {
                var consulta = (from c in conexion.Generos select c).ToList();
                return consulta;
            }
            else
            {
                var consulta = (from c in conexion.Generos 
                where c.Genero.Contains(genero) select c).ToList();
                return consulta;
            }
        }
    }


    public async Task<object> GuardarGenero(Generos generos)
    {
        var Response = new SimpleResponse();
        var conexionBD = new BDBiblioteca();
        string connetionString = conexionBD.Database.GetDbConnection().ConnectionString;
        using (var conexion = new SqlConnection(connetionString))
        {
            using var command = new SqlCommand();
            command.Connection = conexion;
            command.CommandText = "dbo.AdministrarGeneros";
            command.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros
            command.Parameters.Add(new SqlParameter("@Op", SqlDbType.VarChar) { Value = "Guardar" });
            command.Parameters.Add(new SqlParameter("@idGenero", SqlDbType.UniqueIdentifier) { Value = generos.idGenero });
            command.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar) { Value = generos.Descripcion });
            command.Parameters.Add(new SqlParameter("@Genero", SqlDbType.VarChar) { Value = generos.Genero });

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