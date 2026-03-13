using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ejemplo_EF_Avanzado2.Data.Entities;

namespace Ejemplo_EF_Avanzado2.Data.Seeds;

public class AlumnoSeed : IEntityTypeConfiguration<Alumno>
{
    public void Configure(EntityTypeBuilder<Alumno> builder)
    {
        builder.HasData(
            new Alumno { Id = 1, LU = 12001, Nombre = "Lucas Pérez",     Nota = 8.5m },
            new Alumno { Id = 2, LU = 12002, Nombre = "Sofía Ramírez",   Nota = 9.0m },
            new Alumno { Id = 3, LU = 12003, Nombre = "Mateo González",  Nota = 6.5m },
            new Alumno { Id = 4, LU = 12004, Nombre = "Valentina López", Nota = 7.0m }
        );
    }
}