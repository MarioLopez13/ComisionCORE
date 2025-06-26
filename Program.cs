using Microsoft.EntityFrameworkCore;
using ComisionApi.Data;
using ComisionApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 27))
    ));
builder.Services.AddScoped<ComisionService>(); // Registra el servicio

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ UseCors debe ir ANTES de UseRouting (esto está implícito en .NET 6+)
app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); // Importante si usas controladores API

// Sembrar datos iniciales
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        SeedData.Initialize(context); // Llama a SeedData.Initialize
    }
    catch (Exception ex)
    {
        // Opcional: loggear errores
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error al sembrar datos iniciales.");
    }
}

app.Run();