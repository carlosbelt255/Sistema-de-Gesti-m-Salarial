using Microsoft.EntityFrameworkCore; // Importa Entity Framework Core
using SistemaDeGestionSalarial.Data; // Importa el espacio de nombres del contexto de base de datos

var builder = WebApplication.CreateBuilder(args);

// Agregar el servicio del contexto de la base de datos, obteniendo la cadena de conexión de appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(connectionString));

// Agregar controladores (aquí es donde hacemos el cambio importante)
builder.Services.AddControllers();

// Agregar Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // Configurar HSTS para producción
}


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Mapear Razor Pages
app.MapRazorPages();

// Mapear los controladores de la API (esta línea es clave para que las solicitudes a las APIs funcionen)
app.MapControllers();

app.Run();
