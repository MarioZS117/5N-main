using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/Empleados")]

public class EmpleadosController:ControllerBase{
    readonly IEmpleado _empleado;
    public EmpleadosController(IEmpleado empleado){
        this._empleado = empleado;
    }
    [HttpPost("GuadarEmpleados")]
    public async Task<IActionResult> GuardarEmpleado(PuntoVenta.Models.Response.Empleados empleados){
        var _consulta = await _empleado.GuadarEmpleado(empleados);
        return Ok(_consulta);
    }
    [HttpPost("ActualizarEmpleados")]
    public async Task<IActionResult> ActualizarEmpleado(PuntoVenta.Models.Response.Empleados empleados){
        var _consulta = await _empleado.ActualizarEmpleado(empleados);
        return Ok(_consulta);
    }
    [HttpGet("ConsultarEmpleado")]
    public IActionResult ConsultarAutor(string? consulta){
        var _consulta =  _empleado.ConsultarEmpleado(consulta);
        return Ok(_consulta);
    }
    [HttpDelete("EliminarAutores")]
    public async Task<IActionResult> BorrarEmpleado(Guid idEmpleado){
        var _consulta = await _empleado.BorrarEmpleado(idEmpleado);
        return Ok(_consulta);
    }
}