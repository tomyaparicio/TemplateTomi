// Se usa SI O SI PARA CONECTARSE A LA BASE DE DATOS.--------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;

public class TomiContext : DbContext
{
    public TomiContext(DbContextOptions<TomiContext> options) : base(options) { }

    public DbSet<Usuarios> Usuario { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseSqlServer(Environment.GetEnvironmentVariable("Cadena_de_conexion"));
        }
    }
}
//--------------------------------------------------------------------------------------------------------