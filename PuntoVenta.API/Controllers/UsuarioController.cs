using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/Usuario")]
public class UsuarioController : ControllerBase{
    readonly IUsuario _usuarioService;
    public UsuarioController(IUsuario usuario){
        this._usuarioService=usuario;
    }
    [HttpGet("ListaUsuarios")]
    public IActionResult ListaUsuarios(string busqueda){
        var consulta=_usuarioService.ListaUsuario(busqueda);
        return Ok(consulta);
    }
    [HttpPost("GuardarUsuarios")]
    public IActionResult GuardarUsuario(PuntoVenta.Models.Response.Usuarios user){
        var consulta = _usuarioService.GuardarUsuario(user);
        return Ok(consulta);
    }
    [HttpPost("ActualizarUsuarios")]
    public IActionResult ActualizarUsuario(PuntoVenta.Models.Response.Usuarios user){
        var consulta = _usuarioService.ActualizarUsuario(user);
        return Ok(consulta);
    }
    [HttpPost("EliminarUsuarios")]
    public IActionResult EliminarUsuario(Guid Iduser){
        var consulta = _usuarioService.EliminarUsuario(Iduser);
        return Ok(consulta);
    }
}