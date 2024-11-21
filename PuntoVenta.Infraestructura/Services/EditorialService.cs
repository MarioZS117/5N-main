using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Models.Response;

public class EditorialService : IEditorial
{
    public async Task<object> ActualizarEditorial(Editorial editorial)
    {
        var Response = new SimpleResponse();
        var conexionBD = new BDBiblioteca();
        string connetionString = conexionBD.Database.GetDbConnection().ConnectionString;
        using (var conexion = new SqlConnection(connetionString))
        {
            using var command = new SqlCommand();
            command.Connection = conexion;
            command.CommandText = "dbo.AdministrarEditorial";
            command.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros
            command.Parameters.Add(new SqlParameter("@Op", SqlDbType.VarChar) { Value = "Actualizar" });
            command.Parameters.Add(new SqlParameter("@idEditorial", SqlDbType.UniqueIdentifier) { Value = editorial.idEditorial });
            command.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = editorial.Nombre });

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

    public async Task<object> BorrarEditorial(Guid idEditorial)
    {
        var response = new SimpleResponse();

        // Validar entrada
        if (idEditorial == Guid.Empty)
        {
            response.Exito = false;
            response.Mensaje = "El ID de la editorial no puede ser vacío.";
            return response;
        }

        try
        {
            using var dbContext = new BDBiblioteca();

            var editorial = await dbContext.Editorial.FirstOrDefaultAsync(a => a.idEditorial == idEditorial);

            if (editorial == null)
            {
                response.Exito = false;
                response.Mensaje = "La editorial no existe.";
                return response;
            }

            // Eliminar la Clasificacion
            dbContext.Editorial.Remove(editorial);
            await dbContext.SaveChangesAsync();

            response.Exito = true;
            response.Mensaje = "La editorial ha sido eliminado exitosamente.";
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

    public object ConsultarEditorial(string? Nombre)
    {
        List<Editorial> ListaEditorial = new List<Editorial>();
        using (var conexion = new BDBiblioteca())
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                var consulta = (from c in conexion.Editorial select c).ToList();
                return consulta;
            }
            else
            {
                var consulta = (from c in conexion.Editorial 
                where c.Nombre.Contains(Nombre) select c).ToList();
                return consulta;
            }
        }
    }

    public async Task<object> GuardarEditorial(Editorial editorial)
    {
        var Response = new SimpleResponse();
        var conexionBD = new BDBiblioteca();
        string connetionString = conexionBD.Database.GetDbConnection().ConnectionString;
        using (var conexion = new SqlConnection(connetionString))
        {
            Guid llave = Guid.NewGuid();
            editorial.idEditorial = llave;
            using var command = new SqlCommand();
            command.Connection = conexion;
            command.CommandText = "dbo.AdministrarEditorial";
            command.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros
            command.Parameters.Add(new SqlParameter("@Op", SqlDbType.VarChar) { Value = "Guardar" });
            command.Parameters.Add(new SqlParameter("@idEditorial", SqlDbType.UniqueIdentifier) { Value = editorial.idEditorial });
            command.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = editorial.Nombre });

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