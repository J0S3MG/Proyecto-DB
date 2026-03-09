using Scalar.AspNetCore;
using DotNetEnv;
using Ejemplo_ADO.DAOs;
using Ejemplo_ADO.Services;
using Ejemplo_ADO.Services.interfaces;

var builder = WebApplication.CreateBuilder(args);
// Extraemos la cadena de conexión.
Env.Load();
var connectionString = ((Func<string?>)(() => {
    var fromEnv = Env.GetString("POSTGRESDB");
    return !string.IsNullOrWhiteSpace(fromEnv) 
        ? fromEnv : builder.Configuration.GetConnectionString("PostgresDB");
}))();
if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Error Crítico: No se encontró la cadena de conexión ni en .env ni en appsettings.json");
}

builder.Services.AddOpenApi();
builder.Services.AddSingleton(connectionString!); 
builder.Services.AddScoped<IAlumnoDAO, AlumnoDAO>();
builder.Services.AddScoped<IDBService, DBService>();
builder.Services.AddControllers(); 

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseHttpsRedirection();
app.MapControllers(); 
app.Run();
