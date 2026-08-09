using Biblioteca.Web.Models.Entities;

namespace Biblioteca.Web.Services;

public interface ILibroService
{
    Task<List<Libro>> ListarAsync(string? busqueda = null);
    Task<Libro?> ObtenerAsync(int id);
    Task CrearAsync(Libro libro);
    Task<bool> ActualizarAsync(Libro libro);
    Task<(bool eliminado, string? error)> EliminarAsync(int id);
    Task<bool> ExisteIsbnAsync(string? isbn, int? excluirId = null);
    Task<List<Autor>> ListarAutoresAsync();
    Task<List<Categoria>> ListarCategoriasAsync();
}
