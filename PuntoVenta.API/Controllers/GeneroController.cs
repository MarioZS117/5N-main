using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/Generos")]

public class GeneroController : ControllerBase
{
    readonly IGenero _generos;
    public GeneroController(IGenero genero)
    {
        this._generos = genero;
    }
    [HttpPost("Guardar Genero")]
    public async Task<IActionResult> GuardarGenero(PuntoVenta.Models.Response.Generos generos)
    {
        var _consulta = await _generos.GuardarGenero(generos);
        return Ok(_consulta);
    }
    [HttpGet("Consultar Genero")]
    public IActionResult ConsultarGenero(string Genero)
    {
        var _consulta = _generos.ConsultarGenero(Genero);
        return Ok(_consulta);
    }
    [HttpPost("Actualizar Genero")]
    public async Task<IActionResult> ActualizarGenero(PuntoVenta.Models.Response.Generos generos)
    {
        var _consulta = await _generos.ActualizarGenero(generos);
        return Ok(_consulta);
    }
    [HttpDelete("Borrar Genero")]
    public async Task<IActionResult> BorrarGenero(Guid idGenero)
    {
        var _consulta = await _generos.BorrarGenero(idGenero);
        return Ok(_consulta);
    }
}