using Biblioteca.Web.Models.Entities;
namespace Biblioteca.Web.Services;
public interface ICategoriaService { Task<List<Categoria>> ListarAsync(); Task<Categoria?> ObtenerAsync(int id); Task<bool> ExisteNombreAsync(string nombre,int? excluir=null); Task CrearAsync(Categoria categoria); Task<bool> ActualizarAsync(Categoria categoria); Task<(bool eliminado,string? error)> EliminarAsync(int id); }
