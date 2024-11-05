using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args); // CREA EL SERVIDOR WEB

DotNetEnv.Env.Load(); // Cargar variables de entorno desde .env

// Obtener la cadena de conexión desde el archivo .env
var connectionString = Environment.GetEnvironmentVariable("Cadena_de_conexion");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("La variable de entorno 'Cadena_de_conexion' no está configurada.");
}

// AGREGA LOS SERVICIOS
builder.Services.AddEndpointsApiExplorer(); // El endpoint para la documentación
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Documentación de la API",
        Version = "v1",
        Description = "Descripción de tu API",
    });
}); // Levanta Swagger para probar la API

builder.Services.AddControllers(); // Le dice a la aplicación de hacer controladores

// Agregamos el contexto para la base de datos
builder.Services.AddDbContext<TomiContext>(options =>
    options.UseSqlServer(connectionString));

// Agregamos los repositorios
builder.Services.AddScoped<UsuarioRepository>();


// Configuración de CORS para permitir las solicitudes desde el frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:8080")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// CREA LA APLICACIÓN
var app = builder.Build();

// CONFIGURACION DE LA APLICACION
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Documentación de la API v1");
        c.RoutePrefix = string.Empty; // Swagger en la raíz de la URL
    });
}

// Convierte a HTTPS
app.UseHttpsRedirection();

// Habilitar CORS
app.UseCors("FrontendPolicy");

// Habilitar autenticación y autorización
app.UseAuthentication(); // Esto asegura que el middleware de autenticación esté activo
app.UseAuthorization();  // Esto asegura que el middleware de autorización esté activo

// Mapea los controladores
app.MapControllers();

// Ejecutamos el servidor
app.Run();
