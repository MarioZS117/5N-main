using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Models.Response;

public class PrestamoService : IPrestamo
{
    public async Task<object> ActualizarPrestamo(Prestamo Prestamo)
    {
        var Response = new SimpleResponse();
        var conexionBD = new BDBiblioteca();
        string connetionString = conexionBD.Database.GetDbConnection().ConnectionString;
        using (var conexion = new SqlConnection(connetionString))
        {
            using var command = new SqlCommand();
            command.Connection = conexion;
            command.CommandText = "dbo.AdministrarPrestamo";
            command.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros
            command.Parameters.Add(new SqlParameter("@Op", SqlDbType.VarChar) { Value = "Actualizar" });
            command.Parameters.Add(new SqlParameter("@idLibro", SqlDbType.UniqueIdentifier) { Value = Prestamo.idLibro });
            command.Parameters.Add(new SqlParameter("@idPrestamo", SqlDbType.UniqueIdentifier) { Value = Prestamo.idPrestamo });
            command.Parameters.Add(new SqlParameter("@idEmpleado", SqlDbType.UniqueIdentifier) { Value = Prestamo.idEmpleado });
            command.Parameters.Add(new SqlParameter("@Fecha", SqlDbType.Date) { Value = Prestamo.Fecha });
            command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.UniqueIdentifier) { Value = Prestamo.idUsuario });

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

    public async Task<object> BorrarPrestamo(Guid idPrestamo)
    {
        var response = new SimpleResponse();

        // Validar entrada
        if (idPrestamo == Guid.Empty)
        {
            response.Exito = false;
            response.Mensaje = "El ID de el prestamo no puede ser vacío.";
            return response;
        }

        try
        {
            using var dbContext = new BDBiblioteca();

            var prestamo = await dbContext.Prestamo.FirstOrDefaultAsync(a => a.idPrestamo == idPrestamo);

            if (prestamo == null)
            {
                response.Exito = false;
                response.Mensaje = "El prestamo no existe.";
                return response;
            }

            // Eliminar la Clasificacion
            dbContext.Prestamo.Remove(prestamo);
            await dbContext.SaveChangesAsync();

            response.Exito = true;
            response.Mensaje = "El prestamo ha sido eliminado exitosamente.";
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


    public object DetalleLibro(string? busqueda)
    {
        List<Libros> ListaLibros = new List<Libros>();
        using (var conexion = new BDBiblioteca())
        {
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                var consulta = (from l in conexion.Libros join c in conexion.Copias
                on l.idLibro equals c.idLibro select l).ToList();
                return consulta;
            }
            else
            {
                // Realiza la consulta uniendo "Libros" con "Copias" y filtrando por el título
                var consulta = (from l in conexion.Libros
                                join c in conexion.Copias on l.idLibro equals c.idLibro
                                where l.Titulo.Contains(busqueda)
                                select new
                                {
                                    l.Titulo,
                                    c.Estado,
                                    c.Cantidad
                                }
                               ).ToList();
                foreach (var item in consulta)
                {
                    Console.WriteLine(item.Titulo);
                }
                return consulta;
            }
        }

    }


    public object DetallePrestamo(string? Consulta)
    {
        using (var conexion = new BDBiblioteca())
        {
            // Si no se proporciona una consulta, devolver todos los préstamos
            if (string.IsNullOrWhiteSpace(Consulta))
            {
                var consulta = (from p in conexion.Prestamo
                                join c in conexion.Copias on p.idLibro equals c.idLibro
                                select new
                                {
                                    p.idEmpleado,
                                    p.Fecha,
                                    c.Estado,
                                    c.Cantidad
                                }).ToList();
                return consulta;
            }
            else
            {
                // Filtrar por el título proporcionado
                var consulta = (from p in conexion.Prestamo
                                join c in conexion.Copias on p.idLibro equals c.idLibro
                                join l in conexion.Libros on p.idLibro equals l.idLibro
                                where l.Titulo.Contains(Consulta)
                                select new
                                {
                                    l.Titulo,
                                    l.Edicion,
                                    p.idEmpleado,
                                    p.Fecha,
                                    c.Estado,
                                    c.Cantidad
                                }).ToList();
                return consulta;
            }
        }
    }

    public async Task<object> GuardarPrestamo(Prestamo Prestamo)
    {
        var Response = new SimpleResponse();
        var conexionBD = new BDBiblioteca();
        string connetionString = conexionBD.Database.GetDbConnection().ConnectionString;
        using (var conexion = new SqlConnection(connetionString))
        {
            using var command = new SqlCommand();
            command.Connection = conexion;
            command.CommandText = "dbo.AdministrarPrestamo";
            command.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros
            command.Parameters.Add(new SqlParameter("@Op", SqlDbType.VarChar) { Value = "Insertar" });
            command.Parameters.Add(new SqlParameter("@idLibro", SqlDbType.UniqueIdentifier) { Value = Prestamo.idLibro });
            command.Parameters.Add(new SqlParameter("@idPrestamo", SqlDbType.UniqueIdentifier) { Value = Prestamo.idPrestamo });
            command.Parameters.Add(new SqlParameter("@idEmpleado", SqlDbType.UniqueIdentifier) { Value = Prestamo.idEmpleado });
            command.Parameters.Add(new SqlParameter("@Fecha", SqlDbType.Date) { Value = Prestamo.Fecha });
            command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.UniqueIdentifier) { Value = Prestamo.idUsuario });

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
