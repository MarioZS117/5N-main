using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Models.Response;

public class CopiasService : ICopias
{
    public async Task<object> ActualizarCopia(Copias copias)
    {
        var Response = new SimpleResponse();
        var conexionBD = new BDBiblioteca();
        string connetionString = conexionBD.Database.GetDbConnection().ConnectionString;
        using (var conexion = new SqlConnection(connetionString))
        {
            using var command = new SqlCommand();
            command.Connection = conexion;
            command.CommandText = "dbo.AdministrarCopias";
            command.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros
            command.Parameters.Add(new SqlParameter("@Op", SqlDbType.VarChar) { Value = "Actualizar" });
            command.Parameters.Add(new SqlParameter("@idCopia", SqlDbType.UniqueIdentifier) { Value = copias.idCopia });
            command.Parameters.Add(new SqlParameter("@idLibro", SqlDbType.UniqueIdentifier) { Value = copias.idLibro });
            command.Parameters.Add(new SqlParameter("@Cantidad", SqlDbType.Int) { Value = copias.Cantidad });
            command.Parameters.Add(new SqlParameter("@Estado", SqlDbType.Binary) { Value = copias.Estado });

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

    public async Task<object> BorrarCopia(Guid idCopia)
    {
        var response = new SimpleResponse();

        // Validar entrada
        if (idCopia == Guid.Empty)
        {
            response.Exito = false;
            response.Mensaje = "El ID de la copia no puede ser vacío.";
            return response;
        }

        try
        {
            using var dbContext = new BDBiblioteca();

            var copia = await dbContext.Copias.FirstOrDefaultAsync(a => a.idCopia == idCopia);

            if (copia == null)
            {
                response.Exito = false;
                response.Mensaje = "La copia no existe.";
                return response;
            }

            // Eliminar la Clasificacion
            dbContext.Copias.Remove(copia);
            await dbContext.SaveChangesAsync();

            response.Exito = true;
            response.Mensaje = "La copia ha sido eliminado exitosamente.";
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

    public object ConsultarCopias(string? Estado)
    {
        List<Copias> ListaCopias = new List<Copias>();
        using (var conexion = new BDBiblioteca())
        {
            if (string.IsNullOrWhiteSpace(Estado))
            {
                var consulta = (from c in conexion.Copias select c).ToList();
                return consulta;
            }
            else
            {
                var consulta = (from c in conexion.Copias join l in conexion.Libros
                on c.idLibro equals l.idLibro where l.Titulo.Contains(Estado) select c).ToList();
                return consulta;
            }
        }
    }

    public async Task<object> GuadarCopia(Copias copias)
    {
        var Response = new SimpleResponse();
        var conexionBD = new BDBiblioteca();
        string connetionString = conexionBD.Database.GetDbConnection().ConnectionString;
        using (var conexion = new SqlConnection(connetionString))
        {
            Guid llave = Guid.NewGuid();
            copias.idCopia = llave;
            using var command = new SqlCommand();
            command.Connection = conexion;
            command.CommandText = "dbo.AdministrarCopias";
            command.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros
            command.Parameters.Add(new SqlParameter("@Op", SqlDbType.VarChar) { Value = "Guardar" });
            command.Parameters.Add(new SqlParameter("@idCopia", SqlDbType.UniqueIdentifier) { Value = copias.idCopia });
            command.Parameters.Add(new SqlParameter("@idLibro", SqlDbType.UniqueIdentifier) { Value = copias.idLibro });
            command.Parameters.Add(new SqlParameter("@Cantidad", SqlDbType.Int) { Value = copias.Cantidad });
            command.Parameters.Add(new SqlParameter("@Estado", SqlDbType.Binary) { Value = copias.Estado });

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