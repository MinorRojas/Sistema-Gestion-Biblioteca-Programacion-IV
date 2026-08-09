
using Biblioteca.Web.Models.ViewModels;
namespace Biblioteca.Web.Services;

public interface IPrestamoService

{
    Task<List<PrestamoListItemViewModel>> ListarAsync();
    Task<PrestamoCreateViewModel> ObtenerFormularioCreacionAsync();
    Task<(bool ok, string? error)> RegistrarPrestamoAsync(int libroId, string usuarioId, DateTime fechaDevolucionEsperada);
    Task<(bool ok, string? error)> RegistrarDevolucionAsync(int id, DateTime? fechaDevolucion = null);

}