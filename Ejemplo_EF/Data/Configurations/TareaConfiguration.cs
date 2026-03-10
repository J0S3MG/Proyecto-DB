using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ejemplo_EF.Data.Entities;

namespace Ejemplo_EF.Data.Configurations;

public class TareaConfiguration : IEntityTypeConfiguration<Tarea>
{
    public void Configure(EntityTypeBuilder<Tarea> builder)
    {
        builder.Property(t => t.Id).ValueGeneratedOnAdd().UseIdentityAlwaysColumn();
    }
}