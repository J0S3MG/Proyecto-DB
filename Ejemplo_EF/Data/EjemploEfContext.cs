using Ejemplo_EF.Data.Configurations;
using Ejemplo_EF.Data.Entities;
using Ejemplo_EF.Data.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Ejemplo_EF.Data;

public class EjemploEfContext : DbContext
{
    public virtual DbSet<Alumno> Alumno {get; set;} // Tabla alumno.
    public virtual DbSet<Tarea> Tarea {get; set;} // Tabla tarea.

    // Este constructor lo hacemos para conectar a la db desde el Program.
    public EjemploEfContext(DbContextOptions<EjemploEfContext> context): base(context) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Forma Basica: Llama a cada Seed para mappear la entidad a la tabla.
        modelBuilder.ApplyConfiguration(new AlumnoSeed());
        modelBuilder.ApplyConfiguration(new TareaSeed()); 
        modelBuilder.ApplyConfiguration(new AlumnoConfiguration());
        modelBuilder.ApplyConfiguration(new TareaConfiguration());
    }
}
