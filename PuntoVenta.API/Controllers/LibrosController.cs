using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/Libros")]

public class LibrosController:ControllerBase{
    readonly ILibro _libro;
    public LibrosController(ILibro libro){
        _libro = libro;
    }
    [HttpPost("GuardarLibros")]
    public async Task<IActionResult> GuadarLibros(PuntoVenta.Models.Response.Libros libros){
        var consulta = await _libro.GuardarLibro(libros);
        return Ok(consulta);
    }
    [HttpPost("ActualizarLibros")]
    public async Task<IActionResult> ActualizarLibro(PuntoVenta.Models.Response.Libros libros){
        var consulta = await _libro.ActualizarLibro(libros);
        return Ok(consulta);
    }
    [HttpDelete("BorrarLibros")]
    public async Task<IActionResult> BorrarLibro(Guid idLibro){
        var consulta = await _libro.BorrarLibro(idLibro);
        return Ok(consulta);
    }
    [HttpGet("ConsultarLibros")]
    public IActionResult ConsultarLibro(string? busqueda){
        var consulta =  _libro.ConsultarLibro(busqueda);
        return Ok(consulta);
    }
}