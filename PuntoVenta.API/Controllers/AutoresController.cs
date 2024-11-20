using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/Autores")]

public class AutoresController:ControllerBase{
    readonly IAutor  _autores;

    public AutoresController(IAutor autor){
        this._autores = autor;
    }
    [HttpPost("GuardarAutores")]
    public async Task<IActionResult> GuardarAutor(PuntoVenta.Models.Response.Autores autores){
        var _consulta = await _autores.GuardarAutor(autores);
        return Ok(_consulta);
    }
    [HttpPost("ActualizarAutores")]
    public async Task<IActionResult> ActualizarAutor(PuntoVenta.Models.Response.Autores autores){
        var _consulta = await _autores.ActualizarAutor(autores);
        return Ok(_consulta);
    }
    [HttpGet("ConsultarAutores")]
    public IActionResult ConsultarAutor(string? consulta){
        var _consulta =  _autores.ConsultarAutor(consulta);
        return Ok(_consulta);
    }
    [HttpDelete("EliminarAutores")]
    public async Task<IActionResult> EliminarAutor(Guid idAutor){
        var _consulta = await _autores.EliminarAutor(idAutor);
        return Ok(_consulta);
    }
}