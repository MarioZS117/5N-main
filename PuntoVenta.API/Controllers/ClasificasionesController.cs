using Microsoft.AspNetCore.Mvc;
using PuntoVenta.Models.Response;

[ApiController]
[Route("api/Clasificaciones")]
public class ClasificacionesController:ControllerBase{
    readonly IClasificaciones _clasificaciones;
    public ClasificacionesController(IClasificaciones clasificaciones){
        this._clasificaciones = clasificaciones;
    }
    [HttpPost("GuadarClasificacion")]
    public async Task<IActionResult> GuardarClasificacion(PuntoVenta.Models.Response.Clasificaciones clasificaciones){
        var consulta = await _clasificaciones.GuardarClasificacion(clasificaciones);
        return Ok(consulta);
    }
    [HttpPost("ActualizarClasificacion")]
    public async Task<IActionResult> ActualizarClasificacion(PuntoVenta.Models.Response.Clasificaciones clasificaciones){
        var consulta = await _clasificaciones.ActualizarClasificacion(clasificaciones);
        return Ok(consulta);
    }
     [HttpDelete("BorrarClasificacion")]
    public async Task<IActionResult> BorrarClasificacion(Guid idClasificacion){
        var consulta = await _clasificaciones.BorrarClasificacion(idClasificacion);
        return Ok(consulta);
    }
    [HttpGet("ConsultarClasificacion")]
    public IActionResult ConsultarClasificacion(string? busqueda){
        var consulta =  _clasificaciones.ConsultarClasificacion(busqueda);
        return Ok(consulta);
    }
}