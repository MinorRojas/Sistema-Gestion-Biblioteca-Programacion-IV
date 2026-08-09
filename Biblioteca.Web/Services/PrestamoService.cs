
using Biblioteca.Web.Data;
using Biblioteca.Web.Models.Entities;
using Biblioteca.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace Biblioteca.Web.Services;

public class PrestamoService(ApplicationDbContext context, UserManager<IdentityUser> userManager) : IPrestamoService

{
    public async Task<List<PrestamoListItemViewModel>> ListarAsync()

    {
        var prestamos = await context.Set<Prestamo>().AsNoTracking()
            .Include(p => p.Libro)
            .OrderByDescending(p => p.FechaPrestamo)
            .ToListAsync();

        var usuarios = await userManager.Users.ToDictionaryAsync(u => u.Id, u => u.Email ?? u.UserName ?? "");

        return prestamos.Select(p => new PrestamoListItemViewModel

        {
            Id = p.Id,
            LibroTitulo = p.Libro.Titulo,
            UsuarioCorreo = usuarios.GetValueOrDefault(p.UsuarioId, "Desconocido"),
            FechaPrestamo = p.FechaPrestamo,
            FechaDevolucionEsperada = p.FechaDevolucionEsperada,
            FechaDevolucionReal = p.FechaDevolucionReal
        }).ToList();

    }

    public async Task<PrestamoCreateViewModel> ObtenerFormularioCreacionAsync()
    {
        var libros = await context.Libros.AsNoTracking()
            .Where(l => l.EjemplaresDisponibles > 0)
            .OrderBy(l => l.Titulo)
            .Select(l => new SelectListItem($"{l.Titulo} ({l.EjemplaresDisponibles} disp.)", l.Id.ToString()))
            .ToListAsync();

        var usuarios = await userManager.Users
            .OrderBy(u => u.Email)
            .Select(u => new SelectListItem(u.Email, u.Id))
            .ToListAsync();

        return new PrestamoCreateViewModel { Libros = libros, Usuarios = usuarios };

    }

    public async Task<(bool ok, string? error)> RegistrarPrestamoAsync(int libroId, string usuarioId, DateTime fechaDevolucionEsperada)

    {
        if (fechaDevolucionEsperada.Date <= DateTime.Now.Date)
            return (false, "La fecha de devolución esperada debe ser posterior a hoy.");

        var libro = await context.Libros.FindAsync(libroId);
        if (libro is null) return (false, "El libro no existe.");
        if (libro.EjemplaresDisponibles <= 0) return (false, "No hay ejemplares disponibles.");

        libro.EjemplaresDisponibles--;
        context.Set<Prestamo>().Add(new Prestamo
        {
            LibroId = libroId,
            UsuarioId = usuarioId,
            FechaDevolucionEsperada = fechaDevolucionEsperada
        });
        await context.SaveChangesAsync();
        return (true, null);

    }

    public async Task<(bool ok, string? error)> RegistrarDevolucionAsync(int id, DateTime? fechaDevolucion = null)

    {
        var prestamo = await context.Set<Prestamo>().Include(p => p.Libro).FirstOrDefaultAsync(p => p.Id == id);
        if (prestamo is null) return (false, "Prestamo no encontrado.");
        if (prestamo.Devuelto) return (false, "Este prestamo ya fue devuelto.");

        var fecha = fechaDevolucion ?? DateTime.Now;
        if (fecha.Date < prestamo.FechaPrestamo.Date)
            return (false, "La fecha de devolución no puede ser anterior a la fecha del prestamo.");
        if (fecha.Date > DateTime.Now.Date)
            return (false, "La fecha de devolución no puede ser futura.");

        prestamo.FechaDevolucionReal = fecha;
        prestamo.Libro.EjemplaresDisponibles++;
        await context.SaveChangesAsync();
        return (true, null);

    }

}