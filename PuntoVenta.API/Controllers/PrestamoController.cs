using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/Prestamos")]

public class PrestamoController:ControllerBase{
    readonly IPrestamo _prestamo;
    //Utilizar la inyección de dependencias
    public PrestamoController(IPrestamo prestamo)
    {
       this._prestamo = prestamo;
    }

    [HttpGet("Revisar Estado")]
    public IActionResult DetallePrestamo(string? consulta){
        var resultado=_prestamo.DetallePrestamo(consulta);
        return Ok(resultado);
            }
    [HttpGet("Revisar Libro")]
    public IActionResult DetalleLibro(string? consulta){
        var resultado=_prestamo.DetalleLibro(consulta);
        return Ok(resultado);
            }
    [HttpPost("Hacer Prestamo")]
    public async Task<IActionResult> GuardarPrestamo(PuntoVenta.Models.Response.Prestamo prestamos){
        var resultado= await _prestamo.GuardarPrestamo(prestamos);
        return Ok(resultado);
    }
    [HttpPost("Actualizar Prestamo")]
    public async Task<IActionResult> ActualizarPrestamo(PuntoVenta.Models.Response.Prestamo prestamos){
        var resultado= await _prestamo.ActualizarPrestamo(prestamos);
        return Ok(resultado);
    }
    [HttpDelete("Borrar Prestamo")]
    public async Task<IActionResult> BorrarPrestamo(Guid idPrestamo){
        var resultado= await _prestamo.BorrarPrestamo(idPrestamo);
        return Ok(resultado);
    }

}