using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ejemplo_EF_Avanzado1.Data.Entities;

namespace Ejemplo_EF_Avanzado1.Data.Seeds;

public class TareaSeed : IEntityTypeConfiguration<Tarea>
{
    public void Configure(EntityTypeBuilder<Tarea> builder)
    {
        builder.HasData(
            new Tarea { Id = 1, Titulo = "TP1 - Algoritmos",    Descripcion = "Implementar ordenamiento burbuja", FechaEntrega = new DateOnly(2025, 4, 10), Entregada = true,  AlumnoId = 1 },
            new Tarea { Id = 2, Titulo = "TP2 - Base de Datos", Descripcion = "Diseñar modelo relacional",        FechaEntrega = new DateOnly(2025, 4, 20), Entregada = false, AlumnoId = 1 },
            new Tarea { Id = 3, Titulo = "TP1 - Algoritmos",    Descripcion = "Implementar ordenamiento burbuja", FechaEntrega = new DateOnly(2025, 4, 10), Entregada = true,  AlumnoId = 2 },
            new Tarea { Id = 4, Titulo = "TP1 - Redes",         Descripcion = "Configurar una red local",         FechaEntrega = new DateOnly(2025, 5, 1),  Entregada = false, AlumnoId = 3 }
        );
    }
}