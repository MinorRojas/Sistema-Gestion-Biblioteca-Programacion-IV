using Biblioteca.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Biblioteca.Web.Data.Configurations;
public class LibroConfiguration : IEntityTypeConfiguration<Libro>
{
    public void Configure(EntityTypeBuilder<Libro> builder)
    {
        builder.ToTable("Libros"); builder.HasKey(x=>x.Id); builder.Property(x=>x.Titulo).IsRequired().HasMaxLength(150);
        builder.Property(x=>x.Isbn).HasMaxLength(20);
        builder.HasIndex(x=>x.Isbn).IsUnique().HasFilter("[Isbn] IS NOT NULL");
        builder.HasOne(x=>x.Autor).WithMany(x=>x.Libros).HasForeignKey(x=>x.AutorId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x=>x.Categoria).WithMany(x=>x.Libros).HasForeignKey(x=>x.CategoriaId).OnDelete(DeleteBehavior.Restrict);
    }
}
