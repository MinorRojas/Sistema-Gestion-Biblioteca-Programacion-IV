using Biblioteca.Web.Models.ViewModels;
namespace Biblioteca.Web.Services;
public interface ICatalogoService
{
    Task<CatalogoViewModel> ObtenerCatalogoAsync(string? busqueda, int? categoriaId);
    Task<LibroDetalleViewModel?> ObtenerDetalleAsync(int id);
}
