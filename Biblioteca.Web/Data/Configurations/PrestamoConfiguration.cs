
using Biblioteca.Web.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Biblioteca.Web.Data.Configurations;

public class PrestamoConfiguration : IEntityTypeConfiguration<Prestamo>

{
    public void Configure(EntityTypeBuilder<Prestamo> builder)

    {
        builder.ToTable("Prestamos");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Libro).WithMany().HasForeignKey(x => x.LibroId).OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.UsuarioId).HasMaxLength(450).IsRequired();
        builder.HasOne<IdentityUser>()
            .WithMany()
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}