using DotNetEnv;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Ejemplo_EF_Avanzado1.Data;
using Ejemplo_EF_Avanzado1.Repositories;
using Ejemplo_EF_Avanzado1.Services;
using Ejemplo_EF_Avanzado1.Services.Interfaces;
using Ejemplo_EF_Avanzado1.Services.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();


Env.Load();

var connectionString = DotNetEnv.Env.GetString("POSTGRESDB") is { Length: > 0 } fromEnv ? fromEnv : builder.Configuration.GetConnectionString("PostgresDB");

if (string.IsNullOrEmpty(connectionString)) throw new Exception("Error Crítico: No se encontró la cadena de conexión ni en .env ni en appsettings.json");
// Registrar DbContext con PostgreSQL.
builder.Services.AddDbContext<EjemploEfContext>(options => options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());

// Registrar Repositorios.
builder.Services.AddScoped<IAlumnoRepository, AlumnoRepository>();
builder.Services.AddScoped<ITareaRepository, TareaRepository>();

// Registrar Servicios.
builder.Services.AddScoped<IAlumnoService, AlumnoService>();
builder.Services.AddScoped<ITareaService, TareaService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();