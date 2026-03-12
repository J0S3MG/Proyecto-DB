using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ejemplo_EF_Avanzado1.Data.Entities;

namespace Ejemplo_EF_Avanzado1.Data.Configurations;

public class AlumnoConfiguration : IEntityTypeConfiguration<Alumno>
{
    public void Configure(EntityTypeBuilder<Alumno> builder)
    {
        builder.Property(a => a.Id).ValueGeneratedOnAdd().UseIdentityAlwaysColumn();
        builder.HasIndex(a => a.LU).IsUnique().HasFilter("is_deleted = false");
    }
}