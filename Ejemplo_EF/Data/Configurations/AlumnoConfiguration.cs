using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ejemplo_EF.Data.Entities;

namespace Ejemplo_EF.Data.Configurations;

public class AlumnoConfiguration : IEntityTypeConfiguration<Alumno>
{
    public void Configure(EntityTypeBuilder<Alumno> builder)
    {
        builder.Property(a => a.Id).ValueGeneratedOnAdd().UseIdentityAlwaysColumn();
    }
}