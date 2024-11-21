using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PuntoVenta.Models.Response;

public class EmpleadosService : IEmpleado
{
    public async Task<object> ActualizarEmpleado(Empleados empleados)
    {
        var Response = new SimpleResponse();
        var conexionBD = new BDBiblioteca();
        string connetionString = conexionBD.Database.GetDbConnection().ConnectionString;
        using (var conexion = new SqlConnection(connetionString))
        {
            using var command = new SqlCommand();
            command.Connection = conexion;
            command.CommandText = "dbo.AdministrarEmpleados";
            command.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros
            command.Parameters.Add(new SqlParameter("@Op", SqlDbType.VarChar) { Value = "Actualizar" });
            command.Parameters.Add(new SqlParameter("@idEmpleado", SqlDbType.UniqueIdentifier) { Value = empleados.idEmpleado });
            command.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = empleados.Nombre });
            command.Parameters.Add(new SqlParameter("@Cargo", SqlDbType.VarChar) { Value = empleados.Cargo });
            command.Parameters.Add(new SqlParameter("@Usuario", SqlDbType.VarChar) { Value = empleados.Usuario });
            command.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar) { Value = empleados.Password });

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

    public async Task<object> BorrarEmpleado(Guid idEmpleado)
    {
        var response = new SimpleResponse();

        // Validar entrada
        if (idEmpleado == Guid.Empty)
        {
            response.Exito = false;
            response.Mensaje = "El ID de el empleado no puede ser vacío.";
            return response;
        }

        try
        {
            using var dbContext = new BDBiblioteca();

            var empleado = await dbContext.Empleados.FirstOrDefaultAsync(a => a.idEmpleado == idEmpleado);

            if (empleado == null)
            {
                response.Exito = false;
                response.Mensaje = "El empleado no existe.";
                return response;
            }

            // Eliminar la Clasificacion
            dbContext.Empleados.Remove(empleado);
            await dbContext.SaveChangesAsync();

            response.Exito = true;
            response.Mensaje = "El empleado ha sido eliminado exitosamente.";
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

    public object ConsultarEmpleado(string? Nombre)
    {
        List<Empleados> ListaEmpleados = new List<Empleados>();
        using (var conexion = new BDBiblioteca())
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                var consulta = (from c in conexion.Empleados select c).ToList();
                return consulta;
            }
            else
            {
                var consulta = (from c in conexion.Empleados where c.Nombre.Contains(Nombre) select c).ToList();
                return consulta;
            }
        }
    }

    public async Task<object> GuadarEmpleado(Empleados empleados)
    {
        var Response = new SimpleResponse();
        var conexionBD = new BDBiblioteca();
        string connetionString = conexionBD.Database.GetDbConnection().ConnectionString;
        using (var conexion = new SqlConnection(connetionString))
        {
            Guid llave = Guid.NewGuid();
            empleados.idEmpleado = llave;
            using var command = new SqlCommand();
            command.Connection = conexion;
            command.CommandText = "dbo.AdministrarEmpleados";
            command.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros
            command.Parameters.Add(new SqlParameter("@Op", SqlDbType.VarChar) { Value = "Insertar" });
            command.Parameters.Add(new SqlParameter("@idEmpleado", SqlDbType.UniqueIdentifier) { Value = empleados.idEmpleado });
            command.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = empleados.Nombre });
            command.Parameters.Add(new SqlParameter("@Cargo", SqlDbType.VarChar) { Value = empleados.Cargo });
            command.Parameters.Add(new SqlParameter("@Usuario", SqlDbType.VarChar) { Value = empleados.Usuario });
            command.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar) { Value = empleados.Password });

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