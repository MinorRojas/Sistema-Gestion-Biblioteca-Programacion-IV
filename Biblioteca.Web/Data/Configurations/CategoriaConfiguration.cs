using Biblioteca.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Biblioteca.Web.Data.Configurations;
public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categorias"); builder.HasKey(x=>x.Id); builder.Property(x=>x.Nombre).IsRequired().HasMaxLength(80);
        builder.HasIndex(x=>x.Nombre).IsUnique();
    }
}
