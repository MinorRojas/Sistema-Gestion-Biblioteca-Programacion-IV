using Biblioteca.Web.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace Biblioteca.Web.Data;
public static class DbInitializer
{
    public const string RolAdmin = "Admin";
    public static async Task InicializarAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        if (!await roleManager.RoleExistsAsync(RolAdmin)) await roleManager.CreateAsync(new IdentityRole(RolAdmin));
        const string correo = "admin@biblioteca.com"; const string clave = "Admin123";
        var admin = await userManager.FindByEmailAsync(correo);
        if (admin is null) { admin = new IdentityUser { UserName=correo, Email=correo, EmailConfirmed=true }; await userManager.CreateAsync(admin, clave); }
        if (!await userManager.IsInRoleAsync(admin, RolAdmin)) await userManager.AddToRoleAsync(admin, RolAdmin);
        if (await context.Libros.AnyAsync()) return;
        var autores = new[] { new Autor{Nombre="Gabriel García Márquez",Biografia="Escritor colombiano."}, new Autor{Nombre="Isabel Allende",Biografia="Escritora chilena."}, new Autor{Nombre="George Orwell",Biografia="Escritor y periodista británico."} };
        var categorias = new[] { new Categoria{Nombre="Novela",Descripcion="Obras narrativas."}, new Categoria{Nombre="Ciencia ficción",Descripcion="Historias especulativas."}, new Categoria{Nombre="Literatura latinoamericana",Descripcion="Autores y obras de América Latina."} };
        context.AddRange(autores); context.AddRange(categorias); await context.SaveChangesAsync();
        context.Libros.AddRange(
            new Libro{Titulo="Cien años de soledad",Isbn="9780307474728",Sinopsis="Historia de la familia Buendía.",AnioPublicacion=1967,CantidadEjemplares=5,EjemplaresDisponibles=3,AutorId=autores[0].Id,CategoriaId=categorias[2].Id},
            new Libro{Titulo="La casa de los espíritus",Isbn="9780525433477",Sinopsis="Saga familiar con realismo mágico.",AnioPublicacion=1982,CantidadEjemplares=4,EjemplaresDisponibles=1,AutorId=autores[1].Id,CategoriaId=categorias[0].Id},
            new Libro{Titulo="1984",Isbn="9780451524935",Sinopsis="Novela distópica sobre vigilancia y poder.",AnioPublicacion=1949,CantidadEjemplares=3,EjemplaresDisponibles=0,AutorId=autores[2].Id,CategoriaId=categorias[1].Id});
        await context.SaveChangesAsync();
    }
}

