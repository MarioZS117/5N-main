using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/Copias")]

public class CopiasController : ControllerBase
{
    readonly ICopias _copias;
    public CopiasController(ICopias copias)
    {
        this._copias = copias;
    }

    [HttpGet("Consultar Copias")]
    public IActionResult ConsultarCopia(string? Titulo)
    {
        var resultado = _copias.ConsultarCopias(Titulo);
        return Ok(resultado);
    }
    [HttpPost("Guardar Copias")]
    public async Task<IActionResult> GuardarPrestamo(PuntoVenta.Models.Response.Copias copias)
    {
        var resultado = await _copias.GuadarCopia(copias);
        return Ok(resultado);
    }
    [HttpPost("Actualizar Copias")]
    public async Task<IActionResult> ActualizarCopia(PuntoVenta.Models.Response.Copias copias)
    {
        var resultado = await _copias.ActualizarCopia(copias);
        return Ok(resultado);
    }
    [HttpDelete("Borrar Copias")]
    public async Task<IActionResult> BorrarCopia(Guid idCopia)
    {
        var resultado = await _copias.BorrarCopia(idCopia);
        return Ok(resultado);
    }
}