//--------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



// SE USA SI O SI PARA 
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase

{
    private readonly UsuarioRepository _UsuarioRepository;

    public UsuarioController(UsuarioRepository UsuarioRepository)
    {
        _UsuarioRepository = UsuarioRepository;
    }
    //--------------------------------------------------------------------------------------------------------

    [AllowAnonymous]
    [HttpGet]
    public IActionResult GetUsuarios()
    {
        try
        {
            var Usuarios = _UsuarioRepository.GetAll();
            if (Usuarios == null || !Usuarios.Any())
            {
                return NotFound(new { message = "No se encontraron usuarios" });
            }
            return Ok(Usuarios);
        }
        catch (Exception ex)
        {
            // Manejo de excepciones
            Console.WriteLine($"Error al obtener usuarios: {ex.Message}"); // Log de errores
            return StatusCode(500, new { message = ex.Message });
        }
    }
}




