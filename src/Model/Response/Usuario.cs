using System.ComponentModel.DataAnnotations.Schema;

[Table("Usuarios")]

public class Usuarios
{
    public int Id { get; set; }
    public string? Usuario { get; set; }
    public string? Contraseña { get; set; }

}