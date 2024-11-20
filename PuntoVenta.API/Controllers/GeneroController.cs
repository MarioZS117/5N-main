using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/Generos")]

public class GeneroController:ControllerBase{
    readonly IGenero _generos;
    public GeneroController(IGenero genero){
        this._generos = genero;
    }
    [HttpPost("GuardarGenero")]
    public IActionResult GuardarGenero(PuntoVenta.Models.Response.Generos generos){
        var _consulta = _generos.GuardarGenero(generos);
        return Ok(_consulta);
    }
}