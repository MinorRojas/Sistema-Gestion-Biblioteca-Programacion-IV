using Biblioteca.Web.Data;
using Biblioteca.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Web.Services;

public class LibroService(ApplicationDbContext context) : ILibroService
{
    public Task<List<Libro>> ListarAsync(string? busqueda = null)
    {
        var query = context.Libros
            .AsNoTracking()
            .Include(x => x.Autor)
            .Include(x => x.Categoria)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(busqueda))
        {
            var texto = busqueda.Trim();
            query = query.Where(x =>
                x.Titulo.Contains(texto) ||
                (x.Isbn != null && x.Isbn.Contains(texto)) ||
                x.Autor.Nombre.Contains(texto));
        }

        return query.OrderBy(x => x.Titulo).ToListAsync();
    }

    public Task<Libro?> ObtenerAsync(int id) => context.Libros
        .AsNoTracking()
        .Include(x => x.Autor)
        .Include(x => x.Categoria)
        .FirstOrDefaultAsync(x => x.Id == id);

    public async Task CrearAsync(Libro libro)
    {
        context.Libros.Add(libro);
        await context.SaveChangesAsync();
    }

    public async Task<bool> ActualizarAsync(Libro libro)
    {
        var actual = await context.Libros.FindAsync(libro.Id);
        if (actual is null) return false;

        actual.Titulo = libro.Titulo;
        actual.Isbn = libro.Isbn;
        actual.Sinopsis = libro.Sinopsis;
        actual.AnioPublicacion = libro.AnioPublicacion;
        actual.CantidadEjemplares = libro.CantidadEjemplares;
        actual.EjemplaresDisponibles = libro.EjemplaresDisponibles;
        actual.AutorId = libro.AutorId;
        actual.CategoriaId = libro.CategoriaId;

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<(bool eliminado, string? error)> EliminarAsync(int id)
    {
        var libro = await context.Libros.FirstOrDefaultAsync(x => x.Id == id);
        if (libro is null) return (false, "Libro no encontrado.");

        var tienePrestamos = await context.Prestamos.AnyAsync(p =>
            p.LibroId == id && p.FechaDevolucionReal == null);

        if (tienePrestamos)
            return (false, "No se puede eliminar un libro que tiene un préstamo activo.");

        context.Libros.Remove(libro);
        await context.SaveChangesAsync();
        return (true, null);
    }

    public Task<bool> ExisteIsbnAsync(string? isbn, int? excluirId = null)
    {
        if (string.IsNullOrWhiteSpace(isbn)) return Task.FromResult(false);
        return context.Libros.AnyAsync(x =>
            x.Isbn == isbn && (!excluirId.HasValue || x.Id != excluirId.Value));
    }

    public Task<List<Autor>> ListarAutoresAsync() => context.Autores
        .AsNoTracking().OrderBy(x => x.Nombre).ToListAsync();

    public Task<List<Categoria>> ListarCategoriasAsync() => context.Categorias
        .AsNoTracking().OrderBy(x => x.Nombre).ToListAsync();
}
