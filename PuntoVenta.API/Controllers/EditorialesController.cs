using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/Editoriales")]

public class EditorialController : ControllerBase
{
    readonly IEditorial _editorial;
    public EditorialController(IEditorial editorial)
    {
        this._editorial = editorial;
    }

    [HttpGet("Consultar Editoriales")]
    public IActionResult ConsultarEditorial(string? Nombre)
    {
        var resultado = _editorial.ConsultarEditorial(Nombre);
        return Ok(resultado);
    }
    [HttpPost("Guardar Editoriales")]
    public async Task<IActionResult> GuardarEditorial(PuntoVenta.Models.Response.Editorial editorial)
    {
        var resultado = await _editorial.GuardarEditorial(editorial);
        return Ok(resultado);
    }
    [HttpPost("Actualizar Editoriales")]
    public async Task<IActionResult> ActualizarEditoriales(PuntoVenta.Models.Response.Editorial editorial)
    {
        var resultado = await _editorial.ActualizarEditorial(editorial);
        return Ok(resultado);
    }
    [HttpDelete("Borrar Editorial")]
    public async Task<IActionResult> BorrarCopia(Guid idEditorial)
    {
        var resultado = await _editorial.BorrarEditorial(idEditorial);
        return Ok(resultado);
    }
}