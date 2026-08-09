using Biblioteca.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Biblioteca.Web.Data.Configurations;
public class AutorConfiguration : IEntityTypeConfiguration<Autor>
{
    public void Configure(EntityTypeBuilder<Autor> builder)
    {
        builder.ToTable("Autores"); builder.HasKey(x=>x.Id); builder.Property(x=>x.Nombre).IsRequired().HasMaxLength(100);
        builder.HasIndex(x=>x.Nombre).IsUnique();
    }
}
