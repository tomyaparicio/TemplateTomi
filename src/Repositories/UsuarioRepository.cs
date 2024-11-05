using Microsoft.EntityFrameworkCore;


public class UsuarioRepository
{
    private readonly TomiContext _context;

    public UsuarioRepository(TomiContext context)
    {
        _context = context;
    }

    public List<Usuarios> GetAll()
    {
        var usuarios = _context.Usuario.ToList();
        Console.WriteLine($"Número de usuarios recuperados: {usuarios.Count}"); // Verifica el número de usuarios
        return usuarios;
    }

}